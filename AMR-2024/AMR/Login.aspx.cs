using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AMR;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AMR
{
    public partial class Login : System.Web.UI.Page
    {
        Model1Container db = new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {
            string css = $@"
                <style>
                    body {{
                        background-image: url('{ResolveUrl("~/Content/Images/contact_image2.png")}');
                        background-size: cover;
                        background-position: center;
                        background-attachment: fixed;
                        background-repeat: no-repeat;
                    }}
                </style>";
           // litCss.Text = css;

            if (!IsPostBack)
            {
                txtUserID.Focus();
            }
            
        }

        public class UserLoginRequest
        {
            public string userId { get; set; }
            public string password { get; set; }
        }

        public class UserLoginResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string token { get; set; }
            public UserInfo user { get; set; }

            public bool IsValid => success;
        }
        public class UserInfo
        {
            public string userName { get; set; }
            //public string userDesignation { get; set; }
            public int id { get; set; }
            public string userId { get; set; }
        }

        private async Task<UserLoginResponse> ValidateUserAsync(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://172.17.0.16:89/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var loginRequest = new UserLoginRequest { userId = userName, password = password };
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("login", content);
                var responseString = await response.Content.ReadAsStringAsync();

                //Console.WriteLine($"API Response: {responseString}");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<UserLoginResponse>(responseString);
                }
                else
                {
                    return new UserLoginResponse
                    {
                        success = false,
                        message = $"Error contacting the API: {response.StatusCode} - {responseString}"
                    };
                }

            }
        }
        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("btnLogin_Click triggered");

            if (Page.IsPostBack)
            {
                var userName = txtUserID.Text;
                var password = txtUserPassword.Text;
                var result = await ValidateUserAsync(userName, password);

                //Console.WriteLine($"Login Result: StatusCode = {result.StatusCode}, IsValid = {result.IsValid}");

                if (result.success)
                {
                    try
                    {
                        var user=db.Users.Where(x=>x.User_Id == userName && x.User_Pass==password).SingleOrDefault();
                        if (user != null)
                        {
                            if (user.User_Active == false)
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Blocked user! Please contant administrator";
                                return;
                            }

                            //Session["UserObject"] = user;
                            //Session["UserName"] = user.User_Id;
                            //Session["UserGroup"] = user.User_Groups;
                            //HttpContext.Current.Session["UserName"] = user.User_Id;
                            HttpCookie id = new HttpCookie("ID");
                            id.Value = user.ID.ToString();
                            //userid.Expires = DateTime.Now.AddMinutes(30);
                            Response.Cookies.Add(id);

                            HttpCookie userid = new HttpCookie("UserId");
                            userid.Value = user.User_Id;
                            //userid.Expires = DateTime.Now.AddMinutes(30);
                            Response.Cookies.Add(userid);

                            HttpCookie username = new HttpCookie("User_Name");
                            username.Value = user.User_Name;
                            Response.Cookies.Add(username);

                            //HttpCookie usergroup = new HttpCookie("UserGroup");
                            //usergroup.Value = user.User_Groups.ToString();
                            //Response.Cookies.Add(usergroup);

                            HttpCookie usercexport = new HttpCookie("UsercExport");
                            usercexport.Value = user.cExport.ToString();
                            Response.Cookies.Add(usercexport);

                            HttpCookie roleid = new HttpCookie("RoleId");
                            roleid.Value = user.RoleId.ToString();
                            //userid.Expires = DateTime.Now.AddMinutes(30);
                            Response.Cookies.Add(roleid);

                            //Session["UserGroup"] = user.User_Groups;
                            //Session["UsercExport"] = user.cExport;
                            //HttpContext.Current.Session["UserObject"] = user;
                            //HttpContext.Current.Session["UserName"] = user.User_Name;
                            //HttpContext.Current.Session["UserGroup"] = user.User_Groups;
                            //HttpContext.Current.Session["UsercExport"] = user.cExport;

                            // Optional: store token for later
                            HttpCookie tokenCookie = new HttpCookie("AccessToken", result.token);
                            tokenCookie.HttpOnly = true;
                            Response.Cookies.Add(tokenCookie);

                            Response.Redirect("Dashboard.aspx");
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "User not found in DB.";
                        }
                        
                        
                        
                       
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Login succeeded but DB check or redirect failed: " + ex.Message;
                    }
                }
                else
                {
                    lblMessage.Text = result.message;
                    lblMessage.Visible = true;
                }
            }
        }


        //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string userId = txtUserID.Text;
        //        if (string.IsNullOrEmpty(userId))
        //        {
        //            lblMessage.Text = "User ID is empty.";
        //            lblMessage.Visible = true;
        //            return;
        //        }
        //        // Query the database to get the data based on userid
        //        var query = from os in db.OrgSecurities
        //                    join gc in db.GroupComps on os.Sec_Code equals gc.GroupComp_Id
        //                    where os.User_Id.Contains(userId)
        //                    select new
        //                    {
        //                        os.User_Id,
        //                        os.Sec_Type,
        //                        os.Sec_Code,
        //                        gc.GroupComp_Id,
        //                        gc.GroupComp_Name
        //                    };

        //        var result = query.ToList();
        //        if (result == null || result.Count == 0)
        //        {
        //            lblMessage.Text = "No matching records found.";
        //            lblMessage.Visible = true;
        //            return;
        //        }
        //        // Bind the data to the dropdown list
        //        ddlLocation.DataValueField = "GroupComp_Id";
        //        ddlLocation.DataTextField = "GroupComp_Name";
        //        ddlLocation.DataSource = result;
        //        ddlLocation.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = $"Error: {ex.Message}";
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void txtUserID_TextChanged(object sender, EventArgs e)
        //{
        //    ddlLocation_SelectedIndexChanged(sender, e);
        //}
    }
}
