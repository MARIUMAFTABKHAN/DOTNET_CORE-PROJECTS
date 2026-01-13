using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CDSN
{
    public partial class ChannelPosition : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        Literal ltr = new Literal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    mph.Controls.Remove(ltr);
                }
                catch (Exception)
                {

                }

                lblmessage.Text = string.Empty;
                try
                {

                    Int32 UserId = Convert.ToInt32(Session["UserId"]);
                    hduid.Value = UserId.ToString();
                    var ds = db.usp_GetAllTerritoryByUserId(UserId).ToList();
                    ddlTerritory.DataTextField = "TerritoryName";
                    ddlTerritory.DataValueField = "Id";
                    ddlTerritory.DataSource = ds;
                    ddlTerritory.DataBind();
                    ddlTerritory_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ex.Message;
                }

            }

        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //lblmessage.Text = string.Empty;
                //Int32 UserId = Convert.ToInt32(Session["UserId"]);
                //int Territoryid = Convert.ToInt32(ddlTerritory.SelectedValue);
                //var ds = db.usp_GetAllDistrictsByUserIdTerritoryId(UserId, Territoryid);
                //ddlDistrict.DataTextField = "DivisionName";
                //ddlDistrict.DataValueField = "Id";
                //ddlDistrict.DataSource = ds;
                //ddlDistrict.DataBind();
                //ddlDistrict_SelectedIndexChanged(null, null);

                

                int territoryId = Convert.ToInt32(ddlTerritory.SelectedValue);

                var result = db.Database.SqlQuery<DistrictResult>(
                    "EXEC usp_GetAllDistrictsByUserIdTerritoryId @UserId, @TerritoryId",
                    new SqlParameter("@UserId", Helper.UID),
                    new SqlParameter("@TerritoryId", territoryId)
                ).ToList();

                ddlDistrict.DataSource = result;
                ddlDistrict.DataTextField = "DivisionName";
                ddlDistrict.DataValueField = "Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));

            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        public class CityResult
        {
            public int id { get; set; }
            public string CityName { get; set; }
            public int UserId { get; set; }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 UserId = Convert.ToInt32(Session["UserId"]);
                Int32 DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                var result = db.Database.SqlQuery<CityResult>(
                    "EXEC usp_GetCityByUserIdDivisionId @UserId, @DistrictId",
                    new SqlParameter("@UserId", Helper.UID),
                    new SqlParameter("@DistrictId", DistrictId)
                ).ToList();
                ddlcity.DataSource = result;
                ddlcity.DataTextField = "CityName";
                ddlcity.DataValueField = "Id";
                
                ddlcity.DataBind();
                ddlcity_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }



        }

        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Int32 UserId = Convert.ToInt32(Session["UserId"]);
                Int32 CityId = Convert.ToInt32(ddlcity.SelectedValue);
                var ds = db.usp_GetPositionByUserByuCity(UserId, CityId).ToList();
                ddlOperator.DataTextField = "Name";
                ddlOperator.DataValueField = "Id";
                ddlOperator.DataSource = ds;
                ddlOperator.DataBind();
                ddlOperator_SelectedIndexChanged(sender, null);
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        protected void ddlOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 OperatorId = Convert.ToInt32(ddlOperator.SelectedValue);
                GetDataItem(OperatorId);
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        private void GetDataItem(int OperatorId)
        {

            int recid = 0;
            StringBuilder oStringBuilder = new StringBuilder();
            string line = "";

            //var x = db.tblChannelTypes.Where(w => w.active == true).OrderBy(w => w.Orderby).ToList();


            // foreach (var xx in x)
            // {



            //var obj = db.usp_ChannelPositionByTypeByOperatorChannelType(OperatorId, xx.ID).ToList();
            //   oStringBuilder.Append(" <div  id='sortable'  class= \'col-md-12\' >"); 
            Int32 typeid = 0;
            String typeName = "";
            var obj = db.usp_GetChannelPositionByOperatorID2(OperatorId, 0).ToList();
            if (obj.Count > 0)
            {
                //if ( typeid != obj[0].TypeID )
                //{
                typeid = 0;
                if (typeid == 0)
                {
                    typeName = obj[0].ChannelName;
                    oStringBuilder.Append(" <div   class= \'col-md-12  divcolor\' >" + typeName + "</div>");
                    typeid = Convert.ToInt32( obj[0].TypeID);
                }
                //}

                oStringBuilder.Append(" <ul id='sortable' >");
               
                foreach (var v in obj)
                {
                    if (typeid != v.TypeID)
                    {
                        typeName = v.ChannelName ;
                        oStringBuilder.Append(" <div   class= \'col-md-12  divcolor\' >" + typeName + "</div>");

                    }
                    typeid = Convert.ToInt32(v.TypeID);
                    string url = ("<div>  <div class=\'spanchk\'> <input type=\'input\' id=\'txtcp\'  class=\'txt\' type=\'number\' name=\'chkpick\' value=" + v.CurPosition + "  onkeyup=\'UnHighlight(this)\' />   </div>  <strong class=\'divtitle\'> " + v.ShortName + "</strong> <div class=\'clsChild\'   id=\'divchid\'>" + v.ChannelId + ":" + v.CHPositionId + ":" + v.PrevPosition + " </div>   </br> <div class=\'clsppos\'> Position:" + v.PrevPosition.ToString() + "</div> </br></div>");
                    oStringBuilder.Append("<li class=\'ui-state-default\' >" + url + " </li>");
                }
                oStringBuilder.AppendLine("</ul>");
            }
            else
            {
                string url = ("<div>" + "No Channel Available" + "</div>");
                oStringBuilder.Append(" <ul id=\'sortable\'>");
                oStringBuilder.Append("<li class=\'ui-state-default\' >" + url + " </li>");
                oStringBuilder.AppendLine("</ul>");
            }
            //}
            // oStringBuilder.Append(" </div>");

            ltr = new Literal();
            ltr.Text = oStringBuilder.ToString();
            mph.Controls.Add(ltr);
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string str = hdch.Value;
        }
    }

}
