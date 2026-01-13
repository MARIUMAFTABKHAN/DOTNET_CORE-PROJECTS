using AMR;
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
    public partial class Comparison : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePublicationDropdown();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a = from CompGroup in db.CompGroups
                    join Publication in db.Publications
                    on CompGroup.PublicationID equals Publication.Id
                    select new
                    {
                        CompGroup.ID,
                        CompGroup.Title,
                        CompGroup.PublicationID,
                        Publication.Publication_Name
                    };

            var resultList = a.ToList();

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
                        CompGroup obj = new CompGroup();
                        obj.ID = db.usp_IDctr("CompGroup").SingleOrDefault().Value;
                        obj.Title = txtcap.Text;

                        string pub = ddlpub.SelectedValue;
                        obj.PublicationID = Convert.ToInt32(pub);

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.CompGroups.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Group Publication Wise Created Successfully";
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
                        var obj = db.CompGroups.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.Title = txtcap.Text;
                        string pub = ddlpub.SelectedValue;
                        obj.PublicationID = Convert.ToInt32(pub);
                        
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Group Publication Wise Updated Successfully";
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
            txtcap.Text = string.Empty;
            ddlpub.SelectedIndex = 0;
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

        protected void ddlpub_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulatePublicationDropdown()
        {
            var pub = db.Publications.OrderBy(mc => mc.Publication_Name).
                Where(mc=>mc.Status=="A").
                Select(mc => new
            {
                mc.Id,
                mc.Publication_Name
            }).ToList();

            ddlpub.DataSource = pub;
            ddlpub.DataValueField = "Id";
            ddlpub.DataTextField = "Publication_Name";
            ddlpub.DataBind();

            ddlpub.Items.Insert(0, new ListItem("Select Publication", ""));
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.CompGroups.Where(x => x.ID == ID).SingleOrDefault();
            txtcap.Text = obj.Title;
            ddlpub.SelectedValue = obj.PublicationID.ToString();
            btnSave.Text = "Update";
        }
    }
}