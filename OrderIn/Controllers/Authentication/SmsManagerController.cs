using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderIn.Services.Twilio;
using OrderInBackend.Helpers;
using OrderInBackend.Service.Setup;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Authentication
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsManagerController : ControllerBase
    {

        public string PhoneNumber = "";
        private ClassHelper _helper;

        private ISetupUsersService _users;
        private readonly IVerification _verification;

        public SmsManagerController(IHttpContextAccessor context, IVerification verification)
        {
            this._users = new SetupUserservice();
            this._helper = new ClassHelper(context);
            _verification = verification;//new Verification(new ConfigurationTwilio());
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        //private async Task LoadPhoneNumber()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new Exception($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    PhoneNumber = user.PhoneNumber;
        //}

        [HttpPost]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(summary: "untuk mengirim kode verifikasi", description: "")]
        public async Task<IActionResult> KirimVerifikasi([FromBody] string phone = "+6285799959796")
        {

            //var user = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                var verification = await this._verification.StartVerificationAsync(phone, "sms");


                return StatusCode(200, new
                {
                    data = verification
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message
                });
            }
        }





        [HttpPost]
        //[ApiExplorerSettings(IgnoreApi =true)]
        [SwaggerOperation(summary: "untuk memverifikasi kode verifikasi", description: "")]
        public async Task<IActionResult> CekVerifikasi([FromBody] VerificationParam param)
        {

            string message = "";
            int code = 200;
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                var verification = await this._verification.CheckVerificationAsync(param.PhoneNumber, param.VerificationCode);

                if (!String.IsNullOrEmpty(verification.Sid) && verification.IsValid)
                {
                    var verifUser = await this._users.VerifyUsers(param.userid);

                    if (verifUser.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    message = verifUser.ToString();
                }
                else
                {
                    code = 401;
                    message = "Kode verifikasi anda tidak sesuai / salah";
                }

                return StatusCode(code, new
                {
                    data = message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message
                });
            }
        }

    }
}
