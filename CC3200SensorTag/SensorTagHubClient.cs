using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace CC3200SensorTag
{
    public class SensorTagHubClient
    {
        private readonly EventHubClient eventHubClient;

        public SensorTagHubClient(
            string ehConnectionString,
            string ehEntityPath)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(ehConnectionString)
            {
                EntityPath = ehEntityPath
            };

            var connectionString = connectionStringBuilder.ToString();

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
        }

        public async Task Send(SensorReadings readings)
        {
            try
            {
                var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(readings));
                var eventData = new EventData(message);

                // Write the body of the message to the console
                Console.WriteLine($"Sending message: {Encoding.UTF8.GetString(message)}");

                await eventHubClient.SendAsync(eventData);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
            }
        }
    }
}
