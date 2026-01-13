using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDSN.Model
{
    
        public class DataPoint
        {
            public string Label { get; set; }
            public double Value { get; set; }
        }

        public class ChartData
        {
            public List<DataPoint> Series1 { get; set; }
            public List<DataPoint> Series2 { get; set; }
        }

   
}