using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
namespace ABMS
{
    public partial class AdBooking : System.Web.UI.Page
    {
        //DBManager db;
        DBRegister obj;
        // public AdBooking()
        // {
        //  db = new DBManager();
        // db.Open();
        // }
        //~AdBooking()
        // {
        //    db.Close();
        //   db.Dispose();
        // }
        protected void btnaddClient_Click(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["currentpage"] = "AdBooking";
            if (Session["UserName"] == null || Session["UserDesignation"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
            if (!Page.IsPostBack)
            {
                btnCancelAdd.Visible = false;
                divSearchAgency.Visible = false;
                divSearchClient.Visible = false;
                divNewclient.Visible = false;

                ViewState["agname"] = "";
                ViewState["clname"] = "";



             
                //  if ((Session["Admin"].ToString() != "Admin" ) &&( ))

                dtInsertionDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Session["dtInsertionDate"] = dtInsertionDate.Text;

                DataTable dtRecord = new DataTable();

                dtRecord.Columns.Add("ID", typeof(Int32));
                dtRecord.Columns.Add("PID", typeof(Int32));
                dtRecord.Columns.Add("Station", typeof(string));
                dtRecord.Columns.Add("Page", typeof(string));
                dtRecord.Columns.Add("Position", typeof(string));
                dtRecord.Columns.Add("PageID", typeof(Int32));
                dtRecord.Columns.Add("StationID", typeof(Int32));
                dtRecord.Columns.Add("PositionID", typeof(string));
                ViewState["dtRecord"] = dtRecord;
                FillControls();
                if (ddlMaterial.SelectedIndex == 4)
                {
                    RowAsOn.Visible = true;
                }
                else
                {
                    RowAsOn.Visible = false;
                }
                Session["publicationID"] = Convert.ToInt32(ddlPublication.SelectedValue);
                Session["publication"] = ddlPublication.SelectedItem.Text;
            }
        }
        private void FillControls()
        {

            // txtClientdata.Text = "Munir mustafa";

            DBManager db = new DBManager();
            db.Open();
            obj = new DBRegister();
            db.Command.Parameters.Clear();
            db.CreateParameters(0);
            DataTable dt = obj.ExecuteDataTable(db, "sp_BookingExecutives");
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "Select Booking Executive";
            dt.Rows.InsertAt(dr, 0);
            ddlBookingExecutive.DataValueField = "ID";
            ddlBookingExecutive.DataTextField = "UserName";
            ddlBookingExecutive.DataSource = dt;
            ddlBookingExecutive.DataBind();

            dt = obj.ExecuteDataTable(db, "sp_Publication");
            ddlPublication.DataValueField = "ID";
            ddlPublication.DataTextField = "Publication";
            ddlPublication.DataSource = dt;
            ddlPublication.DataBind();

            ddlPublication.SelectedValue = ddlPublication.Items.FindByText("Express").Value;

            ddlPublication_SelectedIndexChanged(null, null);

            dt = obj.ExecuteDataTable(db, "sp_GetBaseStations");
            dr = dt.NewRow();
            ddlBaseStation.DataValueField = "ID";
            ddlBaseStation.DataTextField = "BaseStation";
            ddlBaseStation.DataSource = dt;
            ddlBaseStation.DataBind();
            ddlBaseStation.SelectedValue = ddlBaseStation.Items.FindByText("Karachi").Value;

            dt = obj.ExecuteDataTable(db, "sp_GetBaseStations");
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "Select Station";
            dt.Rows.InsertAt(dr, 0);
            ddlAdStation.DataValueField = "ID";
            ddlAdStation.DataTextField = "BaseStation";
            ddlAdStation.DataSource = dt;
            ddlAdStation.DataBind();

            dt = obj.ExecuteDataTable(db, "sp_SubCategory");
            dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "Select Sub Category";
            dt.Rows.InsertAt(dr, 0);
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataSource = dt;
            ddlCategory.DataBind();

            dt = obj.ExecuteDataTable(db, "sp_GetBaseStations");
            ddlStations.DataValueField = "ID";
            ddlStations.DataTextField = "Abreviation";
            ddlStations.DataSource = dt;
            ddlStations.DataBind();

            ddlClientCity.DataValueField = "ID";
            ddlClientCity.DataTextField = "BaseStation";
            ddlClientCity.DataSource = dt;
            ddlClientCity.DataBind();

            obj.StrSql = "select Id, Category_Title from maincategories order by 2 asc";
            dt = new DataTable();
            dt = obj.GetDataTableByText(db);
            ddlMainCateogry.DataValueField = "ID";
            ddlMainCateogry.DataTextField = "Category_Title";
            ddlMainCateogry.DataSource = dt;
            ddlMainCateogry.DataBind();

            obj.StrSql = "select Id, Category_Title from subcategories order by 2 asc";
            dt = new DataTable();
            dt = obj.GetDataTableByText(db);
            ddlSubCategory.DataValueField = "ID";
            ddlSubCategory.DataTextField = "Category_Title";
            ddlSubCategory.DataSource = dt;
            ddlSubCategory.DataBind();


            db.Close();
            db.Dispose();
            ArrayList ArrayColumn = new ArrayList();
            ArrayList ArrayRow = new ArrayList();
            for (int i = 1; i < 9; i++)
            {
                ArrayColumn.Add(i);
            }
            for (int i = 1; i < 55; i++)
            {
                ArrayRow.Add(i);
            }

            ArrayColumn.Insert(0, "COL");
            ArrayRow.Insert(0, "CM");
            ddlCOL.DataSource = ArrayColumn;
            ddlCOL.DataBind();

            ddlCM.DataSource = ArrayRow;
            ddlCM.DataBind();

            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Description");
            dr = dt.NewRow();
            dr[0] = "C";
            dr[1] = "4 Color";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "S";
            dr[1] = "Spot";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "B";
            dr[1] = "B & W";
            dt.Rows.Add(dr);


            ddlColor.DataTextField = "Description";
            ddlColor.DataValueField = "ID";
            ddlColor.DataSource = dt;
            ddlColor.DataBind();


            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Description");
            dr = dt.NewRow();
            dr[0] = "S";
            dr[1] = "Art Work";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "F";
            dr[1] = "Film";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "T";
            dr[1] = "Text";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "W";
            dr[1] = "Fallow";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "O";
            dr[1] = "AsOn";
            dt.Rows.Add(dr);

            ddlMaterial.DataTextField = "Description";
            ddlMaterial.DataValueField = "ID";
            ddlMaterial.DataSource = dt;
            ddlMaterial.DataBind();


            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Description");
            dr = dt.NewRow();
            dr[0] = "NRM";
            dr[1] = "Normal";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "CRT";
            dr[1] = "Creative";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "HNG";
            dr[1] = "Hanging";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "BPP";
            dr[1] = "Back Page Panel";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "BZR";
            dr[1] = "Bazar";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "EDG";
            dr[1] = "Education Guid";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "CLD";
            dr[1] = "Classified Display";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "CLP";
            dr[1] = "Classifed Panel";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr[0] = "TND";
            dr[1] = "Tender";
            dt.Rows.Add(dr);
            dr = dt.NewRow();

            dr[0] = "WNT";
            dr[1] = "Wanted";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "FNC";
            dr[1] = "Financial";
            dt.Rows.Add(dr);

            ddlPosition.DataTextField = "Description";
            ddlPosition.DataValueField = "ID";
            ddlPosition.DataSource = dt;
            ddlPosition.DataBind();


        }

        #region Clients
        protected void btnSaveClient_Click(object sender, EventArgs e)
        {
            lblclientmessage.Text = string.Empty;
            if (txtClientName.Text.Trim().Length == 0)
            {
                lblclientmessage.Text = "Client Name Required";
                return;
            }
            DBManager db = new DBManager();
            db.Open();
            try
            {
              
                //MPNew.Show();
                db.Command.Parameters.Clear();
                db.BeginTransaction();
                db.CreateParameters(1);
                db.AddParameters(0, "@For_Table", "ClientCompanies");
                int ID = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));
                string InsertDate = DateTime.Now.ToShortDateString();
                string UserID = Convert.ToString(Session["LoginID"]);
                obj = new DBRegister();
                obj.ID = ID;
                obj.ClientName = txtClientName.Text;
                obj.ClientMainCategoryID = Convert.ToInt32(ddlMainCateogry.SelectedValue);
                obj.ClientSubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                obj.Address1 = txtLine1.Text;
                obj.Address2 = txtLine2.Text;
                obj.Address3 = txtLine3.Text;
                obj.Address4 = ddlClientCity.SelectedItem.Text;
                obj.UserID = UserID;
                if (obj.InsertClient(db) == true)
                {
                    db.Transaction.Commit();
                    txtClient.Text = txtClientName.Text;
                    hdclientid.Value = ID.ToString();
                    lblclientmessage.Text = "Client Inserted";
                    txtClientName.Text = string.Empty;
                    txtLine1.Text = string.Empty;
                    txtLine2.Text = string.Empty;
                    txtLine3.Text = string.Empty;
                    // MPNew.Hide();
                }
                else
                {
                    db.Transaction.Rollback();
                    lblclientmessage.Text = "Client not Inserted";
                }

            }
            catch (Exception ex)
            {
                db.Transaction.Rollback();
                lblclientmessage.Text = ex.Message;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        protected void btnShowClient_Click(object sender, EventArgs e)
        {
            if (divSearchAgency.Visible == true || divNewclient.Visible == true)
            {
                divSearchAgency.Visible = false;
                divNewclient.Visible = false;
              
            
            }
              divSearchClient.Visible = true;
            setValue();
        }
        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            lblClientNotFound.Text = string.Empty;
            //  MP.Show();
            DBManager db = new DBManager();
            db.Open();
            try
            {
                if (txtSearchClient.Text.Trim().Length > 3)
                {

                    db.Command.Parameters.Clear();
                    db.CreateParameters(2);
                    db.AddParameters(0, "ClientCity", ddlBaseStation.SelectedItem.Text);
                    db.AddParameters(1, "ClientName", txtSearchClient.Text);
                    DBRegister obj = new DBRegister();
                    DataTable dt = obj.ExecuteDataTable(db, "sp_SearchClients");
                    ViewState["dt"] = dt;
                    gv.DataSource = dt;
                    gv.DataBind();
                    if (dt.Rows.Count == 0)
                    {
                        lblClientNotFound.Text = "No Record Found,Check Filter Criteria";
                        return;
                    }
                }
                else
                {
                    lblClientNotFound.Text = "Please Enter Min. 4 letter";
                    gv.DataSource = null;
                    gv.DataBind();
                    return;
                }
            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }
        private void setValue()
        {
            txtClient.Text = "";
            divSearchClient.Visible = true;
            // divse  client.Visible = true;
        }
        protected void btnAddNewClient_Click(object sender, EventArgs e)
        {
            if (divSearchClient.Visible == true || divSearchAgency.Visible == true)
            {
                divSearchClient.Visible = false;
                divSearchAgency.Visible = false;

            }

            divNewclient.Visible = true;
        }
        protected void btnXClient_Click(object sender, EventArgs e)
        {
            hdclientid.Value = "0";
            txtClient.Text = string.Empty;
            gv.DataSource = null;
            gv.DataBind();
            divSearchClient.Visible = false;
        }
        protected void btnSelectClient_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            try
            {
                hdclientid.Value = ID.ToString();
                txtClient.Text = myRow.Cells[0].Text;
                ViewState["clname"] = myRow.Cells[0].Text;
                divSearchClient.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCloseNewClient_Click(object sender, EventArgs e)
        {
            divNewclient.Visible = false;
            lblclientmessage.Text = "";
        }
        #endregion
        #region Agency
        protected void btnShowAgency_Click(object sender, EventArgs e)
        {
            if (divSearchClient.Visible == true || divNewclient.Visible == true)
            {
            
                divNewclient.Visible = false;
                divSearchClient.Visible = false;
                
            }
            divSearchAgency.Visible = true;

            txtSearchAgency.Text = "";
            hdagencyid.Value = "";
            // $("#MainContent_btnCloseAgency").click(function () {
            //         $('#MainContent_txtAgency').val("");
            //         $('#MainContent_hdagencyid').val("");
            //         $('#MainContent_btnCancel').click();
            //     });
        }

        protected void btnSearchAgency_Click(object sender, EventArgs e)
        {
            lblAgencyFound.Text = string.Empty;
            //MP2.Show();
            DBManager db = new DBManager();
            db.Open();
            try
            {
                if (txtSearchAgency.Text.Trim().Length > 3)
                {

                    db.Command.Parameters.Clear();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@AgencyCity", ddlBaseStation.SelectedItem.Text);
                    db.AddParameters(1, "@AgencyName", txtSearchAgency.Text);
                    DBRegister obj = new DBRegister();
                    DataTable dt = obj.ExecuteDataTable(db, "sp_SearchAgency");
                    ViewState["dtagency"] = dt;
                    gvAgency.DataSource = dt;
                    gvAgency.DataBind();
                    if (dt.Rows.Count == 0)
                    {
                        lblAgencyFound.Text = "Record Not Found,Check Filter Criteria";
                        return;
                    }
                }
                else
                {
                    lblAgencyFound.Text = "Please enter Min 4 letter";
                    gvAgency.DataSource = null;
                    gvAgency.DataBind();
                    return;
                }
            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }


        }

        protected void btnSelectAgency_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gvAgency.DataKeys[myRow.RowIndex].Value.ToString());
            try
            {
                hdagencyid.Value = ID.ToString();
                txtAgency.Text = myRow.Cells[0].Text;
                ViewState["agname"] = myRow.Cells[0].Text;
                divSearchAgency.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSelectAd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gvAd.DataKeys[myRow.RowIndex].Value.ToString());
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnXAgencyClose_Click(object sender, EventArgs e)
        {

            hdagencyid.Value = "0";
            txtAgency.Text = string.Empty;
            gvAgency.DataSource = null;
            gvAgency.DataBind();
            divSearchAgency.Visible = false;


        }
        protected void gvAgency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //  MP2.Show();
            gvAgency.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["dtagency"];
            gvAgency.DataSource = dt;
            gvAgency.DataBind();

        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //MP.Show();
            gv.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();

        }
        protected void btnAddNewAgency_Click(object sender, EventArgs e)
        {

        }
        #endregion
       

        //protected void btnXClient_Click(object sender, EventArgs e)
        //{
            
        //       //  $("#MainContent_btnClose").click(function () {
        //         txtClient.Text ="";//   $('#MainContent_txtClient').val("");
        //         hdclientid.Value ="";   //$('#MainContent_hdclientid').val("");

        //            //$('#MainContent_btnCancel').click();

        //       // });

        //}
       // btnXClient_Click
      
      
      
  
        protected void btnSelectPage_Click(object sender, EventArgs e)
        {
            string str = GetStationList();
            string[] arr = str.Split(';');
            // if (CheckStatus(Convert.ToString(ddlStations.SelectedValue), Convert.ToString(ddlPage.SelectedValue), Convert.ToString(ddlPosition.SelectedValue)) == true)
            if (ddlPage.SelectedValue.ToString().Length > 0 && ddlStations.SelectedValue.ToString().Length > 0 && ddlPosition.SelectedValue.ToString().Length > 0)
            {
                DataTable dt = (DataTable)ViewState["dtRecord"];
                for (int i = 0; i < arr.Count(); i++)
                {
                    string[] s = arr[i].ToString().Split('-');
                    DataRow dr = dt.NewRow();
                    int Rows = gvPosition.Rows.Count;
                    dr[0] = Rows + 1;
                    dr[1] = 0;
                    dr[2] = s[1].ToString();
                    dr[3] = Convert.ToString(ddlPage.SelectedItem.Text);
                    dr[4] = Convert.ToString(ddlPosition.SelectedItem.Text);
                    dr[5] = Convert.ToString(ddlPage.SelectedValue);
                    dr[6] = s[0].ToString();
                    dr[7] = Convert.ToString(ddlPosition.SelectedValue);
                    if (CheckStation(Convert.ToInt32(s[0]), dt) == false)
                        dt.Rows.Add(dr);
                }
                ViewState["dtRecord"] = dt;
                gvPosition.DataSource = dt;
                gvPosition.DataBind();
            }
            else
            {
                lblMessage.Text = "Missing Page/Station/Postion";
                return;
            }
        }
        private bool CheckStation(int StationID, DataTable dt)
        {

            bool Result = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row[6]) == StationID)
                    {
                        Result = true;
                        break;
                    }
                }
            }
            return Result;

        }
        private bool CheckStatus(string stationID, string PageID, string PositionID)
        {
            bool Result = true;
            foreach (GridViewRow dr in gvPosition.Rows)
            {
                HiddenField hdstationid = (HiddenField)dr.FindControl("hdstationid");
                HiddenField hdpageid = (HiddenField)dr.FindControl("hdpageid");
                HiddenField hdpositionid = (HiddenField)dr.FindControl("hdpositionid");
                if ((stationID == hdstationid.Value.ToString()) && (PositionID == hdpositionid.Value.ToString()) && (PageID == hdpageid.Value.ToString()))
                {
                    Result = false;
                    break;

                }
            }
            return Result;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gvPosition.DataKeys[myRow.RowIndex].Value.ToString());
            int idx = myRow.RowIndex;
            DataTable dt = (DataTable)ViewState["dtRecord"];
            dt.Rows.RemoveAt(idx);
            ViewState["dtRecord"] = dt;
            gvPosition.DataSource = dt;
            gvPosition.DataBind();
            if (dt.Rows.Count == 0)
            {
                ViewState["isremove"] = true;
            }
            System.Threading.Thread.Sleep(1000);
           // divImage.Visible = false;
        }

     

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaterial.SelectedIndex == 4)
            {
                RowAsOn.Visible = true;
            }
            else
            {
                RowAsOn.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtClient.Text.Trim().Length ==0)
            {
                lblMessage.Text = "Please provide client information";
                //lblMessage.Text = "<script>document.onload=function(){window.scrollTo(0,0);}</script>";
                return;
            }

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            if (Helper.SetDateFormat(dtInsertionDate.Text) < dt)
            {
                lblMessage.Text = "Date must be greater then equal to current date  ";
                return;
            }
            DataTable dtRecord = (DataTable)ViewState["dtRecord"];
            lblMessage.Text = string.Empty;
            if ((gvPosition.Rows.Count == 0) && (Convert.ToBoolean(ViewState["isremove"]) == false))
            {
                lblMessage.Text = "Please Add atleast 1 - Station/Page/Position";
                return;
            }
            try
            {
                Helper.SetDateFormat(dtInsertionDate.Text);
            }
            catch (Exception)
            {
                lblMessage.Text = "Invalid/Empty Date Input";
                return;
            }
            try
            {


                if (ddlMaterial.SelectedIndex == 4)
                {
                    Helper.SetDateFormat(txtAsOn.Text);
                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Invalid/Empty As On Date Input";
                return;
            }
            try
            {
                if ((ddlCM.SelectedIndex == 0) || (ddlCOL.SelectedIndex == 0))
                {
                    lblMessage.Text = "Please select CM/COL";
                    return;
                }
            }
            catch (Exception)
            {

            }
            DBManager db = new DBManager();
           
            try
            {
                db.Open();
                if (btnSave.Text == "Save")
                {

                    DBRegister obj = new DBRegister();

                    try
                    {
                        obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);

                        obj.CM = 0;// Convert.ToByte(ddlCM.SelectedItem.Text);
                        //if (obj.CheckSpace(db) == true)
                        if (true)
                        {
                            db.Command.Parameters.Clear();
                            db.BeginTransaction();
                            db.CreateParameters(1);
                            db.AddParameters(0, "@For_Table", "BookingRegister");
                            int ID = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));
                            obj.ID = ID;
                            obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);
                            obj.PublicationId = Convert.ToInt32(ddlPublication.SelectedValue);
                            obj.GroupBaseStationID = Convert.ToInt32(ddlBaseStation.SelectedValue);
                            obj.StationID = Convert.ToInt32(ddlAdStation.SelectedValue);
                            obj.BookingDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            obj.UserID = Session["LoginID"].ToString();// ddlBookingExecutive.SelectedValue.ToString();
                            obj.BookingExecutiveID = ddlBookingExecutive.SelectedValue.ToString();
                            obj.SubCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                            if (txtClient.Text.Trim().Length > 0)
                            {
                                obj.ClientID = Convert.ToInt32(hdclientid.Value.ToString());
                            }
                            else
                            {
                                obj.ClientID = 0;
                            }
                            if (txtAgency.Text.Trim().Length > 0)
                            {
                                obj.AgencyID = Convert.ToInt32(hdagencyid.Value.ToString());
                            }
                            else
                            {
                                obj.AgencyID = 0;
                            }

                            obj.Caption = txtCaption.Text;
                            obj.RO = txtRO.Text;
                            obj.CM = Convert.ToByte(ddlCM.SelectedItem.Text);
                            obj.COL = Convert.ToByte(ddlCOL.SelectedItem.Text);
                            obj.Color = Convert.ToString(ddlColor.SelectedValue);
                            obj.Material = Convert.ToString(ddlMaterial.SelectedValue);
                            if (ddlMaterial.SelectedIndex == 4)
                            {
                                obj.AsOnDate = Helper.SetDateFormat(txtAsOn.Text);
                            }
                            obj.RecAddedby = Session["LoginID"].ToString();
                            obj.Remarks = txtRemarks.Text;
                            if (txtRO.Text.Trim().Length > 0)
                            {
                                obj.IsConfirm = true;
                            }
                            else
                            {
                                if (chkConfirm.Checked)
                                {
                                    if (txtRO.Text.Length == 0)
                                    {
                                        obj.IsConfirm = true;
                                        obj.RO = "waiting";
                                    }
                                }
                                else
                                {
                                    obj.IsConfirm = chkConfirm.Checked;
                                }
                            }

                            db.CreateParameters(1);
                            db.AddParameters(0, "@For_Table", "Log");
                            int LogID = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));
                            obj.LogAction = "Insert";
                            obj.LogID = LogID;

                            if ((obj.Insert(db) == true) && (obj.InsertDetails(db, obj.ID, dtRecord) == true) && (obj.InsertLog(db) == true))
                            {
                                db.CommitTransaction();
                                FormReset();
                            }
                            else
                            {
                                db.Transaction.Rollback();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "( " + obj.CMMeasurement.ToString() + " CM)" + "  No more confirmed ad can be taken for this date";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
                else
                {
                    int RecordID = (int)ViewState["RecordID"];
                    DBRegister obj = new DBRegister();

                    try
                    {
                        obj.ID = RecordID;
                        obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);
                        obj.CM = 0;
                        if (true)
                        {
                            db.Command.Parameters.Clear();
                            db.BeginTransaction();
                            obj.InsertionDate = Helper.SetDateFormat(dtInsertionDate.Text);
                            obj.PublicationId = Convert.ToInt32(ddlPublication.SelectedValue);
                            obj.GroupBaseStationID = Convert.ToInt32(ddlBaseStation.SelectedValue);
                            obj.StationID = Convert.ToInt32(ddlAdStation.SelectedValue);
                            obj.BookingDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            obj.UserID = ddlBookingExecutive.SelectedValue.ToString();
                            obj.BookingExecutiveID = Session["LoginID"].ToString(); //ddlBookingExecutive.SelectedValue.ToString();
                            obj.SubCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                            if (txtClient.Text.Trim().Length > 0)
                            {
                                obj.ClientID = Convert.ToInt32(hdclientid.Value.ToString());
                            }
                            else
                            {
                                obj.ClientID = 0;
                            }
                            if (txtAgency.Text.Trim().Length > 0)
                            {
                                try
                                {
                                    obj.AgencyID = Convert.ToInt32(hdagencyid.Value.ToString());
                                }
                                catch (Exception)
                                {
                                    obj.AgencyID = 0;
                                }

                            }
                            else
                            {
                                obj.AgencyID = 0;
                            }

                            obj.Caption = txtCaption.Text;
                            obj.RO = txtRO.Text;
                            obj.CM = Convert.ToByte(ddlCM.SelectedItem.Text);
                            obj.COL = Convert.ToByte(ddlCOL.SelectedItem.Text);
                            obj.Color = Convert.ToString(ddlColor.SelectedValue);
                            obj.Material = Convert.ToString(ddlMaterial.SelectedValue);
                            if (ddlMaterial.SelectedIndex == 4)
                            {
                                obj.AsOnDate = Helper.SetDateFormat(txtAsOn.Text);
                            }
                            obj.RecAddedby = Session["LoginID"].ToString();
                            obj.Remarks = txtRemarks.Text;
                            //obj.IsConfirm = chkConfirm.Checked;
                            if (txtRO.Text.Trim().Length > 0)
                            {
                                obj.IsConfirm = true;
                            }
                            else
                            {
                                if (chkConfirm.Checked)
                                {
                                    if (txtRO.Text.Length == 0)
                                    {
                                        obj.IsConfirm = true;
                                        obj.RO = "waiting";
                                    }
                                }
                                else
                                {
                                    obj.IsConfirm = chkConfirm.Checked;
                                }
                            }

                            db.CreateParameters(1);
                            db.AddParameters(0, "@For_Table", "Log");
                            int LogID = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));
                            obj.UserID = Session["LoginID"].ToString();
                            obj.LogAction = "Update";
                            obj.LogID = LogID;


                            if ((obj.UpDate(db) == true) && (obj.DeleteDetails(db) == true) && (obj.InsertDetails(db, obj.ID, dtRecord) == true) && (obj.InsertLog(db) == true))
                            {
                                db.CommitTransaction();
                                FormReset();
                            }
                            else
                            {
                                db.Transaction.Rollback();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "( " + obj.CMMeasurement.ToString() + " CM)" + "  No more confirmed ad can be taken for this date";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }

            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }
        private string GetStationList()
        {
            string str = "";
            int i = 0;
            foreach (ListItem item in ddlStations.Items)
            {
                if (item.Selected == true)
                {

                    if (str.Trim().Length == 0)
                    {
                        str = item.Value.ToString() + "-" + item.Text;

                    }
                    else
                    {
                        str = str + ";" + item.Value.ToString() + "-" + item.Text;

                    }
                    i++;
                }


            }
            return str;
        }
        private void FormReset()
        {

            ViewState["agname"] = "";
            ViewState["clname"] = "";
            ViewState["isremove"] = false;
            // dtInsertionDate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            //ddlPublication.SelectedIndex = 0;
            btnCancelAdd.Visible = false;
            //ddlBaseStation.SelectedIndex = 0;
            ddlAdStation.SelectedIndex = 0;
            ddlBookingExecutive.SelectedIndex = 0;
            txtClient.Text = string.Empty;
            txtAgency.Text = string.Empty;
            txtClient.Text = string.Empty;
            txtRO.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ddlCM.SelectedIndex = 0;
            ddlCOL.SelectedIndex = 0;
            ddlPage.SelectedIndex = 0;
            ddlPosition.SelectedIndex = 0;

            ddlCOL.SelectedIndex = 0;
            ddlMaterial.SelectedIndex = 0;
            gvPosition.DataSource = null;
            gvPosition.DataBind();
            lblMessage.Text = string.Empty;
            RowAsOn.Visible = false;
            hdclientid.Value = "";
            hdagencyid.Value = "";
            chkConfirm.Checked = false;
            ddlCategory.SelectedIndex = 0;
            txtCaption.Text = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtRecord = new DataTable();

            dtRecord.Columns.Add("ID", typeof(Int32));
            dtRecord.Columns.Add("PID", typeof(Int32));
            dtRecord.Columns.Add("Station", typeof(string));
            dtRecord.Columns.Add("Page", typeof(string));
            dtRecord.Columns.Add("Position", typeof(string));
            dtRecord.Columns.Add("PageID", typeof(Int32));
            dtRecord.Columns.Add("StationID", typeof(Int32));
            dtRecord.Columns.Add("PositionID", typeof(string));
            ViewState["dtRecord"] = dtRecord;
            RowSearch.Visible = false;
            gvAd.DataSource = null;
            gvAd.DataBind();
            gvAgency.DataSource = null;
            gvAgency.DataBind();
            gv.DataSource = null;
            gv.DataBind();

            btnSearchAds.Text = "Search";
            btnSave.Text = "Save";
            obj = new DBRegister();
            DBManager db = new DBManager();
            db.Open();
            db.Command.Parameters.Clear();
            db.CreateParameters(0);
            dt = obj.ExecuteDataTable(db, "sp_GetBaseStations");
            ddlStations.DataValueField = "ID";
            ddlStations.DataTextField = "Abreviation";
            ddlStations.DataSource = dt;
            ddlStations.DataBind();
            db.Close();
            db.Dispose();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dtInsertionDate.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
            FormReset();
        }

        protected void dtInsertionDate_TextChanged(object sender, EventArgs e)
        {
            // lblAdDate.Text = "Searching in " +  dtInsertionDate.Text ;
        }

        protected void btnSearchAds_Click(object sender, EventArgs e)
        {
            ViewState["isremove"] = false;
            lblMessage.Text = string.Empty;
            btnCancelAdd.Visible = false;
            DBManager db = new DBManager();
            db.Open();
            try
            {
                if (btnSearchAds.Text == "Search")
                {
                    btnSearchAds.Text = "Hide";
                    db.Command.Parameters.Clear();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@InsertionDate", Helper.SetDateFormat(dtInsertionDate.Text));
                    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_SearchAdsByDate");
                    gvAd.DataSource = ds.Tables[0];
                    gvAd.DataBind();
                    RowSearch.Visible = true;
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        btnSearchAds.Text = "Search";
                        RowSearch.Visible = false;
                    }
                    //ScriptManager.RegisterStartupScript(up,up.GetType(),"myFunction", "myFunction('show');", true);
                    // 
                }
                else
                {
                    gvAd.DataSource = null;
                    gvAd.DataBind();
                    gvPosition.DataSource = null;
                    gvPosition.DataBind();

                    btnSearchAds.Text = "Search";
                    //ScriptManager.RegisterStartupScript(up, up.GetType(), "myFunction", "myFunction('hide');", true);
                    RowSearch.Visible = false;
                }
            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }

        protected void btnSelectAds_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gvAd.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            DBManager db = new DBManager();
            db.Open();
            try
            {
                db.Command.Parameters.Clear();
                db.CreateParameters(1);
                db.AddParameters(0, "RecordID", ID);
                DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetBookingRegisterByID");
                string adm = "";
                if (Session["Admin"].ToString() == "True")
                    adm = "Admin";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((Session["LoginID"].ToString() == ds.Tables[0].Rows[0]["User_Id"].ToString()) || (adm == "Admin"))
                    {

                        btnCancelAdd.Visible = true;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnCancelAdd.Visible = false;
                    }
                    ddlPublication.SelectedValue = ds.Tables[0].Rows[0]["Publication_Id"].ToString();
                    ddlBaseStation.SelectedValue = ds.Tables[0].Rows[0]["GroupComp_BaseStation"].ToString();

                    ddlAdStation.SelectedValue = ds.Tables[0].Rows[0]["Station_id"].ToString();
                    //ddlBookingExecutive.SelectedValue = ds.Tables[0].Rows[0]["User_Id"].ToString();
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["SubCategory_Id"].ToString();
                    txtAgency.Text = ds.Tables[0].Rows[0]["Agency"].ToString();
                    txtCaption.Text = ds.Tables[0].Rows[0]["Caption"].ToString();
                    txtRO.Text = ds.Tables[0].Rows[0]["RO"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    txtClient.Text = ds.Tables[0].Rows[0]["Client"].ToString();
                    hdclientid.Value = ds.Tables[0].Rows[0]["ClientID"].ToString();
                    ddlColor.SelectedValue = ds.Tables[0].Rows[0]["Color"].ToString().Trim();
                    ddlCOL.SelectedValue = ds.Tables[0].Rows[0]["COL"].ToString().Trim();
                    ddlCM.SelectedValue = ds.Tables[0].Rows[0]["CM"].ToString().Trim();
                    ddlMaterial.SelectedValue = ds.Tables[0].Rows[0]["Material"].ToString().Trim();
                    chkConfirm.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsConfirm"]);
                    if (ds.Tables[0].Rows[0]["Material"].ToString() == "O")
                    {
                        try
                        {
                            txtAsOn.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["AsOn_Date"]).ToString("dd-MM-yyyy");
                        }
                        catch (Exception)
                        {

                            txtAsOn.Text = string.Empty;
                        }

                        RowAsOn.Visible = true;
                    }
                    else
                    {
                        RowAsOn.Visible = false;
                        txtAsOn.Text = string.Empty;
                    }
                    db.Command.Parameters.Clear();
                    db.CreateParameters(1);
                    db.AddParameters(0, "PID", ID);
                    ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_BookingRegisterDetails");
                    int i = 1;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ds.Tables[0].Rows[i - 1][0] = i;
                        i++;
                    }
                    gvPosition.DataSource = ds.Tables[0];
                    gvPosition.DataBind();

                    ViewState["dtRecord"] = ds.Tables[0];
                    btnSave.Text = "Update";

                }
            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }

        protected void gvPosition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            //gvPosition.PageIndex = e.NewPageIndex;
            //DataTable dt = (DataTable)ViewState["dtRecord"];
            //gvPosition.DataSource = dt;
            //gvPosition.DataBind();
        }

        protected void btnChart_Click(object sender, EventArgs e)
        {
            string publicationID = ddlPublication.SelectedValue.ToString();
            Session["publication"] = ddlPublication.SelectedItem.Text;
            Response.Redirect("BookingStatus.aspx?InsertionDate=" + dtInsertionDate.Text + "&publicationID=" + publicationID, true);
        }

        protected void gvAd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hd = (HiddenField)e.Row.FindControl("hdConfirm");
                bool bit = Convert.ToBoolean(hd.Value);
                if (bit == true)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(214, 251, 214);
                }
            }

        }

        protected void dtInsertionDate_TextChanged1(object sender, EventArgs e)
        {
            Session["dtInsertionDate"] = dtInsertionDate.Text;
        }

        protected void ddlPublication_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            db.Open();
            try
            {
                Session["publicationID"] = Convert.ToInt32(ddlPublication.SelectedValue);
                Session["publication"] = ddlPublication.SelectedItem.Text;
                obj = new DBRegister();
                db.CreateParameters(1);
                db.AddParameters(0, "@PubID", Convert.ToInt32(ddlPublication.SelectedValue));
                DataTable dt = obj.ExecuteDataTable(db, "sp_PublicationPages");
                ddlPage.DataValueField = "ID";
                ddlPage.DataTextField = "Caption";
                ddlPage.DataSource = dt;
                ddlPage.DataBind();
            }
            catch (Exception)
            {
                db.Close();
                db.Dispose();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            db.Open();
            Int32 RecordID = Convert.ToInt32(ViewState["RecordID"]);
            obj = new DBRegister();
            obj.ID = RecordID;
            try
            {

                db.Command.Parameters.Clear();
                db.BeginTransaction();
                obj.CancelBooking(db);
                db.Command.Parameters.Clear();
                db.CreateParameters(1);
                db.AddParameters(0, "@For_Table", "Log");
                int LogID = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure, "usp_GetCounter"));
                obj.UserID = Session["LoginID"].ToString();
                obj.LogAction = "Cancel";
                obj.LogID = LogID;
                obj.InsertLog(db);
                db.CommitTransaction();
                btnCancel_Click(null, null);
                lblMessage.Text = "Booking Cancelled, SMS Sent to All Executives";

            }
            catch (Exception)
            {
                db.Transaction.Rollback();
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }

        protected void gvPosition_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["dtRecord"];
            //gvPosition.DeleteRow(e.RowIndex);
            //gvPosition.DataBind();
            ////.DataSource = dt;
            ////gvPosition.DataBind();
            //if (dt.Rows.Count == 0)
            //{
            //    ViewState["isremove"] = true;
            //}

        }


    }
}
