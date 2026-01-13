using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
//using Subgurim.Controles;                 // GMap3
//using System.Data.DataSetExtensions;      // AsEnumerable, CopyToDataTable

namespace CDSN
{
    public partial class CPRNEW : System.Web.UI.Page
    {
        int ii = 0;
        int b = 0;

        // EF context
        private readonly CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
                // Keep same initial behavior as before (optional)
                SetGoogleMap(0);
            }
        }

        private void BindControl()
        {
            using (var ctx = new CDSEntities())
            {
                FillChannelType(ctx);
                FillCountry(ctx);
            }
        }

        private void FillPropritors(CDSEntities ctx)
        {
            // (intentionally blank, same as legacy)
        }

        private void FillChannelType(CDSEntities ctx)
        {
            var r = ctx.tblChannelTypes
                       .Where(x => x.active == true)
                       .Select(x => new { x.ID, x.ChannelType })
                       .OrderBy(x => x.ChannelType)
                       .ToList();

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

        private void FillCountry(CDSEntities ctx)
        {
            // You were using Helper.UID; keep it
            var result = ctx.Database.SqlQuery<CountryResult>(
                "EXEC usp_GetCountryByUserId @UserId",
                new SqlParameter("@UserId", Helper.UID)
            ).ToList();

            ddlCountry.DataSource = result;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();

            ddlCountry_SelectedIndexChanged(null, null);
        }

        private void FillRegion(CDSEntities ctx)
        {
            try
            {
                int countryId = Convert.ToInt32(ddlCountry.SelectedValue);
                int userId = Convert.ToInt32(Session["userid"]);

                var r = ctx.usp_GetRegionByCountryIdUserId(countryId, userId)
                           .Select(u => new { Regionid = u.RegionId, regionname = u.RegionName })
                           .OrderBy(u => u.regionname)
                           .ToList();

                ddlRegion.DataTextField = "regionname";
                ddlRegion.DataValueField = "Regionid";
                ddlRegion.DataSource = r;
                ddlRegion.DataBind();

                ddlRegion_SelectedIndexChanged(null, null);
            }
            catch
            {
                // ignore
            }
        }

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if you want to refresh map on channel-type change:
            // SetGoogleMap(0);
        }

        // --- MAP: uses Subgurim the same way you had, but with SqlClient ---
        private void SetGoogleMap(int operatorId)
        {
            try
            {
                // 1) Load data (same SP you used before)
                var conStr = db.Database.Connection.ConnectionString;
                var dt = new DataTable();
                using (var con = new SqlConnection(conStr))
                using (var cmd = new SqlCommand("usp_GetMergedGoogleData", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperatorId", SqlDbType.Int).Value = operatorId;
                    con.Open();
                    da.Fill(dt);
                }

                // 2) Build marker payload for JS (lat, lng, html, icon)
                var markers = dt.Rows.Cast<DataRow>()
                    .Select(r =>
                    {
                        var lat = SafeDouble(r, "lat");
                        var lng = SafeDouble(r, "lng");

                        // Use InfoWindowText from DB if present (often contains <a onclick="showalert(id)">…</a>)
                        var html = SafeString(r, "InfoWindowText");
                        if (string.IsNullOrWhiteSpace(html))
                        {
                            // Fallback if your SP doesn’t provide InfoWindowText
                            var subAreaId = SafeString(r, "SubAreaId");
                            var subAreaName = HttpUtility.HtmlEncode(SafeString(r, "SubAreaName"));
                            if (!string.IsNullOrEmpty(subAreaId) && !string.IsNullOrEmpty(subAreaName))
                                html = $"<a href='#' onclick='showalert({subAreaId});return false;'>{subAreaName}</a>";
                        }

                        var icon = SafeString(r, "IconImage"); // e.g. "Images/Pin_Red.png"
                        return new { lat, lng, html, icon };
                    })
                    .Where(m => !double.IsNaN(m.lat) && !double.IsNaN(m.lng))
                    .ToList();

                // 3) Center logic: prefer a flagged “A” row; else first marker; else default
                double centerLat = 24.8607, centerLng = 67.0011; // default center
                var active = dt.Rows.Cast<DataRow>().FirstOrDefault(row =>
                {
                    // old code used dr[9] == 'A' — try a named column first (“Flag”), else index 9
                    var flag = SafeString(row, "Flag");
                    if (string.IsNullOrEmpty(flag))
                    {
                        try { flag = Convert.ToString(row[9]); } catch { }
                    }
                    return flag == "A";
                });
                if (active != null)
                {
                    centerLat = SafeDouble(active, "lat");
                    centerLng = SafeDouble(active, "lng");
                }
                else if (markers.Count > 0)
                {
                    centerLat = markers[0].lat;
                    centerLng = markers[0].lng;
                }

                // 4) Push into the page; initMap() (in ASPX) consumes these globals
                var ser = new JavaScriptSerializer();
                var markersJson = ser.Serialize(markers);
                var centerJson = ser.Serialize(new { lat = centerLat, lng = centerLng });

                var script = $@"
            window.__markers = {markersJson};
            window.__mapCenter = {centerJson};
            if (window.google && window.google.maps && typeof window.initMap === 'function') {{
                window.initMap();
            }}";

                ScriptManager.RegisterStartupScript(this, GetType(), "pushMapData_" + Guid.NewGuid(), script, true);
            }
            catch
            {
                // swallow/log — map just won’t render if something fails
            }
        }

        private static string SafeString(DataRow r, string col)
        {
            try { return Convert.ToString(r[col]); } catch { return ""; }
        }
        private static double SafeDouble(DataRow r, string col)
        {
            try { return Convert.ToDouble(r[col]); } catch { return double.NaN; }
        }

        protected void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                ii = 0; b = 0;
                SetGoogleMap(0);

                string iChannelType = ddlChannelType.SelectedValue;
                string sRegion = ddlRegion.SelectedValue;
                string sTerritory = Helper.GetDDLValues(ddlTerritory);
                string sDivision = Helper.GetDDLValues(ddlDivision);
                string sCity = Helper.GetDDLValues(ddlCity);

                string conStr = db.Database.Connection.ConnectionString;

                using (var con = new SqlConnection(conStr))
                {
                    con.Open();

                    // 1) get base SQL from SP
                    DataSet ds = new DataSet();
                    using (var cmd = new SqlCommand("sp_GetPVTListCPRNews", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ChannelTypeId", SqlDbType.VarChar, 50).Value = iChannelType ?? string.Empty;
                        cmd.Parameters.Add("@RegionString", SqlDbType.VarChar, -1).Value = sRegion ?? string.Empty;
                        cmd.Parameters.Add("@DivisionString", SqlDbType.VarChar, -1).Value = sDivision ?? string.Empty;
                        cmd.Parameters.Add("@CityString", SqlDbType.VarChar, -1).Value = sCity ?? string.Empty;
                        cmd.Parameters.Add("@TerritoryString", SqlDbType.VarChar, -1).Value = sTerritory ?? string.Empty;

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }

                    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        gvChannelView.DataSource = null;
                        gvChannelView.DataBind();
                        return;
                    }

                    string baseSql = Convert.ToString(ds.Tables[0].Rows[0][0]);

                    // 2) run the filtered SQL text with parameters
                    string filteredSql =
                        baseSql +
                        " WHERE p.CityId      IN (SELECT * FROM dbo.SplitList_(@pCity))" +
                        "   AND p.DivisionId  IN (SELECT * FROM dbo.SplitList_(@pDivision))" +
                        "   AND p.TerritoryId IN (SELECT * FROM dbo.SplitList_(@pTerritory))" +
                        " ORDER BY 2;";

                    DataTable selectedTable = new DataTable();
                    using (var cmd2 = new SqlCommand(filteredSql, con))
                    {
                        cmd2.Parameters.Add("@pCity", SqlDbType.VarChar, -1).Value = sCity ?? string.Empty;
                        cmd2.Parameters.Add("@pDivision", SqlDbType.VarChar, -1).Value = sDivision ?? string.Empty;
                        cmd2.Parameters.Add("@pTerritory", SqlDbType.VarChar, -1).Value = sTerritory ?? string.Empty;

                        using (var da2 = new SqlDataAdapter(cmd2))
                        {
                            da2.Fill(selectedTable);
                        }
                    }

                    if (selectedTable.Rows.Count == 0)
                    {
                        gvChannelView.DataSource = null;
                        gvChannelView.DataBind();
                        return;
                    }

                    // headers list (for your repeating header logic)
                    var al = new ArrayList { "Info", "Area", "Activity" };
                    foreach (DataColumn col in selectedTable.Columns) al.Add(col.ColumnName);
                    ViewState["gvData"] = al;

                    // sort
                    var sorted = selectedTable.AsEnumerable()
                        .OrderBy(r => selectedTable.Columns.Contains("District") ? r.Field<string>("District") : null)
                        .ThenBy(r => selectedTable.Columns.Contains("City") ? r.Field<string>("City") : null)
                        .ThenBy(r => selectedTable.Columns.Contains("Operator") ? r.Field<string>("Operator") : null);

                    DataTable sortedTable = sorted.CopyToDataTable();

                    gvChannelView.DataSource = sortedTable;
                    gvChannelView.DataBind();

                    SetColors();
                }
            }
            catch
            {
                gvChannelView.DataSource = null;
                gvChannelView.DataBind();
            }
        }

        // --- Cascading selectors ---

        private void FillTerritory(string str, CDSEntities ctx)
        {
            int regionid = Convert.ToInt32(ddlRegion.SelectedValue);
            int userid = Convert.ToInt32(Session["UserId"]);

            var r = ctx.usp_GetTerritoryByRegionIdByUser(regionid, userid).ToList();

            ddlTerritory.DataTextField = "TerritoryName";
            ddlTerritory.DataValueField = "id";
            ddlTerritory.DataSource = r;
            ddlTerritory.DataBind();

            ddlTerritory_SelectedIndexChanged(null, null);
        }

        private void FillDivision(string str, CDSEntities ctx)
        {
            var r = ctx.usp_GetDivisionListByTerritoryList(str).ToList();

            ddlDivision.DataTextField = "DivisionName";
            ddlDivision.DataValueField = "id";
            ddlDivision.DataSource = r;
            ddlDivision.DataBind();

            ddlDivision_SelectedIndexChanged(null, null);
        }

        private void FillCity(string str, CDSEntities ctx)
        {
            var r = ctx.usp_GetCityListByDivisionId(str).ToList();

            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataSource = r;
            ddlCity.DataBind();
        }

        private void FillCity(string str)
        {
            // If you need a version without context
            var r = db.usp_GetCityListByDivisionId(str).ToList();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataSource = r;
            ddlCity.DataBind();
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTerritory("", db);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegion(db);
        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = "";
            int count = 0;

            if (e != null)
            {
                foreach (ListItem item in (sender as ListControl).Items)
                {
                    if (item.Selected)
                    {
                        selected = (selected.Length == 0) ? item.Value : selected + ";" + item.Value;
                        count++;
                    }
                }

                if (count > 0)
                {
                    FillDivision(selected, db);
                }
                else
                {
                    ddlDivision.Items.Clear();
                    ddlCity.Items.Clear();
                }
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = "";
            int count = 0;

            if (e != null)
            {
                foreach (ListItem item in (sender as ListControl).Items)
                {
                    if (item.Selected)
                    {
                        selected = (selected.Length == 0) ? item.Value : selected + ";" + item.Value;
                        count++;
                    }
                }

                ddlCity.Items.Clear();
                if (count > 0)
                {
                    FillCity(selected, db);
                }
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no-op (keep for parity)
        }

        // --- Grid events / UI coloring ---

        protected void gvChannelView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // if you still need to hide the first few key columns at render time:
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                // adjust indexes to match your bound columns
                if (e.Row.Cells.Count > 3)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
            }
        }

        protected void gvChannelView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Repeating header every 25 rows (same behavior as your legacy code)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ii++;
                if (ii == 26)
                {
                    b += 25;
                    try
                    {
                        ArrayList Al = (ArrayList)ViewState["gvData"];
                        if (Al != null && gvChannelView.Controls.Count > 0)
                        {
                            GridViewRow headerRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                            foreach (var item in Al)
                            {
                                string name = Convert.ToString(item);
                                if (name == "OperatorId" || name == "DivisionId" || name == "CityId" || name == "TerritoryId")
                                    continue;

                                TableCell cell = new TableCell
                                {
                                    Text = name
                                };
                                cell.Font.Bold = true;
                                cell.ColumnSpan = 1;
                                headerRow.Cells.Add(cell);
                            }
                            gvChannelView.Controls[0].Controls.AddAt(b, headerRow);
                            ii = 0;
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }

        private void SetColors()
        {
            foreach (GridViewRow drv in gvChannelView.Rows)
            {
                int columnscount = drv.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    try
                    {
                        int val;
                        if (!int.TryParse(drv.Cells[j].Text, out val)) val = 0;

                        if (val > 0 && val < 30)
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(164, 224, 125);
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                        else if (val > 29 && val < 50)
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(247, 247, 168);
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                        else if (val > 49)
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(246, 195, 195);
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }

                        if (drv.Cells[j].Text == "&nbsp;")
                        {
                            drv.Cells[j].BackColor = System.Drawing.Color.FromArgb(250, 100, 100);
                            drv.Cells[j].ForeColor = System.Drawing.Color.Black;
                            drv.Cells[j].Text = "NA";
                            drv.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }

        // --- Search button (text filters) ---
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string conStr = db.Database.Connection.ConnectionString;
                DataSet ds;

                using (var con = new SqlConnection(conStr))
                using (var cmd = new SqlCommand("sp_GetPVTListCPRSearch", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ChannelTypeId", SqlDbType.Int).Value = Convert.ToInt32(ddlChannelType.SelectedValue);
                    cmd.Parameters.Add("@OperatorString", SqlDbType.VarChar, -1).Value = txtOperator.Text.Trim();
                    cmd.Parameters.Add("@DivisionString", SqlDbType.VarChar, -1).Value = txtDistrict.Text.Trim();
                    cmd.Parameters.Add("@CityString", SqlDbType.VarChar, -1).Value = txtCity.Text.Trim();
                    cmd.Parameters.Add("@UserString", SqlDbType.VarChar, 128).Value = Convert.ToString(Session["LoginID"]);

                    ds = new DataSet();
                    con.Open();
                    da.Fill(ds);
                }

                gvChannelView.DataSource = ds;
                gvChannelView.DataBind();
                SetColors();
            }
            catch
            {
                gvChannelView.DataSource = null;
                gvChannelView.DataBind();
            }
        }

        // --- Optional: Subgurim marker server event (kept same signature) ---
        //protected string GMap3_MarkerClick(object s, GAjaxServerEventArgs e)
        //{
        //    string id = "";
        //    try
        //    {
        //        if ((e.point.lat.ToString().Length >= 6) && (e.point.lng.ToString().Length >= 6))
        //        {
        //            string lat = e.point.lat.ToString().Substring(0, 6);
        //            string lng = e.point.lng.ToString().Substring(0, 6);

        //            // If you still have SubAreaDB, keep using it. Otherwise, replace with a SP call.
        //            var subAreaRepo = new SubAreaDB(db);            // pass your CDSEntities
        //            DataTable dt = subAreaRepo.GetOperatorList(lat, lng);
        //            string ID = (dt.Rows.Count > 0) ? Convert.ToString(dt.Rows[0][0]) : "";
        //            return $"showalert('{ID}')";

        //        }
        //    }
        //    catch { }
        //    return $"showalert('{id}')";
        //}
    }
}
