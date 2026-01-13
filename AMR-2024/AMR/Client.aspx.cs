using AMR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class Client : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateMainCatDropdown(); 
                PopulateClientSectorDropdown();
                PopulateEditionDropdown();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string txtclient = txtclientfilter.Text;

            var query = from client in db.ClientCompanies
                        join mainCategory in db.MainCategories
                            on client.Main_Category equals mainCategory.Id
                        join groupComp in db.GroupComps
                            on (int?)client.Edition_Responsible equals groupComp.GroupComp_Id into gj
                        from subGroupComp in gj.DefaultIfEmpty()
                        join clientsector in db.ClientSectors
                            on client.Client_Sector equals clientsector.ID into gjj
                        from subClientSector in gjj.DefaultIfEmpty()
                        where client.Status =="A" && client.Client_Name.Contains(txtclient)
                        orderby client.Rec_Added_Date descending
                        select new
                        {
                            client.Id,
                            client.Client_Name,
                            client.Address_Line_4,
                            client.Client_Sector,
                            clientSectorName=subClientSector.Name,
                            client.Main_Category,
                            mainCategory.Category_Title,
                            client.Edition_Responsible,
                            GroupCompName=subGroupComp.GroupComp_Name,
                            ClientType = client.Client_Type == "N" ? "Normal" :
                                         client.Client_Type == "G" ? "Group" :
                                         client.Client_Type == "B" ? "Barter" : "Unknown",
                            StatusDes =client.Status=="A"?"Active":"InActive"
                        };

            var resultList = query.ToList();
            DataTable dt = Helper.ToDataTable(resultList);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        ClientCompany obj = new ClientCompany();
                        obj.Id = db.usp_IDctr("ClientCompanies").SingleOrDefault().Value;
                        obj.Client_Name = txtname.Text;
                        obj.Client_Abreviation = txtabb.Text;
                        obj.Address_Line_1 = txtadd1.Text;
                        obj.Address_Line_2 = txtadd2.Text;
                        obj.Address_Line_3 = txtadd3.Text;
                        obj.Address_Line_4 = txtcity.Text;
                        obj.Telephone_Nos = txttel.Text;
                        obj.Fax_Nos = txtfax.Text;
                        obj.EMail_Address = txtemail.Text;

                        string selectedType = ddlclienttype.SelectedValue.Trim();
                        lblmessage.Text = $"Selected Type: {selectedType}";
                        switch (selectedType)
                        {
                            case "N":
                                obj.Client_Type = "N";
                                break;
                            case "G":
                                obj.Client_Type = "G";
                                break;
                            case "B":
                                obj.Client_Type = "B";
                                break;
                            default:
                                obj.Client_Type = null;
                                break;
                        }

                        string mainCategory = ddlcat.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(mainCategory);

                        string clientsector= ddlsec.SelectedValue;
                        if (int.TryParse(ddlsec.SelectedValue, out int secValue))
                        {
                            obj.Client_Sector = secValue;
                        }
                        else
                        {
                            obj.Client_Sector = 0;
                        }

                        string editionSelectedValue = ddledition.SelectedValue;
                        if (int.TryParse(ddledition.SelectedValue, out int editionValue))
                        {
                            obj.Edition_Responsible = (byte)editionValue;
                        }
                        else
                        {
                            obj.Edition_Responsible = 0;
                        }

                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.AMR = chamr.Checked;
                        obj.cExport = chreport.Checked;

                        obj.Sub_Category = null;
                        obj.Agency = null;
                        obj.Postal_Code = null;
                        obj.Dealing_Executive = null;
                        obj.Client_Brand = null;
                        obj.Client_Agency = null;
                        obj.Client_MBH = null;
                        obj.Credit_Days = null;
                        obj.Credit_Limit = null;

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.ClientCompanies.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Client Created Successfully";
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                lblmessage.Text += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}\n";
                            }
                        }
                    }
                    //catch (Exception ex)
                    //{
                    //    lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";

                    //}
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.ClientCompanies.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Client_Name = txtname.Text;
                        obj.Client_Abreviation = txtabb.Text;
                        obj.Address_Line_1 = txtadd1.Text;
                        obj.Address_Line_2 = txtadd2.Text;
                        obj.Address_Line_3 = txtadd3.Text;
                        obj.Address_Line_4 = txtcity.Text;
                        obj.Telephone_Nos = txttel.Text;
                        obj.Fax_Nos = txtfax.Text;
                        obj.EMail_Address = txtemail.Text;

                        string selectedType = ddlclienttype.SelectedValue.Trim();
                        lblmessage.Text = $"Selected Type: {selectedType}";
                        switch (selectedType)
                        {
                            case "N":
                                obj.Client_Type = "N";
                                break;
                            case "G":
                                obj.Client_Type = "G";
                                break;
                            case "B":
                                obj.Client_Type = "B";
                                break;
                            default:
                                obj.Client_Type = null;
                                break;
                        }

                        string mainCategory = ddlcat.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(mainCategory);

                        string clientsector = ddlsec.SelectedValue;
                        if (int.TryParse(ddlsec.SelectedValue, out int secValue))
                        {
                            obj.Client_Sector = secValue;
                        }
                        else
                        {
                            obj.Client_Sector = 0;
                        }

                        string editionSelectedValue = ddledition.SelectedValue;
                        if (int.TryParse(ddledition.SelectedValue, out int editionValue))
                        {
                            obj.Edition_Responsible = (byte)editionValue;
                        }
                        else
                        {
                            obj.Edition_Responsible = 0;
                        }

                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.AMR = chamr.Checked;
                        obj.cExport = chreport.Checked;

                        obj.Sub_Category = null;
                        obj.Agency = null;
                        obj.Postal_Code = null;
                        obj.Dealing_Executive = null;
                        obj.Client_Brand = null;
                        obj.Client_Agency = null;
                        obj.Client_MBH = null;
                        obj.Credit_Days = null;
                        obj.Credit_Limit = null;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Client Updated Successfully";
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
            txtabb.Text = string.Empty;
            txtadd1.Text = string.Empty;
            txtadd2.Text = string.Empty;
            txtadd3.Text = string.Empty;
            txtcity.Text = string.Empty;
            txttel.Text = string.Empty;
            txtfax.Text = string.Empty;
            txtemail.Text = string.Empty;
            ddledition.SelectedIndex = 0;
            ddlclienttype.SelectedIndex = 0;
            ddlcat.SelectedIndex = 0;
            ddlsec.SelectedIndex = 0;
            chamr.Checked = false;
            chreport.Checked = false;
            chstatus.Checked = false;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.ClientCompanies.Where(x => x.Id == ID).SingleOrDefault();
            txtname.Text = obj.Client_Name;
            txtabb.Text = obj.Client_Abreviation;
            txtadd1.Text = obj.Address_Line_1;
            txtadd2.Text = obj.Address_Line_2;
            txtadd3.Text = obj.Address_Line_3;
            txtcity.Text = obj.Address_Line_4;
            txttel.Text = obj.Telephone_Nos;
            txtfax.Text = obj.Fax_Nos;
            txtemail.Text = obj.EMail_Address;
            ddlclienttype.SelectedValue =obj.Client_Type;
            ddlcat.SelectedValue = obj.Main_Category.ToString();
            ddlsec.SelectedValue = obj.Client_Sector.ToString();
            ddledition.SelectedValue = obj.Edition_Responsible.ToString();
            chstatus.Checked = obj.Status == "A";
            chamr.Checked = obj.AMR ?? false;
            chreport.Checked = obj.cExport;
            btnSave.Text = "Update";
        }

        protected void ddlcat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Category_Title
                }).ToList();

            ddlcat.DataSource = maincat;
            ddlcat.DataValueField = "Id";
            ddlcat.DataTextField = "Category_Title";
            ddlcat.DataBind();

            ddlcat.Items.Insert(0, new ListItem("Select  Category", ""));
        }

        protected void ddlsec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateClientSectorDropdown()
        {
            var clientsec = db.ClientSectors
                .OrderBy(mc => mc.Name).Select(mc => new
                {
                    mc.ID,
                    mc.Name
                }).ToList();

            ddlsec.DataSource = clientsec;
            ddlsec.DataValueField = "ID";
            ddlsec.DataTextField = "Name";
            ddlsec.DataBind();

            ddlsec.Items.Insert(0, new ListItem("Select  Client Sector", ""));
        }

        protected void ddledition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateEditionDropdown()
        {
            var editions = db.GroupComps.OrderBy(gc => gc.GroupComp_Name).Select(gc => new
            {
                gc.GroupComp_Id,
                gc.GroupComp_Name
            }).ToList();

            ddledition.DataSource = editions;
            ddledition.DataValueField = "GroupComp_Id";
            ddledition.DataTextField = "GroupComp_Name";
            ddledition.DataBind();

            ddledition.Items.Insert(0, new ListItem("Select Edition", ""));
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.ClientCompanies.SingleOrDefault(x => x.Id == id);
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

        protected void btnfilter_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}