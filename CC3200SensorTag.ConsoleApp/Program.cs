using System;
using System.Configuration;
using System.Threading;

namespace CC3200SensorTag.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sensorUrl = Setting("SensorUrl");
            var sendToAzure = Setting("SendToAzure") == "1";
            var ehConnectionString = Setting("EhConnectionString");
            var ehEntityPath = Setting("EhEntityPath");
            var pollSleepTimeoutInMilliseconds = Convert.ToInt32(Setting("PollSleepTimeoutInMilliseconds"));

            using (var poller = new SensorTagPoller(sensorUrl, sendToAzure, ehConnectionString, ehEntityPath))
            {
                while (true)
                {
                    poller.Poll().GetAwaiter().GetResult();

                    Thread.Sleep(pollSleepTimeoutInMilliseconds);
                }
            }
        }

        private static string Setting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /* var testReading = new SensorReadings(DateTime.Now, new Dictionary<string, string>
        {
            { "tmp", "09CC 0C48 19.59 24.56" },
            { "hum", "5D20 6470 36.38 24.74" }, 	
            { "bar", "6A7360 7FD700 24.94 1015.1" }, 	
            { "gyr", "FEB0 FFE9 00AD -2.56 -0.18 1.32" }, 	
            { "acc", "1057 004C FF84 1.02 0.02 -0.03" }, 	
            { "opt", "2E9A 149.52" }, 	
            { "mag", "FEDC 0277 0036 -292 631 54" }, 	
            { "key", "0" }, 	
            { "syn", "11362" }
        });

        poller.SendReadings(testReading).GetAwaiter().GetResult(); */
    }
}
