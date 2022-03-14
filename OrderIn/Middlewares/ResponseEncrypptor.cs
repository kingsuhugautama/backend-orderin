using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrderIn.Helpers;
using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OrderIn.Middlewares
{
    public static class ResponseEncryptorExtensions
    {
        public static IApplicationBuilder UseHttpResponseEncryptor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseEncrypptor>();
        }
    }

    public class ResponseEncrypptor
    {
        private readonly RequestDelegate _next;
        private CryptoHelper _crypto;
        private readonly string secretKey = "mat_http_request" ; //"mat_http_request"

        public ResponseEncrypptor(RequestDelegate next)
        {
            _next = next;
            _crypto = new CryptoHelper();
        }

        private async Task<string> responseEncryptor(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            var content = await new StreamReader(response.Body).ReadToEndAsync();

            content = content != "" ? this._crypto.OpenSSLEncrypt(content, secretKey) : "";

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return content;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            var originalBodyStream = response.Body;

            try
            {

                using (var newResponse = new MemoryStream())
                {
                    context.Response.Body = newResponse;
                    await _next(context);

                    //newResponse.Position = 0;
                    string responseBody = await responseEncryptor(response);

                    //newResponse.Position = 0;
                    object result = new
                    {
                        result = responseBody
                    };

                    var responseData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                    var stream = new MemoryStream(responseData);
                    await stream.CopyToAsync(originalBodyStream);
                }

            }
            catch(Exception ex)
            {
                throw ex;
                //log error
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

    }
}
