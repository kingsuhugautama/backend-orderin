using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderIn.Helpers
{
    public class RequestHeaderData
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class HttpHelper
    {
        private HttpClient _http;

        public HttpHelper()
        {
            this._http = new HttpClient();
        }


        public async Task<HttpResponseMessage> PostData(string url, HttpContent content, List<RequestHeaderData> headers)
        {
            if(headers.Count > 0)
            {
                _http.DefaultRequestHeaders.Clear();

                foreach (var item in headers)
                {
                    this._http.DefaultRequestHeaders.Add(item.name, item.value);
                }
            }


            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            var response = await this._http.SendAsync(postRequest);

            return response;
        }


        //harus berupa object datanya
        public async Task<HttpResponseMessage> PostJsonData(string url, object content, List<RequestHeaderData> headers)
        {
            if (headers.Count > 0)
            {
                _http.DefaultRequestHeaders.Clear();

                foreach (var item in headers)
                {
                    this._http.DefaultRequestHeaders.Add(item.name, item.value);
                }
            }

            var response = await this._http.PostAsJsonAsync(url, content);

            return response;
        }
    }
}
