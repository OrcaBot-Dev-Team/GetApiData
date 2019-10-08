using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types
{
    /// <summary>
    /// This is the data object excluding response data (like HTTP codes)
    /// </summary>
    public interface IApiConverted
    {
 
    }
    /// <summary>
    /// This is the model that the json data will be serialized to.
    /// </summary>
    public interface IApiResponse
    {


    }
    /// <summary>
    /// This will take the given Generic, and will return an array of the given type
    /// </summary>
    public interface IApiResonseAsArray
    {

    }

    public class EDSMResponse<T> where T:IApiConverted
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int MessageNumber { get; set; }
        public int HTMLStatus { get; set; }
        public string HTMLStatusResponse { get; set; }
    }
}
