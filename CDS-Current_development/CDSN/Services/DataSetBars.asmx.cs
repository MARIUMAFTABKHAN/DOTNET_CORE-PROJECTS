using AjaxControlToolkit;
using CDSN.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace CDSN.Services
{
    /// <summary>
    /// Summary description for DataSetBars
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DataSetBars : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<DsBarData> GetBarData(int territoryid, int channelid)
        {
            List<DsBarData> lstBaxter = new List<DsBarData>();
            using (CDSEntities db = new CDSEntities())
            {
                var numbers = new List<int> { 110000093, 110000094, 110000095, 110000096, 110000121 };
                var data = db.Get_25PositionBars("B", territoryid, "0", "0", "0", channelid).ToList();
                int idx = 0;
                int idx2  = 0;
                var Baxter = new DsBarData();
                var result = ( from u in db.tblOperators
                      .Where(f => numbers.Contains(f.Id))
                             select (u.Name)).ToList().Take(5);
                string Op = "";
                string Op1 = "";
                string Op2 = "";
                string Op3 = "";
                string Op4 = "";

                foreach (var item in result)
                {   switch (idx2 )
                    {
                        case 0:
                            Op = item.ToString();
                            idx2++;
                            break;
                        case 1:
                            Op1 = item.ToString();
                            idx2++;
                            break;
                        case 2:
                            Op2 = item.ToString();
                            idx2++;
                            break;
                        case 3:
                            Op3 = item.ToString();
                            idx2++;
                            break;
                        case 4:
                            Op4 = item.ToString();
                            idx2++;
                            break;
                    }

                    
                }
                foreach (var number in numbers)
                {
                    
                   // var bar = data.Where(x => x.OperatorId == Convert.ToInt32(110000121)).ToList();
                    //var Baxter = new DsBarData();
                    //Baxter.NewBars = new List<DsBars>();
                    //foreach (var b1 in bar)
                    //{
                    //    Baxter.NewBars.Add(new DsBars { ChannelName = b1.ChannelName, CurrentPosition = Convert.ToInt32(b1.CurPosition), OperatorName = b1.Name });
                    //}
                    //lstBaxter.Add(Baxter);



                    switch (idx)
                    {
                        case 0:
                            
                            var bar = data.Where(x => x.OperatorId == Convert.ToInt32(number)).ToList();
                            Baxter = new DsBarData();
                            Baxter.NewBars = new List<DsBars>();
                            for (int i = 0; i < 5; i++)
                            {
                                
                                try
                                {
                                    if (bar[i] == null)
                                    {
                                        Baxter.NewBars.Add(new DsBars { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op.ToString() });
                                    }
                                    else
                                    {
                                        Baxter.NewBars.Add(new DsBars { ChannelName = bar[i].ChannelName, CurrentPosition = Convert.ToInt32(bar[i].CurPosition), OperatorName = bar[i].Name });
                                    }
                                }
                                catch (Exception)
                                {
                                    Baxter.NewBars.Add(new DsBars { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op.ToString() });
                                }
                               
                            }

                            lstBaxter.Add(Baxter);
                            idx++;

                            break;
                        case 1:
                            var bar1 = data.Where(x => x.OperatorId == Convert.ToInt32(number)).ToList();                            
                            Baxter.NewBars1 = new List<DsBars1>();
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    if (bar1[i] == null)
                                    {
                                        Baxter.NewBars1.Add(new DsBars1 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op1.ToString() });
                                    }
                                    else
                                    {
                                        Baxter.NewBars1.Add(new DsBars1 { ChannelName = bar1[i].ChannelName, CurrentPosition = Convert.ToInt32(bar1[i].CurPosition), OperatorName = bar1[i].Name });
                                    }
                                }
                                catch (Exception)
                                {
                                    Baxter.NewBars1.Add(new DsBars1 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op1.ToString() });
                                }
                                
                            }
                            lstBaxter.Add(Baxter);
                            idx++;
                            break;
                        case 2:
                            var bar2 = data.Where(x => x.OperatorId == Convert.ToInt32(number)).ToList();                           
                            Baxter.NewBars2 = new List<DsBars2>();

                           


                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    if (bar2[i] == null)
                                    {
                                        Baxter.NewBars2.Add(new DsBars2 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op2.ToString() });
                                    }
                                    else
                                    {
                                        Baxter.NewBars2.Add(new DsBars2 { ChannelName = bar2[i].ChannelName, CurrentPosition = Convert.ToInt32(bar2[i].CurPosition), OperatorName = bar2[i].Name });
                                    }
                                }
                                catch (Exception)
                                {
                                    Baxter.NewBars2.Add(new DsBars2 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op2.ToString() });
                                }
                                
                            }
                            lstBaxter.Add(Baxter);
                            idx++;
                            break;
                        case 3:
                            var bar3 = data.Where(x => x.OperatorId == Convert.ToInt32(number)).ToList();                           
                            Baxter.NewBars3 = new List<DsBars3>();
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    if (bar3[i] == null)
                                    {
                                        Baxter.NewBars3.Add(new DsBars3 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op3 });
                                    }
                                    else
                                    {
                                        Baxter.NewBars3.Add(new DsBars3 { ChannelName = bar3[i].ChannelName, CurrentPosition = Convert.ToInt32(bar3[i].CurPosition), OperatorName = bar3[i].Name });
                                    }
                                }
                                catch (Exception)
                                {
                                    Baxter.NewBars3.Add(new DsBars3 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op3.ToString() });
                                }
                                
                            }
                            lstBaxter.Add(Baxter);
                            idx++;
                            break;
                        case 4:
                            var bar4 = data.Where(x => x.OperatorId == Convert.ToInt32(number)).ToList();                            
                            Baxter.NewBars4 = new List<DsBars4>();
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    if (bar4[i] == null)
                                    {
                                        Baxter.NewBars4.Add(new DsBars4 { ChannelName = "N/A", CurrentPosition = Convert.ToInt32(0), OperatorName = Op4.ToString() });
                                    }
                                    else
                                    {
                                        Baxter.NewBars4.Add(new DsBars4 { ChannelName = bar4[i].ChannelName, CurrentPosition = Convert.ToInt32(bar4[i].CurPosition), OperatorName = bar4[i].Name });
                                    }
                                }
                                catch (Exception)
                                {
                                    Baxter.NewBars4.Add(new DsBars4 { ChannelName = "-", CurrentPosition = Convert.ToInt32(0), OperatorName = Op4.ToString() });
                                }
                            
                            }
                            lstBaxter.Add(Baxter);
                            idx++;
                            break;
                    }

                }
                //lstBaxter.Add(Baxter);
            }
                return lstBaxter;
        
    } 


        public class DsBars
        {
            public string ChannelName { get; set; }
            public int CurrentPosition { get; set; }
            public string OperatorName { get; set; }
        }
        public class DsBars1
        {
            public string ChannelName { get; set; }
            public int CurrentPosition { get; set; }
            public string OperatorName { get; set; }
        }
        public class DsBars2
        {
            public string ChannelName { get; set; }
            public int CurrentPosition { get; set; }
            public string OperatorName { get; set; }
        }
        public class DsBars3
        {
            public string ChannelName { get; set; }
            public int CurrentPosition { get; set; }
            public string OperatorName { get; set; }
        }
        public class DsBars4
        {
            public string ChannelName { get; set; }
            public int CurrentPosition { get; set; }
            public string OperatorName { get; set; }
        }



        public class DsBarData
        {
            public List<DsBars> NewBars { get; set; }
            public List<DsBars1> NewBars1 { get; set; }
            public List<DsBars2> NewBars2 { get; set; }
            public List<DsBars3> NewBars3 { get; set; }
            public List<DsBars4> NewBars4 { get; set; }
        }
    }
}