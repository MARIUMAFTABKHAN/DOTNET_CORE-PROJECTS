using CDSN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
    public static class Helper
    {
        public static string Page { get; set; }    
        public static int UID { get; set; }
        public static Int32 DefaultCountry  { get; set; } = 110000001;

    public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> enumerable,
                                                      int chunkSize)
    {
        int itemsReturned = 0;
        var list = enumerable.ToList(); // Prevent multiple execution of IEnumerable.
        int count = list.Count;
        while (itemsReturned < count)
        {
            int currentChunkSize = Math.Min(chunkSize, count - itemsReturned);
            yield return list.GetRange(itemsReturned, currentChunkSize);
            itemsReturned += currentChunkSize;
        }
    }

    private static Random random = new Random((int)DateTime.Now.Ticks);

     
            public static class Messages
            {
                public const string Authorization = "You are not authorised to edit this record ...";
                
            }
        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static DateTime SetDateFormat(string txt)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            try
            {
                string[] str = txt.Split('/');

                string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

                // dt       
                dt = Convert.ToDateTime(a);
            }
            catch (Exception)
            {
                SetDateFormat(DateTime.Now.ToString("dd/MM/yyy"));
            }
            return dt;

        }

    public static DateTime SetDateFormat2(string txt)
    {
        DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        try
        {
            string[] str = txt.Split('-');

            string a = str[2].ToString() + "-" + str[1].ToString()+ "-" + str[0].ToString();

            // dt       
            dt = Convert.ToDateTime(a);
        }
        catch (Exception)
        {
            SetDateFormat(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        return dt;

    }
    public static string SetDateFormatString(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                txt = DateTime.Now.ToString("dd/MM/yyyy");
            string[] str = txt.Substring(0, 10).Split('/');

            string a = str[1].ToString() + "/" + str[0].ToString() + "/" + str[2].ToString();

            return a;

        }
        public static bool IsEmail(string Email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return (true);
            else
                return (false);
        }
        public static DataSet ToDataSetNew<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }
        public static DataTable ToDataTable<T>(List<T> items)
            {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
                {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
                }
            foreach (T item in items)
                {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                    {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                    }
                dataTable.Rows.Add(values);
                }
            //put a breakpoint here and check datatable
            return dataTable;
            }

        //public static Boolean CheckValidPage(JTSEntities  db)
        //{
        //    bool Result = false;

        //    var lst = db.ExamptedForms.Where(x => x.FormName.Contains(Page)).SingleOrDefault();
        //    if (lst != null)
        //    {

        //        Result = true;
        //    }
        //    else
        //    {
        //        db = new JTSEntities();
        //        var s = db.usp_CheckValidPage(Page, UID).ToList().Count();
        //        if (s > 0)
        //            Result = true;
        //    }
        //    return Result;
        //}
      //  public static int INTEGER_NULL;

        public enum NullValue
        {
            INTEGER_NULL = 0
        }

        public static DateTime GetDefaultDateTime()
        {
            return DateTime.Now;
        }


      //  public Helper()
      //  {
     //   }


        public static String GetDate(String ToDate)
        {
            if (ToDate == String.Empty)
            {
                String monthName = GetMonthName(System.DateTime.Now.Month);
                String dayName = "";
                if (System.DateTime.Now.Day.ToString().Length == 1)
                {
                    dayName = "0" + System.DateTime.Now.Day.ToString();
                }
                else
                {
                    dayName = System.DateTime.Now.Day.ToString();
                }
                ToDate = dayName + "-" + monthName + "-" + System.DateTime.Now.Year.ToString();
                return ToDate;
            }
            else
            {
                return ToDate;
            }
        }

        public static String GetMonthName(Int32 monthNo)
        {
            String MonthName = String.Empty;
            switch (monthNo)
            {
                case 1:
                    MonthName = "January";
                    break;
                case 2:
                    MonthName = "February";
                    break;
                case 3:
                    MonthName = "March";
                    break;
                case 4:
                    MonthName = "April";
                    break;
                case 5:
                    MonthName = "May";
                    break;
                case 6:
                    MonthName = "June";
                    break;
                case 7:
                    MonthName = "July";
                    break;
                case 8:
                    MonthName = "August";
                    break;
                case 9:
                    MonthName = "September";
                    break;
                case 10:
                    MonthName = "October";
                    break;
                case 11:
                    MonthName = "November";
                    break;
                case 12:
                    MonthName = "December";
                    break;
            }
            return MonthName;

        }

        public static string GetDBConnectionString()
        {

            return System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");

        }

        public static string GetArchiveDBConnectionString()
        {

            return System.Configuration.ConfigurationManager.AppSettings.Get("ArchiveConnectionString");

        }

        public static void BoundDropDownList(DropDownList ddlList, DataTable _dt, String FieldName, object ValueField)
        {
            try
            {
                ddlList.DataSource = _dt;
                ddlList.DataTextField = FieldName;
                ddlList.DataValueField = ValueField.ToString();
                ddlList.DataBind();
                ddlList.Items.Insert(0, "-- Please Select --");
            }
            catch { }

        }
        public static void BoundDropDownListWithZeroValueSelectAll(DropDownList ddlList, DataTable _dt, String FieldName, object ValueField)
        {
            try
            {
                ddlList.DataSource = _dt;
                ddlList.DataTextField = FieldName;
                ddlList.DataValueField = ValueField.ToString();
                ddlList.DataBind();
                ddlList.Items.Insert(0, new ListItem("-- Please Select --", "0"));
            }
            catch { }

        }


        public static void SetDropDownListValue(DropDownList ddlList, String SelectedValue)
        {
            try
            {
                if (ddlList.Items.FindByValue(SelectedValue) != null)
                {
                    ddlList.ClearSelection();
                    ddlList.Items.FindByValue(SelectedValue).Selected = true;
                }
                else
                {
                    ddlList.SelectedIndex = 0;
                }
            }
            catch { }
        }

        public static void SetRadioListValue(RadioButtonList rblList, String SelectedValue)
        {
            rblList.ClearSelection();
            for (int i = 0; i < rblList.Items.Count; i++)
            {
                if (rblList.Items[i].Value.Equals(SelectedValue))

                    rblList.Items[i].Selected = true;
            }
        }

        public static String RemoveSpaces(String Text)
        {

            String[] str;

            str = Text.Split((char)32);
            String lastVal = String.Empty;
            List<String> lst = new List<string>();
            for (int i = 0; i <= str.Length - 1; i++)
            {
                if ((lastVal == "") & (str[i] == ""))
                {
                    lastVal = str[i];
                    str[i] = "-";
                }
                else
                {
                    lst.Add(str[i]);
                    lastVal = str[i];
                }

            }

            lst.TrimExcess();

            lst.RemoveAll(EmptyString);



            return String.Join(" ", lst.ToArray());




        }

        private static bool EmptyString(String s)
        {
            if (s == "")
                return true;
            else
                return false;

        }
    //public static string GetDBConnectionString()
    //{
    //    return System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");
    //}
    //public static DataSet GetContactReport(DBManager db, int OperatorID)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(1);

    //    db.AddParameters(0, "@OperatorID", OperatorID);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetails_ByOperatorID");
    //    return ds;

    //}
    //public static int GetSlabByRegion(DBManager db, int ChannelId, int TerritoryId, int ChannelTypeId, int StartingPostion, int EndingPosition)
    //{

    //    int i = 0;
    //    //DBManager db = new DBManager();
    //    //db.Open();
    //    //db.Command.Parameters.Clear();
    //    //db.CreateParameters(6);
    //    //db.AddParameters(0, "@ChannelId", ChannelId);
    //    //db.AddParameters(1, "@RegionId", RegionId);
    //    //db.AddParameters(2, "@TerritoryId", TerritoryId);
    //    //db.AddParameters(3, "@ChannelTypeId", ChannelTypeId);
    //    //db.AddParameters(4, "@StartingPostion", StartingPostion);
    //    //db.AddParameters(5, "@EndingPosition", EndingPosition);
    //    //i = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[usp_GetChannelSlab]"));
    //    // db.Close();
    //    return i;
    //}



    //public static string GetChannelConnectionString()
    //{
    //    return System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");
    //}
    //public static int GetSlab(DBManager db, int ChannelId, int RegionId, int TerritoryId, int ChannelTypeId, int StartingPostion, int EndingPosition)
    //{

    //    int i = 0;
    //    //DBManager db = new DBManager();
    //    //db.Open();
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(6);
    //    db.AddParameters(0, "@ChannelId", ChannelId);
    //    db.AddParameters(1, "@RegionId", RegionId);
    //    db.AddParameters(2, "@TerritoryId", TerritoryId);
    //    db.AddParameters(3, "@ChannelTypeId", ChannelTypeId);
    //    db.AddParameters(4, "@StartingPostion", StartingPostion);
    //    db.AddParameters(5, "@EndingPosition", EndingPosition);
    //    i = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[usp_GetChannelSlab]"));
    //    // db.Close();
    //    return i;
    //}

    //public static int GetSlabRegion(DBManager db, int ChannelId, int RegionId, int ChannelTypeId, int StartingPostion, int EndingPosition)
    //{

    //    int i = 0;
    //    //DBManager db = new DBManager();
    //    //db.Open();
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(5);
    //    db.AddParameters(0, "@ChannelId", ChannelId);
    //    db.AddParameters(1, "@RegionId", RegionId);
    //    db.AddParameters(2, "@ChannelTypeId", ChannelTypeId);
    //    db.AddParameters(3, "@StartingPostion", StartingPostion);
    //    db.AddParameters(4, "@EndingPosition", EndingPosition);
    //    i = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[usp_GetChannelSlab_Region]"));
    //    // db.Close();
    //    return i;
    //}

    public enum RecordStatus
    {
        Active = 1,
        Delete = 2,
        InActive = 3,
        All = 4
    }
    public enum Role
    {
        Admin = 3,
        PurchaseOfficer = 5
    }
    public enum ItemType
    {
        Material = 1,
        Service = 2,
        Asset = 3
    }
    public enum QuatationProcessed
    {
        Processed = 1,
        NotProcessed = 0
    }

    public enum Operation
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }

    public enum Action
    {
        Receive = 1,
        Issue = 2,
        Return = 3,
        PositiveAdjustment = 4,
        NegativeAdjustment = 5
    }

    public enum PurchaseRequestStatus : int
    {
        Pending = 1,
        Approved = 2,
        PartialApproved = 3,
        Closed = 4,
        Unknown = 5
    }
    public enum POStatus : int
    {
        Initiate = 1,
        Issue = 2,
        Cancelled = 3,
        PartialReceived = 4,
        Closed = 5,
        Print = 6,
        Received = 7,
        PartialCancelled = 8
    }

    public enum ApproverStatus
    {
        Approver = 1,
        NonApprover = 0
    }

    public enum ApprovalAction
    {
        Approved = 1,
        SendForComment = 2,
        SendForApproval = 3,
        Rejected = 4
    }

    public enum MaterialMovement
    {
        Transfer = 1,
        PositiveAdjustment = 2,
        NegativeAdjustment = 3
    }

    public static Boolean CheckValidPage(CDSEntities db)
    {
        return true;
        bool Result = false;

        var lst = db.ExamptedForms.Where(x => x.FormName.Contains(Page)).SingleOrDefault();
        if (lst != null)
        {
            //if ((Page == "home.aspx" || (Page == "rptreleaseorder.aspx") || (Page == "releaseorderdetails.aspx") 
            ///  || (Page == "suspendresotregroupagency.aspx") 
            // || (Page == "suspendresotreagency.aspx") || (Page == "suspendresotreclient.aspx") || (Page == "documentsearchreport.aspx")
            //||(Page == "documentacknowledgereport.aspx")||
            //(Page == "reportdebitcredit.aspx")
            //))                
            Result = true;
        }

        else
        {
            //db = new  CDSEntities ();
            //var s = db.usp_CheckValidPage(Page, UID).ToList().Count();
            //if (s > 0)
            //    Result = true;
        }
        return Result;
    }

    //public static int GetCounter(string Fortable)
    //{
    //    Int32 RecCounter = 0;
    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    DBManager db = new DBManager();
    //    db.Open();
    //    db.CreateParameters(2);
    //    db.AddParameters(0, "@fortable", Fortable, 0);
    //    db.AddParameters(1, "@IdColumn", 0, 1);

    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[usp_GetCounter]");
    //    RecCounter = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
    //    db.Close();
    //    return RecCounter;
    //}
    //public static int GetCounter(DBManager db, string Fortable)
    //{
    //    Int32 RecCounter = 0;
    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(2);
    //    db.AddParameters(0, "@fortable", Fortable, 0);
    //    db.AddParameters(1, "@IdColumn", 0, 1);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[usp_GetCounter]");
    //    RecCounter = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

    //    return RecCounter;
    //}
    //public static DateTime SetDateFormat(string txt)
    //{
    //    string[] str = txt.Split('-');

    //    string a = str[1].ToString() + "-" + str[0].ToString() + "-" + str[2].ToString();

    //    return Convert.ToDateTime(a);

    //}

    public static int GetTerritory(CDSEntities  db, int UserID)
    {
        Int32 TerritoryID = 0;       
       
        var xx = db.tblUserTerritories.Where(x => x.UserId == UserID).Take(1).SingleOrDefault();
        if (xx != null)
        {
            TerritoryID = Convert.ToInt32(xx.TerritoryId);
        }
        return TerritoryID;
    }

    //public static DateTime GetHistoryDate(DBManager db, int OptrID)
    //{
    //    string strsql = " select min(dateincentive) from tblOpIncentive  where operatorid = " + OptrID;
    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    DataSet ds = db.ExecuteDataSet(CommandType.Text, strsql);
    //    try
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //            return Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
    //        else
    //            return DateTime.Now;
    //    }
    //    catch (Exception)
    //    {

    //        return DateTime.Now;
    //    }

    //}

    //public static DataSet GetContactReport(DBManager db, int UserId, int OperatorID)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(2);
    //    db.AddParameters(0, "@UserID", UserId);
    //    db.AddParameters(1, "@OperatorID", OperatorID);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetails");
    //    return ds;

    //}
    //public static DataSet GetTerritoryList(DBManager db, int UserID)
    //{
    //    string strsql = " SELECT   distinct   tblUserTerritory.TerritoryId as Id, tblTerritory.ShortNames FROM  tblUserTerritory INNER JOIN " +
    //                    " tblTerritory ON tblUserTerritory.TerritoryId = tblTerritory.Id " +
    //                    " where tblUserTerritory.UserId =" + UserID + " order by  2";
    //    db.Command.Parameters.Clear();
    //    DataSet ds = db.ExecuteDataSet(CommandType.Text, strsql);

    //    return ds;

    //}


    //public static DataSet GetContactActivityDetatilsbyOperatorID(DBManager db, int OperatorID)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(1);
    //    db.AddParameters(0, "@OperatorID", OperatorID);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetailsByOperatorID");
    //    return ds;

    //}

    //public static DataSet GetContactReportAdmin(DBManager db, int UserId)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(1);
    //    db.AddParameters(0, "@UserID", UserId);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetailsAdmin");
    //    return ds;

    //}
    //public static DataSet GetNewActivityList(DBManager db, int UserId, string TerritoryList)
    //{
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(2);
    //    db.AddParameters(0, "@UserID", UserId);
    //    db.AddParameters(1, "@TerritoryList", TerritoryList);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "GetNewActivityList");
    //    return ds;

    //}

    //public static DataSet GetContactReportAdmin(DBManager db, int UserId, string TerritoryList)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
    //    db.Command.Parameters.Clear();
    //    db.CreateParameters(2);
    //    db.AddParameters(0, "@UserID", UserId);
    //    db.AddParameters(1, "@TerritoryList", TerritoryList);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetailsAdminString");
    //    return ds;

    //}
    //public static bool UpdateContact(DBManager db, int RecordID)
    //{
    //    try
    //    {
    //        string Strsql = "update tblContact set isResponded = " + 1 + " where ID= " + RecordID + "";
    //        db.Command.Parameters.Clear();
    //        db.CreateParameters(0);
    //        db.ExecuteNonQuery(CommandType.Text, Strsql);
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }

    //}
    public static  string GetDDLValues(ListBox ddl)
    {
        string str = "";
        int i = 0;
        foreach (ListItem item in ddl.Items)
        {
            if (item.Selected == true)
            {

                if (str.Trim().Length == 0)
                {
                    str = item.Value.ToString();

                }
                else
                {
                    str = str + ";" + item.Value.ToString();

                }
                i++;
            }


        }
        return str;
    }

    // For Saplin DropDownCheckBoxes
    public static string GetDDLValues(Saplin.Controls.DropDownCheckBoxes ddl)
    {
        var vals = ddl.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value);
        return string.Join(";", vals);
    }


    //public static DateTime GetHistoryDate(CDSEntities  db, int OptrID)
    //{
    //    var xx = db.tblOpIncentives.Where(x => x.OperatorId == OptrID).Min(x => x.DateIncentive).Date;

    //    try
    //    {
    //        if (xx != null)
    //            return Convert.ToDateTime(xx.Date);
    //        else
    //            return DateTime.Now;
    //    }
    //    catch (Exception)
    //    {

    //        return DateTime.Now;
    //    }

    //}

    //public static DataTable  GetContactReport(CDSEntities  db, int UserId, int OperatorID)
    //{

    //    var ds = db.sp_GetContactDetails(UserId, OperatorID).ToList(); ;

    //    DataTable dt = Helper.ToDataTable(ds);
    //    return dt;

    //}
    //public static DataSet GetTerritoryList(CDSEntities db, int UserID)
    //{
    //    string strsql = " SELECT   distinct   tblUserTerritory.TerritoryId as Id, tblTerritory.ShortNames FROM  tblUserTerritory INNER JOIN " +
    //                    " tblTerritory ON tblUserTerritory.TerritoryId = tblTerritory.Id " +
    //                    " where tblUserTerritory.UserId =" + UserID + " order by  2";

    //    var  ds = db.Database.ExecuteSqlCommand(strsql);


    //    return ds;

    //}
    public static DataTable  GetTerritoryList(CDSEntities db, int UserID)
    {
        var xx = db.usp_GetTerritoryList(UserID).ToList();       
        
        return Helper.ToDataTable(xx);

    }
   
    //public static DataTable  GetContactActivityDetatilsbyOperatorID(CDSEntities  db, int OperatorID)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
        
    //    var xx = db.sp_GetContactDetailsByOperatorID(OperatorID).ToList();
    //    //DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetContactDetailsByOperatorID");
    //    return Helper.ToDataTable(xx);

    //}

    //public static DataTable  GetContactReportAdmin(CDSEntities db, int UserId)
    //{

    //    //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
     
    //    var xx  = db.sp_GetContactDetailsAdmin(UserId).ToList(); ;
    //    return Helper.ToDataTable (xx);

    //}
    //public static DataTable  GetNewActivityList(CDSEntities  db, int UserId, string TerritoryList)
    //{

    //    var ds = db.GetNewActivityList(UserId, TerritoryList).ToList();
    //    return Helper.ToDataTable(ds); ;

    //}

    //public static DataTable  GetContactReportAdmin(CDSEntities db, int UserId, string TerritoryList)
    //{

      
    //    var  ds = db.sp_GetContactDetailsAdminString(UserId, TerritoryList).ToList();
    //    return Helper.ToDataTable(ds);

    //}
    public static bool UpdateContact(CDSEntities  db, int RecordID)
    {
        bool result = false;

        try
        {
            var cont = db.tblContacts.Where(x => x.ID == RecordID ).SingleOrDefault();
            if (cont!= null)
            {
                cont.isViewed = true;
                result = true;
            }
            
            return result;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public static string GetAlerts(int UserID)
    {
        using (CDSEntities db = new CDSEntities())
        {
            var xx = db.sp_GetAlerts(UserID).SingleOrDefault(); ;
            return xx.Value.ToString(); ;
            //DBManager db = new DBManager();
            //db.Open();
            //DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "sp_GetAlerts");        
            //db.Close();
        }
    }
    public static string DDLStr(DropDownList ddl)
    {
      
        string str = string.Empty;
        foreach (ListItem item in ddl.Items)
        {
            if (item.Selected == true)
            {

                // Territory.Add(item.Value.ToString());
                if (str.Trim().Length == 0)
                {
                    str = item.Value.ToString();
                }
                else
                {
                    str = str + ";" + item.Value.ToString();
                }
        
            }       

        } 
        return str;
        
    }
    public static string LBoxStr(ListBox  ddl)
    {

        string str = string.Empty;
        foreach (ListItem item in ddl.Items)
        {
            if (item.Selected == true)
            {

                // Territory.Add(item.Value.ToString());
                if (str.Trim().Length == 0)
                {
                    str = item.Value.ToString();
                }
                else
                {
                    str = str + ";" + item.Value.ToString();
                }

            }

        }
        return str;

    }
    public static int GetCounter(CDSEntities db, string Fortable)
    {
        Int32 RecCounter = 0;
        //String strsql ="select [Counter] from IdCtr where ForTable = '"+ Fortable  +"'";
        
        var   xx = db.usp_GetIDCTRCounter(Fortable).SingleOrDefault();

        return Convert.ToInt32(xx.Value) ;
    }
    public static string GetOperatorList(CDSEntities  db, string txtArea)
    {
        string _list = "";
        if (!string.IsNullOrEmpty(txtArea))
        {

            var ds = db.usp_GetOperatorlistbyAreaText(txtArea).ToList();
            
                foreach (var x  in ds)
                {
                    _list += x.Value .ToString() + ";";
                }
            


        }
        string str = "";
        if (_list.Length > 2)
        {
            str = _list.Substring(0, _list.Length - 1);
        }
        else
        {
            str = "0";
        }
        return str;
    }
    public static  string GetTerritoryListbyUser(CDSEntities  db, int UID)
    {
        string str = "";       
        var  ds = db.usp_GetTerritoryByUserId(UID).ToList();
        try
        {

            foreach (var x   in ds)
            {

                str = str + ";" + x.Id ;
            }

        }
        catch (Exception)
        {
            str = "0";
        }
        if (str.Length > 0)
        {
            return str.Substring(1, str.Length - 1);
        }
        else
            return "0";
    }
    public static DataTable Pivot(this DataTable tbl)
    {
        var tblPivot = new DataTable();
        tblPivot.Columns.Add(tbl.Columns[0].ColumnName);
        for (int i = 1; i < tbl.Rows.Count; i++)
        {
            tblPivot.Columns.Add(Convert.ToString(i));
        }
        for (int col = 0; col < tbl.Columns.Count; col++)
        {
            var r = tblPivot.NewRow();
            r[0] = tbl.Columns[col].ToString();
            for (int j = 1; j < tbl.Rows.Count; j++)
                r[j] = tbl.Rows[j][col];

            tblPivot.Rows.Add(r);
        }
        return tblPivot;
    }



}


