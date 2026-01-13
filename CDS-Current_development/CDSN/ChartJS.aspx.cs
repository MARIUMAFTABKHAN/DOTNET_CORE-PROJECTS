using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class ChartJS : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillTerritory();
                FillDistricts();
                FillCity();
                FillArea();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }


        private void FillTerritory()
        {
            var t = db.usp_GetUserTerritoryListByCountry(Helper.UID, 110000001).ToList();
            {

                ddlTerritory.DataTextField = "TerritoryName";
                ddlTerritory.DataValueField = "id";
                ddlTerritory.DataSource = t;
                ddlTerritory.DataBind();
                ddlTerritory.Items.Insert(0, new ListItem("Select Territory", "0"));

            }
        }

        private void FillDistricts()
        {

            //int id = Convert.ToInt32(ddlTerritory.SelectedValue);
            //var r = db.usp_GetAllDistrictsByUserIdTerritoryId(Helper.UID, id);
            //ddlDistricts.DataTextField = "DivisionName";
            //ddlDistricts.DataValueField = "id";
            //ddlDistricts.DataSource = r;
            //ddlDistricts.DataBind();
            //ddlDistricts.Items.Insert(0, new ListItem("Select District", "0"));

            

            int territoryId = Convert.ToInt32(ddlTerritory.SelectedValue);

            var result = db.Database.SqlQuery<DistrictResult>(
                "EXEC usp_GetAllDistrictsByUserIdTerritoryId @UserId, @TerritoryId",
                new SqlParameter("@UserId", Helper.UID),
                new SqlParameter("@TerritoryId", territoryId)
            ).ToList();

            ddlDistricts.DataSource = result;
            ddlDistricts.DataTextField = "DivisionName";
            ddlDistricts.DataValueField = "Id";
            ddlDistricts.DataBind();
            ddlDistricts.Items.Insert(0, new ListItem("Select District", "0"));


        }
        private void FillCity()
        {
            string str = Helper.LBoxStr(ddlDistricts);
            var r = db.usp_GetCityListByDivisionId(str).ToList();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataSource = r;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
        }

        private void FillArea()
        {
            string str = Helper.LBoxStr(ddlCity);
            var r = db.usp_GetAllAreaByCityList(str).ToList().OrderBy(x => x.AreaId);
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Areaid";
            ddlArea.DataSource = r;
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("Select Area", "0"));
        }
        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistricts();
        }
        protected void ddlDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillArea();
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnTerritory_Click(object sender, ImageClickEventArgs e)
        {
            string id = Helper.DDLStr(ddlTerritory);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showalert(" + id + ");", true);

            // string str = Helper.DDLStr(ddlTerritory);
            // Int32  r = Convert.ToInt32(  db.usp_GetRemainingViewersByTerritory(110000001, str).SingleOrDefault());



            //Int32 remaining = 0;

            //if (r  > 0)
            //{
            //    // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
            //    try
            //    {
            //        remaining = Convert.ToInt32(r);
            //    }
            //    catch (Exception)
            //    {

            //        remaining = 0;// Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            //    }

            //}

            //var dr  = db.usp_SubsribersByTerritoryList(str).ToList ();
            //DataTable ds1 =  Helper.ToDataTable(dr);
            //DataRow dataRow = ds1.NewRow();
            //dataRow[0] = "Remaining";
            //dataRow[1] = remaining;
            //ds1.Rows.InsertAt(dataRow, 0);            
            //ds1.TableName = "demoChar";
            ShowLegendsTerritory(Helper.DefaultCountry, Convert.ToInt32(ddlTerritory.SelectedValue));
        }
        protected void BtnArea_Click(object sender, ImageClickEventArgs e)
        {
            int remaining = 0;
            int total = 0;
            int balance = 0;

            string citylist = Helper.LBoxStr(ddlCity);
            string arealist = Helper.LBoxStr(ddlArea); ;

            //DataSet ds = db.ExecuteDataSet (CommandType .StoredProcedure ,"[dbo].[usp_GetViewersByTerritory]");
            var ds = db.usp_GetRemainingViewersByAreaList(citylist, arealist).ToList();
            try
            {
                remaining = ds.Count();
            }
            catch (Exception)
            {
                remaining = 0;// Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            var ds11 = db.usp_SubsribersByAreaList(citylist, arealist).ToList();
            DataTable ds1 = Helper.ToDataTable(ds11);
            DataRow dataRow = ds1.NewRow();
            dataRow[0] = "Remaining";
            dataRow[1] = remaining;
            ds1.Rows.InsertAt(dataRow, 0);
            ds1.TableName = "demoChar";
            ShowLegendsArea(citylist, arealist);
        }
        private void ShowLegendsTerritory(int CountryId, int TerritoryList)
        {
            // var  ds = db.usp_GetViewership_TerritoryLegends(CountryId, TerritoryList).ToList();
            //var ds = db.Get_PositionBars("B", TerritoryList, "0", "0", "0", 110000001).ToList();
            var ds = db.sp_GetLast5List(TerritoryList).ToList();


            if (ds.Count > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {

                    GridViewViewership.DataSource = ds;
                    GridViewViewership.DataBind();
                }
                catch (Exception)
                {

                    GridViewViewership.DataSource = null;
                    GridViewViewership.DataBind();
                }

            }
        }

        private void ShowLegendsArea(string CityList, string AreaList)
        {



            //DataSet ds = db.ExecuteDataSet (CommandType .StoredProcedure ,"[dbo].[usp_GetViewersByTerritory]");
            var ds = db.usp_GetViewership_AreaLegends(CityList, AreaList).ToList();


            if (ds.Count > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {

                    GridViewViewership.DataSource = ds;
                    GridViewViewership.DataBind();
                }
                catch (Exception)
                {

                    GridViewViewership.DataSource = null;
                    GridViewViewership.DataBind();
                }

            }
        }
        protected void BtnCity_Click(object sender, ImageClickEventArgs e)
        {



            string citylist = Helper.LBoxStr(ddlCity);
            string districtlist = Helper.LBoxStr(ddlDistricts);
            int remaining = 0;


            //DataSet ds = db.ExecuteDataSet (CommandType .StoredProcedure ,"[dbo].[usp_GetViewersByTerritory]");
            var ds = db.usp_GetRemainingViewersByCityList(districtlist, citylist).SingleOrDefault();


            if (ds.Value > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {
                    remaining = ds.Value;
                }
                catch (Exception)
                {

                    remaining = 0;// Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

            }

            var ds11 = db.usp_SubsribersByCityList(citylist).ToList();
            DataTable ds1 = Helper.ToDataTable(ds11);
            DataRow dataRow = ds1.NewRow();
            dataRow[0] = "Remaining";
            dataRow[1] = remaining;
            ds1.Rows.InsertAt(dataRow, 0);
            ds1.TableName = "demoChar";
            Showviewership_CityLegends(districtlist, citylist);
        }
        protected void BtnDistrict_Click(object sender, ImageClickEventArgs e)
        {

            string TerritoryList = Helper.DDLStr(ddlTerritory);
            string DistrictList = Helper.LBoxStr(ddlDistricts);
            int remaining = 0;
            int total = 0;
            int balance = 0;

            //DataSet ds = db.ExecuteDataSet (CommandType .StoredProcedure ,"[dbo].[usp_GetViewersByTerritory]");
            var ds = db.usp_GetRemainingViewersByDistrict(TerritoryList, DistrictList).SingleOrDefault();

            if (ds.Value > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {
                    remaining = Convert.ToInt32(ds.Value);
                }
                catch (Exception)
                {

                    remaining = 0;// Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

            }

            var ds11 = db.usp_SubsribersByDistrict(DistrictList).ToList();
            DataTable ds1 = Helper.ToDataTable(ds11);
            DataRow dr = ds1.NewRow();
            dr[0] = "Remaining";
            dr[1] = remaining;
            ds1.Rows.InsertAt(dr, 0);


            ds1.TableName = "demoChar";

            Showviewership_DistrictLegends(TerritoryList, DistrictList);

        }

        private void Showviewership_DistrictLegends(string TerritoryList, string DistrictList)
        {
            string districtlist = Helper.LBoxStr(ddlDistricts);
            string territorylist = Helper.DDLStr(ddlTerritory);
            var ds = db.usp_GetViewership_DistrictLegends(territorylist, DistrictList).ToList(); ;


            if (ds.Count > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {
                    GridViewViewership.DataSource = ds;
                    GridViewViewership.DataBind();
                }
                catch (Exception)
                {

                    GridViewViewership.DataSource = null;
                    GridViewViewership.DataBind();
                }

            }
        }
        private void Showviewership_CityLegends(string DistrictList, string CityList)
        {
            string districtlist = Helper.LBoxStr(ddlDistricts);
            string citylist = Helper.LBoxStr(ddlCity);


            var ds = db.usp_GetViewership_CityLegends(districtlist, citylist).ToList();

            if (ds.Count > 0)
            {
                // total = Convert.ToInt32 (ds.Tables [0].Rows [0][0]);
                try
                {
                    GridViewViewership.DataSource = ds;
                    GridViewViewership.DataBind();
                }
                catch (Exception)
                {

                    GridViewViewership.DataSource = null;
                    GridViewViewership.DataBind();
                }

            }
        }
        protected void lblTerritory2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnTempshow_Click(object sender, EventArgs e)
        {

        }

        protected void BtnTerritory_Click1(object sender, ImageClickEventArgs e)
        {

        }
    }

}