using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ABMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Session["UserName"] = null;
                Session["UserDesignation"] = null;
                Session["Admin"] = null;
                Session["LoginID"] = null;
                
                try
                {
                    Session.Abandon();

                }
                catch (Exception)
                {
                    
                }
                Session["currentpage"] = "BookingStatus";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            DBManager db = new DBManager ();
            db.Open ();
            db.CreateParameters (2);
            db.AddParameters(0, "@UserId", loginusername.Text);
            db.AddParameters (1,"@UserPassword",loginpassword.Text);
            DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_CheckUserloginDetails");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                Session["UserName"] = ds.Tables[0].Rows[0]["UserName"];
                Session["LoginID"] = ds.Tables[0].Rows[0]["User_Id"];
                Session["UserDesignation"] = ds.Tables[0].Rows[0]["UserDesignation"];
                Session["Admin"] =Convert.ToBoolean (ds.Tables[0].Rows[0]["IsAdmin"]);
                Response.Redirect("BookingStatus.aspx", true);
            }
            else
            {
                lblMessage.Text = "Invalid userid/password";
                return;
              //  Response.Redirect("Login.aspx", true);

            }
                            
            
            
        }
    }
}