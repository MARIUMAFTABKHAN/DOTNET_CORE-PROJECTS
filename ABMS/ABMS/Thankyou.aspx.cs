using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABMS
{
    public partial class Thankyou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)        
        {
            //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
           // Session["candidatename"] = obj.ApplicantName;
          //  Session["candidateid"] = obj.ID.ToString();
            if ((Session["candidatename"] == null) || (Session["candidateid"] == null))
            {
                Session["candidatename"] = null;
                Session["candidateid"] = null;
                Response.Redirect("AppForm.aspx", true);
            }
            else
            {
                SendMail();
                lbltitle.Text = Session["candidatename"].ToString();
                lblcliam.Text = Session["candidateid"].ToString();
            }

            
        }
        private void SendMail()
        {

        }
    }
}