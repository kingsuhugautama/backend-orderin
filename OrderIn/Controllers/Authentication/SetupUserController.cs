using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderIn.Filters;
using OrderIn.Models.Utility;
using OrderInBackend.Helpers;
using OrderInBackend.Model;
using OrderInBackend.Model.Setup;
using OrderInBackend.Service.Setup;
using PhoneNumbers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Authentication
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupUserController : ControllerBase
    {
        private ClassHelper _helper;
        private CryptoHelper _crypto;
        private ISetupUsersService _user;

        public SetupUserController()
        {
            this._user = new SetupUserservice();
            this._helper = new ClassHelper();
            this._crypto = new CryptoHelper();
        }

        [HttpPost]
        [SwaggerOperation(summary: "Untuk get data user ", description: "prefix user = u, prefix merchant = mm")]
        [JwtFilter]
        public async Task<IActionResult> getAllUser([FromBody] List<ParameterSearchModel> param)
        {
            var result = await this._user.GetAllDataUsersByParams(param);

            return StatusCode(200, new
            {
                data = result
            });
        }


        [HttpGet]
        [SwaggerOperation(summary: "Untuk get data user with limit", description: "prefix user = u, prefix merchant = mm")]
        [JwtFilter]
        public async Task<IActionResult> getAllUserWithLimit([FromQuery(Name = "page")]int page)
        {
            var param = new ParameterSearchWithLimit
            {
                paramSearch = new List<ParameterSearchModel>(),
                page = page,
                limit = Convert.ToInt32(Properties.Resources.limit_data_count)
            };

            var result = await this._user.GetAllDataUsersByParamsWithLimit(param);

            return StatusCode(200, new
            {
                data = result
            });
        }


        [HttpDelete("{id}")]
        [JwtFilter]
        public async Task<IActionResult> deleteUser([FromRoute] int id)
        {
            var result = await this._user.DeleteUsers(id);

            return StatusCode(200, new
            {
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> insertUpdateUser([FromBody] Users model)
        {
            string pesan = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    model.phone = this._helper.convertPhone(model.phone);
                    model.password = this._helper.encryptSHA1(model.password, Properties.Resources.sec_key_userlogin);

                    var result = model.userid == 0 ? await this._user.AddUsers(model) : await this._user.UpdateUsers(model);

                    if (result.ToString().ToLower().StartsWith("success"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    pesan = result.ToString();

                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }
            }

            return StatusCode(code, new
            {
                data = pesan
            });
        }

        [HttpPost]
        [JwtFilter]
        public async Task<IActionResult> updateBiometric([FromBody] UserUpdateBiometric model)
        {
            string pesan = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._user.UpdateBiometric(model);

                    if (result.ToString().ToLower().StartsWith("success"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    pesan = result.ToString();

                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }
            }

            return StatusCode(code, new
            {
                data = pesan
            });
        }

        [HttpPost]
        [SwaggerOperation(summary: "Untuk testing konversi nomer hp")]
        public IActionResult ConvertPhone([FromBody] string phone)
        {
            string newPhone = this._helper.convertPhone(phone);
            
            return StatusCode(200,new
            {
                data = newPhone
            });
        }

        [HttpPost]
        [JwtFilter]
        public async Task<IActionResult> updatePunishment([FromBody] int userid)
        {
            string pesan = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._user.UpdatePunishment(userid);

                    if (result.ToString().ToLower().StartsWith("success"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    pesan = result.ToString();

                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }
            }

            return StatusCode(code, new
            {
                data = pesan
            });
        }

        //[HttpPost]
        //[SwaggerOperation(summary: "Untuk verifikasi user ", description: "")]
        //public async Task<IActionResult> VerifikasiUser([FromBody] int userid)
        //{
        //    string message = "";
        //    int code = 200;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var result = await this._user.VerifyUsers(userid);

        //            if (result.ToString().StartsWith("SUCCESS"))
        //            {
        //                code = 200;
        //            }
        //            else
        //            {
        //                code = 501;
        //            }

        //            message = result.ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            message = ex.Message;
        //            code = 501;
        //        }
        //    }
        //    else
        //    {
        //        code = 400;
        //        message = "Format data salah!";
        //    }

        //    return StatusCode(code, new
        //    {
        //        data = message
        //    });
        //}


        [HttpPost]
        [SwaggerOperation(summary: "Untuk login web (menggunakan session) ")]
        public async Task<IActionResult> signInMerchant([FromBody] UserLogin user)
        {
            int code = 200;
            string pesan = "";
            ViewUsers data = null;

            if (ModelState.IsValid)
            {
                try
                {
                    if (user != null)
                    {
                        //user.password = this._helper.encryptSHA1(user.password, Properties.Resources.sec_key_userlogin);

                        var result = await this._user.GetAllDataUsersByPhoneAndPassword(user);

                        if (result != null)
                        {
                            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(result));

                            var login = await this._user.UpdateStatusLogin(new UserUpdateStatus
                            {
                                userid = result.userid,
                                status = "login",
                            });

                            if ((Int32)login > 0)
                            {
                                //simpan session
                                //HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(result[0]));

                                string token = _helper.GenerateToken(result.userid.ToString());
                                pesan = token;
                                data = result;
                                data.password = "****************";
                            }
                            else
                            {
                                pesan = "Terjadi kesalahan saat login";
                            }
                        }
                        else
                        {
                            code = 401;
                            pesan = "Periksa username dan password anda!";
                        }

                    }

                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }
            }

            return StatusCode(code, new
            {
                message = pesan,
                data = data
            });
        }


        [HttpPost]
        public async Task<IActionResult> signIn([FromBody] UserLogin user)
        {
            int code = 200;
            string pesan = "";
            ViewUsers data = null;

            if (ModelState.IsValid)
            {
                try
                {
                    if (user != null)
                    {

                        user.phone = this._helper.convertPhone(user.phone);
                        var result = await this._user.GetAllDataUsersByPhoneAndPassword(user);

                        if (result != null)
                        {
                            var login = await this._user.UpdateStatusLogin(new UserUpdateStatus
                            {
                                userid = result.userid,
                                status = "login",
                            });

                            if ((Int32)login > 0)
                            {
                                //simpan session
                                //HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(result[0]));

                                string token = _helper.GenerateToken(result.userid.ToString());
                                pesan = token;
                                data = result;
                                data.password = "****************";
                            }
                            else
                            {
                                pesan = "Terjadi kesalahan saat login";
                            }
                        }
                        else
                        {
                            code = 401;
                            pesan = "Periksa username dan password anda!";
                        }

                    }

                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }
            }

            return StatusCode(code, new
            {
                message = pesan,
                data = data
            });
        }

        [HttpPost]
        [JwtFilter]
        public async Task<IActionResult> signOut([FromBody] int userid)
        {
            int code = 200;
            string pesan = "";
            object data = null;

            if (ModelState.IsValid)
            {
                try
                {
                    var login = await this._user.UpdateStatusLogin(new UserUpdateStatus
                    {
                        userid = userid,
                        status = "logout",
                    });

                    if ((Int32)login > 0)
                    {
                        HttpContext.Session.Remove("UserData");
                        pesan = "Anda telah keluar";
                    }
                    else
                    {
                        pesan = "Terjadi kendala saat logout";
                    }
                }
                catch (Exception ex)
                {
                    pesan = ex.Message;
                    code = 501;
                }

                data = userid;
            }

            return StatusCode(code, new
            {
                message = pesan,
                data = data
            });
        }

        [HttpPost]
        //[SwaggerOperation(summary: "Untuk web cek apakah ada session yg aktif ")]
        public IActionResult CheckingSessionActive([FromBody] SessionCek model)
        {
            string session = "";
            string sessionID = this._crypto.OpenSSLDecrypt(model.Prefix, Properties.Resources.sec_key_session);
            session = HttpContext.Session.GetString("UserData");

            return StatusCode(200, new
            {
                data = string.IsNullOrEmpty(session) ? false : true
            });
        }


        //[HttpPost]
        //[IgnoreAntiforgeryToken]
        //public IActionResult RequestToken([FromBody] int userid)
        //{
        //    string token = _helper.GenerateToken(userid);

        //    return StatusCode(200, new
        //    {
        //        result = token
        //    });
        //}


        //[HttpPost]
        //public IActionResult ValidasiToken([FromBody] string token)
        //{
        //    int? result = _helper.ValidateToken(token);


        //    return StatusCode(200, new
        //    {
        //        data = result == null ? "error" : "success"
        //    });
        //}
    }
}
