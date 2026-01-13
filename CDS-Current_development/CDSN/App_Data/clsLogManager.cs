
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDSN;
public class clsLogManager
{
    public static int RecordID { get; set; }
    public static string ActionOnForm { get; set; }
    public static int ActionBy { get; set; }
    public static DateTime ActionOn { get; set; }
    public static string ActionTaken { get; set; }


    public static bool SetLog(CDSEntities db)
    {

        bool Result = false;
        try
        {
            db = new CDSEntities();            
            tblLog log = new tblLog();
            log.RecordID = RecordID;
            log.ActionTaken_Form  = ActionOnForm;
            log.UserID  = ActionBy;
            log.ActionDate  = ActionOn;
            log.ActionTaken_Method  = ActionTaken;
            log.IPAddress = GetIP();
            db.tblLogs.Add(log);
            db.SaveChanges();
            Result = true;
        }
        catch (Exception)
        {
        }
        return Result;
    }

    private static string GetIP()
    {
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }
}