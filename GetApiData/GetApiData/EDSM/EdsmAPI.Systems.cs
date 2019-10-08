using System;
using System.Threading.Tasks;
using Orcabot.Api.Types.EDSM.StarSystem;
using Orcabot.Api.Types.EDSM.StarSystem.Helper;
using Orcabot.Api.Types.EDSM.Systems;
using Orcabot.Api.Types;
using Orcabot.Api.EDSM;
using Orcabot.Api.Types.EDSM.Systems.Helper;

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
        public static async Task<EDSMResponse<Deaths>> GetSystemDeaths(string systemName) {
            string url = "https://www.edsm.net/api-system-v1/deaths?systemName=" + systemName;
            var response = await EdsmApiCaller<DeathsJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return new EDSMResponse<Deaths> {
                    Data = null,
                    HTMLStatus = (int)response.StatusCode,
                    HTMLStatusResponse = response.StatusCodeMessage,
                    Message = response.err.Message,
                    MessageNumber = 1
                };
            }
            return response.response.Convert((int)response.StatusCode,response.StatusCodeMessage);
        }
        public static async Task<EDSMResponse<Traffic>> GetSystemTraffic(string systemName) {
            string url = "https://www.edsm.net/api-system-v1/traffic?systemName=" + systemName;
            var response = await EdsmApiCaller<TrafficJSON>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return new EDSMResponse<Traffic> {
                    Data = null,
                    HTMLStatus = (int)response.StatusCode,
                    HTMLStatusResponse = response.StatusCodeMessage,
                    Message = response.err.Message,
                    MessageNumber = 1
                };
            }
            return response.response.Convert((int)response.StatusCode,response.StatusCodeMessage);
        }

        public static async Task<EDSMResponse<InProximity>> GetSystemsInRadius(string systemName, Orcabot.Types.Coordinate coordinate = default, uint radius = 40, bool useSquareInsteadOfSphere = false) {
            uint maxRadiusAllowed = (useSquareInsteadOfSphere) ? (uint)200 : (uint)100;
            if (radius > maxRadiusAllowed)
                radius = maxRadiusAllowed;
            string url = GenerateURL();
            var response = await EdsmApiCallerArray<InProximityJSONEntry>.GetWebJSONAsync(url);
            if (response.HasError) {
                ErrorEvent(response.err);
                return new EDSMResponse<InProximity> {
                    HTMLStatus = (int?)response.StatusCode ?? -1,
                    HTMLStatusResponse = response.StatusCodeMessage,
                    Message = response.err.Message,
                    MessageNumber = -1,
                    Data = null

                };
            }
           
        
            return response.response.Convert((int?)response.StatusCode,response.StatusCodeMessage);

            string GenerateURL() {
                bool useCoordinate = !coordinate.Equals(default(Orcabot.Types.Coordinate));
                string seachParam = (useCoordinate) ? $"x={coordinate.X}&y={coordinate.Y}&z={coordinate.Z}" : "systemName=" + systemName;
                string type = (useSquareInsteadOfSphere) ? "cube" : "sphere";
                return $"https://www.edsm.net/api-v1/{type}-systems?radius={radius}&showCoordinates=1&showPermit=1&showInformation=1&{seachParam}";
            }
        }
        public static async Task<EDSMResponse<StarSystem>> GetSystemData(string systemName) {
            string url = "https://www.edsm.net/api-v1/system?showInformation=1&showId=1&showCoordinates=1&showPrimaryStar=1&showPermit=1&systemName=" + systemName;
            var response = await EdsmApiCaller<SystemJSON>.GetWebJSONAsync(url);
           
            if (response.HasError) {
                ErrorEvent(response.err);
                return new EDSMResponse<StarSystem> {
                    HTMLStatus = (int?)response.StatusCode ?? -1,
                    HTMLStatusResponse = response.StatusCodeMessage,
                    Message = response.err.Message,
                    MessageNumber = -1,
                    Data = null
                };
            }
            
            return response.response.Convert((int?)response.StatusCode, response.StatusCodeMessage) ;
        }
    }
}
