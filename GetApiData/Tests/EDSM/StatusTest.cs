using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orcabot.Api.EDSM.Test
{
    class StatusTest
    {
        [Test]
        public async Task GetApiStatus() {

            var response = await EdsmAPI.GetApiStatus();
            response.Dump();
            Assert.IsNotNull(response);
           
        }
    }
}
