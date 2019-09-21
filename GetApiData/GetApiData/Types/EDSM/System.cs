using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM.System_
{
    #region Stations
    public class StationsJSON
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
            public static Stations Convert(this StationsJSON stations) {
                throw new NotImplementedException(); //TODO;
            }
        }
    }
    public class Stations
    {
        //TODO;
    }
    #endregion
    #region Traffic
    public class TrafficJSON
    {
        public int? id, id64;
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
            public static Traffic Convert(this TrafficJSON traffic) {
                throw new NotImplementedException();
            }
        }
    }
    public class Traffic
    {
        //TODO;
    }
    #endregion
    #region Deaths
    public class DeathsJSON
    {
        public int? id, id64;
        public string name, url;
        public TrafficJSONSummary deaths;
    }
    namespace Helper
    {
        public static class DeathsHelper
        {
            public static Deaths Convert(this DeathsJSON deaths) {
                throw new NotImplementedException();
            }
        }
    }
    public class Deaths
    {
        //TODO
    }
    #endregion
}
