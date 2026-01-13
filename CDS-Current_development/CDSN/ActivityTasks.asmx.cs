using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace CDSN
{
    /// <summary>
    /// Summary description for ActivityTasks
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class ActivityTasks : System.Web.Services.WebService
    {

        
        [WebMethod]
        public string GetOperatorAllActivities(object opid)
        {
            string mess = "";
            try
            {
                using (CDSEntities db = new CDSEntities())
                {

                    try
                    {
                        int id = Convert.ToInt32(opid.ToString());
                        // int TerriroryId = Convert.ToInt32(ddlTerritory.SelectedValue);
                        //int xx = db.sp_GetContactDetailsByOperatorIDCDSN1N(id)
                        //    //OrderByDescending(x => x.MessageDate).
                        //    //ThenBy(x=> x.OperatorName).
                        //    //ToList()
                        //    ; 
                        //JavaScriptSerializer jscript = new JavaScriptSerializer();
                        //return jscript.Serialize(xx);

                        var result = db.sp_GetContactDetailsByOperatorIDCDSN1N(id).ToList();
                        JavaScriptSerializer jscript = new JavaScriptSerializer();
                        return jscript.Serialize(result);

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
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public string SendContent(object Messagetxt, object TerritoryID, object OperatorID, object IsResponded, object IsAdminContact, object isViewed, object sendto)
        {

            try
            {
                int optrid = Convert.ToInt32(OperatorID);
                using (CDSEntities db = new CDSEntities())
                {
                    // ✅ Always use the session user for CreatedBy
                    if (HttpContext.Current.Session["userid"] == null)
                    {
                        return "Session expired. Please log in again.";
                    }

                    int createdById = Convert.ToInt32(HttpContext.Current.Session["userid"]);

                    // ✅ Validate CreatedBy exists in tblUser
                    bool createdByExists = db.tblUsers.Any(u => u.UserId == createdById);
                    if (!createdByExists)
                    {
                        return $"Invalid CreatedBy. UserId {createdById} does not exist in tblUser.";
                    }

                    bool b = false;
                    if (Convert.ToString(IsAdminContact) == "True")
                        b = true;
                    else
                        b = false;

                    int ID = Convert.ToInt32(db.usp_GetCounter_CDSApps("Contact").SingleOrDefault().Value);
                    tblContact obj = new tblContact();
                    obj.ID = ID;
                    obj.Messagetxt = Messagetxt.ToString();
                    obj.MessageDate = DateTime.Now;
                    obj.CreatedBy = createdById;
                    obj.TerritoryID = Convert.ToInt32(TerritoryID);
                    obj.OperatorID = Convert.ToInt32(OperatorID);
                    obj.IsResponded = false;
                    obj.IsAdminContact = b;
                    obj.isViewed = false;
                    obj.isClosed = false;
                    obj.Sentto = Convert.ToInt32(sendto);
                    db.tblContacts.Add(obj);
                    db.SaveChanges();
                    return "Message send Sucessfully.....";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        

        [WebMethod]
        public string MessageCounter(object RecordID)
        {
            Int32 sts = 0;
            try
            {
                
                int id = Convert.ToInt32(RecordID);
                if (id != 0)
                {
                    using (CDSEntities db = new CDSEntities())
                    {

                       
                            var cont = db.tblContacts.Where(x => x.OperatorID == id && x.isClosed == false).ToList();
                            Int32 rv = cont.Where(x => x.IsResponded == true && x.isViewed == true).Count();
                            Int32 v = cont.Where(x => x.IsResponded == false && x.isViewed == true).Count();
                            Int64 nv = cont.Where(x => x.IsResponded == false && x.isViewed == false).Count();


                            sts = 0;

                            if (rv > 0)
                                sts = 1;
                            if (v > 0)
                                sts = 2;
                            if (nv > 0)
                                sts = 3; //reviewed;           
                            
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
            return  sts.ToString();


        }
        [WebMethod]
        public string GetTask(object RecordID)
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

                        //int xx = db.sp_GetContactDetailsByID(id); 
                        //JavaScriptSerializer jscript = new JavaScriptSerializer();
                        //return jscript.Serialize(xx);

                        var result = db.sp_GetContactDetailsByID(id).ToList();
                        JavaScriptSerializer jscript = new JavaScriptSerializer();
                        return jscript.Serialize(result);


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
        [WebMethod]
        public string GetOperator(object UserID)
        {
            string mess = "";
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                   
                    try
                    {
                        int id = Convert.ToInt32(UserID.ToString());
                        
                        // int TerriroryId = Convert.ToInt32(ddlTerritory.SelectedValue);
                      
                                var xx = db.usp_GetHeadEndsByUserId(id).OrderBy(x => x.Name).ToList(); ;
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

        //[WebMethod]
        //public string GetAllOperator(int UserID)
        //{
        //    string mess = "";
        //    try
        //    {
        //        using (CDSEntities db = new CDSEntities())
        //        {

        //            try
        //            {

        //                // int TerriroryId = Convert.ToInt32(ddlTerritory.SelectedValue);

        //                var xx = db.usp_AllOperatorsExceptAdmin(UserID).OrderBy(x => x.Name).ToList(); ;
        //                JavaScriptSerializer jscript = new JavaScriptSerializer();

        //                return jscript.Serialize(xx);
        //            }
        //            catch (Exception ex)
        //            {

        //                mess = ex.Message;
        //            }
        //            return mess;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        
    }
}
