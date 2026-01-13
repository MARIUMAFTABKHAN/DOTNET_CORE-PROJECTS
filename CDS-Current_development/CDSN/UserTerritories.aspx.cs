using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class UserTerritories : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities ();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillUsers();
                LoadCountry();
                LoadRegion();
                fillGrid();
            }
        }
        private void LoadCountry()
        {

            var dtCountry = db.tblCountries.Where(x => x.active == true).ToList();
            
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataBind();
            ddlCountry_SelectedIndexChanged(null, null);
        }

        private void LoadRegion()
        {
            int id = Convert.ToInt32(ddlCountry.SelectedValue);
            var dtRegion = db.tblRegions.Where(x => x.CountryId == id).ToList();            
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "RegionId";
            ddlRegion.DataSource = dtRegion;
            ddlRegion.DataBind();

            ddlRegion_SelectedIndexChanged(null, null);
         
        }
        private void fillUsers()
        {

            var ds = db.tblUsers.Where(x => x.IsActive == true).ToList();            
            ddlUser.DataTextField = "FirstName";
            ddlUser.DataValueField = "UserID";
            ddlUser.DataSource = ds;
            ddlUser.DataBind();
            
        }
        private void fillGrid()
        {
            try
            {

                int UserId = Convert.ToInt32(ddlUser.SelectedValue);//Convert.ToInt32 (ddlUser.SelectedValue ));
                int RegionId = Convert.ToInt32(ddlRegion.SelectedValue);

                var ds = db.usp_GetUserTerritoryList(UserId, RegionId).ToList();
                gvRoles.DataSource = ds;
                gvRoles.DataBind();
            }
            catch (Exception)
            {
                gvRoles.DataSource = null;// ds;
                gvRoles.DataBind();
                //
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            if (ddlRegion.Items.Count > 0)
            {
                
                int nRecId = 0;             
                int territoryid = 0;
                int userid = Convert.ToInt32(ddlUser.SelectedValue);
                tblUserTerritory obj;//= new tblUserTerritory();
                try
                {
                    foreach (GridViewRow row in gvRoles.Rows)
                    {
                        territoryid = Convert.ToInt32(gvRoles.DataKeys[row.RowIndex].Value);
                        
                        CheckBox chkGrid = (CheckBox)row.FindControl("formSelector");
                        if (chkGrid.Checked == true)
                        {
                            nRecId = Helper.GetCounter(db, "tblUserTerritory");
                            territoryid = Convert.ToInt32(row.Cells[0].Text);
                            obj = new tblUserTerritory();
                            obj.Id = nRecId;
                            obj.TerritoryId = territoryid;
                            obj.UserId = userid;
                            db.tblUserTerritories.Add(obj);
                            db.SaveChanges();

                        }
                        else
                        {
                           // nRecId = Convert.ToInt32(gvRoles.DataKeys[row.RowIndex].Value);
                            var obj1 = db.tblUserTerritories.Where(x => x.TerritoryId == territoryid && x.UserId == userid ).ToList(); ;
                            db.tblUserTerritories.RemoveRange(obj1);
                            db.SaveChanges();


                        }

                    }
                    lblMsg.Visible = true;
                    lblMsg.Text = "Assignment Completed Successfully ...";
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
               
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //  LoadProvince(ddlRegion.SelectedValue);
                // LoadDivision(ddlTerritory.SelectedValue);
                
                fillGrid();
            }
            catch (Exception ex)
            {
                fillGrid();
            }

        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRegion();
                fillGrid();// LoadDivision(ddlProvince.SelectedValue);
            }
            catch (Exception)
            {
                fillGrid();
            }
            

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }

        protected void ddlHeadEnds_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCountry.SelectedIndex = 0;
        }
    }

}