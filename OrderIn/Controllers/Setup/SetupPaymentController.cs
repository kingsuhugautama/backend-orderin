using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
using OrderInBackend.Model.Setup;
using OrderInBackend.Service.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Setup
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [JwtFilter]
    public class SetupPaymentController : ControllerBase
    {

        private ISetupPaymentService _payment;

        public SetupPaymentController()
        {
            this._payment = new SetupPaymentService();
        }


        [HttpPost]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._payment.GetAllDataMasterPaymentMethodByParams(param);
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
        public async Task<IActionResult> insertUpdate([FromBody] MasterPaymentMethod model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.paymentmethodid == 0 ? await this._payment.AddMasterPaymentMethod(model) : await this._payment.UpdateMasterPaymentMethod(model);

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
        public async Task<IActionResult> delete([FromRoute] int paymentmethodid)
        {
            object result;
            try
            {
                result = await this._payment.DeleteMasterPaymentMethod(paymentmethodid);
            }
            catch (Exception ex)
            {

                return StatusCode(200, new
                {
                    data = ex.Message
                });
            }

            return StatusCode(200, new
            {
                data = result
            });
        }
    }
}
