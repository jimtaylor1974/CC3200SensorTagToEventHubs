using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CC3200SensorTag
{
    public class CC3200SensorTag : IDisposable
    {
        private readonly HttpClient httpClient;

        public CC3200SensorTag(string sensorUrl)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(sensorUrl)
            };
        }

        public async Task<SensorReadings> SensorReadings()
        {
            try
            {
                var response = await httpClient.GetAsync("/param_sensortag_poll.html");

                var readingLocalTime = DateTime.Now;

                var html = await response.Content.ReadAsStringAsync();

                var document = XDocument.Load(new StringReader(html));

                var rawReadings = document
                    .XPathSelectElements("/html/body/p[@id]")
                    .ToDictionary(key => key.Attribute("id")?.Value, value => value.Value);

                return new SensorReadings(readingLocalTime, rawReadings);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return null;
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
