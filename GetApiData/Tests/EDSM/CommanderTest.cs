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
        const string publicCommanderName = "Schitt Staynes";
        const string privateCommanderName = "WDX";
        const string nonexistentCommanderName = "ddsaDSAD2312";
        const string emptyCommanderNameWhiteSpaces = "   ";
        public class Ranks

        {
            

            [Test]
            public async Task GetCommanderPublic() {

                var response = await EdsmAPI.GetCommanderRanks(publicCommanderName);
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.AreEqual(100, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderPrivate() {
                var response = await EdsmAPI.GetCommanderRanks(privateCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual(207, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderWhiteSpaces() {
                var response = await EdsmAPI.GetCommanderRanks(emptyCommanderNameWhiteSpaces);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual( 201, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderEmptyInput() {
                var response = await EdsmAPI.GetCommanderRanks(String.Empty);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual( 201, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderWrongInput() {
                var response = await EdsmAPI.GetCommanderRanks(nonexistentCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual( 203, response.MessageNumber);
            }
        }
        
        public class Position
        {
            [Test]
            public async Task GetCommanderPublic() {

                var response = await EdsmAPI.GetCommanderPosition(publicCommanderName);
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.AreEqual(100, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderPrivate() {
                var response = await EdsmAPI.GetCommanderPosition(privateCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual(203, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderWhiteSpaces() {
                var response = await EdsmAPI.GetCommanderPosition(emptyCommanderNameWhiteSpaces);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual(201, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderEmptyInput() {
                var response = await EdsmAPI.GetCommanderPosition(String.Empty);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual(201, response.MessageNumber);
            }
            [Test]
            public async Task FailCommanderWrongInput() {
                var response = await EdsmAPI.GetCommanderPosition(nonexistentCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.AreEqual(203, response.MessageNumber);
            }
        }
    }

    
}