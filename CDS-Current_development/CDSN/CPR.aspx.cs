using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Microsoft.Ajax.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CDSN
{
    public partial class CPR : System.Web.UI.Page
    {

        CDSEntities db = new CDSEntities();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
               hfGoogleMapsKey.Value = System.Configuration.ConfigurationManager.AppSettings["GoogleMapsApiKey"];
            }
            if (IsPostBack)
            {

            }
        }
        private void BindControl()
        {
            using (CDSEntities db = new CDSEntities())
            {
                FillChannelType(db);
                FillCountry(db);
            }
        }

        private void FillChannelType(CDSEntities db)
        {

            var r = (from u in db.tblChannelTypes.Where(x => x.active == true)
                     select new { u.ID, u.ChannelType }).OrderBy(u => u.ChannelType).ToList();


            ddlChannelType.DataTextField = "ChannelType";
            ddlChannelType.DataValueField = "ID";
            ddlChannelType.DataSource = r;
            ddlChannelType.DataBind();
        }

        public class CountryResult
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
            public int UserId { get; set; }
        }

        private void FillCountry(CDSEntities db)
        {
            int uid = Convert.ToInt32(Session["userid"]);

            var result = db.Database.SqlQuery<CountryResult>(
                    "EXEC usp_GetCountryByUserId @UserId",
                    new SqlParameter("@UserId", Helper.UID)
                ).ToList();

            var r = (from u in db.tblusercountries.Where(x => x.UserId == uid)
                     select new { u.CountryId, u.tblCountry.CountryName }).OrderBy(u => u.CountryName).ToList();

            ddlCountry.DataSource = result;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();

            ddlCountry_SelectedIndexChanged(null, null);

        }
        private void FillRegion(CDSEntities db)
        {

            try
            {
                int CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                int UserId = Convert.ToInt32(Session["userid"]);
                var r = (from u in db.usp_GetRegionByCountryIdUserId(CountryId, UserId)
                         select new { Regionid = u.RegionId, regionname = u.RegionName }).OrderBy(u => u.regionname).ToList();

                ddlRegion.DataTextField = "regionname";
                ddlRegion.DataValueField = "Regionid";
                ddlRegion.DataSource = r.ToList();
                ddlRegion.DataBind();
                ddlRegion_SelectedIndexChanged(null, null);

            }
            catch (Exception)
            {


            }

        }

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillTerritory(string str, CDSEntities db)
        {
            int regionid = Convert.ToInt32(ddlRegion.SelectedValue);
            int userid = Convert.ToInt32(Session["UserId"]);


            var r = (from u in db.usp_GetTerritoryByRegionIdByUser(regionid, userid)
                     select u).ToList();

            ddlTerritory.DataTextField = "TerritoryName";
            ddlTerritory.DataValueField = "id";
            ddlTerritory.DataSource = r;
            ddlTerritory.DataBind();

            ddlTerritory_SelectedIndexChanged(null, null);


        }
        private void FillDivision(string str, CDSEntities db)
        {
            var r = (from u in db.usp_GetDivisionListByTerritoryList(str)
                     select u).ToList();

            ddlDivision.DataTextField = "DivisionName";
            ddlDivision.DataValueField = "id";
            ddlDivision.DataSource = r;
            ddlDivision.DataBind();
            ddlDivision_SelectedIndexChanged(null, null);
        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList Territory = new ArrayList();
            string str = "";
            int i = 0;
            if (e != null)
            {
                try
                {

                    int itemcount = (sender as ListControl).Items.Count;

                    string strTerritory = "";
                    foreach (ListItem item in (sender as ListControl).Items)
                    {
                        if (item.Selected == true)
                        {

                            // Territory.Add(item.Value.ToString());
                            if (str.Trim().Length == 0)
                            {
                                str = item.Value.ToString();
                                strTerritory = item.Text;
                            }
                            else
                            {
                                str = str + ";" + item.Value.ToString();
                                strTerritory = strTerritory + ";" + item.Text;
                            }
                            i++;
                        }


                    }
                }
                catch (Exception)
                {

                    throw;
                }
                if (i > 0)
                {
                    FillDivision(str, db);
                }
                else
                {

                }
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 ii = 0;
            Int32 b = 0;
            ArrayList Division = new ArrayList();
            int i = 0;
            if (e != null)
            {
                int itemcount = (sender as ListControl).Items.Count;
                string str = "";
                string strDistrict = "";
                foreach (ListItem item in (sender as ListControl).Items)
                {
                    if (item.Selected == true)
                    {

                        Division.Add(item.Value.ToString());
                        if (str.Trim().Length == 0)
                        {
                            str = item.Value.ToString();
                            strDistrict = item.Text;
                        }
                        else
                        {
                            str = str + ";" + item.Value.ToString();
                            strDistrict = strDistrict + ";" + item.Text;
                        }
                        i++;
                    }
                    ViewState["ALDivision"] = Division;

                }
                ddlCity.Items.Clear();
                if (i > 0)
                {
                    FillCity(str, db);
                }
                else
                {

                }
            }

        }

        private void FillCity(string str, CDSEntities db)
        {

            var r = (from u in db.usp_GetCityListByDivisionId(str)
                     select u).ToList();
            ddlCity.DataSource = r;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataBind();

        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTerritory("", db);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegion(db);
        }


        protected void BtnShow_Click(object sender, EventArgs e)
        {

            try
            {


                string iCountryId = ddlCountry.SelectedValue;// .ClientID %> option:selected').val()
                string iChannelType = Convert.ToString(ddlChannelType.SelectedValue);//   $('#<%=ddlChannelType.ClientID %> option:selected').val();

                string sRegion = Convert.ToString(ddlRegion.SelectedValue);// .ClientID %> option:selected').val()//$('#<%=HDFRegion.ClientID %>').val();

                string sTerritory = Helper.GetDDLValues(ddlTerritory);// GetTerritoryList();// Convert.ToString(HDFTerritory.Value);//.ClientID %>').val();
                string sDivision = Helper.GetDDLValues(ddlDivision);// GetDivisionList();// Convert.ToString(HDFDivision.Value);//.ClientID %>').val();
                string sCity = Helper.GetDDLValues(ddlCity); ;// GetCityList();// Convert.ToString(HDFCity.Value);//.ClientID %>').val();

                string ConString = db.Database.Connection.ConnectionString;

                SqlCommand cmd = new SqlCommand();
                using (SqlConnection sql = new SqlConnection(ConString))
                {
                    sql.Open();
                    cmd.Connection = sql;

                    cmd.Parameters.AddWithValue("@ChannelTypeId", SqlDbType.VarChar).Value = iChannelType;
                    cmd.Parameters.AddWithValue("@RegionString", SqlDbType.VarChar).Value = sRegion;
                    cmd.Parameters.AddWithValue("@DivisionString", SqlDbType.VarChar).Value = sDivision;
                    cmd.Parameters.AddWithValue("@CityString", SqlDbType.VarChar).Value = sCity;
                    cmd.Parameters.AddWithValue("@TerritoryString", SqlDbType.VarChar).Value = sTerritory;



                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetPVTListCPRNews";

                    //var sp = db.sp_GetPVTListNCDS2
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    ViewState["dt"] = ds;


                    sql.Dispose();
                    sql.Close();

                    gvChannelView.DataSource = ds;
                    gvChannelView.DataBind();

                    SetColors();
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetColors()
        {
            foreach (GridViewRow drv in gvChannelView.Rows)
            {
                int val = 0;
                int columnscount = drv.Cells.Count;




                for (int j = 0; j < columnscount; j++)
                {
                    try
                    {

                        try
                        {
                            val = Convert.ToInt32(drv.Cells[j].Text);
                        }
                        catch (Exception)
                        {
                            val = 0;
                        }

                        if ((val > 0) && (val < 30))
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(164, 224, 125);//.Green;
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                        if ((val > 29) && (val < 50))
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(247, 247, 168);
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                        if ((val > 49))
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(246, 195, 195);//.Red;
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                        if (drv.Cells[j].Text == "&nbsp;")
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(250, 100, 100);//.Blue;
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].Text = "NA";// val = 0;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }


                    }

                    catch (Exception)
                    {

                    }

                }

            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string ConString = db.Database.Connection.ConnectionString;
                //string querystring = d ;
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                string sqltxt = "";
                string userstr = Session["LoginID"].ToString();
                using (SqlConnection sql = new SqlConnection(ConString))
                {
                    sql.Open();
                    cmd.Connection = sql; ;

                    cmd.Parameters.AddWithValue("@ChannelTypeId", SqlDbType.VarChar).Value = Convert.ToInt32(ddlChannelType.SelectedValue);
                    cmd.Parameters.AddWithValue("@OperatorString", SqlDbType.VarChar).Value = txtOperator.Text.Trim();
                    cmd.Parameters.AddWithValue("@DivisionString", SqlDbType.VarChar).Value = txtDistrict.Text.Trim();
                    cmd.Parameters.AddWithValue("@CityString", SqlDbType.VarChar).Value = txtCity.Text.Trim();
                    cmd.Parameters.AddWithValue("@UserString", SqlDbType.VarChar).Value = userstr;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetPVTListCPRSearch";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    dataAdapter.Fill(ds);


                    gvChannelView.DataSource = ds;
                    gvChannelView.DataBind();
                    SetColors();
                    Int32 UID = Convert.ToInt32(Session["userid"]);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void gvChannelView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Area")
            {
                string opId = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "openMap",
                    $"openAreaMap({opId});", true);
            }

            else if (e.CommandName == "Info")
            {
                string rowIndex = e.CommandArgument.ToString();
                GridViewRow row = gvChannelView.Rows[Convert.ToInt32(rowIndex)];
                string operatorId = gvChannelView.DataKeys[row.RowIndex].Values["OperatorId"].ToString();

                // Pass operatorId to JS to fetch modal data
                ScriptManager.RegisterStartupScript(this, GetType(), "showInfo",
                    $"openInfoModal({operatorId});", true);
            }
        }




        protected void gvChannelView_DataBound(object sender, EventArgs e)
        {
            HideColumnByHeader("OperatorId");
            HideColumnByHeader("CityId");
            HideColumnByHeader("DivisionId");
            HideColumnByHeader("TerritoryId");
        }

        private void HideColumnByHeader(string headerText)
        {
            if (gvChannelView.HeaderRow == null) return;

            int idx = -1;
            for (int i = 0; i < gvChannelView.HeaderRow.Cells.Count; i++)
            {
                if (string.Equals(gvChannelView.HeaderRow.Cells[i].Text, headerText, StringComparison.OrdinalIgnoreCase))
                {
                    idx = i; break;
                }
            }
            if (idx >= 0)
            {
                gvChannelView.HeaderRow.Cells[idx].Visible = false;
                foreach (GridViewRow r in gvChannelView.Rows)
                    r.Cells[idx].Visible = false;
            }
        }

        



    }
}


