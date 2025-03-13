using InterfaceWMSToRedpanda.Feature.Global.Model;
using InterfaceWMSToRedpanda.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Global.Dao
{
    internal class GlobalDao
    {
        public ms_config getMsConfig(string configType)
        {
            string strErr = "";
            ms_config data = null;

            string strSql =
                "SELECT config_value1, config_value2, config_value3, config_value4, config_value5 " +
                "FROM ms_config WHERE config_type = @type ";

            Hashtable listParam = new Hashtable();
            listParam.Add("@type", configType);

            DataTable dt = ClassQuery.ExecuteQuery(ClassVar.strconnrtl, CommandType.Text, strSql, listParam, null, out strErr);

            if (string.IsNullOrWhiteSpace(strErr) && dt != null && dt.Rows.Count > 0)
            {
                data = new ms_config();
                data.config_value1 = dt.Rows[0]["config_value1"].ToString();
                data.config_value2 = dt.Rows[0]["config_value2"].ToString();
                data.config_value3 = dt.Rows[0]["config_value3"].ToString();
                data.config_value4 = dt.Rows[0]["config_value4"].ToString();
                data.config_value5 = dt.Rows[0]["config_value5"].ToString();
            }

            return data;
        }

        public bool checkIsInitialTable(string kode, string whseid)
        {
            bool check = true;
            string strErr = "";

            string strSql =
                "SELECT * FROM t_LogDataInitial WHERE Kode = @kode";


            Hashtable listParam = new Hashtable();
            listParam.Add("@kode", kode);

            if (!string.IsNullOrWhiteSpace(whseid))
            {
                listParam.Add("@whseid", whseid);

                strSql += " AND WHSEID = @whseid";
            }

            DataTable dt = ClassQuery.ExecuteQuery(ClassVar.strconnexp, CommandType.Text, strSql, listParam, null, out strErr);

            if (string.IsNullOrWhiteSpace(strErr) && dt != null && dt.Rows.Count > 0)
            {
                check = false;
            }

            return check;
        }

        public bool saveInitialTable(string kode)
        {
            bool status = true;
            string strErr = "";

            string strSql =
                "INSERT INTO t_LogDataInitial (KODE, WHSEID) " +
                "VALUES (@kode, '') ";


            Hashtable listParam = new Hashtable();
            listParam.Add("@kode", kode);

            ClassQuery.ExecuteQuery(ClassVar.strconnexp, CommandType.Text, strSql, listParam, null, out strErr);

            if (!string.IsNullOrWhiteSpace(strErr))
            {
                status = false;
            }

            return status;
        }
    }
}
