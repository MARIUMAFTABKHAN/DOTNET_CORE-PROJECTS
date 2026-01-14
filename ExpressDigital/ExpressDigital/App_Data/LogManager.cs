using ExpressDigital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ExpressDigital
{
    public static class LogManagers
    {
        public static int RecordID { get; set; }
        public static string ActionOnForm { get; set; }
        public static int ActionBy { get; set; }
        public static DateTime ActionOn { get; set; }
        public static string ActionTaken { get; set; }


        public static bool SetLog(DbDigitalEntities db)
        {

            bool Result = false;
            try
            {
                db = new DbDigitalEntities();
                LogManager log = new LogManager();
                log.ID  = RecordID;
                log.ActionOnForm = ActionOnForm;
                log.ActionBy = ActionBy;
                log.ActionOn = ActionOn;
                log.ActionTaken = ActionTaken;
                db.LogManagers.Add(log);
                db.SaveChanges();
                Result = true;
            }
            catch (Exception)
            {
            }
            return Result;
        }
       
    }
}