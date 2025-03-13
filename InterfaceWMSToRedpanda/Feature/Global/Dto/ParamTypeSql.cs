using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Global.Dto
{
    public class ParamTypeSql
    {
        public List<SqlDataRecord> listData { get; set; }
        public string typeTableName { get; set; }
        public string typeTableAlias { get; set; }
    }
}
