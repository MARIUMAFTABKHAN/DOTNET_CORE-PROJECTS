using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMR;
using AMR.Data;

namespace AMR
{
    public partial class Agencyform : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PopulateAgencyDropdown();
                PopulateEditionDropdown();
                BindGrid();
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
                        Agency agency = new Agency();
                        agency.Id= db.usp_IDctr("Agencies").SingleOrDefault().Value;
                        agency.Agency_Name = txtname.Text;
                        string selectedType = ddltype.SelectedValue.Trim();
                        lblmessage.Text = $"Selected Type: {selectedType}";
                        switch (selectedType)
                        {
                            case "A":
                                agency.Agency_Type = "A";
                                break;
                            case "N":
                                agency.Agency_Type = "N";
                                break;
                            default:
                                agency.Agency_Type = null;
                                break;
                        }

                        agency.Address_1 = txtadd1.Text;
                        agency.Address_2 = txtadd2.Text;
                        agency.Address_3 = txtadd3.Text;
                        agency.Address_4 = txtadd4.Text;
                        agency.Telephone_Nos = txttel.Text;
                        agency.Fax_Nos = txtfax.Text;
                        agency.EMail_Address = txtemail.Text;
                        //agency.Credit_Days = Convert.ToByte(txtdays.Text);
                        byte creditDays = 0;

                        if (!string.IsNullOrWhiteSpace(txtdays.Text))
                        {
                            if (!byte.TryParse(txtdays.Text.Trim(), out creditDays))
                            {
                                lblmessage.Text = "Please enter a valid number for Credit Days (0–255).";
                                return;
                            }
                        }

                        agency.Credit_Days = creditDays;   // 0 if empty

                        //agency.Credit_Limit = Convert.ToDouble(txtlimit.Text);
                        double creditLimit = 0;

                        if (!string.IsNullOrWhiteSpace(txtlimit.Text))
                        {
                            if (!double.TryParse(txtlimit.Text.Trim(), out creditLimit))
                            {
                                lblmessage.Text = "Please enter a valid number for Credit Limit.";
                                return;
                            }
                        }

                        agency.Credit_Limit = creditLimit;   // 0 if empty



                        if (string.IsNullOrEmpty(ddlagency.SelectedValue))
                        {
                            agency.Dealing_Executive = 0; 
                        }
                        else if (int.TryParse(ddlagency.SelectedValue, out int dealingExecutive))
                        {
                            agency.Dealing_Executive = dealingExecutive;
                        }
                        else
                        {
                            agency.Dealing_Executive = 0; 
                        }

                        string editionSelectedValue= ddledition.SelectedValue;
                        if (int.TryParse(ddledition.SelectedValue, out int editionValue))
                        {
                            agency.Edition_Responsible = (byte)editionValue;
                        }
                        else
                        {
                            agency.Edition_Responsible = 0; 
                        }
                        agency.AMR = chamr.Checked;
                        agency.cExport = chreport.Checked;
                        agency.Status = chstatus.Checked ? "A" : "I";
                        agency.Postal_Code = null;
                        //string usergroup = "";
                        //int grp;
                        //if (int.TryParse(usergroup, out grp))
                        //{
                            agency.Grp = 0;
                        //}
                        //else
                        //{
                        //    throw new Exception("Failed to parse UserGroup from cookie.");
                        //}
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        agency.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        agency.Rec_Added_Date = currentDateTime.Date;
                        agency.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        agency.Rec_Edited_By = null;
                        agency.Rec_Edited_Date = null;
                        agency.Rec_Edited_Time = null;

                        db.Agencies.Add(agency);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Agency Created Successfully";
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
                        var obj = db.Agencies.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Agency_Name = txtname.Text;
                        obj.Agency_Type = ddltype.SelectedValue;
                        obj.Address_1 = txtadd1.Text;
                        obj.Address_2 = txtadd2.Text;
                        obj.Address_3 = txtadd2.Text;
                        obj.Address_4 = txtadd4.Text;
                        obj.Telephone_Nos = txttel.Text;
                        obj.Fax_Nos = txtfax.Text;
                        obj.EMail_Address = txtemail.Text;
                        obj.Credit_Days =Convert.ToByte(txtdays.Text);
                        obj.Credit_Limit =Convert.ToDouble(txtlimit.Text);
                        //obj.Dealing_Executive =Convert.ToInt32(ddlagency.SelectedValue);
                        if (string.IsNullOrEmpty(ddlagency.SelectedValue))
                        {
                            obj.Dealing_Executive = 0;
                        }
                        else if (int.TryParse(ddlagency.SelectedValue, out int dealingExecutive))
                        {
                            obj.Dealing_Executive = dealingExecutive;
                        }
                        else
                        {
                            obj.Dealing_Executive = 0;
                        }
                        // obj.Edition_Responsible =Convert.ToByte(ddledition.SelectedValue);
                        if (int.TryParse(ddledition.SelectedValue, out int editionValue))
                        {
                            obj.Edition_Responsible = (byte)editionValue;
                        }
                        else
                        {
                            obj.Edition_Responsible = 0;
                        }
                        obj.AMR = chamr.Checked;
                        obj.cExport = chreport.Checked;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        string usergroup = Request.Cookies["UserGroup"]?.Value;
                        int grp;
                        if (int.TryParse(usergroup, out grp))
                        {
                            obj.Grp = (byte?)grp;
                        }
                        else
                        {
                            throw new Exception("Failed to parse UserGroup from cookie.");
                        }

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
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
            ClearForm();
            btnSave.Text = "Save";
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        private void BindGrid()
        {
            string txtclient = txtclientfilter.Text;

            var query = from Agency in db.Agencies
                        join GroupComp in db.GroupComps
                        on (int)Agency.Edition_Responsible equals GroupComp.GroupComp_Id
                        where Agency.Status == "A" && Agency.Agency_Name.Contains(txtclient)
                        orderby Agency.Id
                        select new AgencyGroupCompData
                        {
                            Id = Agency.Id,
                            Agency_Name = Agency.Agency_Name,
                            Agency_Type = Agency.Agency_Type,
                            Accredited_Status = Agency.Agency_Type == "A"
                                ? "Accredited"
                                : (Agency.Agency_Type == null ? null : "Non-Accredited"),
                            GroupComp_Name = GroupComp.GroupComp_Name,
                            Status = Agency.Status,
                            _Status = Agency.Status == "A"
                                ? "Active"
                                : (Agency.Status == null ? null : "InActive"),
                            _AMR = (Agency.AMR ?? false) ? "Yes" : "No",
                            _cExport = Agency.cExport ? "Yes" : "No"
                        };

            List<AgencyGroupCompData> resultList = query.ToList();
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
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Agencies.Where(x => x.Id == ID).SingleOrDefault();
            txtname.Text = obj.Agency_Name;
            ddltype.SelectedValue = obj.Agency_Type;
            txtadd1.Text = obj.Address_1;
            txtadd2.Text = obj.Address_2;
            txtadd3.Text = obj.Address_3;
            txtadd4.Text = obj.Address_4;
            txttel.Text = obj.Telephone_Nos;
            txtfax.Text = obj.Fax_Nos;
            txtemail.Text = obj.EMail_Address;
            txtdays.Text = obj.Credit_Days.ToString();
            txtlimit.Text = obj.Credit_Limit.ToString();
            if (ddlagency.Items.FindByValue(obj.Dealing_Executive.ToString()) != null)
            {
                ddlagency.SelectedValue = obj.Dealing_Executive.ToString();
            }
            ddledition.SelectedValue = obj.Edition_Responsible.ToString();
            chamr.Checked = obj.AMR.GetValueOrDefault();
            chreport.Checked = obj.cExport;
            chstatus.Checked = obj.Status == "A";

            btnSave.Text = "Update";
        }

        protected void ddlagency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var query = from agencyGroup in db.AgencyGroups
            //            select new
            //            {
            //                agencyGroup.RecID,
            //                agencyGroup.Group_Caption
            //            };
            //ddlagency.DataSource = query.ToList();
            //ddlagency.DataValueField = "RecID";
            //ddlagency.DataTextField = "Group_Caption";
            //ddlagency.DataBind();

            //ddlagency.Items.Insert(0, new ListItem("Select Agency Group", ""));
        }

        protected void ddledition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var a = db.GroupComps.OrderBy(x => x.GroupComp_Name).ToList();
            ////var query = from GroupComp in db.GroupComps
            ////            select new
            ////            {
            ////                GroupComp.GroupComp_Id,
            ////                GroupComp.GroupComp_Name
            ////            };
            ////ddledition.DataSource = query.ToList();
            //ddledition.DataValueField = "GroupComp_Id";
            //ddledition.DataTextField = "GroupComp_Name";
            //ddledition.DataSource = a;
            //ddledition.DataBind();

            //ddledition.Items.Insert(0, new ListItem("Select Edition", ""));
        }
        private void PopulateAgencyDropdown()
        {
            var agencyGroups = db.AgencyGroups.Select(ag => new
            {
                ag.RecID,
                ag.Group_Caption
            }).ToList();

            ddlagency.DataSource = agencyGroups;
            ddlagency.DataValueField = "RecID";
            ddlagency.DataTextField = "Group_Caption";
            ddlagency.DataBind();

            ddlagency.Items.Insert(0, new ListItem("Select Agency Group", ""));
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
        private void ClearForm()
        {
            txtname.Text = string.Empty;
            ddltype.SelectedIndex = 0;
            txtadd1.Text = string.Empty;
            txtadd2.Text = string.Empty;
            txtadd3.Text = string.Empty;
            txtadd4.Text = string.Empty;
            txttel.Text = string.Empty;
            txtfax.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtdays.Text = string.Empty;
            txtlimit.Text = string.Empty;
            ddlagency.SelectedIndex = 0;
            ddledition.SelectedIndex = 0;
            chamr.Checked = false;
            chreport.Checked = false;
            chstatus.Checked = false;
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.Agencies.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.Status = "I";
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime;
                        string usergroup = Request.Cookies["UserGroup"]?.Value;
                        int grp;
                        if (int.TryParse(usergroup, out grp))
                        {
                            record.Grp = (byte?)grp;
                        }
                        else
                        {
                            throw new Exception("Failed to parse UserGroup from cookie.");
                        }

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