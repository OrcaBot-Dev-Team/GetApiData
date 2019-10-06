using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orcabot.Api.Types.EDSM.ApiStatus.Helper;
using Orcabot.Api.Types.EDSM.Commander.Helper;

namespace Orcabot.Api.EDSM
{
    static public partial class EdsmAPI
    {
 

        public static async Task<Types.EDSM.ApiStatus.ApiStatus> GetApiStatus() {
            string url = "https://www.edsm.net/api-status-v1/elite-server";
            var response = await EdsmApiCaller<Types.EDSM.ApiStatus.JSON>.GetWebJSONAsync(url);
            if (!response.HasError) {

                return response.response.Convert();
                
            }
           
                ErrorEvent(response.err, "Failed to get the current Elite:Dangerous servers' status.");
                return null;
            
        }
    

     
    }


}
