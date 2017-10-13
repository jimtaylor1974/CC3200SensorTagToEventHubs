using System;
using System.Threading.Tasks;

namespace CC3200SensorTag
{
    public class SensorTagPoller : IDisposable
    {
        private readonly CC3200SensorTag sensorTag;
        private readonly SensorTagHubClient azureServiceBus;
        private readonly bool sendToAzure;

        public SensorTagPoller(
            string sensorUrl,
            bool sendToAzure,
            string ehConnectionString,
            string ehEntityPath)
        {
            this.sendToAzure = sendToAzure;
            sensorTag = new CC3200SensorTag(sensorUrl);
            azureServiceBus = new SensorTagHubClient(ehConnectionString, ehEntityPath);
        }

        public async Task Poll()
        {
            var readings = await sensorTag.SensorReadings();

            Console.WriteLine(readings);
            Console.WriteLine();

            await SendReadings(readings);
        }

        public async Task SendReadings(SensorReadings readings)
        {
            if (sendToAzure)
            {
                await azureServiceBus.Send(readings);
            }
        }

        public void Dispose()
        {
            sensorTag?.Dispose();
        }
    }
}
