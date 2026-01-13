using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class ClientExecutiveView : BaseClass
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
            string clientText = txtclientfilter.Text.Trim();
            string executiveText = txtexecname.Text.Trim();

            var query = from ce in db.ClientExecutives
                         join cc in db.ClientCompanies on ce.Client_Id equals cc.Id
                         join u1 in db.Users on ce.Exec1_Id equals u1.User_Id into exec1
                         from User1 in exec1.DefaultIfEmpty() // aliasing for Exec1
                         join u2 in db.Users on ce.Exec2_Id equals u2.User_Id into exec2
                         from User2 in exec2.DefaultIfEmpty() // aliasing for Exec2
                         where ce.Status=="A"
                         orderby ce.Rec_Added_Date descending
                         select new
                         {
                             ce.Id,
                             ce.Client_Id,
                             cc.Client_Name,
                             ce.Executive_Name,
                             ce.Designation,
                             ce.Department,
                             ce.Exec1_Id,
                             Exec1_UserName =User1!=null? User1.User_Name:null,
                             ce.Exec2_Id,
                             Exec2_UserName =User2!=null? User2.User_Name:null,
                             StatusDescription = ce.Status == "A" ? "Active" : "Inactive",
                             ce.Status
                         };

            // 🔍 Client filter
            if (!string.IsNullOrEmpty(clientText))
            {
                query = query.Where(x => x.Client_Name.Contains(clientText));
            }

            // 🔍 Executive filter
            if (!string.IsNullOrEmpty(executiveText))
            {
                query = query.Where(x => x.Executive_Name.Contains(executiveText));
            }


            var resultList = query
                            .OrderByDescending(x => x.Id)
                            .ToList();

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
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.ClientExecutives.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.Status = "I";
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;
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
        protected void btnview_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientExecutiveform.aspx");
        }
        protected void btnfilter_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}