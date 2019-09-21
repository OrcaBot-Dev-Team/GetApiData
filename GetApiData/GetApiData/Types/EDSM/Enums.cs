using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM.Enums
{

    public enum ApiStatusType
    {
        success,
        warning,
        danger
    }

    public static class Helper
    {
        public static ApiStatusType GetApiStatusType(string s) {
            
            switch (s.ToLower()) {
                case "success": return ApiStatusType.success;
                case "warning": return ApiStatusType.warning;
                case "danger" : return ApiStatusType.danger;
                default: throw new Exception("Given String cannot be converted to enum of type ApiStatusType");
            }
        }
    }

   
}
