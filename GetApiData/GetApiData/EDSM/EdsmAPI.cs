using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orcabot.Api.EDSM
{
    static public partial class EdsmAPI {

        public delegate void ErrorHandler(Exception e, string additionalInfo = "");
        public static event ErrorHandler ErrorEvent = delegate { };
        
        private static void PushError(Exception e, string info = "") {
            Errors.Add(new ErrorData {
                Exception = e,
                Info = info
            });
        }
        public class ErrorData
        {
            public Exception Exception { get; set; }
            public string Info { get; set; }
        }
        public static List<ErrorData> Errors { get; } = new List<ErrorData>();
        protected static class EdsmApiCaller<T> where T : Types.IApiResponse {

            public class ResponseWrapper {
                public T response;
                public Exception err;
                public bool HasError { get { return err != null; } }
                public System.Net.HttpStatusCode StatusCode { get; set; }
                public string StatusCodeMessage { get; set; }
            }

            public static async Task<ResponseWrapper> GetWebJSONAsync(string url) {
                try {
                    using (HttpClient httpClient = new HttpClient())
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url)) {
                        request.Version = new Version(1, 1);
                        using (HttpResponseMessage response = await httpClient.SendAsync(request)) {
                            var status = response.StatusCode;
                            var statusMsg = response.ReasonPhrase;
                            if (response.IsSuccessStatusCode) {
                                var jsonAsString = await response.Content.ReadAsStringAsync();
                                
                                var result = JsonConvert.DeserializeObject<T>(jsonAsString);
                                
                                return new ResponseWrapper {
                                    response = result,
                                    StatusCode = status,
                                    StatusCodeMessage = statusMsg
                                };
                            }
                            else {
                                return new ResponseWrapper {
                                    response = default,
                                    err = new Exception("HttpResponse didnt respond with a successful status code. Code is:" + response.StatusCode + " - " + response.ReasonPhrase),
                                    StatusCode = status,
                                    StatusCodeMessage = statusMsg
                                };
                            }
                        }
                    }
                }
                catch (Exception e) {

                    return new ResponseWrapper {
                        response = default,
                        err = e,
                   

                    };
                    throw e;

                }
            }
        }
    }
}
