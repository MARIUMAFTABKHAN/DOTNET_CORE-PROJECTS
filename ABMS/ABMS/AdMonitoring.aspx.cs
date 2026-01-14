using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ABMS
{
    public partial class AdMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                Literal lt = new Literal();
                StringBuilder str = new StringBuilder();
                str.Append("<div  style='background-color:#fb4545; width:90%; height:20px; border:1px solid #FFF;float:left'> </div>");
                str.Append("<div  style='background-color:green;width:10%;  height:200px;border:1px solid #FFF;float:left'> </div>");
                str.Append("<div  style='background-color:#fb4545;width:40%; height:200px;border:1px solid #FFF;float:left'> </div>");
                str.Append("<div  style='background-color:#fb4545;width:80%; height:200px;border:1px solid #FFF;float:left'> </div>");
                str.Append("<div  style='background-color:#fb4545;width:20%; height:20px;border:1px solid #FFF;float:left'> </div>");
                str.Append("<div  style='background-color:green;width:20%; height:20px;border:1px solid #FFF;float:left'> </div>");
                lt.Text = str.ToString();
                //Main.Controls.Add(lt);
                FillControls();
            }
        }
        private void FillControls()
        {

            // txtClientdata.Text = "Munir mustafa";
            DBManager db = new DBManager();
            db.Open();
            DBRegister obj = new DBRegister();

            DataTable dt = obj.ExecuteDataTable(db, "sp_BookingExecutives");
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "Select Booking Executive";
            dt.Rows.InsertAt(dr, 0);
            //ddlBookingExecutive.DataValueField = "ID";
           // ddlBookingExecutive.DataTextField = "UserName";
            //ddlBookingExecutive.DataSource = dt;
           // ddlBookingExecutive.DataBind();

            dt = Helper.BlankDataTable();
            //ddlClient.DataValueField = "ID";
            //ddlClient.DataTextField = "Description";
            //ddlClient.DataSource = dt;
            //ddlClient.DataBind();

        }

        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            setValue();

        }

        private void setValue()
        {
            //this.txtClientdata.Text = "Munir";
            this.TextBox1.Enabled = true;
            this.TextBox1.Visible = true;
            this.TextBox1.Text = "My Text";

        }
    }
}
