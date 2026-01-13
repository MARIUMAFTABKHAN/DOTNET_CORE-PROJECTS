using CDSN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var chartData = new ChartData
                {
                    Series1 = new List<DataPoint>
                {
                    new DataPoint { Label = "January", Value = 30 },
                    new DataPoint { Label = "February", Value = 20 },
                    new DataPoint { Label = "March", Value = 25 },
                },
                    Series2 = new List<DataPoint>
                {
                    new DataPoint { Label = "January", Value = 40 },
                    new DataPoint { Label = "February", Value = 30 },
                    new DataPoint { Label = "March", Value = 35 },
                }
                };

                var jsonSerializer = new JavaScriptSerializer();
                string series1Json = jsonSerializer.Serialize(chartData.Series1);
                string series2Json = jsonSerializer.Serialize(chartData.Series2);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ChartData", $"var series1Data = {series1Json}; var series2Data = {series2Json};", true);
            }
        }
    }
}
