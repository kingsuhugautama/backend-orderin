using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderIn.Filters;
using OrderInBackend.Helpers;
using OrderInBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderIn.Controllers.MidTransHub
{
    [Route("api/[controller]/[action]")]
    [JwtFilter]
    [ApiController]
    public class MidTransController : ControllerBase
    {

        private ClassHelper _helper;
        private string serverKey = "SB-Mid-server-lDUhGDctnNm-XKzqxwuWesfW:";

        private HttpClient http;

        public MidTransController()
        {
            this.http = new HttpClient();
            this._helper = new ClassHelper();
        }

        [HttpPost]
        public async Task<IActionResult> charge([FromBody] Root data)
        {
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Add("Authorization", "Basic " + this._helper.convertStringToBase64(serverKey));
            http.DefaultRequestHeaders.Add("Accept", "application/json");

            var jsonData = new StringContent(
                JsonConvert.SerializeObject(data),
                Encoding.UTF8, "application/json"
                );

            var result = await this.http.PostAsync("https://app.sandbox.midtrans.com/snap/v1/transactions", jsonData);


            return StatusCode(200, JsonConvert.DeserializeObject<MidTransAuthorizationResponse>(result.Content.ReadAsStringAsync().Result));
        }


        [HttpPost]
        public async Task<IActionResult> notif([FromBody] Notification data)
        {
            var jsonData = new StringContent(
                JsonConvert.SerializeObject(data),
                Encoding.UTF8, "application/json"
                );

            var result = await this.http.PostAsync("https://open-po-prototype.firebaseio.com/midtrans/child.json", jsonData);


            return StatusCode(200, result.Content.ReadAsStringAsync().Result);
        }
    }
}
