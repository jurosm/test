using CODA11;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace ConsoleApp2
{
    public class TestClient
    {
        [Fact]
        public async Task IsValueEqualAsync()
        {
            var uriB = new UriBuilder()
            {
                Port = 8000,
                Path = "/concentrator/ws"
            };

            var endpoint = new EndpointAddress(uriB.Uri);

            var client = new ConcentratorClient(GetBasicHttpBinding(), endpoint);

            var bla = await client.GetUnitsListAsync();
            Assert.Equal("234LKJ32KLJL3", bla.UnitsList[0].ID);
        }

        public static BasicHttpBinding GetBasicHttpBinding()
        {
            var quota = new XmlDictionaryReaderQuotas
            {
                MaxBytesPerRead = 2097152,
                MaxStringContentLength = 100000000
            };

            var ts = new TimeSpan(0, 0, 3, 0);
            var bb = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                OpenTimeout = ts,
                CloseTimeout = ts,
                ReceiveTimeout = ts,
                SendTimeout = ts,
                ReaderQuotas = quota,
                MaxReceivedMessageSize = 2097152
            };

            return bb;
        }
    }
}
