using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["username"] == null) || (Session["UserID"] == null))
            {
                Response.Redirect("Login.aspx", true);
            }
            Session["HomeURL"] = "CurrentActivity.aspx";

            // Page.Header.DataBind();
            if (Page.IsPostBack)
            { }


            string pageName = Page.ToString().Replace("ASP.", "").Replace("_", ".");
            Helper.UID = (Convert.ToInt32(Session["userid"]));
            Helper.Page = pageName;

            if ((Session["username"] != null) || (Session["HomeURL"] != null) || pageName == "home.aspx")
            if (pageName == "home.aspx")
            {
                Session["HomeURL"] = "home.aspx";
                lblloginuser.Text = Session["username"].ToString();// + "  ( " + Session["userrole"].ToString () +" )";
            }
            else
            {
                try
                {
                    using (CDSEntities db = new CDSEntities())
                    {
                        if (Helper.CheckValidPage(db) == false)
                        {
                            Response.Redirect("Login.aspx", true);
                        }

                        lblloginuser.Visible = true;
                        int userid = Convert.ToInt32(Session["userid"]);
                        lblloginuser.Text = Session["username"].ToString();// + "( " + Session["userrole"].ToString() + " )";

                        var s = db.tblContacts.Where(x => x.Sentto == userid).OrderByDescending(x => x.MessageDate).ToList();


                        // var dt = db.sp_GetAlerts(userid).SingleOrDefault();
                        var dt = db.Usp_GetViewedNewMessages2(userid).SingleOrDefault(); ;
                        if (dt != null)
                        {

                            Response.Cache.SetCacheability(HttpCacheability.NoCache);

                            try
                            {
                                if (dt != null)
                                {
                                    //lblblink.Text = " (" + dt.Newmessages  + " ) Messages ";


                                    //lblblink2.Text = " (" + dt.Viewed + " ) Viewed Messages ";
                                    //blink.Visible = true;

                                    // 🔴 Show new messages
                                    lblblink.Text = " (" + dt.Newmessages + " ) New Messages ";
                                    lblblink.Visible = dt.Newmessages > 0;

                                    // 🟢 Show viewed messages
                                    lblblink2.Text = " (" + dt.Viewed + " ) Viewed Messages ";
                                    lblblink2.Visible = dt.Viewed > 0;

                                    blink.Visible = true;
                                    blink2.Visible = true;

                                }
                                else
                                {
                                    //blink.Visible = false;

                                    // Default to zero
                                    lblblink.Text = " (0) New Messages ";
                                    lblblink2.Text = " (0) Viewed Messages ";
                                    blink.Visible = false;
                                    blink2.Visible = false;
                                }

                            }

                            catch (Exception)
                            {
                                blink.Visible = false;
                                blink2.Visible = false;
                            }
                        }
                        else
                        {
                            //Response.Redirect("ChPosition.aspx");
                            //Response.Redirect("Login.aspx");
                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
            }
                
        }

        
    }
}


