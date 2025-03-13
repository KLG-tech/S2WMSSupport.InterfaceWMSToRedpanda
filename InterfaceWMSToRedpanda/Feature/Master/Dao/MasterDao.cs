using InterfaceWMSToRedpanda.Feature.Master.Model;
using InterfaceWMSToRedpanda.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Master.Dao
{
    public class MasterDao
    {
        public List<Storer> getStoreShipto(bool isInitial)
        {
            string strErr = "";
            List<Storer> listData = null;

            string strSql =
                "SELECT STORERKEY, COMPANY, ISNULL(ADDRESS1,'') + ISNULL(ADDRESS2,'') + ISNULL(ADDRESS3,'') + ISNULL(ADDRESS4,'') ADDRESS, ISNULL(ZIP, '') ZIP, ISNULL(CITY,'') CITY " +
                "FROM ENTERPRISE.STORER WHERE [TYPE] = '10' ";

            if (!isInitial)
            {
                strSql += "  AND DATEDIFF(MINUTE, ISNULL(EDITDATE,ADDDATE) <= 30";
            }

            DataTable dt = ClassQuery.ExecuteQuery(ClassVar.strconnrtl, CommandType.Text, strSql, null, null, out strErr);

            if (string.IsNullOrWhiteSpace(strErr) && dt != null && dt.Rows.Count > 0)
            {
                listData = new List<Storer>();
                foreach (DataRow row in dt.Rows)
                {
                    Storer st = new Storer();
                    st.zip = row["ZIP"].ToString();
                    st.storerkey = row["STORERKEY"].ToString();
                    st.company = row["COMPANY"].ToString();
                    st.address = row["ADDRESS"].ToString();
                    st.city = row["CITY"].ToString();

                    listData.Add(st);
                }
            }

            return listData;
        }
    }
}
