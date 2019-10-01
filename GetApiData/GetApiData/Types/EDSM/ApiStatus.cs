using Orcabot.Api.Types.EDSM.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace Orcabot.Api.Types.EDSM.ApiStatus
{
    public class JSON : IApiResponse
    {
        public string lastUpdate, type, message;
        public int status;
    }
    namespace Helper
    {
       public static class ApiStatusHelper
        {
            public static ApiStatus Convert(this JSON apiStatusJSON) {
                return new ApiStatus {
                    LastUpdate = DateTime.ParseExact(apiStatusJSON.lastUpdate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Type = Enums.Helper.GetApiStatusType(apiStatusJSON.type),
                    Message = apiStatusJSON.message,
                    Status = apiStatusJSON.status
                };

            }
            
        }
    }
    public class ApiStatus
    {
        public DateTime LastUpdate;
        public ApiStatusType Type;
        public string Message;
        public int Status;
    }
    
    
}
