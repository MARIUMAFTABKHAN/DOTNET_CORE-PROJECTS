using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace CDSN.Services
{
    /// <summary>
    /// Summary description for MuliBars
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class MuliBars : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<BarData> GetBarData(int territoryid, int channelid)
        {
            List<BarData> lstBaxter = new List<BarData>();
            using (CDSEntities db = new CDSEntities())
            {
                var r = db.Get_25PositionBars("B", territoryid, "0", "0", "0", channelid).ToList();
                var operators = r.Select(x => new { x.OperatorId, x.Name }).Distinct().ToList();

                foreach (var item in operators)
                {
                    var Baxter = new BarData();
                    Baxter.Name = item.Name;
                    Baxter.NewBars = new List<DsBars>();

                    var channels = r.Where(x => x.OperatorId == item.OperatorId).Select(x => new { x.ChannelName, x.CurPosition , OperatorName= x.Name}).ToList();
                    foreach (var data in channels)
                    {
                        Baxter.NewBars.Add(new DsBars { ChannelName = data.ChannelName, CurrentPosition = Convert.ToInt32(data.CurPosition), OperatorName= data.OperatorName });
                    }

                    lstBaxter.Add(Baxter);
                }

                return lstBaxter;
            }
        }
    }

    public class DsBars
    {
        public string ChannelName { get; set; }
        public int CurrentPosition { get; set; }
        public string  OperatorName { get; set; }
    }

    public class BarData
    {
        public string Name { get; set; }
        public List<DsBars> NewBars { get; set; }
    }


}