using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderIn.Middlewares
{
    public static class RequestDecriptorExtensions
    {
        public static IApplicationBuilder UseHttpRequestDecriptor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestDecriptor>();
        }
    }

    public class RequestDecriptor
    {
        private readonly RequestDelegate _next;
        private CryptoHelper _crypto;
        private readonly string secretKey = "mat_http_request"; //"mat_http_request"

        public RequestDecriptor(RequestDelegate next)
        {
            _next = next;
            _crypto = new CryptoHelper();

        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            //get the request body and put it back for the downstream items to read
            var stream = request.Body;// currently holds the original stream                    
            var originalContent = await new StreamReader(stream).ReadToEndAsync();
            originalContent = originalContent.Replace("\"", "");

            var notModified = true;
            try
            {
                if (originalContent != "[]" && !string.IsNullOrEmpty(originalContent))
                {
                    var dataSource = this._crypto.OpenSSLDecrypt(originalContent, secretKey);
                    if (dataSource != null)
                    {
                        //modified stream
                        //var modifydata = new TestVM() { Name = "vbb", Hobby = "sss" };
                        //var json = JsonConvert.SerializeObject(modifydata);
                        var requestData = Encoding.UTF8.GetBytes(dataSource);
                        stream = new MemoryStream(requestData);
                        notModified = false;
                    }
                }
            }
            catch
            {
                //log error
            }

            if (notModified)
            {
                //put original data back for the downstream to read
                var requestData = Encoding.UTF8.GetBytes(originalContent);
                stream = new MemoryStream(requestData);
            }

            request.Body = stream;
            await _next.Invoke(context);
        }

    }
}
