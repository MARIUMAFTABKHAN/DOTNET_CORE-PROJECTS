using AMR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class DesignationManagement : System.Web.UI.Page
    {
        Model1Container db = new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

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
                        var s = db.usp_IDctr("Designation").SingleOrDefault();
                        int ID = s.Value;
                        Designation obj = new Designation();
                        obj.Id  = ID;
                        obj.Desgn_Name  = txtDesignation.Text;
                        //obj.Active = ChkIsActive.Checked;
                        db.Designations.Add(obj);
                        db.SaveChanges();
                        BindGrid();
                        db.SaveChanges();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Designation  Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);

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
                        var obj = db.Designations.Where(x => x.Id == ID).SingleOrDefault();
                        
                        obj.Desgn_Name = txtDesignation.Text;
                        //obj.Active = ChkIsActive.Checked;
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Designation Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }

        }

        private void BindGrid()
        {
            var g = db.Designations.OrderBy(x => x.Desgn_Name ).ToList();

            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtDesignation.Text = string.Empty;
            //ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Designations.Where(x => x.Id == ID).SingleOrDefault();
            txtDesignation.Text = obj.Desgn_Name ;
            //ChkIsActive.Checked =Convert.ToBoolean(obj.Active);
            btnSave.Text = "Update";
        }
        protected void DelButton_Click(object sender, EventArgs e)
        {
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;

        }
        [WebMethod]
        public static string OnSubmit(string id)
        {
            string mess = "";
            Model1Container db = new Model1Container();
            int ID = Convert.ToInt32(id);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var info = db.Designations.Find(ID);
                    if (info != null)
                    {
                        db.Designations.Remove(info);
                        db.SaveChanges();
                        //LogManager.RecordID = ID;
                        //LogManager.ActionOnForm = "Designation";
                        //LogManager.ActionBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                        //LogManager.ActionOn = DateTime.Now;
                        //LogManager.ActionTaken = "Delete";
                        //LogManager.SetLog(db);
                        scope.Complete();
                        mess = "Ok";
                    }
                }
                catch (Exception ex)
                {
                    mess = ExceptionHandler.GetException(ex);
                }

            }
            return mess;
        }
    }
}