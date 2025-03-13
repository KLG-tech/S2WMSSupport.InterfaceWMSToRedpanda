using InterfaceWMSToRedpanda.Feature.Global.Dto;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Helper
{
    public class ClassQuery
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static DataTable ExecuteQuery(string strConn, CommandType commandType, string strSql, Hashtable listParam, List<ParamTypeSql> listParamType, out string strErr)
        {
            strErr = "";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cm = new SqlCommand(strSql, conn) { CommandType = commandType };
                    if (listParamType != null && listParamType.Count > 0)
                    {
                        foreach (ParamTypeSql param in listParamType)
                        {
                            var pm1 = new SqlParameter(param.typeTableAlias, SqlDbType.Structured) { TypeName = param.typeTableName, Value = param.listData };
                            cm.Parameters.Add(pm1);
                        }
                    }
                    if (listParam != null && listParam.Count > 0)
                    {
                        foreach (DictionaryEntry param in listParam)
                        {
                            var pm3 = new SqlParameter(param.Key.ToString(), param.Value);
                            cm.Parameters.Add(pm3);
                        }
                    }
                    cm.CommandTimeout = 360;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cm;
                    da.Fill(dt);
                    conn.Close();
                    conn.Dispose();

                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                log.Error(ex.Message); //Added by Jamiel 21 July 2023 AR2023-7959 logging for DB related issue
                return null;
            }
            return dt;
        }
    }
}
