using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM.Systems
{

    #region Sphere/Cube
    /// <summary>
    /// This class is for the sphere and cube api. They both return a response with the same structure
    /// </summary>
    public class InProximityJSON : IApiResponse
    {
        public float? distance;
        public int? bodyCount;
        public string name;
        public ulong id;
        public SystemJSONCoordinates coords;
        public bool requirePermit;
        public string permitName;

    }
    
    #endregion
    #region System
    //example: https://www.edsm.net/api-v1/system?systemName=Alioth&showInformation=1&showId=1&showCoordinates=1&showPrimaryStar=1
    public class SystemJSON : IApiResponse
    {
        public string name;
        public long? id, id64;
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
        public long population;
    }
    public class SystemJSONPrimaryStar
    {
        public string type, name;
        public bool isScoopable;
    }
    
    #endregion

}
