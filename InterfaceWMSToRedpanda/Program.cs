// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using InterfaceWMSToRedpanda.Helper;
using log4net.Config;
using log4net;
using System.Reflection;
using InterfaceWMSToRedpanda.Feature.Master;


var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

string server = string.Empty;
DateTime? date = null;
int code = 0;

args = new[] { "" };

ClassVar.OpenDBConnExp();
ClassVar.OpenDBConnRtl();

args = new[] { "Code=1" };

//List Code job 
//  1 = Job Interface Master Shipto (Code=1)

if (args != null && args.Length > 0)
{
    foreach (var data in args)
    {
        if (data.Contains("Code"))
        {
            string[] codes = data.Split('=');
            code = Convert.ToInt32(codes[1].ToString().Replace(" ", ""));
        }

        if (data.Contains("Server"))
        {
            string[] servers = data.Split('=');
            server = servers[1].ToString().Replace(" ", "");

        }
        //Added by Jamiel 28 July 2023 AR2023-7959 AdHoc datetime manual kirim email
        if (data.Contains("Date"))
        {
            string[] Dates = data.Split('=');
            date = Convert.ToDateTime(Dates[1].ToString().Replace(" ", ""));
        }
    }
}
else
{
    Console.WriteLine("Parameter Salah");
}

try
{
    switch (code)
    {
        case 1:
            MasterShipto();
            break;
        default:
            Console.WriteLine("Code tidak valid");
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine("Process Error");
}

void MasterShipto()
{
    MasterFacade mf = new MasterFacade();
    mf.sendMasterShipto();
}

