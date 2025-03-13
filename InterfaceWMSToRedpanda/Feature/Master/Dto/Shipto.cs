using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Master.Dto
{
    public class Shipto
    {
        public string siteCode { get; set; }
        public string siteName { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string locationName { get; set; }
        public string locationCode { get; set; }
    }
}
