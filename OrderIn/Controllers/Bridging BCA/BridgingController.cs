using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderIn.Helpers;
using OrderIn.Models.Bridging_BCA;
using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Bridging_BCA
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BridgingController : ControllerBase
    {
        //private string signatureID = "";

        //private string apiKey = "79c729d0-f508-463c-9d49-aa9cf3a24d12";
        //private string apiSecKey = "002d42a8-797d-4df0-9c04-280ea9268caf"; //api secret key
        //private string clientId = "fbeed5f6-0c65-42ec-b3a1-17dc0127de37";
        //private string clientSecKey = "f32e7373-4eb7-47b9-ae93-d2ab703c8fa7"; //client secret key
        //private string aksesToken = "";

        public BridgingBcaHelper _bridging;
        public HttpHelper _http;
        public ClassHelper _helper;

        public BridgingController(IHttpContextAccessor context)
        {
            this._bridging = new BridgingBcaHelper(context);
            this._helper = new ClassHelper(context);
            this._http = new HttpHelper();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAccessToken()
        {

            string token = await this._bridging.getAccessToken("fbeed5f6-0c65-42ec-b3a1-17dc0127de37", "f32e7373-4eb7-47b9-ae93-d2ab703c8fa7");

            return StatusCode(200, new
            {
                data = token
            });
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSignature([FromBody] SignatureParams model)
        {

            string signature = "";

            signature = await this._bridging.generateSignature(model);
            
            return StatusCode(200, new
            {
                data = signature
            });
        }


        [HttpPost]
        public async Task<IActionResult> TransferUang([FromBody] SignatureParams model)
        {
            object result = null;
            int status = 200;

            try
            {
                result = await this._bridging.TransferUang(model);
                var tipe = result.GetType();

                if (tipe.Name == "ErrorMessageResponse")
                {
                    status = 400;
                }

            }
            catch (Exception ex)
            {
                status = 501;
                result = ex.Message;
            }

            return StatusCode(status, result);
        }

    }
}
