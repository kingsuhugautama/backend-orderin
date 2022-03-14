using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
using OrderInBackend.Model.Setup;
using OrderInBackend.Service.Setup;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Setup
{
    [Route("api/[controller]/[action]")]
    [JwtFilter]
    [ApiController]
    public class SetupMerchantController : ControllerBase
    {
        private ISetupMerchantService _merchant;
        private ISetupRatingService _rating;

        public SetupMerchantController()
        {
            this._merchant = new SetupMerchantService();
            this._rating = new SetupRatingService();
        }
        [HttpPost]
        [SwaggerOperation(summary: "Untuk get data merchant yang not verified ", description: "prefix user = u, prefix merchant = m")]
        public async Task<IActionResult> getAllDataNotVerified([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                param.Add(new ParameterSearchModel
                {
                    columnName = "u.isverifiedmerchant",
                    filter = "equal",
                    searchText = "false",
                    searchText2 = ""
                });

                result = await this._merchant.GetAllDataMasterMerchantByParams(param);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message.IndexOf("ambiguous") > -1 ? ex.Message.Replace("column reference ", "Nama Kolom ").Replace("is ambiguous", "ambigu") : ex.Message
                });
            }

            return StatusCode(200, new
            {
                data = result
            });
        }

        [HttpPost]
        [SwaggerOperation(summary: "Untuk get data merchant ", description: "prefix user = u, prefix merchant = m")]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                param.Add(new ParameterSearchModel
                {
                    columnName = "u.isverifiedmerchant",
                    filter = "equal",
                    searchText = "true",
                    searchText2 = ""
                });

                result = await this._merchant.GetAllDataMasterMerchantByParams(param);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message.IndexOf("ambiguous") > -1 ? ex.Message.Replace("column reference ", "Nama Kolom ").Replace("is ambiguous", "ambigu") : ex.Message
                });
            }

            return StatusCode(200, new
            {
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> insertUpdate([FromBody] MasterMerchant model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.merchantid == 0 ? await this._merchant.AddMasterMerchant(model) : await this._merchant.UpdateMasterMerchant(model);

                    if (result.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    message = result.ToString();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    code = 501;
                }
            }
            else
            {
                code = 400;
                message = "Format data salah!";
            }

            return StatusCode(code, new
            {
                data = message
            });
        }

        [HttpPost]
        [SwaggerOperation(summary: "Untuk verifikasi merchant ", description: "")]
        public async Task<IActionResult> VerifikasiMerchant([FromBody] MasterMerchant model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._merchant.VerifyMerchant(model); 

                    if (result.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    message = result.ToString();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    code = 501;
                }
            }
            else
            {
                code = 400;
                message = "Format data salah!";
            }

            return StatusCode(code, new
            {
                data = message
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {
            object result;
            try
            {
                result = await this._merchant.DeleteMasterMerchant(id);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    data = ex.Message.IndexOf("constraint") > -1 ? "Data ini sudah terpakai dan tidak dapat dihapus" : ex.Message
                });
            }

            return StatusCode(200, new
            {
                data = result
            });
        }


        #region rating


        [HttpPost]
        public async Task<IActionResult> insertRating([FromBody] MasterRating model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result =  await this._rating.AddRating(model) ;

                    if (result.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }

                    message = result.ToString();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    code = 501;
                }
            }
            else
            {
                code = 400;
                message = "Format data salah!";
            }

            return StatusCode(code, new
            {
                data = message
            });
        }

        #endregion
    }
}
