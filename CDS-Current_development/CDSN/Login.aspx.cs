using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CDSN
{
    public partial class Login : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                lbl.Text = string.Empty;
                ValidateUser();
            }

        }

        protected void ValidateUser()
        {
            
            using (CDSEntities db = new CDSEntities())
            {
                try
                {
                    String Username;
                    String Password;
                    String UserID;

                    Username = Convert.ToString(txtUserID.Text.Trim());
                    Password = Convert.ToString(txtUserPassword.Text.Trim());
                    var _dtUser = db.tblUsers.Where(x => x.IsActive == true && x.UserName == Username && x.Password == Password).FirstOrDefault();

                    if (_dtUser != null)
                    {
                        Session["username"] = _dtUser.UserName;
                        Session["userid"] = _dtUser.UserId;
                        Session["LoginID"] = _dtUser.UserName;
                        Session["IsAdmin"] =  _dtUser.IsAdmin;
                        Session["roleid"] = _dtUser.RoleId;
                        Helper.UID = _dtUser.UserId;

                        // 🔹 Set the Forms Authentication cookie
                        FormsAuthentication.SetAuthCookie(_dtUser.UserName, false);



                        clsLogManager.ActionOnForm = "Login";
                        clsLogManager.ActionTaken = "Site Accessed";
                        clsLogManager.SetLog(db);
                        if (_dtUser.UserName == "sultan")
                        {
                            Response.Redirect("Monitoring.aspx");
                        }
                        else
                        {
                            Response.Redirect("Dashboard.aspx", true);
                        }
                    }
                    else
                    {
                        txtUserID.Text = "";
                        txtUserPassword.Text = "";
                        txtUserID.Focus();
                        lbl.Visible = true;
                        lbl.ForeColor = System.Drawing.Color.Red;
                        lbl.Text = "Invalid UserID/Passowrd";
                        return;
                    }

                }
                catch (Exception ex)
                {
                    lbl.Visible = true;
                    lbl.ForeColor = System.Drawing.Color.Red;
                    lbl.Text = ex.Message;
                }
            }
        }

    }
}