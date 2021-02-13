using System;
using System.Collections.Generic;
using System.Text;

namespace ServerProfitCenterTest
{
    [Serializable]
    public class XmlData
    {
        public string Multicast { get; set; }
        public double StartRange { get; set; }
        public double EndRange { get; set; }

        public XmlData()
        { }

        public XmlData(string multicast, double startRange, double endRange)
        {
            Multicast   = multicast;
            StartRange  = startRange;
            EndRange    = endRange;
        }
    }
}
