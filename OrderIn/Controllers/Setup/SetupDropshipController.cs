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
    public class SetupDropshipController : ControllerBase
    {

        private ISetupDropshipService _dropship;

        public SetupDropshipController()
        {
            this._dropship = new SetupDropshipService();
        }


        [HttpPost]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._dropship.GetAllDataMasterDropshipByParams(param);
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
        public async Task<IActionResult> insertUpdate([FromBody] MasterDropship model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.dropshipid == 0 ? await this._dropship.AddMasterDropship(model) : await this._dropship.UpdateMasterDropship(model);

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
        public async Task<IActionResult> updateStatusActive([FromBody] UpdateDropshipStatusActive model)
        {
            int code = 200;
            string pesan = "";

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._dropship.UpdateStatusActive(model);

                    if (result.ToString().StartsWith("SUCCESS"))
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
                    code = 501;
                    pesan = ex.Message;

                    throw ex;
                }

            }
            else
            {
                code = 400;
                pesan = "Format data salah !";
            }

            return StatusCode(code, new
            {
                data = pesan
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {

            object result;
            try
            {
                result = await this._dropship.DeleteMasterDropship(id);
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
    }
}
