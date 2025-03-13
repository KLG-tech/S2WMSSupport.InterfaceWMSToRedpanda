using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Helper
{
    public static class ClassVar
    {
        public static string RemoteFolderUtamaPath = "";
        public static string RemoteFolderArchivePath = "";
        public static string RemoteFolderErrorPath = "";
        public static string LocalFolderPath = "";
        public static bool SaveErrorToDatabase = true;
        public static string EmailErrorTo = "";

        public static string strconnrtl = "";
        public static string strconnexp = "";

        public static bool OpenDBConnExp()
        {
            ClassGeneral GEN = new ClassGeneral();
            try
            {
                string strcon1 = "";
                String strFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\connectionExp.ini";
                GEN.GetSettings(strFileName, ref strcon1);
                strconnexp = strcon1;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool OpenDBConnRtl()
        {
            ClassGeneral GEN = new ClassGeneral();
            try
            {
                string strcon1 = "";
                String strFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\connectionRTL.ini";
                GEN.GetSettings(strFileName, ref strcon1);
                strconnrtl = strcon1;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
