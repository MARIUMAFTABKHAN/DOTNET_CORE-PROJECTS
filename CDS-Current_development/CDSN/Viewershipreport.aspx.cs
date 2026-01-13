using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Viewershipreport : System.Web.UI.Page
    {
        CDSEntities db =  new CDSEntities ();       
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
            var t = db.usp_GetUserTerritoryListByCountry(Helper.UID,110000001).ToList(); 
            {
                
                ddlTerritory.DataTextField = "TerritoryName";
                ddlTerritory.DataValueField = "id";
                ddlTerritory.DataSource = t;
                ddlTerritory.DataBind();
                //ddlTerritory.Items.Insert(0, new ListItem("Select Territory", "0")) ;

            }
        }
        
        private void FillDistricts()
        {
            var selectedItems = ddlTerritory.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                            .ToList();

            if (selectedItems.Count == 0)
            {
                return;
            }

            string territoryIdList = string.Join(";", selectedItems);

            var result = db.Database.SqlQuery<DistrictResult>(
                "EXEC usp_GetAllDistrictsByUserIdTerritoryId @UserId, @TerritoryId",
                new SqlParameter("@UserId", Helper.UID),
                new SqlParameter("@TerritoryId", territoryIdList)
            ).ToList();

            ddlDistricts.DataSource = result;
            ddlDistricts.DataTextField = "DivisionName";
            ddlDistricts.DataValueField = "Id";
            ddlDistricts.DataBind();
           // ddlDistricts.Items.Insert(0, new ListItem("Select District", "0"));

        }
        private void FillCity()
        {
            string str = Helper.LBoxStr(ddlDistricts);
            var  r = db.usp_GetCityListByDivisionId(str).ToList();            
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataSource = r;
            ddlCity.DataBind();
            //ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
        }

        private void FillArea()
        {
            string str = Helper.LBoxStr(ddlCity);
            var r = db.usp_GetAllAreaByCityList(str).ToList().OrderBy(x=> x.AreaId);            
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Areaid";
            ddlArea.DataSource = r;
            ddlArea.DataBind();
           // ddlArea.Items.Insert(0, new ListItem("Select Area", "0"));
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
            string selectedTerritories = Helper.LBoxStr(ddlTerritory); 

            var legendList = db.usp_GetViewership_TerritoryLegends(110000001, selectedTerritories).ToList();

            int selectedTotal = legendList.Sum(x => x.Subscribers ?? 0);

            int remainingViewers = Convert.ToInt32(db.usp_GetRemainingViewersByTerritory(110000001, selectedTerritories).SingleOrDefault());

            GridViewTerritory.DataSource = legendList;
            GridViewTerritory.DataBind();

            // Prepare chart data
            var labels = legendList.Select(x => x.AreaName).ToList();
            var values = legendList.Select(x => x.Subscribers ?? 0).ToList();

            labels.Add("Remaining");
            values.Add(remainingViewers);

            string jsonLabels = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(labels);
            string jsonValues = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "renderChart",
                $"renderDynamicPieChart({jsonLabels}, {jsonValues});",
                true
            );
            //ShowLegendsTerritory(Helper.DefaultCountry, Convert.ToInt32( ddlTerritory.SelectedValue ));
        }

        protected void BtnDistrict_Click(object sender, ImageClickEventArgs e)
        {
            string selectedterritories = Helper.LBoxStr(ddlTerritory);

            string selectedDistricts = Helper.LBoxStr(ddlDistricts);

            var legendList = db.usp_GetViewership_DistrictLegends(selectedterritories, selectedDistricts).ToList();

            int selectedTotal = legendList.Sum(x => x.Subscribers ?? 0);

            int remainingViewers = Convert.ToInt32(db.usp_GetRemainingViewersByDistrict(selectedterritories, selectedDistricts).SingleOrDefault());

            GridViewTerritory.DataSource = legendList;
            GridViewTerritory.DataBind();

            // Prepare chart data
            var labels = legendList.Select(x => x.AreaName).ToList();
            var values = legendList.Select(x => x.Subscribers ?? 0).ToList();

            labels.Add("Remaining");
            values.Add(remainingViewers);

            string jsonLabels = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(labels);
            string jsonValues = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "renderChart",
                $"renderDynamicPieChart2({jsonLabels}, {jsonValues});",
                true
            );

        }

        protected void BtnCity_Click(object sender, ImageClickEventArgs e)
        {
            string selectedDistricts = Helper.LBoxStr(ddlDistricts);

            string selectedcities = Helper.LBoxStr(ddlCity);

            var legendList = db.usp_GetViewership_CityLegends(selectedDistricts, selectedcities).ToList();

            int selectedTotal = legendList.Sum(x => x.Subscribers ?? 0);

            int remainingViewers = Convert.ToInt32(db.usp_GetRemainingViewersByCityList(selectedDistricts, selectedcities).SingleOrDefault());

            GridViewTerritory.DataSource = legendList;
            GridViewTerritory.DataBind();

            // Prepare chart data
            var labels = legendList.Select(x => x.AreaName).ToList();
            var values = legendList.Select(x => x.Subscribers ?? 0).ToList();

            labels.Add("Remaining");
            values.Add(remainingViewers);

            string jsonLabels = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(labels);
            string jsonValues = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "renderChart",
                $"renderDynamicPieChart3({jsonLabels}, {jsonValues});",
                true
            );
        }

        protected void BtnArea_Click(object sender, ImageClickEventArgs e)
        {
            string selectedcities = Helper.LBoxStr(ddlCity);

            string selectedareas = Helper.LBoxStr(ddlArea);

            var legendList = db.usp_GetViewership_AreaLegends(selectedcities, selectedareas).ToList();

            int selectedTotal = legendList.Sum(x => x.Subscribers ?? 0);

            int remainingViewers = Convert.ToInt32(db.usp_GetRemainingViewersByAreaList(selectedcities, selectedareas).SingleOrDefault());

            GridViewTerritory.DataSource = legendList;
            GridViewTerritory.DataBind();

            // Prepare chart data
            var labels = legendList.Select(x => x.AreaName).ToList();
            var values = legendList.Select(x => x.Subscribers ?? 0).ToList();

            labels.Add("Remaining");
            values.Add(remainingViewers);

            string jsonLabels = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(labels);
            string jsonValues = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values);

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "renderChart",
                $"renderDynamicPieChart4({jsonLabels}, {jsonValues});",
                true
            );
        }
        
    }

}