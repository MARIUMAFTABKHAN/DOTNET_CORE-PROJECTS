using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class Brandform : BaseClass
    {
        Model1Container db= new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid();
               // PopulateClientDropdown();
            }
        }

        private void BindGrid()
        {
            string txtclient = txtbrandfilter.Text;

            var result = (from brand in db.Brands
                          join clientCompany in db.ClientCompanies
                          on brand.Company equals clientCompany.Id
                          where brand.Status == "A" && brand.Brand_Name.Contains(txtclient)
                          orderby brand.Rec_Added_Date descending
                          select new
                          {
                              brand.Id,
                              brand.Brand_Name,
                              brand.Company,
                              clientCompany.Client_Name,
                              StatusDes = brand.Status == "A" ? "Active" : "InActive"
                          }).ToList();

            DataTable dt = Helper.ToDataTable(result);
            ViewState["dt"] = dt;
            if (gv != null)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                lblmessage.Text = "Error: GridView control is not available.";
            }
        }
        //private void PopulateClientDropdown()
        //{
        //    var client = db.ClientCompanies
        //        .Where(mc => mc.Status == "A")
        //        .OrderBy(mc => mc.Client_Name)//.Take(100)
        //        .Select(mc => new
        //        {
        //            mc.Id,
        //            mc.Client_Name
        //        }).ToList();

        //    ddlclient.DataSource = client;
        //    ddlclient.DataValueField = "Id";
        //    ddlclient.DataTextField = "Client_Name";
        //    ddlclient.DataBind();

        //    ddlclient.Items.Insert(0, new ListItem("Select Client", ""));
        //}
        private void PopulateClientExeDropdown()
        {
            int selectedClientId =Convert.ToInt32(hiddenClientId);

            var clientexe = db.ClientExecutives
                .Where(mc => mc.Status == "A" && mc.Client_Id == selectedClientId)
                .OrderBy(mc => mc.Executive_Name).Take(100).Select(mc => new
                {
                    mc.Id,
                    mc.Executive_Name
                }).ToList();

            ddlexe.DataSource = clientexe;
            ddlexe.DataValueField = "Id";
            ddlexe.DataTextField = "Executive_Name";
            ddlexe.DataBind();

            ddlexe.Items.Insert(0, new ListItem("Select Client Executive", ""));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Brand obj = new Brand();
                        obj.Id = db.usp_IDctr("Brands").SingleOrDefault().Value;
                        obj.Brand_Name = txtname.Text;

                        string selectedClientId = hiddenClientId.Value;

                        obj.Company = Convert.ToInt32(selectedClientId);

                        if (string.IsNullOrEmpty(ddlexe.SelectedValue))
                        {
                            obj.Company_Executive = 0;
                        }
                        else if (int.TryParse(ddlexe.SelectedValue, out int clientexe))
                        {
                            obj.Company_Executive = clientexe;
                        }
                        else
                        {
                            obj.Company_Executive = 0;
                        }

                        obj.Media_Buying_House = 0;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Grp = 0;
                        obj.cExport = false;

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.Brands.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Brand Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";

                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.Brands.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Brand_Name = txtname.Text;

                        string selectedClientId = hiddenClientId.Value;

                        obj.Company = Convert.ToInt32(selectedClientId);

                        if (string.IsNullOrEmpty(ddlexe.SelectedValue))
                        {
                            obj.Company_Executive = 0;
                        }
                        else if (int.TryParse(ddlexe.SelectedValue, out int clientexe))
                        {
                            obj.Company_Executive = clientexe;
                        }
                        else
                        {
                            obj.Company_Executive = 0;
                        }

                        obj.Media_Buying_House = 0;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Grp = 0;
                        obj.cExport = false;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Brand Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtname.Text = string.Empty;
            txtClient.Text=string.Empty;
            ddlexe.SelectedIndex = 0;
            chstatus.Checked = false;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateClientExeDropdown();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Brands.Where(x => x.Id == ID).SingleOrDefault();
            txtname.Text = obj.Brand_Name;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "populateClient",
            $"populateClient({obj.Company});", true);

            if (ddlexe.Items.FindByValue(obj.Company_Executive.ToString()) != null)
            {
                ddlexe.SelectedValue = obj.Company_Executive.ToString();
            }
            chstatus.Checked = obj.Status == "A";
            btnSave.Text = "Update";
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.Brands.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.Status = "I";
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;
                        
                        db.SaveChanges();
                        scope.Complete();

                        lblmessage.Text = "Record deleted successfully.";
                    }
                    else
                    {
                        lblmessage.Text = "Record not found.";
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = $"Error: {ex.Message}";
                }
            }
            BindGrid();
        }
        [System.Web.Services.WebMethod]
        public static List<Client> SearchClients(string searchText)
        {
            using (var db = new Model1Container())
            {
                return db.ClientCompanies
                    .Where(c => c.Status == "A" &&
                                !string.IsNullOrEmpty(c.Client_Name) &&
                                c.Client_Name.Contains(searchText))
                    .Select(c => new Client
                    {
                        Id = c.Id,
                        Client_Name = c.Client_Name
                    })
                    .ToList();
            }
        }

        public class Client
        {
            public int Id { get; set; }
            public string Client_Name { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static ClientCompanyDTO PopulateClientDetails(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from client in db.ClientCompanies

                              where client.Id == clientId
                              select new ClientCompanyDTO
                              {
                                  ClientName = client.Client_Name,

                              }).FirstOrDefault();

                return result ?? new ClientCompanyDTO { ClientName = "No Client Found" };
            }
        }

        public class ClientCompanyDTO
        {
            public string ClientName { get; set; }

        }

        protected void btnfilter_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}