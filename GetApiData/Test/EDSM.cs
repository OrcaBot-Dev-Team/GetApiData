using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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

    [TestClass]
    public class Status
    {
        

        [TestMethod]
        public async Task GetStatus() {
            
            var response = await EdsmAPI.GetApiStatus();
            response.Dump();
           
        }
    }
    [TestClass]
    public class Commander
    {
        const string publicCommanderName = "Schitt Staynes";
        const string privateCommanderName = "WDX";
        const string nonexistentCommanderName = "ddsaDSAD2312";
        const string emptyCommanderNameWhiteSpaces = "   ";
  
        [TestClass]
        public class Ranks
        {
            [TestMethod]
            public async Task GetCommanderPublic() {

                var response = await EdsmAPI.GetCommanderRanks(publicCommanderName);
                response.Dump();
                Assert.IsNotNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 100);
            }
            [TestMethod]
            public async Task FailCommanderPrivate() {
                var response = await EdsmAPI.GetCommanderRanks(privateCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 207);
            }
            [TestMethod]
            public async Task FailCommanderWhiteSpaces() {
                var response = await EdsmAPI.GetCommanderRanks(emptyCommanderNameWhiteSpaces);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 201);
            }
            [TestMethod]
            public async Task FailCommanderEmptyInput() {
                var response = await EdsmAPI.GetCommanderRanks(String.Empty);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 201);
            }
            [TestMethod]
            public async Task FailCommanderWrongInput() {
                var response = await EdsmAPI.GetCommanderRanks(nonexistentCommanderName);
                response.Dump();
                Assert.IsNull(response.Data);
                Assert.IsTrue(response.MessageNumber == 203);
            }
        }
        
       
    }
}
