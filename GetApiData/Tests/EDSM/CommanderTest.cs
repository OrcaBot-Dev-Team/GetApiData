using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Orcabot.Api.EDSM.Test
{
    public static class Helper
    {
        public static void Dump(this object data) {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("");
        }
    }
    public class CommanderTest
    {
       
        public class Ranks

        {
            const string publicCommanderName = "Schitt Staynes";
            const string privateCommanderName = "WDX";
            const string nonexistentCommanderName = "ddsaDSAD2312";
            const string emptyCommanderNameWhiteSpaces = "   ";

            [Test]
            public async Task GetCommanderPublic() {

                var response = await EdsmAPI.GetCommanderRanks(publicCommanderName);
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 100);
            }
            [Test]
            public async Task FailCommanderPrivate() {
                var response = await EdsmAPI.GetCommanderRanks(privateCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 207);
            }
            [Test]
            public async Task FailCommanderWhiteSpaces() {
                var response = await EdsmAPI.GetCommanderRanks(emptyCommanderNameWhiteSpaces);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 201);
            }
            [Test]
            public async Task FailCommanderEmptyInput() {
                var response = await EdsmAPI.GetCommanderRanks(String.Empty);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 201);
            }
            [Test]
            public async Task FailCommanderWrongInput() {
                var response = await EdsmAPI.GetCommanderRanks(nonexistentCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 203);
            }
        }


    }
}