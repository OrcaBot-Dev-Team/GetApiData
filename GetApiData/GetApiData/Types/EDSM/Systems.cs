using Newtonsoft.Json;
using Orcabot.Api.Types.EDSM;
using System;
using Orcabot.Types.Enums;

namespace Orcabot.Api.Types.EDSM.Systems
{

    #region Sphere/Cube
    /// <summary>
    /// This class is for the sphere and cube api. They both return a response with the same structure
    /// </summary>

    
    public class InProximityJSONEntry : IApiResonseAsArray
    {
        public float? distance;
        public int? bodyCount;
        public string name;
        public ulong id;
        public SystemJSONCoordinates coords;
        public bool requirePermit;
        public string permitName;
    }
    public class InProximity : IApiConverted
    {
        public InProximityEntry[] entries;
    }
    public class InProximityEntry
    {
        public float Distance { get; set; }
        public string Name { get; set; }

        public string PermitName { get; set; }
        public bool NeedsPermit { get; set; }


    }
    namespace Helper
    {
        public static class InProximityHelper
        {
            public static EDSMResponse<InProximity> Convert(this InProximityJSONEntry[] json, int? HTMLStatus, string HTMLStatusMessage) {
                var wrapper = new EDSMResponse<InProximity> {
                    HTMLStatus = HTMLStatus ?? -1,
                    HTMLStatusResponse = HTMLStatusMessage

                };
                if (json == null) {
                    wrapper.Message = "API responded with an empty object. This probably means that the system does not exist";
                    wrapper.MessageNumber = 1;
                    wrapper.Data = null;
                    return wrapper;
                }
                if (json.Length == 0) {
                    wrapper.Message = "API responded with an empty array. This probably means that there are no known systems in the vicinity or said system does not exist.";
                    wrapper.MessageNumber = 3;
                    return wrapper;
                }

                var data = new InProximity {
                    entries = GenerateEntries()
                };
                wrapper.Data = data;
                wrapper.Message = null;
                wrapper.MessageNumber = -1;
                return wrapper;

                InProximityEntry[] GenerateEntries() {
                    var entries = new InProximityEntry[json.Length];
                    for (int i = 0; i < json.Length; i++) {
                        var entry = json[i];
                        var converted = new InProximityEntry {
                            Distance = entry.distance ?? -1,
                            Name = entry.name,
                            PermitName = entry.permitName,
                            NeedsPermit = entry.requirePermit
                        };
                        entries[i] = converted;
                    }
                    return entries;
                }





            }
        }
    }

    #endregion
    #region System
    //example: https://www.edsm.net/api-v1/system?systemName=Alioth&showInformation=1&showId=1&showCoordinates=1&showPrimaryStar=1
    public class SystemJSON : IApiResponse
    {
        public string name;
        public long? id;
        public SystemJSONCoordinates coords;
        public SystemJSONInfo information;
        public SystemJSONPrimaryStar primaryStar;
        public bool requirePermit;
        public string permitName;

    }
    public class SystemJSONCoordinates
    {
        public float x, y, z;
    }
    public class SystemJSONInfo
    {
        public string allegiance, government, faction, factionState, security, economy, secondeconomy, reserve;
        public long? population;
    }
    public class SystemJSONPrimaryStar
    {
        public string type, name;
        public bool isScoopable;
    }
    public class StarSystem : IApiConverted
    {
        public string Name { get; set; }
        public Orcabot.Types.Coordinate Coordinate { get; set; }
        public Security Security { get; set; }
        public bool IsScoopable { get; set; }
        public string PermitName { get; set; }
        public bool NeedsPermit {
            get {
                return PermitName != null;
            }
        }
        public uint Id { get; set; }
        public Orcabot.Types.Enums.Economy Economy { get; set; }
        public string ControllingFactionName { get; set; }
        public string ControllingFactionState { get; set; }
        public ulong? Population { get; set; } 


    }
    namespace Helper
    {



        public static class StarSystemHelper
        {
            /// <summary>
            /// Converts to the SharedTypes Type, although some fields may be missing
            /// </summary>
            public static Orcabot.Types.System Convert(this StarSystem sy) {
                return new Orcabot.Types.System {
                    Name = sy.Name,
                    Coordinate = sy.Coordinate,
                    SystemSecurity = sy.Security,
                    Stations = null
                };
            }
            public static Economy GetEconomyFromEDSMString(string eco) {
                {
                    if (string.IsNullOrWhiteSpace(eco))
                        return Economy.Unknown;
                    switch (eco.ToLower().Trim()) {
                        case "agriculture":
                            return Economy.Agriculture;
                        case "colony":
                            return Economy.Colony;
                        case "extraction":
                            return Economy.Extraction;
                        case "high tech":
                            return Economy.HighTech;
                        case "industrial":
                            return Economy.Industrial;
                        case "military":
                            return Economy.Military;
                        case "refinery":
                            return Economy.Refinery;
                        case "service":
                            return Economy.Service;
                        case "terraforming":
                            return Economy.Terraforming;
                        case "tourism":
                            return Economy.Tourism;
                        case "prison":
                            return Economy.PrisonColony;
                        case "repair":
                            return Economy.Repair;
                        case "rescue":
                            return Economy.Rescue;
                        case "damaged":
                            return Economy.Damaged;
                        default:
                            return Economy.Unknown;
                    }
                }
            }
            public static Security GetSecurityFromEDSMString(string sec) {
                if (string.IsNullOrWhiteSpace(sec))
                    return Security.Unknown;
                switch (sec.ToLower()) {
                    case "high":
                        return Security.High;
                    case "anarchy":
                        return Security.Anarchy;
                    case "medium":
                        return Security.Medium;
                    case "low":
                        return Security.Low;
                    default:
                        return Security.Unknown;
                }
            }
     
            public static EDSMResponse< StarSystem >Convert(this SystemJSON json, int? HTMLStatus, string HTMLStatusMessage) {
          
                var wrapper = new EDSMResponse<StarSystem> {
                    HTMLStatus = HTMLStatus ?? -1,
                    HTMLStatusResponse = HTMLStatusMessage
                };
                if(json == null) {
                    wrapper.Message = "Empty response. This probably means that EDSM could not find said System.";
                    wrapper.MessageNumber = 1;
                    return wrapper;
                }

                var returnObj = new StarSystem {
                    Id = (uint?)json.id ?? 0,
                    Name = json.name,
                    PermitName = json.permitName
                };
                if(json.coords != null) {
                    returnObj.Coordinate = new Orcabot.Types.Coordinate {
                        X = json.coords.x,
                        Y = json.coords.y,
                        Z = json.coords.z
                    };
                }
                if (json.primaryStar != null) {
                    returnObj.IsScoopable = json.primaryStar.isScoopable;
                }
                if(json.information != null) {
                    var info = json.information;
                    returnObj.ControllingFactionName = info.faction;
                    returnObj.ControllingFactionState = info.factionState;
                    returnObj.Population = (ulong?)info.population ?? 0;
                    returnObj.Security = GetSecurityFromEDSMString(info.security);
                    returnObj.Economy = GetEconomyFromEDSMString(info.economy);

                }
                wrapper.Data = returnObj;
                return wrapper;

            }
        }
    }

    #endregion

}
