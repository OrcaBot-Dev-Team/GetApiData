using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orcabot.Api.EDSM
{
    static partial class EdsmAPI
    {

        public delegate void ErrorHandler(Exception e, string additionalInfo = "");
        public static event ErrorHandler ErrorEvent;
        protected static class EdsmApiCaller<T> where T : Types.IApiResponse {

            public class ResponseWrapper {
                public T response;
                public Exception err;
                public bool hasError { get { return err != null; } }
            }

            public static async Task<ResponseWrapper> GetWebJSONAsync(string url) {
                try {
                    using (HttpClient httpClient = new HttpClient())
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url)) {
                        request.Version = new Version(1, 1);
                        using (HttpResponseMessage response = await httpClient.SendAsync(request)) {
                            var status = response.StatusCode;
                            if (response.IsSuccessStatusCode) {
                                var jsonAsString = await response.Content.ReadAsStringAsync();

                                var result = JsonConvert.DeserializeObject<T>(jsonAsString);
                                return new ResponseWrapper {
                                    response = result
                                };
                            }
                            else {
                                return new ResponseWrapper {
                                    response = default,
                                    err = new Exception("HttpResponse didnt respond with a successful status code. Code is:" + response.StatusCode + " - " + response.ReasonPhrase),
                          
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

                }
            }
        }
    }
}
