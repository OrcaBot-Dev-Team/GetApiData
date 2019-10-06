using System;
using System.Threading.Tasks;
using Orcabot.Api.Types.EDSM.StarSystem;
using Orcabot.Api.Types.EDSM.StarSystem.Helper;
using Orcabot.Api.Types.EDSM.Systems;
using Orcabot.Api.Types;

namespace Orcabot.Api.EDSM
{
    static public partial class EdsmAPI
    {
        public static async Task<EDSMResponse<Stations>> GetSystemStations(string systemName) {
            string url = "https://www.edsm.net/api-system-v1/stations?systemName=" + systemName;
            var response = await EdsmApiCaller<StationsJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert((int)response.StatusCode, response.StatusCodeMessage);
            
        }
        public static async Task<Deaths> GetSystemDeaths(string systemName) {
            string url = "https://www.edsm.net/api-system-v1/deaths?systemName=" + systemName;
            var response = await EdsmApiCaller<DeathsJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert();
        }
        public static async Task<Traffic> GetSystemTraffic(string systemName) {
            string url = "https://www.edsm.net/api-system-v1/traffic?systemName=" + systemName;
            var response = await EdsmApiCaller<TrafficJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response.Convert();
        }

        public static async Task<InProximityJSON> GetSystemsInRadius(string systemName, Orcabot.Types.Coordinate coordinate = default, uint radius = 40, bool useSquareInsteadOfSphere = false) {

            string url = GenerateURL();
            var response = await EdsmApiCaller<InProximityJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response;

            string GenerateURL() {
                bool useCoordinate = !coordinate.Equals(default(Orcabot.Types.Coordinate));
                string seachParam = (useCoordinate) ? $"x={coordinate.X}&y={coordinate.Y}&z={coordinate.Z}" : "systemName=" + systemName;
                string type = (useSquareInsteadOfSphere) ? "cube" : "sphere";
                return $"https://www.edsm.net/api-v1/{type}-systems?radius={radius}&showCoordinates=1&showPermit=1&showInformation=1&{seachParam}";
            }
        }
        public static async Task<SystemJSON> GetSystemData(string systemName) {
            string url = "https://www.edsm.net/api-v1/system?systemName=" + systemName;
            var response = await EdsmApiCaller<SystemJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return null;
            }
            return response.response;
        }
    }
}
