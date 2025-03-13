using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Master.Model
{
    public class Storer
    {
        public string storerkey { get; set; }
        public string company { get; set; }
        public string address { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
    }
}
