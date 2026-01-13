using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CDSN
{
    public class DeshBoardController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        public List<BarData> GetBarData(int territoryid, int channelid)
        {
            List<BarData> lstBaxter = new List<BarData>();
            using (CDSEntities db = new CDSEntities())
            {               
                List<string> bar1 = new List<string>();
                var r = db.Get_25PositionBars("B", territoryid, "0", "0", "0", channelid).ToList();
                var operators = r.Select(x => new { x.OperatorId, x.Name }).Distinct().ToList();
                foreach (var item in operators)
                {
                    var Baxter = new BarData();
                    Baxter.Name = item.Name;
                    var channels = r.Where(x => x.OperatorId == item.OperatorId).Select(x => new { x.ChannelName }).ToList();
                    foreach (var data in channels)
                    {
                        //Baxter.NewBars = new List<Bars>();
                        Baxter.NewBars = new List<Bars>();
                        Baxter.NewBars.Add(new Bars() { ChannelName = data.ChannelName });
                        lstBaxter.Add(Baxter);
                    }
                    lstBaxter.Add(Baxter);
                }


                return lstBaxter.ToList();// jscript.Serialize(lstBaxter);



            }
        }
    }
    public class Bars
    {
        public string ChannelName { get; set; }
    }

    public class BarData
    {
        public string Name { get; set; }
        public List<Bars> NewBars { get; set; }
    }

}
