using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace ABMS
{

    public partial class BookingList : System.Web.UI.Page
    {
        DBManager db = new DBManager();
        public BookingList()
        {
            //db = new DBManager();
            //db.Open();
        }
        ~BookingList()
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

                try
                {

                    DBRegister obj = new DBRegister();
                    DataTable dt = obj.ExecuteDataTable(db, "sp_Publication");
                    ddlPublication.DataValueField = "ID";
                    ddlPublication.DataTextField = "Publication";
                    ddlPublication.DataSource = dt;
                    ddlPublication.DataBind();
                    ddlPublication.Items.FindByText("Express");

                    ddlPublication.SelectedValue = ddlPublication.Items.FindByText("Express").Value;
                }
                catch (Exception ex)
                {

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            StringBuilder txt = new StringBuilder();    
            if (ddlPublication.SelectedValue.ToString().Length > 0)
            {
             
                try
                {


                    int PublicationID = Convert.ToInt32(ddlPublication.SelectedValue);//.ToString();        
                    DBRegister obj = new DBRegister();
                    obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);
                    obj.PublicationId = PublicationID;
                    DataTable dt = obj.BookingRegisterByPublicationInsertionDate(db);
                                  
                    Int32 bkRec = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                            if (bkRec != Convert.ToInt32(dr["RecId"]))
                            {
                                txt.Append("<div style='width:100%;background-color:#d2f4eb;color:#5f021f'> Agency     : " + dr["Agency"] + " </div>");
                                txt.Append("<div style='width:100%;background-color:#d2f4eb;color:#5f021f'> Client     : " + dr["Client"] + " </div>");
                                txt.Append("<div style='width:100%;background-color:#d2f4eb;color:#5f021f'> Bk.Exec    : " + dr["BookingExecutive"] + " </div>");
                                txt.Append("<div style='width:100%;background-color:#d2f4eb;color:#5f021f'> Publication: " + dr["Publication_Name"] + " </div>");

                                txt.Append("<div style='width:100%;'>");
                                txt.Append("<div style='float:left; width:40%;background-color:#CCC;' >Station</div>");
                                txt.Append("<div style='float:left; width:20%;background-color:#CCC;' >Size</div>");
                                txt.Append("<div style='float:left; width:40%;background-color:#CCC;' >Caption</div>");
                                txt.Append("</div><br/>");
                            }
                            if (Convert.ToBoolean(dr["isConfirm"]) == false)
                            {
                                txt.Append("<div style='float:left; width:40%;background-color:#f89fbb'>" + dr["GroupComp_Name"] + "</div>");
                                txt.Append("<div style='float:left; width:20%;background-color:#f89fbb'>" + dr["Size"] + "</div>");
                                txt.Append("<div style='float:left; width:40%;background-color:#f89fbb'>" + dr["Caption"] + "</div><br/>");
                            }
                            else
                            {
                                txt.Append("<div style='float:left; width:40%'>" + dr["GroupComp_Name"] + "</div>");
                                txt.Append("<div style='float:left; width:20%'>" + dr["Size"] + "</div>");
                                txt.Append("<div style='float:left; width:40%'>" + dr["Caption"] + "</div><br/>");
                            }
                            bkRec = Convert.ToInt32(dr["RecId"]);
                    }
                    Literal lt = new Literal();
                    lt.Text = txt.ToString();
                    ph.Controls.Add(lt);

                }
                catch (Exception ex)
                {
                    db.Close();
                }
                finally
                {

                }
            }
        }
    }
}