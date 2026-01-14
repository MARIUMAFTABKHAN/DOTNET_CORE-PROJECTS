using System;
using System.Linq;

namespace ExpressDigital
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["HomeURL"] = "Home.aspx";
            using (var obj = new DbDigitalEntities())
            {
                var user = obj.UserInfoes.Where(x => x.UserID == txtUserID.Text && x.UserPassword == txtUserPassword.Text).SingleOrDefault();
                if (user != null)
                {
                    if (user.IsActive == false)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Blocked user! Please contant administrator";                        
                        return;
                    }
                    else
                    {
                        Session["UserObject"] = user;
                        Session["UserName"] = user.UserName + " | " + user.Role.UserRole;

                        LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;
                        LogManagers.RecordID = ((UserInfo)Session["UserObject"]).ID;
                        LogManagers.ActionOnForm = "Login";
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "Success";
                        LogManagers.SetLog(obj);
                        obj.SaveChanges();
                        Response.Redirect("Home.aspx", true);
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Wrong username or password. Try again";
                    return;
                }
            }
        }
    }
}