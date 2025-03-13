using Confluent.Kafka;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace InterfaceWMSToRedpanda.Helper
{
    public class SendData
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool sendToRedpanda(string url, string topic, string key, string value)
        {
            bool status = true;

            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = url
                };

                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {
                    var message = new Message<string, string>
                    {
                        Key = key,
                        Value = value
                    };

                    var data = producer.ProduceAsync(topic, message).Result;
                }
            }
            catch(Exception ex)
            {
                log.Error(string.Format("Error Send To Redpanda - Topic {0} - Key {1} - Value {2} - Error {3}", topic, key, value, ex.Message));
                status = false;
            }


            return status;
        }
    }
}
