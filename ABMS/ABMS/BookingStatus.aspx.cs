using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ABMS
{
    public partial class BookingStatus : System.Web.UI.Page
    {
        DBManager db =  new DBManager ();
        public BookingStatus()
        {
            //db = new DBManager();
            //db.Open();
        }
        ~BookingStatus()
        {
            db.Close();
            db.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((db.Connection == null) || (db.Connection.State == ConnectionState.Closed))
                {
                    db = new DBManager();
                    db.Open();
                }
            }
            catch (Exception)
            {
                db = new DBManager();
                db.Open();
            }


            if (!Page.IsPostBack)
            {
                //db = new DBManager();
                //db.Open();
                try
                {


                   // db.Open();

                    DBRegister obj = new DBRegister();
                    DataTable dt = obj.ExecuteDataTable(db, "sp_Publication");
                    ddlPublication.DataValueField = "ID";
                    ddlPublication.DataTextField = "Publication";
                    ddlPublication.DataSource = dt;
                    ddlPublication.DataBind();
                    ddlPublication.Items.FindByText("Express");

                    ddlPublication.SelectedValue = ddlPublication.Items.FindByText("Express").Value;

                    ddlPublication_SelectedIndexChanged(null, null);


                    string strDate = "";
                    int strPublication;
                    //strDate = (string)Session["dtInsertionDate"];
                    dtInsertionDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    if (Request.QueryString.Count > 0)
                    {
                        strDate = Request.QueryString[0].ToString();
                        strPublication = Convert.ToInt32(Request.QueryString[1].ToString());
                        strPublication = (int)Session["publicationID"];
                        dtInsertionDate.Text = strDate;
                        ddlPublication.SelectedValue = strPublication.ToString();
                    }


                    dt = obj.ExecuteDataTable(db, "sp_GetBaseStations");
                    DataTable tblFiltered = dt.AsEnumerable()
              .Where(row => row.Field<String>("Abreviation") != "NW")
              .CopyToDataTable();
                    RdoStations.DataValueField = "ID";
                    RdoStations.DataTextField = "Abreviation";
                    RdoStations.DataSource = tblFiltered;
                    RdoStations.DataBind();

                    RdoStations.Items[0].Selected = true;
                   // RdoStations_SelectedIndexChanged(null, null);
                }

                catch (Exception)
                {
                    //  db.Close();
                    // db.Dispose();
                }
                finally
                {
                    //  db.Close();
                    // db.Dispose();
                }
            }
        }

        protected void RdoStations_SelectedIndexChanged(object sender, EventArgs e)
        {            
            btnSubmit_Click(null, null);
        }

        protected void ddlPublication_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DBManager db = new DBManager();
            try
            {
                // db.Open();
                Int32 PubID = Convert.ToInt32(ddlPublication.SelectedValue);
                DBRegister obj = new DBRegister();
                obj.PublicationId = PubID;
                db.CreateParameters(1);
                db.AddParameters(0, "@PubID", PubID);
                DataTable dt = obj.ExecuteDataTable(db, "sp_PublicationPages");
                ddlPage.DataValueField = "ID";
                ddlPage.DataTextField = "Caption";
                ddlPage.DataSource = dt;
                ddlPage.DataBind();
            }
            catch (Exception)
            {
                //  db.Close();
                // db.Dispose();

            }
            finally
            {
                //  db.Close();
                //  db.Dispose();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (RdoStations.SelectedIndex > 0)
                GetDetails();
            else
            {
                RdoStations.Items[0].Selected = true;
                GetDetails();
            }
        }

        private void GetDetails()
        {
            if (ddlPublication.SelectedValue.ToString().Length > 0 && ddlPage.SelectedValue.ToString().Length > 0)
            {

                try
                {                    

                    int StationID = Convert.ToInt32(RdoStations.SelectedValue);
                    int PublicationID = Convert.ToInt32(ddlPublication.SelectedValue);//.ToString();        
                    DBRegister obj = new DBRegister();
                    Literal lt = new Literal();
                    StringBuilder str = new StringBuilder();
                    obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);

                    obj.PublicationId = PublicationID;
                    obj.GroupBaseStationID = StationID;
                    obj.PageID = Convert.ToInt32(ddlPage.SelectedValue);

                    string cityListTen = "";
                    string cityList = "";
                    obj.IsConfirm = false;
                    DataTable dtTen = obj.AdGraphicView_UnConfirm(db);
                    obj.IsConfirm = true;
                    DataTable dt = obj.AdGraphicView_Confirm(db);
                    Int16 i = 0;

                    if (dt.Rows.Count == 0)
                    {
                        Literal ltr = new Literal();
                        lt.Text = "";
                        DivConfirmed.Controls.Add(ltr);

                    }

                    foreach (DataRow dr in dt.Rows)
                    {

                        cityList = GetCityList(Convert.ToInt32(dr[0]));
                        string Agency = "Agency :" + dr[9].ToString();
                        string Client = "Client :" + dr[3].ToString();
                        string bkexec = "Bk-Exec:" + dr[10].ToString();
                        float _COL = (float)(Convert.ToInt32(dr["COL"]));// *8.333;
                        string CM = (Convert.ToInt32(dr["CM"]) * 10).ToString() + "px";
                        string COL = ((_COL * 8.333) * 1.5).ToString() + "%";
                        System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        dynDiv.ID = "dynDivCode" + i.ToString();// +"-" + dr[0].ToString();
                        dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#0fa366");
                        dynDiv.Style.Add(HtmlTextWriterStyle.Height, CM);
                        dynDiv.Style.Add(HtmlTextWriterStyle.Width, COL);
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderStyle, "Solid");
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderColor, "#FFFFFF");
                        dynDiv.Style.Add(HtmlTextWriterStyle.Position, "Left");
                        dynDiv.Style.Add("float", "left");
                        dynDiv.Attributes.Add("onclick", "checkCheck('" + cityList + "',  '" + Agency + "','" + Client + "')");
                        dynDiv.InnerHtml = "<apan style='Color:#FFFpadding:1px''>" + "(" + dr["Size"].ToString() + ")<br/>" + Agency + "<br/>" + Client + "<br/>" + bkexec + "</Span>";
                        DivConfirmed.Controls.Add(dynDiv);
                        i++;
                    }

                    //dtTen = obj.AdGraphicView(db);
                    if (dtTen.Rows.Count == 0)
                    {
                        Literal ltr = new Literal();
                        lt.Text = "";
                        Divtentative.Controls.Add(ltr);
                    }
                    foreach (DataRow dr in dtTen.Rows)
                    {
                        cityList = GetCityList(Convert.ToInt32(dr[0]));
                        string Agency = "Agency :" + dr[9].ToString();
                        string Client = "Client :" + dr[3].ToString();
                        string bkexec = "Bk-Exec:" + dr[10].ToString();
                        float _COL = (float)(Convert.ToInt32(dr["COL"]));// *8.333;
                        string CM = (Convert.ToInt32(dr["CM"]) * 10).ToString() + "px";
                        string COL = ((_COL * 8.333) * 1.5).ToString() + "%";
                        System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        dynDiv.ID = "dynDivCode" + i.ToString();
                        dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#bd1e26");
                        dynDiv.Style.Add(HtmlTextWriterStyle.Height, CM);
                        dynDiv.Style.Add(HtmlTextWriterStyle.Width, COL);
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderStyle, "Solid");
                        dynDiv.Style.Add(HtmlTextWriterStyle.BorderColor, "#FFFFFF");
                        dynDiv.Style.Add(HtmlTextWriterStyle.Position, "Left");
                        dynDiv.Style.Add("float", "left");
                        dynDiv.Attributes.Add("onclick", "checkCheck('" + cityList + "','" + Agency + "','" + Client + "')");
                        dynDiv.InnerHtml = "<apan style='Color:#FFF; padding:1px''>" + "(" + dr["Size"].ToString() + ")<br/>" + Agency + "<br/>" + Client + "<br/>"+ bkexec +"</Span>";
                        Divtentative.Controls.Add(dynDiv);
                        i++;
                    }
                }


                catch (Exception)
                {
                    db.Close();
                    //db.Dispose();
                }
                finally
                {
                    // db.Close();
                    //db.Dispose();
                }

            }
        }

        private string GetCityList(Int32 ID)
        {
            string cityList = "";
            DBRegister obj = new DBRegister();
            obj.ID = ID;
            DataTable dtPubCity = new DataTable();
            // DBManager db = new DBManager();
            // db.Open();
            try
            {
                dtPubCity = obj.GetCitiesByPubID(db);
            }
            catch (Exception)
            {
                //  db.Close();
                //db.Dispose();
            }
            finally
            {
                //   db.Close();
                //  db.Dispose();
            }

            int i = 0;
            StringBuilder sb = new StringBuilder();
            string line1 = "";
            string line2 = "";
            foreach (DataRow dr in dtPubCity.Rows)
            {
                if (i < 6)
                {
                    line1 += "," + dr[0].ToString();

                }
                else
                {
                    line2 += "," + dr[0].ToString();
                }
                i++;

            }
            if (i == 0)
                line1 = ",";
            if (i < 6)
                line2 = ",";

            sb.AppendLine(line1.Substring(1, line1.Length - 1) + Environment.NewLine + line2.Substring(1, line2.Length - 1));
            return sb.ToString().Replace(Environment.NewLine, "<br />");
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDetails();
        }
    }
}