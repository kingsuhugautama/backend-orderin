using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Service.Transaksi;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Transaksi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [JwtFilter]
    public class TransOpenPoController : ControllerBase
    {
        private ITransOpenPoService _transopenpo;

        public TransOpenPoController()
        {
            this._transopenpo = new TransOpenPoService();
        }

        #region TRANS OPEN PO HEADER

        [HttpPost]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._transopenpo.GetAllDataTransOpenPoHeaderByParams(param);
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
        public async Task<IActionResult> insert([FromBody] TransOpenPoHeader model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    //var result = model.openpoheaderid == 0 ? await this._transopenpo.AddTransOpenPoHeader(model) : await this._transopenpo.UpdateTransOpenPoHeader(model);
                    var result = await this._transopenpo.AddTransOpenPoHeader(model);

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


        #region TRANS OPEN PO DETAIL PRODUK

        [HttpPost]
        [SwaggerOperation(summary:"Mendapatkan detail produk transaksi open po (tambahkan userid => WAJIB, agar tidak duplikat data)",
            description: "prefix produk = mp,prefix transaksi detail = t, prefix user = fp (ini hanya untuk memfilter data favorit product untuk user siapa)")]
        public async Task<IActionResult> getAllDetailProduct([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._transopenpo.GetAllDataTransOpenPoDetailProductByParams(param);
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

        [SwaggerOperation(summary: "Mendapatkan po detail product berdasarkan kategori", description: "")]
        [HttpPost]
        public async Task<IActionResult> getAllDetailProductByCategoryMenuId(int categoryMenuId)
        {
            object result;

            List<ParameterSearchModel> param = new List<ParameterSearchModel>
            {
                new ParameterSearchModel
                {
                    columnName = "categorymenuid",
                    filter = "equal",
                    searchText = categoryMenuId.ToString(),
                    searchText2=""
                }
            };

            try
            {
                result = await this._transopenpo.GetAllDataTransOpenPoDetailProductByParams(param);
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

        #endregion


        #region TRANS OPEN PO DETAIL DROPSHIP

        [SwaggerOperation(summary: "Mendapatkan detail dropship transaksi open po", description: "prefix dropship = md, prefix merchant = mm,prefix transaksi po header = toph,prefix transaksi detail = t,prefix kategori = topddk")]
        [HttpPost]
        public async Task<IActionResult> getAllDetailDropship([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._transopenpo.GetAllDataTransOpenPoDetailDropshipByParams(param);
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

        [SwaggerOperation(summary: "Mendapatkan po detail dropship berdasarkan kategori", description: "")]
        [HttpPost]
        public async Task<IActionResult> getAllDetailDropshipByCategoryMenuId(int categoryMenuId)
        {
            object result;

            List<ParameterSearchModel> param = new List<ParameterSearchModel>
            {
                new ParameterSearchModel
                {
                    columnName = "categorymenuid",
                    filter = "equal",
                    searchText = categoryMenuId.ToString(),
                    searchText2=""
                }
            };

            try
            {
                result = await this._transopenpo.GetAllDataTransOpenPoDetailDropshipByParams(param);
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

        [SwaggerOperation(summary: "pengecekan apakah ada dropship yg masih aktif", 
            description: "")]
        [HttpPost]
        public async Task<IActionResult> getExistingDropship([FromBody] CekExistingDropshipParams model)
        {
            object result;

            try
            {
                result = await this._transopenpo.GetExistingDropshipByParams(model);
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
        #endregion

    }
}
