using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for GLIntigration
/// </summary>
public static class GLIntigration
{

    public static string StrReason { get; set; }
    public static string GetVRNumber { get; set; }
    public static int PublicationID { get; set; }
    public static int CostCenterID { get; set; }
    public static int DepartmentID { get; set; }
    public static string FromDate { get; set; }
    public static string ToDate { get; set; }
    public static string IDs { get; set; }
    public static string invStatus { get; set; }


    //public static DataTable GetCollections()
    //{

    //    DBManager db = new DBManager();
    //    db.Open();
    //    db.CreateParameters(5);
    //    db.AddParameters(0, "@ChannelId", PublicationID);
    //    db.AddParameters(1, "@FromDate", FromDate);
    //    db.AddParameters(2, "@ToDate", ToDate);
    //    db.AddParameters(3, "@Ids", IDs);
    //    db.AddParameters(4, "@invStatus", invStatus);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "ups_GetMonthlyCollections");
    //    db.Close();
    //    db.Dispose();
    //    return ds.Tables[0];
    //}

    //public static DataTable GetCollectionsDebit()
    //{

    //    DBManager db = new DBManager();
    //    db.Open();
    //    db.CreateParameters(5);
    //    db.AddParameters(0, "@ChannelId", PublicationID);
    //    db.AddParameters(1, "@FromDate", FromDate);
    //    db.AddParameters(2, "@ToDate", ToDate);
    //    db.AddParameters(3, "@Ids", IDs);
    //    db.AddParameters(4, "@invStatus", invStatus);
    //    DataSet ds = db.ExecuteDataSet(CommandType.StoredProcedure, "ups_GetMonthlyCollections_Debit");
    //    db.Close();
    //    db.Dispose();
    //    return ds.Tables[0];
    //}



}