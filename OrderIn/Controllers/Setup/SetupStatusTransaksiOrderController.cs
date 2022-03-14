using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Service.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Setup
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [JwtFilter]
    public class SetupStatusTransaksiOrderController : ControllerBase
    {

        private ITransOrderService _order;

        public SetupStatusTransaksiOrderController()
        {
            this._order = new TransOrderService();
        }


        [HttpPost]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataMasterStatusTransaksiOrderByParams(param);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message
                });
            }

            return StatusCode(200, new
            {
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> insertUpdate([FromBody] MasterStatusTransaksiOrder model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.statustransorderid == 0 ? await this._order.AddMasterStatusTransaksiOrder(model) : await this._order.UpdateMasterStatusTransaksiOrder(model);

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
        public async Task<IActionResult> delete([FromRoute] int statustransorderid)
        {
            object result;
            try
            {
                result = await this._order.DeleteMasterStatusTransaksiOrder(statustransorderid);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
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
