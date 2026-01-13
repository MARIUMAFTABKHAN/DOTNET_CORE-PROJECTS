using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace CDSN
{
    /// <summary>
    /// Summary description for GetCPR
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetCPR : System.Web.Services.WebService
    {



        [WebMethod]
        public string sendtask(int OptrID, int sendBy, int sendto, string task, string taskdate,object IsAdminContact, int TerritoryID)
        {
            string message = "";
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                    tblContact   obj = new tblContact ();
                    int ID = Convert.ToInt32(db.usp_GetCounter_CDSApps("Contact").SingleOrDefault().Value);                   
                    obj.ID = ID;
                    obj.Messagetxt = task;
                    obj.MessageDate = DateTime.Now;
                    obj.SentBy = Convert.ToInt32(taskdate);
                    obj.TerritoryID = Convert.ToInt32(TerritoryID);
                    obj.OperatorID = Convert.ToInt32(OptrID);
                    obj.IsResponded = true;
                    bool b = false;
                    if (Convert.ToString(IsAdminContact) == "True")
                        b = true;
                    else
                        b = false;

                    obj.IsAdminContact = b;
                    obj.isViewed = false;
                    obj.isClosed = false;
                    db.tblContacts.Add(obj);
                    db.SaveChanges();


                    message = "Record Marked as Viewed";
                }
            }
            catch(Exception ex)
            {
                message = "";
            }

            return message;                    
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]

        public string SendContent(object Messagetxt, object CreatedBy, object TerritoryID, object OperatorID, object IsResponded, object IsAdminContact, object isViewed, object Sentto)
        {
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                    int sentBy = Convert.ToInt32(CreatedBy);
                    int sentTo = Convert.ToInt32(Sentto);

                    if (HttpContext.Current.Session["userid"] == null)
                    {
                        return "Session expired. Please log in again.";
                    }

                    int createdById = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    bool createdByExists = db.tblUsers.Any(u => u.UserId == createdById);
                    if (!createdByExists)
                    {
                        return $"Invalid CreatedBy. UserId {createdById} does not exist in tblUser.";
                    }

                    // ✅ validate foreign keys
                    if (!db.tblUsers.Any(u => u.UserId == sentBy))
                        return $"Invalid SentBy user ID: {sentBy}";

                    if (!db.tblUsers.Any(u => u.UserId == sentTo))
                        return $"Invalid Sentto user ID: {sentTo}";

                    int ID = Convert.ToInt32(db.usp_GetCounter_CDSApps("Contact").SingleOrDefault().Value);

                    tblContact obj = new tblContact
                    {
                        ID = ID,
                        Messagetxt = Messagetxt?.ToString(),
                        MessageDate = DateTime.Now,
                        CreatedBy = createdById,
                        TerritoryID = Convert.ToInt32(TerritoryID),
                        OperatorID = Convert.ToInt32(OperatorID),
                        IsResponded = true,
                        IsAdminContact = (IsAdminContact?.ToString() == "True"),
                        isViewed = false,
                        isClosed = false,
                        Sentto = sentTo
                    };

                    db.tblContacts.Add(obj);
                    db.SaveChanges();
                    return "Reply send Successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public string ViewedMessage(object RecordID)
        {
            string message = "";
            try
            {
                int id = Convert.ToInt32(RecordID);
                if (id != 0)
                {
                    using (CDSEntities db = new CDSEntities())
                    {

                        var obj = db.tblContacts.Where(x => x.ID == id).FirstOrDefault();
                        obj.isViewed = true;
                        db.SaveChanges();

                    }

                }
                else
                {
                     message =  "Record not found/Invalid Record ID";
                }
            }
            catch (Exception ex)
            {
                message= ex.Message;
            }
            return message;


        }
        [WebMethod]
        public string CloseMessage(object RecordID, object Messagetxt)
        {

            try
            {
                int id = Convert.ToInt32(RecordID);
                if (id != 0)
                {
                    using (CDSEntities db = new CDSEntities())
                    {

                        var obj = db.tblContacts.Where(x => x.ID == id).FirstOrDefault();
                        obj.isClosed = true;
                        obj.Messagetxt = "( Chat Closed ) " + Messagetxt.ToString();
                        db.SaveChanges();

                    }

                }
                else
                {
                    return "Record not found/Invalid Record ID";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Chat Closed Successfully";


        }
        [WebMethod]

        public string GetJSON(ChList[] data)
        //public string GetJSON(int OptrId, int UserId, int ChannelId, int Cpos, int Ppos, int Totalchannels, int PosId)
        {

            using (CDSEntities db = new CDSEntities())
            {
                tblChannelPosition tbl = new tblChannelPosition();
                foreach (var v in data)
                {
                    try
                    {
                        //if ( v.Channelid == 110000176 || v.Channelid == 110000177)
                        //{
                        //    string abc ="";
                        //}
                        Int32 vnID = v.PosId;
                        if (vnID == 0)
                        {
                            tbl = new tblChannelPosition();
                            if (v.Cpos != 0)
                            {
                                tbl = new tblChannelPosition();
                                int id = Convert.ToInt32(db.usp_GetCounter_CDSApps("tblChannelPosition").SingleOrDefault().Value);

                                tbl.Id = id;
                                tbl.ChannelId = v.Channelid;
                                tbl.OperatorId = v.OptrId;
                                tbl.PrevPosition = v.Ppos;
                                tbl.CurPosition = v.Cpos;
                                tbl.WEF = DateTime.Now;
                                tbl.CreateBuy = v.UserId;

                                db.tblChannelPositions.Add(tbl);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            var cpp = db.tblChannelPositions.Where(x => x.Id == vnID).SingleOrDefault();
                            if (cpp != null)
                            {
                                cpp.PrevPosition = cpp.CurPosition;
                                cpp.CurPosition = v.Cpos;
                                db.SaveChanges();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //return ex.Message;
                    }

                }
                return "Data Update Sucessfully.....";


            }


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetOperatorAllActivities(int RecordID)
        {
            string mess = "";
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                    try
                    {
                        int id = Convert.ToInt32(RecordID.ToString());

                        // int TerriroryId = Convert.ToInt32(ddlTerritory.SelectedValue);

                        var xx = db.sp_GetContactDetailsByOperatorIDCDSN1N2(id).OrderByDescending(x => x.MessageDate).ThenBy(x => x.OperatorName).ToList(); ;
                        JavaScriptSerializer jscript = new JavaScriptSerializer();
                        return jscript.Serialize(xx);



                    }
                    catch (Exception ex)
                    {

                        mess = ex.Message;
                    }
                    return mess;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
public class ChList
{


    public int OptrId { get; set; }
    public int Channelid { get; set; }
    public int UserId { get; set; }
    public int Cpos { get; set; }
    public int Ppos { get; set; }
    public int Totalchannels { get; set; }
    public int PosId { get; set; }



}
public class RootObject
{
    public List<ChMessage> data { get; set; }
}
public class ChMessage
{
    public int RecordID { get; set; }
    public string Messagetxt { get; set; }
    public DateTime MessageDate { get; set; }
    public int CreatedBy { get; set; }
    public int TerritoryID { get; set; }
    public int OperatorID { get; set; }
    public Boolean IsResponded { get; set; }
    public Boolean IsAdminContact { get; set; }
    public Boolean isViewed { get; set; }


}
