using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM.Logs
{
    public class JSONPosition
    {
        public int? msgnum;
        public long? systemId;
        public string msg, system, date, url;
        public bool? firstDiscover;
        public JSONPositionCoordinate coordinates;

    }
    public class JSONPositionCoordinate
    {
        public float x, y, z;
    }
    namespace Helper
    {
        public static class JSONPositionHelper
        {
            public static Position Convert(this JSONPosition position) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class Position
    {
        //TODO;
    }
}
