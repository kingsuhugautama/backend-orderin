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
    public class SetupShipmentController : ControllerBase
    {
        private ISetupShipmentService _shipment;

        public SetupShipmentController()
        {
            this._shipment = new SetupShipmentService();
        }

        [HttpPost]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._shipment.GetAllDataMasterShipmentByParams(param);
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
        public async Task<IActionResult> insertUpdate([FromBody] MasterShipment model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.shipmentid == 0 ? await this._shipment.AddMasterShipment(model) : await this._shipment.UpdateMasterShipment(model);

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
                result = await this._shipment.DeleteMasterShipment(id);
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
