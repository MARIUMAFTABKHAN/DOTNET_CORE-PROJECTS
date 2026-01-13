using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace CDSN.Services
{
    /// <summary>
    /// Summary description for ChartData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ChartData : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetData(int territoryid, int channelid)
        {
            using (CDSEntities db = new CDSEntities())
            {
                List<ClsChannelList> lst = new List<ClsChannelList>();
                List<string> bar1 = new List<string>();
                var r = db.Get_PositionBars("B", territoryid, "0", "0", "0", channelid).ToList();
                foreach (var s in r)
                {
                    if (s.OperatorId == 110000970)
                    {
                        bar1.Add(s.Name);
                    }
                    //lst.Add(new ClsChannelList
                    //{

                    //    Bar1 = s.ChannelName.ToString(),
                    //    POS1 = Convert.ToInt32(s.CurPosition)

                    //});
                }
              //  lst.Add(bar1[0].ToString());
              //  lst.Select(x => x.Bar2 = bar1);

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(lst);

            }
        }
    }


    public class ClsChannelList
    {
        public List<string> Bar1 { get; set; }
        public List<string> Bar2 { get; set; }
        public List<string> Bar3 { get; set; }
        public List<string> Bar4 { get; set; }
        public List<string> Bar5 { get; set; }
        public List<int> POS1 { get; set; }
        public List<int> POS2 { get; set; }
        public List<int> POS3 { get; set; }
        public List<int> POS4 { get; set; }
        public List<int> POS5 { get; set; }
    }
}