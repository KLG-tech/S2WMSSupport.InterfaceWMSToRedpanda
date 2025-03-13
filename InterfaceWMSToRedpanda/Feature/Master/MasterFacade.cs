using InterfaceWMSToRedpanda.Feature.Global.Dao;
using InterfaceWMSToRedpanda.Feature.Global.Model;
using InterfaceWMSToRedpanda.Feature.Master.Dao;
using InterfaceWMSToRedpanda.Feature.Master.Dto;
using InterfaceWMSToRedpanda.Feature.Master.Model;
using InterfaceWMSToRedpanda.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Feature.Master
{
    public class MasterFacade
    {
        MasterDao mDao = new MasterDao();
        GlobalDao gDao = new GlobalDao();
        SendData sendProc = new SendData();
        public void sendMasterShipto()
        {
            ms_config conf = gDao.getMsConfig("MSREDPANDA");
            ms_config confTopic = gDao.getMsConfig("RPTOPICSHIPTO");

            if (conf != null && confTopic != null)
            {
                bool checkinitial = gDao.checkIsInitialTable("MasterShipto", "");
                List<Storer> listData = mDao.getStoreShipto(checkinitial);

                if (listData != null && listData.Count > 0)
                {
                    foreach (Storer data in listData)
                    {
                        Shipto shp = new Shipto();
                        shp.postalCode = data.zip;
                        shp.locationCode = "";
                        shp.address = data.address;
                        shp.locationName = data.city;
                        shp.siteCode = data.storerkey;
                        shp.siteName = data.company;

                        string value = JsonConvert.SerializeObject(shp);

                        sendProc.sendToRedpanda(conf.config_value1, confTopic.config_value1, "", value);
                    }

                    if (checkinitial)
                    {
                        gDao.saveInitialTable("MasterShipto");
                    }
                }
            }
        }
    }
}
