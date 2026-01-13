using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class AddUserInfo : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var r = db.Roles.OrderBy(x => x.UserRole).ToList();
                ddlRole.DataValueField = "ID";
                ddlRole.DataTextField = "UserRole";
                ddlRole.DataSource = r;
                ddlRole.DataBind();

                var c = db.tblCountries.Where(x => x.active == true).OrderBy(x => x.Seq).ToList();
                ddlcountry.DataValueField = "CountryId";
                ddlcountry.DataTextField = "CountryName";
                ddlcountry.DataSource = c;
                ddlcountry.DataBind();
                ddlcountry.SelectedIndex = 0;


                var rr = db.tblCities.Where(x => x.active == true).OrderBy(x => x.CityName).ToList();

                ddlCity.DataSource = rr;
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "Id";
                ddlCity.DataBind();

                BindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlcountry.SelectedItem.Text != "")
            {

                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            tblUser obj = new tblUser();
                            int id = Convert.ToInt32(db.usp_GetIDCTRCounter("tblUser").SingleOrDefault().Value);
                            obj.UserId = id;
                            obj.UserName = txtUserName.Text;
                            obj.FirstName = txtfirstname.Text;
                            obj.LastName = txtlastname.Text;
                            obj.Password = txtPWD.Text;
                            obj.Email = txtEmail.Text;
                            obj.ContactNo = txtContact.Text;
                            obj.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
                            obj.IsActive = ChkIsActive.Checked;
                            obj.IsAdmin = ChkIsAdmin.Checked;
                            obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.Address = txtAddress.Text;
                            obj.CreatedDate = DateTime.Now;
                            obj.CreatedBy = Session["username"].ToString();
                            obj.IsAdmin=ChkIsAdmin.Checked;
                            obj.IsActive=ChkIsActive.Checked;   
                            db.tblUsers.Add(obj);
                            db.SaveChanges();

                            var usir = Convert.ToInt32(Session["userid"]);

                            //var uc = db.tblusercountries.Where(x => x.UserId == usir);
                            //if (uc.Any())
                            //{
                            //    db.tblusercountries.RemoveRange(uc);
                            //    db.SaveChanges();
                            //}
                            //tblusercountry objc;
                            //foreach (ListItem item in ddlcountry.Items)
                            //{
                            //    if (item.Selected)
                            //    {
                            //        var ucid = db.usp_GetIDCTRCounter("usercountry").SingleOrDefault().Value;
                            //        objc = new tblusercountry();
                            //        objc.ID = Convert.ToInt32(ucid);
                            //        objc.CountryId = Convert.ToInt32(item.Value);
                            //        db.tblusercountries.Add(objc);
                            //        db.SaveChanges();

                            //    }
                            //}


                            BindGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblmessage.Text = "User Created Successfully";
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
                            var obj = db.tblUsers.Where(x => x.UserId == ID).SingleOrDefault();
                            obj.UserName = txtUserName.Text;
                            obj.FirstName = txtfirstname.Text;
                            obj.LastName = txtlastname.Text;
                            obj.Password = txtPWD.Text;
                            obj.Email = txtEmail.Text;
                            obj.ContactNo = txtContact.Text;
                            obj.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
                            obj.IsActive = ChkIsActive.Checked;
                            obj.IsAdmin = ChkIsAdmin.Checked;
                            obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.Address = txtAddress.Text;
                            obj.CreatedDate = DateTime.Now;
                            obj.CreatedBy = Session["username"].ToString();
                            obj.IsAdmin = ChkIsAdmin.Checked;
                            obj.IsActive = ChkIsActive.Checked;
                            db.SaveChanges();

                            var usir = Convert.ToInt32(Session["userid"]);

                            var uc = db.tblusercountries.Where(x => x.UserId == usir);
                            if (uc.Any())
                            {
                                db.tblusercountries.RemoveRange(uc);
                                db.SaveChanges();
                            }
                            tblusercountry objc;
                            foreach (ListItem item in ddlcountry.Items)
                            {
                                if (item.Selected)
                                {
                                    var ucid = db.usp_GetIDCTRCounter("usercountry").SingleOrDefault().Value;
                                    objc = new tblusercountry();
                                    objc.ID = Convert.ToInt32(ucid);
                                    objc.CountryId = Convert.ToInt32(item.Value);
                                    objc.UserId = usir;
                                    db.tblusercountries.Add(objc);
                                    db.SaveChanges();

                                }
                            }


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
                txtPWD.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                lblmessage.Text = "Please select country from drop down";
            }
        }

        private void BindGrid()
        {
            var g = (from u in db.tblUsers.ToList()
                     select new
                     {
                         u.UserId,
                         u.UserName,
                         u.Email,
                         u.ContactNo,
                         UserRole = u.Role != null ? u.Role.UserRole : "",
                         u.IsActive,
                         u.IsAdmin,
                         u.FirstName,
                         u.LastName
                     }).ToList();

            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtPWD.TextMode = TextBoxMode.Password;
            txtUserName.Text = string.Empty;
            txtfirstname.Text = string.Empty;
            txtlastname.Text = string.Empty;
            txtPWD.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
            ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
            ChkIsAdmin.Checked = false;
            txtAddress.Text = string.Empty;
            ddlcountry.SelectedIndex = 0;
            foreach (ListItem item in ddlcountry.Items)
            {
                item.Selected = false;
            }
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.tblUsers.Where(x => x.UserId == ID).SingleOrDefault();
            txtUserName.Text = obj.UserName;
            txtfirstname.Text = obj.FirstName;
            txtlastname.Text = obj.LastName;
            txtPWD.TextMode = TextBoxMode.SingleLine;
            txtPWD.Text = obj.Password;
            txtEmail.Text = obj.Email;
            txtContact.Text = obj.ContactNo;
            ddlRole.SelectedValue = Convert.ToString(obj.RoleId);
            ChkIsActive.Checked = obj.IsActive ?? false;
            ChkIsAdmin.Checked = obj.IsAdmin ?? false;
            ddlCity.SelectedValue = obj.CityId.ToString();
            txtAddress.Text = obj.Address;
            var usir = Convert.ToInt32(Session["userid"]);
            var uc = db.tblusercountries.Where(x => x.UserId == usir);
            foreach (ListItem item in ddlcountry.Items)
            {
                item.Selected = false;
            }
            foreach (var u in uc)
            {
                foreach (ListItem item in ddlcountry.Items)
                {

                    if (u.CountryId == Convert.ToInt32(item.Value))
                    {
                        item.Selected = true;

                    }
                }
            }
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
            CDSEntities db = new CDSEntities();
            int ID = Convert.ToInt32(id);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var info = db.tblUsers.Where(x => x.UserId == ID).SingleOrDefault();
                    if (info != null)
                    {
                        info.IsActive = false;
                        db.SaveChanges();
                        clsLogManager.RecordID = ID;
                        clsLogManager.ActionOnForm = "UserManagement";
                        clsLogManager.ActionBy = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                        clsLogManager.ActionOn = DateTime.Now;
                        clsLogManager.ActionTaken = "DeActive";
                        clsLogManager.SetLog(db);
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

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}