using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderInBackend.Model;
using OrderInBackend.Service.Setup;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Setup
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupRekeningController : ControllerBase
    {
        private ISetupRekeningService _rekening;

        public SetupRekeningController()
        {
            this._rekening = new SetupRekeningService();
        }

        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data rekening ")]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._rekening.GetAllDataMasterRekeningByParams(param);
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

    }
}
