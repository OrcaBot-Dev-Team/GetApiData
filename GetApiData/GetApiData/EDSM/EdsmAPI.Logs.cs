using System;
using System.Collections.Generic;
using System.Text;
using Orcabot.Api.Types.EDSM.Logs;
using Orcabot.Api.Types.EDSM.Logs.Helper;
using Orcabot.Api.Types.EDSM;
using Orcabot.Api.Types;
using System.Threading.Tasks;


namespace Orcabot.Api.EDSM
{
    static public partial class EdsmAPI
    {
        public static async Task<EDSMResponse<Position>> GetCommanderPosition(string commanderName) {
            string url = "https://www.edsm.net/api-logs-v1/get-position?showCoordinates=1&commanderName=" + commanderName;
            var response = await EdsmApiCaller<JSONPosition>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return new EDSMResponse<Position> {
                    Data = null,
                    HTMLStatus = (int)response.StatusCode,
                    HTMLStatusResponse = response.StatusCodeMessage,
                    Message = response.err.Message,
                    MessageNumber = 1
                };
            }
            return response.response.Convert((int?)response.StatusCode, response.StatusCodeMessage);
        }
    }
}
