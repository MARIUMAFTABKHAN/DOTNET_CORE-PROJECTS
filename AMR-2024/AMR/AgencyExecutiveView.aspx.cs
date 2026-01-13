using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class AgencyExecutiveView : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string agencyText = txtclientfilter.Text.Trim();
            string executiveText = txtexecname.Text.Trim();

            var query = (from ae in db.AgencyExecutives
                          join ag in db.Agencies on ae.Agency_Id equals ag.Id
                          join u1 in db.Users on ae.Exec1_Id equals u1.User_Id into exec1Group
                          from u1 in exec1Group.DefaultIfEmpty()
                          join u2 in db.Users on ae.Exec2_Id equals u2.User_Id into exec2Group
                          from u2 in exec2Group.DefaultIfEmpty()
                          where ae.Status == "A"
                          orderby ae.Rec_Added_Date descending
                          select new
                          {
                              ae.Id,
                              ae.Agency_Id,
                              ag.Agency_Name,
                              ae.Executive_Name,
                              ae.Designation,
                              ae.Department,
                              ae.Exec1_Id,
                              Exec1_UserName = u1 != null ? u1.User_Name : null,
                              ae.Exec2_Id,
                              Exec2_UserName = u2 != null ? u2.User_Name : null,
                              StatusDescription = ae.Status == "A" ? "Active" : "Inactive",
                              ae.Status
                          });

            // 🔍 Agency filter
            if (!string.IsNullOrEmpty(agencyText))
            {
                query = query.Where(x => x.Agency_Name.Contains(agencyText));
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
                    var record = db.AgencyExecutives.SingleOrDefault(x => x.Id == id);
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
            Response.Redirect("AgencyExecutiveform.aspx");
        }
        protected void btnfilter_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}