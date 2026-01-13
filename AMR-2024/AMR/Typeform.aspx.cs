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
    public partial class Typeform : BaseClass
    {
        Model1Container db=new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a = db.Types.
                OrderBy(x => x.Type1).Select(x => new {
                    x.Id,
                    x.Type1,
                    x.Remarks,
                }).ToList();

            DataTable dt = Helper.ToDataTable(a);
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
                        AMR.Type obj = new AMR.Type();
                        obj.Id = db.usp_IDctr("Types").SingleOrDefault().Value;
                        obj.Type1 = txttype.Text;
                        obj.Remarks = txtremark.Text;

                        obj.add_by = Request.Cookies["UserId"]?.Value;
                        //obj.add_by = HttpContext.Current.Session["UserName"]?.ToString();

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Add_DateTime = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Edit_by = null;
                        obj.Edit_DateTime = null;

                        db.Types.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Type Created Successfully";
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
                        var obj = db.Types.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Type1 = txttype.Text;
                        obj.Remarks = txtremark.Text;
                        obj.Edit_by = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Edit_DateTime = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Type Updated Successfully";
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
            txttype.Text = string.Empty;
            txtremark.Text = string.Empty;
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
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Types.Where(x => x.Id == ID).SingleOrDefault();
            txttype.Text = obj.Type1;
            txtremark.Text = obj.Remarks;
            btnSave.Text = "Update";
        }
    }
}