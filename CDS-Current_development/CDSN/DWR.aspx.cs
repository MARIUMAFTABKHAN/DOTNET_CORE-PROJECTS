using AjaxControlToolkit;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class DWR : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var r = (from u in db.tblChannelTypes
                         select new { u.ID, u.ChannelType }).OrderBy(u => u.ChannelType).ToList();
                ddlChannelType.DataTextField = "ChannelType";
                ddlChannelType.DataValueField = "ID";
                ddlChannelType.DataSource = r;
                ddlChannelType.DataBind();

                var cc = (from u in db.usp_GetCountryByUserId(Convert.ToInt32(Session["UserId"]))
                          select new { u.CountryId, u.CountryName }).OrderBy(u => u.CountryName).ToList();

                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataSource = cc;
                ddlCountry.DataBind();

                

                

                ddlCountry_SelectedIndexChanged(sender, e);

            }
        }

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                Int32 CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                Int32 UserID = Convert.ToInt32(Session["UserId"]);

                var s = db.usp_GetRegionByCountryIdUserId(CountryId, UserID).ToList();


                ddlRegion.DataTextField = "RegionName";
                ddlRegion.DataValueField = "RegionId";
                ddlRegion.DataSource = s;
                ddlRegion.DataBind();

                ddlRegion_SelectedIndexChanged(sender, e);
            
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string str = GetRegionList();
            var r = (from u in db.usp_GetUserTerritoryListByRegion(Convert.ToInt32(Session["UserId"]), str)
                     select u).ToList();
            ddlTerritory.DataSource = r;
            ddlTerritory.DataTextField = "TerritoryName";
            ddlTerritory.DataValueField = "id";
            ddlTerritory.DataBind();
        }

        protected void ChkRegion_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private string GetRegionList()
        {
            string str = "";
            foreach (ListItem item in ddlRegion.Items)
            {
                if (item.Selected == true)
                {
                    str += item.Value + ";";
                }
            }
            return str;
        }
        private string GetTerritoryList()
        {
            string str = "";
            foreach (ListItem item in ddlTerritory.Items)
            {
                if (item.Selected == true)
                {
                    str += item.Value + ";";
                }
            }
            return str;
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            PreparaDataTable();
        }
        private void PreparaDataTable()
        {

            Int32 ChannelTypeID = Convert.ToInt32(ddlChannelType.SelectedValue);
            string RegionList = GetRegionList();//  Convert.ToString(ViewState["RegionList"]);
            string TerritoryList = GetTerritoryList(); // Convert.ToString(ViewState["TerritoryList"]);

            DataTable dt = new DataTable();
            DataRow dtRow;
            DataColumn dtColumn = new DataColumn();
            int CountryID = 0;
            try
            {
                CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            catch (Exception)
            {
                CountryID = 0;
            }

            Int32 RegionID, TerritoryID,viewers;
            var ds = db.usp_GetSummary_N(CountryID, RegionList, TerritoryList, 1).ToList();
            dt = Helper.ToDataTable(ds.ToList());
            foreach (DataRow dr in dt.Rows)
            {
                RegionID = Convert.ToInt32(dr[1]);
                TerritoryID = Convert.ToInt32(dr[2]);
                
                dr[5] = db.usp_GetHeadEnds(CountryID, RegionID ,TerritoryID, 2).SingleOrDefault ().Value;
                dr[6] = db.usp_GetViewers(CountryID, RegionID, TerritoryID, 2).SingleOrDefault().Value;
                viewers = Convert.ToInt32(dr[6]);
                dr[7] = db.usp_GetPercentage(CountryID,RegionID, TerritoryID,viewers).SingleOrDefault().Value;
                // Already calculated: viewers = total viewers for territory
                //int viewersCount = Convert.ToInt32(dr[6]);
                //dr[7] = Math.Round(((double)viewersCount / viewers) * 100.0, 2); // percentage for that row

            }
            int Viewers = 0;
            int HeadEnds = 0;

            if (ds.Count() > 0)
            {
                Viewers = db.usp_GetViewersByCountryRegionTerritory(ChannelTypeID, RegionList, TerritoryList).SingleOrDefault().Value;
                HeadEnds = db.usp_GetHeadEndsByCountryRegionTerritory(Convert.ToInt32(ddlChannelType.SelectedValue), RegionList, TerritoryList).SingleOrDefault().Value;
                ViewState["Viewers"] = Viewers;
                ViewState["HeadEnds"] = HeadEnds;

            }

            dtColumn = new DataColumn("Good");
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn("Fair");
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn("Ugly");
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn("NotAvailable");
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn("ChannelName");
            dt.Columns.Add(dtColumn);
            //------------ Subreport Data                    
            DataTable dttemp = null;
            SetColumns(dt, Viewers, HeadEnds, dttemp);


        }

        private int GetHeadend(int cid, int rid, int tid, int flag)
        {
            var dt = db.usp_GetHeadEnds(cid, rid, tid, flag).ToList().Count();
            return dt;
        }
        private int GetViewers(int cid, int rid, int tid, int flag)
        {
            var ds1 = db.usp_GetViewers(cid, rid, tid, flag).SingleOrDefault().Value;
            return Convert.ToInt32(ds1);
        }

        private int GetPercentage(int cid, int rid, int tid, int Viewers)
        {
            int tviewers = Convert.ToInt32(TotalViewers.Text);
            var ds1 = db.usp_GetPercentage(cid, rid, tid, tviewers).SingleOrDefault().Value;
            return Convert.ToInt32(ds1);
        }
        private int GetPercentage_ByRegion(int cid, int rid, int Viewers)
        {
            int tviewers = Convert.ToInt32(TotalViewers.Text);
            var ds1 = db.usp_GetTerritoryPercentage_Region(cid, rid.ToString(), tviewers).SingleOrDefault().Value;
            return Convert.ToInt32(ds1);
        }
        private int GetSlab(int ChannelId, int RegionId, int TerritoryId, int ChannelTypeId, int StartingPostion, int EndingPosition)
        {

            int i = 0;
            i = Convert.ToInt32(db.usp_GetChannelSlab(ChannelId, RegionId, TerritoryId, ChannelTypeId, StartingPostion, EndingPosition).SingleOrDefault().Value);
            return i;
        }
        private void SetColumns(DataTable ds, int Viewers, int HeadEnds, DataTable dsSubReport)
        {

            DataTable dt = new DataTable();
            DataRow dtRow;
            Int32 typeid = Convert.ToInt32(ddlChannelType.SelectedValue);
            var setdt = db.tblChannels.Where(x => x.TypeID == typeid && x.IsActive == true).ToList();

            DataTable dsRows = Helper.ToDataTable(setdt);
            string ColumnName = "";
            string ColumnData = "";
            int i = 0;
            float Ratio = 0.00f;
            double totalItems = 0;
            foreach (DataRow drOuter in dsRows.Rows)
            {
                if (i == 0)
                {

                    foreach (DataRow drinner in ds.Rows)
                    {
                        foreach (DataColumn column in ds.Columns)
                        {
                            //drinner["ChannelName"] = drOuter["ChannelName"].ToString();
                            if (column.ColumnName == "Channel")
                            {
                                //drinner["ChannelName"] = drOuter["ChannelName"].ToString();
                                drinner[8] = drOuter[1].ToString();
                                drinner[9] =  Convert.ToInt32(drOuter[0]);
                              
                                int Slab_1 = GetSlab(Convert.ToInt32(drOuter[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), typeid, 1, 29);

                                double mappedItems = Convert.ToDouble(Slab_1);
                                try
                                {
                                    totalItems = Convert.ToDouble(drinner[5].ToString());
                                }
                                catch (Exception)
                                {
                                }

                               

                                double Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);

                                drinner[10] = Slab_1.ToString() + " - (" + Ratio_.ToString() + "%)";
                                int Slab_2 = GetSlab(Convert.ToInt32(drOuter[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), typeid, 30, 49);
                                mappedItems = Convert.ToDouble(Slab_2);
                               // totalItems = Convert.ToDouble(drinner[5]);

                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[11] = Slab_2.ToString() + " - (" + Ratio_.ToString() + "%)";
                                int Slab_3 = GetSlab(Convert.ToInt32(drOuter[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), typeid, 50, 2000);
                                mappedItems = Convert.ToDouble(Slab_3);
                              // totalItems = Convert.ToDouble(drinner[5]);

                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[12] = Slab_3.ToString() + " - (" + Ratio_.ToString() + "%)"; 
                                ViewState["RegionID"] = Convert.ToInt32(drinner[1]);
                                ViewState["TerritoryID"] = Convert.ToInt32(drinner[2]);
                                int Slab_4 = Convert.ToInt32(drinner[5]) - (Slab_1 + Slab_2 + Slab_3);
                                mappedItems = Convert.ToDouble(Slab_4);
                               // totalItems = Convert.ToDouble(drinner[5]);
                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[13] = Slab_4.ToString() + " - (" + Ratio_.ToString() + "%)";
                                break;
                            }
                        }
                    }
                    i++;
                }

            }

            AddRows(ds, dsRows, Viewers, HeadEnds, dsSubReport);
        }
        //private void AddRows(DataTable ds, DataTable dsRows, int Viewers, int HeadEnds, DataTable dsSubReport)
        //{
        //    DataTable copyDataTable;
        //    copyDataTable = ds.Copy();
        //    foreach (DataRow drinner in copyDataTable.Rows)
        //    {
        //        int ChannelID = Convert.ToInt32(drinner[9]);
        //        int RegionID = Convert.ToInt32(drinner[1]);
        //        int TerritoryID = Convert.ToInt32(drinner[2]);
        //        foreach (DataRow dr in dsRows.Rows)
        //        {
        //            if ((ChannelID != Convert.ToInt32(dr[0])))
        //            {
        //                foreach (DataColumn column in copyDataTable.Columns)
        //                {
        //                    if (column.ColumnName == "Channel")
        //                    {
        //                        DataRow drNew = ds.NewRow();
        //                        drNew[0] = Convert.ToInt32(drinner[0]);
        //                        drNew[1] = Convert.ToInt32(drinner[1]);
        //                        drNew[2] = Convert.ToInt32(drinner[2]);
        //                        drNew[3] = drinner[3].ToString();
        //                        drNew[4] = drinner[4].ToString();
        //                        drNew[5] = Convert.ToInt32(drinner[5]);
        //                        drNew[6] = Convert.ToInt32(drinner[6]);
        //                        drNew[7] = Convert.ToString(drinner[7]);
        //                        drNew[8] = dr[1].ToString();
        //                        drNew[9] = Convert.ToInt32(dr[0]);

        //                        int Slab_1 = GetSlab(Convert.ToInt32(dr[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), Convert.ToInt32(ddlChannelType.SelectedValue), 1, 29);
        //                        double mappedItems = Convert.ToDouble(Slab_1);
        //                        double totalItems = Convert.ToDouble(drinner[5]);
        //                        double Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
        //                        drNew[10] = Slab_1.ToString() + " - (" + Ratio_.ToString() + "%)";
        //                        int Slab_2 = GetSlab(Convert.ToInt32(dr[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), Convert.ToInt32(ddlChannelType.SelectedValue), 30, 49);
        //                        mappedItems = Convert.ToDouble(Slab_2);
        //                        totalItems = Convert.ToDouble(drinner[5]);
        //                        Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
        //                        drNew[11] = Slab_2.ToString() + " - (" + Ratio_.ToString() + "%)";
        //                        int Slab_3 = GetSlab(Convert.ToInt32(dr[0]), Convert.ToInt32(drinner[1]), Convert.ToInt32(drinner[2]), Convert.ToInt32(ddlChannelType.SelectedValue), 50, 2000);
        //                        mappedItems = Convert.ToDouble(Slab_3);
        //                        totalItems = Convert.ToDouble(drinner[5]);
        //                        Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
        //                        drNew[12] = Slab_3.ToString() + " - (" + Ratio_.ToString() + "%)";
        //                        int Slab_4 = Convert.ToInt32(drNew[5]) - (Slab_1 + Slab_2 + Slab_3);
        //                        mappedItems = Convert.ToDouble(Slab_4);
        //                        totalItems = Convert.ToDouble(drinner[5]);
        //                        Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
        //                        drNew[13] = Slab_4.ToString() + " - (" + Ratio_.ToString() + "%)";
        //                        ds.Rows.Add(drNew);
        //                    }
        //                }

        //            }

        //        }

        //    }
        //    ShowReport(ds, Viewers, HeadEnds, dsSubReport);
        //}

        private void AddRows(DataTable ds, DataTable dsRows, int Viewers, int HeadEnds, DataTable dsSubReport)
        {
            DataTable original = ds.Copy();  // Existing one-row-per-base records (initial set)

            foreach (DataRow baseRow in original.Rows)
            {
                int existingChannelId = Convert.ToInt32(baseRow[9]);  // Current channel in that row
                int regionId = Convert.ToInt32(baseRow[1]);
                int territoryId = Convert.ToInt32(baseRow[2]);
                int headEnd = Convert.ToInt32(baseRow[5]);
                double totalItems = headEnd > 0 ? headEnd : 1;

                foreach (DataRow channelRow in dsRows.Rows)
                {
                    int thisChannelId = Convert.ToInt32(channelRow[0]);

                    // Skip if this channel already exists (baseRow already has it)
                    if (thisChannelId == existingChannelId)
                        continue;

                    DataRow newRow = ds.NewRow();

                    // Copy base info
                    newRow[0] = baseRow[0]; // CountryId
                    newRow[1] = baseRow[1]; // RegionId
                    newRow[2] = baseRow[2]; // TerritoryId
                    newRow[3] = baseRow[3]; // RegionName
                    newRow[4] = baseRow[4]; // TerritoryName
                    newRow[5] = baseRow[5]; // HeadEnd
                    newRow[6] = baseRow[6]; // Viewers
                    newRow[7] = baseRow[7]; // Percentage

                    // Channel info
                    newRow[8] = channelRow[2].ToString();  // tblChannel.ChannelName
                    newRow[9] = thisChannelId;

                    // Slabs
                    int slab1 = GetSlab(thisChannelId, regionId, territoryId, Convert.ToInt32(ddlChannelType.SelectedValue), 1, 29);
                    int slab2 = GetSlab(thisChannelId, regionId, territoryId, Convert.ToInt32(ddlChannelType.SelectedValue), 30, 49);
                    int slab3 = GetSlab(thisChannelId, regionId, territoryId, Convert.ToInt32(ddlChannelType.SelectedValue), 50, 2000);
                    int slab4 = headEnd - (slab1 + slab2 + slab3);

                    newRow[10] = $"{slab1} - ({Math.Round((slab1 / totalItems) * 100)}%)";
                    newRow[11] = $"{slab2} - ({Math.Round((slab2 / totalItems) * 100)}%)";
                    newRow[12] = $"{slab3} - ({Math.Round((slab3 / totalItems) * 100)}%)";
                    newRow[13] = $"{slab4} - ({Math.Round((slab4 / totalItems) * 100)}%)";

                    ds.Rows.Add(newRow);
                }
            }

            ShowReport(ds, Viewers, HeadEnds, dsSubReport);
        }

        private void ShowReport(DataTable ds, int TotalViewers, int HeadEnds, DataTable dsSubReport)
        {


            DataView dv = ds.DefaultView;
            dv.Sort = "TerritoryId asc";
            DataTable sortedDT = dv.ToTable();

            gvRecords.DataSource = sortedDT;
            gvRecords.DataBind();



        }



        private int GetSlabRegion(int ChannelId, int RegionId, int ChannelTypeId, int StartingPostion, int EndingPosition)
        {
            int i = 0;
            i = Convert.ToInt32(db.usp_GetChannelSlab_Region(ChannelId, RegionId, ChannelTypeId, StartingPostion, EndingPosition).SingleOrDefault().Value);
            return i;
        }
        protected void gvRecords_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool isvalid = true;
            if (ChkRegion.Checked == true)
            {
                if (isvalid == true)
                {
                    gvRecords.Columns[1].Visible = false;
                    isvalid = false;
                }
            }
        }

        private DataTable MakeSlabRegion(string strRegion)
        {
            DataTable dt = new DataTable();

            DataColumn col1 = new DataColumn("ShortName");
            dt.Columns.Add(col1);
            DataColumn col2 = new DataColumn("CurrPosition");
            dt.Columns.Add(col2);
            DataColumn col3 = new DataColumn("Status");
            dt.Columns.Add(col3);


            Int32 ChannelTypeID = Convert.ToInt32(ddlChannelType.SelectedValue);
            string RegionID = strRegion;
            var dsv = db.usp_GetHeadEndsListRegionTerritory_Region(ChannelTypeID, RegionID).ToList();
            DataTable ds = Helper.ToDataTable(dsv);
            DataTable dsChannel;
            foreach (DataRow dr in ds.Rows)
            {
                var dsChannelv = db.tblChannels.Where(x => x.TypeID == ChannelTypeID && x.IsActive == true).ToList();
                dsChannel = Helper.ToDataTable(dsChannelv);
                foreach (DataRow drinner in dsChannel.Rows)
                {
                    int i = Convert.ToInt32(drinner[0]);
                    int ii = Convert.ToInt32(dr[0]);
                    var dsSlapv = db.usp_GetSlabByOperatorIDChannelID__(i, ii, ChannelTypeID).ToList();
                    DataRow drN = dt.NewRow();
                    DataTable dsSlap = Helper.ToDataTable(dsSlapv);
                    if (dsSlap.Rows.Count > 0)
                    {
                        drN[0] = dsSlap.Rows[0][2].ToString();
                        drN[1] = Convert.ToInt32(dsSlap.Rows[0][0]);
                        drN[2] = dsSlap.ToString();
                        if (dsSlap.Rows[0][0].ToString() == "0")
                        {
                            drN[2] = "NA";
                        }

                    }
                    else
                    {

                        drN[0] = drinner[1].ToString();
                        drN[1] = 0;
                        drN[2] = "NA";

                    }
                    dt.Rows.Add(drN);

                }

            }
            return dt;
        }
        protected void gvRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Google")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvRow.RowIndex;
                    HiddenField hdRegion = (HiddenField)gvRow.FindControl("HDRegion");
                    HiddenField hdTerritory = (HiddenField)gvRow.FindControl("HDTerritory");
                    Session["RegionName"] = hdRegion.Value.ToString();
                    Session["TerritoryName"] = hdTerritory.Value.ToString();
                    DataTable dsSubReport = null;
                    string[] str = e.CommandArgument.ToString().Split(';');
                    string RegionList = str[0].ToString();
                    string TerritoryList = str[1].ToString();

                    if (ChkRegion.Checked == true)
                    {
                        dsSubReport = MakeSlabRegion(RegionList);
                    }
                    else
                    {
                        dsSubReport = MakeSlab(RegionList, TerritoryList);
                    }

                    pnlChart.Visible = true;
                    ShowChart(dsSubReport);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ShowChart(DataTable dts)
        {
            if (dts != null)
            {

            }

        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            pnlChart.Visible = false;
            DataTable dt = null;
            ShowChart(dt);
        }
        private DataTable MakeSlab(string strRegion, string strTerritory)
        {
            DataTable dt = new DataTable();

            DataColumn col1 = new DataColumn("ShortName");
            dt.Columns.Add(col1);
            DataColumn col2 = new DataColumn("CurrPosition");
            dt.Columns.Add(col2);
            DataColumn col3 = new DataColumn("Status");
            dt.Columns.Add(col3);
            int ChannelTypeID = Convert.ToInt32(ddlChannelType.SelectedValue);
            string RegionID = strRegion;
            string TerritoryID = strTerritory;
            var dsv = db.usp_GetHeadEndsListRegionTerritory(ChannelTypeID, strRegion, strTerritory).ToList();
            DataTable ds = Helper.ToDataTable(dsv);
            var dsChannelv = db.tblChannels.Where(x => x.TypeID == ChannelTypeID && x.IsActive == true).ToList();
            DataTable dsChannel = Helper.ToDataTable(dsChannelv);
            foreach (DataRow dr in ds.Rows)
            {

                foreach (DataRow drinner in dsChannel.Rows)
                {

                    int i = Convert.ToInt32(drinner[0]);
                    int ii = Convert.ToInt32(dr[0]);
                    var dsSlapv = db.usp_GetSlabByOperatorIDChannelID__(i, ii, ChannelTypeID).ToList();
                    DataTable dsSlap = Helper.ToDataTable(dsSlapv);
                    DataRow drN = dt.NewRow();
                    int headend = Convert.ToInt32(ViewState["HeadEnds"]);
                    if (dsSlap.Rows.Count > 0)
                    {
                        drN[0] = dsSlap.Rows[0][2].ToString();
                        drN[1] = Convert.ToInt32(dsSlap.Rows[0][0]);
                        drN[2] = dsSlap.Rows[0][1].ToString();
                    }
                    else
                    {
                    }
                    dt.Rows.Add(drN);
                }
            }
            return dt;
        }
        private void SetColumnsRegion(DataTable ds, int Viewers, int HeadEnds)
        {

            DataTable dt = new DataTable();
            DataRow dtRow;
            Int32 typeid = Convert.ToInt32(ddlChannelType.SelectedValue);
            var setdt = db.tblChannels.Where(x => x.TypeID == typeid && x.IsActive == true).ToList();
            DataTable dsRows = Helper.ToDataTable(setdt);
            string ColumnName = "";
            string ColumnData = "";
            int i = 0;
            float Ratio = 0.00f;
            foreach (DataRow drOuter in dsRows.Rows)
            {
                if (i == 0)
                {

                    foreach (DataRow drinner in ds.Rows)
                    {
                        foreach (DataColumn column in ds.Columns)
                        {
                            if (column.ColumnName == "Channel")
                            {

                                drinner[6] = drOuter[1].ToString();
                                drinner[7] = Convert.ToInt32(drOuter[0]);
                                Int32 ChannelID = Convert.ToInt32(drOuter[0]);
                                Int32 RegionID = Convert.ToInt32(drinner[1]);
                                Int32 ChannelType = Convert.ToInt32(ddlChannelType.SelectedValue);


                                int Slab_1 = GetSlabRegion(ChannelID, RegionID, ChannelType, 1, 29);

                                double mappedItems = Convert.ToDouble(Slab_1);
                                double totalItems = Convert.ToDouble(drinner[3]);

                                double Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[8] = Slab_1.ToString() + " - (" + Ratio_.ToString() + "%)";


                                int Slab_2 = GetSlabRegion(ChannelID, RegionID, ChannelType, 30, 49);
                                mappedItems = Convert.ToDouble(Slab_2);
                                totalItems = Convert.ToDouble(drinner[3]);

                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[9] = Slab_2.ToString() + " - (" + Ratio_.ToString() + "%)";
                                int Slab_3 = GetSlabRegion(ChannelID, RegionID, ChannelType, 50, 2000);
                                mappedItems = Convert.ToDouble(Slab_3);
                                totalItems = Convert.ToDouble(drinner[3]);

                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[10] = Slab_3.ToString() + " - (" + Ratio_.ToString() + "%)";
                                ViewState["RegionID"] = RegionID;
                                int Slab_4 = Convert.ToInt32(drinner[3]) - (Slab_1 + Slab_2 + Slab_3);
                                mappedItems = Convert.ToDouble(Slab_4);
                                totalItems = Convert.ToDouble(drinner[3]);

                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drinner[11] = Slab_4.ToString() + " - (" + Ratio_.ToString() + "%)";
                                break;
                            }
                        }
                    }
                    i++;
                }

            }
            AddRowsRegion(ds, dsRows, Viewers, HeadEnds);
        }
        private void AddRowsRegion(DataTable ds, DataTable dsRows, int Viewers, int HeadEnds)
        {
            DataTable copyDataTable;
            copyDataTable = ds.Copy();
            foreach (DataRow drinner in copyDataTable.Rows)
            {
                int ChannelID = Convert.ToInt32(drinner[7]);
                int RegionID = Convert.ToInt32(drinner[1]);
                int ChannelType = Convert.ToInt32(ddlChannelType.SelectedValue);
                foreach (DataRow dr in dsRows.Rows)
                {
                    int ChannelID_ = Convert.ToInt32(dr[0]);
                    if ((ChannelID != Convert.ToInt32(dr[0])))
                    {
                        foreach (DataColumn column in copyDataTable.Columns)
                        {
                            if (column.ColumnName == "Channel")
                            {
                                DataRow drNew = ds.NewRow();
                                drNew[0] = Convert.ToInt32(drinner[0]);
                                drNew[1] = Convert.ToInt32(drinner[1]);
                                drNew[2] = Convert.ToString(drinner[2]);
                                drNew[3] = drinner[3].ToString();
                                drNew[4] = drinner[4].ToString();
                                drNew[5] = Convert.ToString(drinner[5]);
                                drNew[6] = Convert.ToString(dr[1]);
                                drNew[7] = Convert.ToString(dr[0]);
                                int Slab_1 = GetSlabRegion(ChannelID_, RegionID, ChannelType, 1, 29);
                                double mappedItems = Convert.ToDouble(Slab_1);
                                double totalItems = Convert.ToDouble(drinner[3]);
                                double Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drNew[8] = Slab_1.ToString() + " - (" + Ratio_.ToString() + "%)";
                                int Slab_2 = GetSlabRegion(ChannelID_, RegionID, ChannelType, 30, 49);
                                mappedItems = Convert.ToDouble(Slab_2);
                                totalItems = Convert.ToDouble(drinner[3]);
                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drNew[9] = Slab_2.ToString() + " - (" + Ratio_.ToString() + "%)";
                                int Slab_3 = GetSlabRegion(ChannelID_, RegionID, ChannelType, 50, 2000);
                                mappedItems = Convert.ToDouble(Slab_3);
                                totalItems = Convert.ToDouble(drinner[3]);
                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drNew[10] = Slab_3.ToString() + " - (" + Ratio_.ToString() + "%)";
                                ViewState["RegionID"] = RegionID;
                                int Slab_4 = Convert.ToInt32(drinner[3]) - (Slab_1 + Slab_2 + Slab_3);
                                mappedItems = Convert.ToDouble(Slab_4);
                                totalItems = Convert.ToDouble(drinner[3]);
                                Ratio_ = Math.Round(((mappedItems / totalItems) * 100.0), 0);
                                drNew[11] = Slab_4.ToString() + " - (" + Ratio_.ToString() + "%)";
                                ds.Rows.Add(drNew);
                            }
                        }

                    }

                }

            }

            ShowReportRegion(ds, Viewers, HeadEnds);
        }
        private void ShowReportRegion(DataTable ds, int TotalViewers, int HeadEnds)
        {
            DataColumn dt = new DataColumn();

            dt.ColumnName = "TerritoryName";
            ds.Columns.Add(dt);
            dt = new DataColumn();
            dt.ColumnName = "TerritoryId";
            ds.Columns.Add(dt);
            ds.AcceptChanges();
            DataView dv = ds.DefaultView;
            dv.Sort = "RegionName asc";
            DataTable sortedDT = dv.ToTable();
            gvRecords.DataSource = sortedDT;
            gvRecords.DataBind();
        }

        private string previousRegion = "", previousBaseStation = "", previousHeadEnd = "", previousPercentage = "";
        private int previousViewers = -1;


        protected void gvRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string currentRegion = DataBinder.Eval(e.Row.DataItem, "RegionName")?.ToString() ?? "";
                string currentBaseStation = DataBinder.Eval(e.Row.DataItem, "TerritoryName")?.ToString() ?? "";
                string currentHeadEnd = DataBinder.Eval(e.Row.DataItem, "HeadEnd")?.ToString() ?? "";
                string currentPercentage = DataBinder.Eval(e.Row.DataItem, "Percentage")?.ToString() ?? "";
                int currentViewers = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Viewers"));

                bool isRepeat = currentRegion == previousRegion &&
                                currentBaseStation == previousBaseStation &&
                                currentHeadEnd == previousHeadEnd &&
                                currentViewers == previousViewers &&
                                currentPercentage == previousPercentage;

                if (isRepeat)
                {
                    e.Row.Cells[0].Text = "&nbsp;";
                    e.Row.Cells[1].Text = "&nbsp;";
                    e.Row.Cells[2].Text = "&nbsp;";
                    e.Row.Cells[3].Text = "&nbsp;";
                    e.Row.Cells[4].Text = "&nbsp;";
                }
                else
                {
                    previousRegion = currentRegion;
                    previousBaseStation = currentBaseStation;
                    previousHeadEnd = currentHeadEnd;
                    previousViewers = currentViewers;
                    previousPercentage = currentPercentage;
                }
            }
        }

    }
}