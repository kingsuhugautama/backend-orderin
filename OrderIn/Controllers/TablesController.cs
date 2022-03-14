using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderInBackend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private ITableService _table;

        public TablesController()
        {
            this._table = new TableService();
        }

        [HttpGet]
        public async Task<IActionResult> getTable()
        {
            var result = await this._table.GetAllTables();

            return StatusCode(200, result);
        }
    }
}
