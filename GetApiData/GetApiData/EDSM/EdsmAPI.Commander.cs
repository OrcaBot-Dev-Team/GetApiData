using System.Threading.Tasks;
using Orcabot.Api.Types.EDSM.Commander;
using Orcabot.Api.Types.EDSM.Commander.Helper;

namespace Orcabot.Api.EDSM
{
    static partial class EdsmAPI
    {
        public static async Task<Ranks> GetCommanderRanks(string commanderName) {
            string url = "https://www.edsm.net/api-commander-v1/get-ranks?commanderName=" + commanderName;
            var response = await EdsmApiCaller<JSONRanks>.GetWebJSONAsync(url);
            if (response.hasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert();
        }

        public static async Task<Credits> GetCommanderCredits(string commanderName) {
            string url = "https://www.edsm.net/api-commander-v1/get-credits?commanderName=" + commanderName;
            var response = await EdsmApiCaller<JSONCredits>.GetWebJSONAsync(url);
            if (response.hasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert();
        }
        public static async Task<Materials> GetCommanderMaterials(string commanderName) {
            string url = "https://www.edsm.net/api-commander-v1/get-materials?commanderName=" + commanderName;
            var response = await EdsmApiCaller<JSONMaterials>.GetWebJSONAsync(url);
            if (response.hasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert();

        }
    }
}
