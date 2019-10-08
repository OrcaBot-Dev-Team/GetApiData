using System;
using System.Collections.Generic;
using Orcabot.Helpers;
using Orcabot.Types.Enums;
using System.Linq;

namespace Orcabot.Api.Types.EDSM.StarSystem
{
    #region Stations
    public class StationsJSON : IApiResponse
    {
        public int? id;
        public string name;
        public StationsJSONEntry[] stations;

    }
    public class StationsJSONEntry
    {
        public int? id;
        public long? distanceToArrival;
        public string name, type, allegiance, government, economy;
        public bool haveMarket, haveShipyard;
        public string[] otherServices;
        public StationsJSONEntryFaction controllingFaction;

    }
    public class StationsJSONEntryFaction
    {
        public long? id;
        public string name;
    }
    namespace Helper
    {
        public static class StationsJSONHelper
        {
            public static EDSMResponse<Stations> Convert(this StationsJSON stations,int? htmlStatus, string htmlStatusMessage) {
                var stationsArray = ParseStations(stations.stations);
                return new EDSMResponse<Stations> {
                    Data = new Stations {
                        StationsArray = stationsArray
                    },
                    HTMLStatus = htmlStatus ?? -1,
                    HTMLStatusResponse = htmlStatusMessage,
                    Message = null, // Message and MessageNumber are not passed by this API endpoint. A+ for consistency, EDSM. /s
                    MessageNumber = 0

                };



                Orcabot.Types.Station[] ParseStations(StationsJSONEntry[] json) {
                    if (json == null) //If the json is null, return null. the JSON is null when the system does not exist
                        return null;
                    List<Orcabot.Types.Station> returnStations = new List<Orcabot.Types.Station>(json.Length);
                    foreach(var entry in json) {
                        Orcabot.Types.Station s = new Orcabot.Types.Station {
                            Distance = entry.distanceToArrival ?? -1,
                            Economy = Systems.Helper.StarSystemHelper.GetEconomyFromEDSMString(entry.economy),
                            Name = entry.name,
                            Type = GetStationType(entry.type),
                            StationFacilities = GetStationFacilities(entry.haveMarket, entry.haveShipyard, entry.otherServices, Systems.Helper.StarSystemHelper.GetEconomyFromEDSMString(entry.economy))
                        };
                        returnStations.Add(s);
                    }
                    return returnStations.ToArray();

                    StationType GetStationType(string type) {
                        if (string.IsNullOrWhiteSpace(type))
                            return StationType.Unknown;
                        switch (type.ToLower().Trim()) {
                            case "coriolis starport":
                                return StationType.Coriolis;
                            case "ocellus starport":
                                return StationType.Ocellus;
                            case "orbis starport":
                                return StationType.Orbis;
                            case "outpost":
                            case "civilian outpost":
                            case "commercial outpost":
                            case "industrial outpost":
                            case "military outpost":
                            case "mining outpost":
                            case "scientific outpost":
                                return StationType.Outpost;
                            case "planetary port":
                            case "planetary outpost":
                                return StationType.SurfaceStation;
                            case "asteroid base":
                                return StationType.AsteroidBase;
                            case "mega ship":
                            case "civilian mega ship":
                                return StationType.MegaShip;
                            default:
                                return StationType.Unknown;
                        }
                    }
                    
                    
                    ///<summary> Returns a list of Facilities. unfortunately the API call only returns market and shipyard.</summary>
                    List<StationFacility> GetStationFacilities(bool market, bool shipyard,string[] services,Economy eco) {
                        var list = new List<StationFacility>();
                        if (market)
                            list.Add(StationFacility.Market);
                        if (shipyard)
                            list.Add(StationFacility.Shipyard);
                        if(services == null)
                        return list;
                        foreach(var service in services) {
                            if (string.IsNullOrWhiteSpace(service))
                                continue;
                            list.Add(GetService(service));
                        }
                        return list.Distinct().ToList();

                        StationFacility GetService(string s) {
                            switch (s.Trim().ToLower()) {
                                case "commodities":
                                    return StationFacility.Market;
                                case "shipyard":
                                    return StationFacility.Shipyard;
                                case "outfitting":
                                    return StationFacility.Outfitting;
                                case "black market":
                                    return StationFacility.BlackMarket;
                                case "restock":
                                    return StationFacility.Restock;
                                case "refuel":
                                    return StationFacility.Refuel;
                                case "repair":
                                    return StationFacility.Repair;
                                case "contacts":
                                    return StationFacility.Contacts;
                                case "universal cartographics":
                                    return StationFacility.UniversalCartographics;
                                case "missions":
                                    return StationFacility.Missions;
                                case "crew lounge":
                                    return StationFacility.CrewLounge;
                                case "tuning":
                                    return StationFacility.RemoteEngineering;
                                case "interstellar factors contact":
                                    return StationFacility.InterstellarFactors;
                                case "search and rescue":
                                    return StationFacility.SearchAndResque;
                                case "technology broken":
                                    return StationFacility.TechnologyBroker;
                                case "material trader":
                                    return GetMaterialTraderType();
                                default:
                                    return StationFacility.Unknown;                           
                            }
                            ///<summary> This method expects there to be a mat trader. it only checks by economy.</summary>
                            StationFacility GetMaterialTraderType() {
                                switch (eco) {
                                    case Economy.Refinery:
                                    case Economy.Extraction:
                                        return StationFacility.TraderRaw;
                                    case Economy.Industrial:
                                        return StationFacility.TraderManufactured;
                                    case Economy.HighTech:
                                    case Economy.Military:
                                        return StationFacility.TraderEncoded;
                                    default:
                                        return StationFacility.Unknown;
                                }
                            }
                        }

                    }
                    
                }
            }
        }
    }
    public class Stations : IApiConverted
    {
        public Orcabot.Types.Station[] StationsArray { get; set; }
    }
    #endregion
    #region Traffic
    public class TrafficJSON : IApiResponse
    {
        public int? id;
        public string name, url;
        public TrafficJSONSummary traffic;
        public Dictionary<string, int> breakdown;

    }
    public class TrafficJSONSummary
    {
        public int total, week, day;
    }
    namespace Helper
    {
        public static class TrafficHelper
        {
            public static EDSMResponse<Traffic> Convert(this TrafficJSON traffic, int? htmlStatus, string htmlStatusMessage) {
                
                if (traffic == null || traffic.traffic == null)
                    return new EDSMResponse<Traffic> {
                        HTMLStatus = htmlStatus ?? -1,
                        HTMLStatusResponse = htmlStatusMessage,
                        Message = "The API responded with an empty Object. There is probably no such system.",
                        MessageNumber = 1
                    };

                var returnObj = new Traffic {
                    Day = traffic.traffic.day,
                    Week = traffic.traffic.week,
                    AllTime = traffic.traffic.total,
                    SystemName = traffic.name,
                    SystemURL = traffic.url,
                    Breakdown = traffic.breakdown
                };
                return new EDSMResponse<Traffic> {
                    HTMLStatus = htmlStatus ?? -1,
                    HTMLStatusResponse = htmlStatusMessage,
                    Data = returnObj,
                    Message = null,
                    MessageNumber = -1 // N/A on this endpoint
                };


            }
        }
    }
    public class Traffic : IApiConverted
    {
        public int Day { get; set; }
        public int Week { get; set; }
        public int AllTime { get; set; }

        public Dictionary<string,int> Breakdown { get; set; }
        public string SystemName { get; set; }
        public string SystemURL { get; set; }
    }
    #endregion
    #region Deaths
    public class DeathsJSON : IApiResponse
    {
        public int? id;
        public string name, url;
        public TrafficJSONSummary deaths;
    }
    namespace Helper
    {
        public static class DeathsHelper
        {
            public static EDSMResponse< Deaths >Convert(this DeathsJSON deaths,int? htmlStatus, string htmlStatusMessage) {
                if (deaths == null || deaths.deaths == null)
                    return new EDSMResponse<Deaths> {
                        HTMLStatus = htmlStatus ?? -1,
                        HTMLStatusResponse = htmlStatusMessage,
                        Message = "The API responded with an empty Object. There is probably no such system.",
                        MessageNumber = 1
                    };
                var returnObj = new Deaths {
                    SystemName = deaths.name,
                    SystemURL = deaths.url,

                    Day = deaths.deaths.day,
                    Week = deaths.deaths.week,
                    AllTime = deaths.deaths.total

                };
              
                return new EDSMResponse<Deaths> {
                    Data = returnObj,
                    HTMLStatus = htmlStatus ?? -1,
                    HTMLStatusResponse = htmlStatusMessage,
                    Message = null,
                    MessageNumber = -1 //Endpoint doesnt respond with a messagenumber
                };

            }
        }
    }
    public class Deaths : IApiConverted
    {
        public string SystemName { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public int AllTime { get; set; }
        public string SystemURL { get; set; }
    }
    #endregion
}
