using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Orcabot.Api.EDSM.Test
{
    public class SystemsTest
    {
        public class GetSystemStations
        {
            [Test]
            public async Task SystemNonExistent() {
                var response = await EdsmAPI.GetSystemStations("dsadsalkajld");
                response.Dump();

            }
            [Test]
            public async Task SystemNoStations() {
                var response = await EdsmAPI.GetSystemStations("Lysoorb AX-L b49-7");
                response.Dump();
                Assert.AreEqual(0, response.Data.StationsArray.Length, "Should return no stations, as no exist in said system");
            }
            [Test]
            public async Task SystemStations() {
                var response = await EdsmAPI.GetSystemStations("Eravate");
                response.Dump();
                Assert.Greater(response.Data.StationsArray.Length, 0, "Should have more than one station, as stations exist");
                // [+] - Eravate has 8 Stations (at the writing of this test)
                Assert.GreaterOrEqual(response.Data.StationsArray.Length, 8, "Eravate has 8 Stations (at the writing of this test)");
                // 
                Console.WriteLine("Station Count: " + response.Data.StationsArray.Length);

            }
            

        }
        public class GetSystemDeaths
        {
            [Test]
            public async Task SystemNonExistent() {
                var response = await EdsmAPI.GetSystemDeaths("sdsadas");
                response.Dump();
                Assert.IsNull(response.Data,"System does not exist. Empty object should be returned by API.");
                Assert.AreEqual(1, response.MessageNumber,"1 indicates that the response was empty");

            }
            [Test]
            public async Task SystemNoDeaths() {
                var response = await EdsmAPI.GetSystemDeaths("Lysoorb AX-L b49-7");
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.AreEqual(0, response.Data.AllTime);
               
            }
            [Test]
            public async Task SystemDeaths() {
                var response = await EdsmAPI.GetSystemDeaths("Deciat");
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.Greater( response.Data.AllTime,0,"Deciat has Deaths");

            }
        }
        public class GetSystemTraffic
        {
            [Test]
            public async Task SystemNonExistent() {
                var response = await EdsmAPI.GetSystemTraffic("sdsadas");
                response.Dump();
                Assert.IsNull(response.Data, "System does not exist. Empty object should be returned by API.");
                Assert.AreEqual(1, response.MessageNumber, "1 indicates that the response was empty");
            }
           

            [Test]
            public async Task SystemTraffic() {
                var response = await EdsmAPI.GetSystemTraffic("Deciat");
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.Greater(response.Data.AllTime, 0, "Deciat has traffic");
            }
        }
        public class SystemRadius
        {
            [Test]
            public async Task FailGetSystemNonExistent() {
                var response = await EdsmAPI.GetSystemsInRadius("sdajasd");
                response.Dump();
                Assert.IsNull(response.Data);
            }
            [Test]
            public async Task GetRadiusBySystemName() {
                var response = await EdsmAPI.GetSystemsInRadius("Colonia");
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.Greater(response.Data.entries.Length,0);
            }
            [Test]
            public async Task GetRadiusByCoordinates() {
                var response = await EdsmAPI.GetSystemsInRadius(null,new Orcabot.Types.Coordinate { X = 120, Y = 1337, Z = 420});
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.Greater(response.Data.entries.Length, 0);
            }
        }
        public class GetSystemData
        {
            [Test]
            public async Task GetSystemNonExistent() {
                var response = await EdsmAPI.GetSystemData("jhasdsadhjkdsajhk");
                response.Dump();
                Assert.IsNull(response.Data);
            }
            [Test]
            public async Task GetSystemEmptyInput() {
                var response = await EdsmAPI.GetSystemData(String.Empty);
                response.Dump();
                Assert.IsNull(response.Data);
            }
            [Test]
            public async Task GetSystemExistent() {
                var response = await EdsmAPI.GetSystemData("Colonia");
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.IsNotNull(response.Data.Name);
                Assert.IsNotNull(response.Data.Coordinate);
                Assert.AreEqual(-9530.5f, response.Data.Coordinate.X);
                Assert.AreEqual(-910.28125f, response.Data.Coordinate.Y);
                Assert.AreEqual(19808.125f, response.Data.Coordinate.Z);
                Assert.IsTrue(response.Data.IsScoopable);
            }
        }
    }
}
