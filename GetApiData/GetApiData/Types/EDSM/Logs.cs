using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;


namespace Orcabot.Api.Types.EDSM.Logs
{
    public class JSONPosition :IApiResponse
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
            public static EDSMResponse< Position >Convert(this JSONPosition position, int? HttpResponse, string HttpResponseMessage) {

                var wrapper = new EDSMResponse<Position> {
                    HTMLStatus = HttpResponse ?? -1,
                    HTMLStatusResponse = HttpResponseMessage
                };
                if (position == null) {
                    //  Workaround for a bug in the EDSM Api. Refer to https://github.com/EDSM-NET/FrontEnd/issues/322
                    wrapper.Data = null;
                    wrapper.MessageNumber = HttpResponse ?? 203;                                           
                    wrapper.Message = HttpResponseMessage ?? "Commander name not found. CMDR might not exist or has their profile set to private";    
                    return wrapper;
                }
                if(position.coordinates == null) {
                    //  Workaround for a bug in the EDSM Api. Refer to https://github.com/EDSM-NET/FrontEnd/issues/322
                    wrapper.MessageNumber = (position.msgnum == 201)? 201 : 203;                                       
                    wrapper.Message = (position.msgnum == 201) ? HttpResponseMessage : "Commander name not found. CMDR might not exist or has their profile set to private";                  

                    wrapper.Data = null;
                    return wrapper;
                }
                
                Position data = new Position {
                    Coordinates = position.coordinates,
                    Time = DateTime.ParseExact(position.date, "yyyy-MM-dd HH:mm:ss", null),
                    SystemName = position.system
                };
                wrapper.Message = position.msg;
                wrapper.MessageNumber = position.msgnum ?? -1;
                wrapper.Data = data;
                return wrapper;
            }
        }
    }
    public class Position : IApiConverted
    {
        public string SystemName { get; set; }
        public  JSONPositionCoordinate Coordinates { get; set; }
        public DateTime Time { get; set; }
    }
}
