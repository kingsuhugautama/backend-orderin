using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
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
    public class SetupDaerahController : ControllerBase
    {
        private ISetupDaerahService _daerah;

        public SetupDaerahController()
        {
            this._daerah = new SetupDaerahService();
        }

        [HttpPost]
        public async Task<IActionResult> getAllKota([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._daerah.GetAllDataKotaByParams(param);
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
        public async Task<IActionResult> getAllProvinsi([FromBody] List<ParameterSearchModel> param)
        {

            object result;

            try
            {
                result = await this._daerah.GetAllDataProvinsiByParams(param);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    data = ex.Message.IndexOf("ambiguous") > -1 ? ex.Message.Replace("column reference ", "Nama Kolom ").Replace("is ambiguous", "ambigu") : ex.Message
                });
            }

            return StatusCode(500, new
            {
                data = result
            });
        }
    }
}
