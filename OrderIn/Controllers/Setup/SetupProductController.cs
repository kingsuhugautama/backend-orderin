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
    public class SetupProductController : ControllerBase
    {
        private ISetupProductService _product;

        public SetupProductController()
        {
            this._product = new SetupProductService();
        }


        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data master produk", description: "prefix produk = m")]
        public async Task<IActionResult> getAllDataForMerchant([FromBody] List<ParameterSearchModel> param)
        {
            object result = String.Empty;

            try
            {
                result = await this._product.GetAllDataMasterProductForMarketByParams(param);

            }catch(Exception ex)
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
        [SwaggerOperation(summary: "untuk mendapatkan data produk dan favorit itu favorit user atau tidak", description: "prefix produk = m,prefix user = fp")]
        public async Task<IActionResult> getAllDataForUser([FromBody] List<ParameterSearchModel> param)
        {
            object result = String.Empty;

            try
            {
                result = await this._product.GetAllDataMasterProductForUserByParams(param);

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
        public async Task<IActionResult> insertUpdate([FromBody] MasterProduct model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.productid == 0 ? await this._product.AddMasterProduct(model) : await this._product.UpdateMasterProduct(model);

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
        public async Task<IActionResult> updateStatusActive([FromBody] UpdateProductStatusActive model)
        {
            int code = 200;
            string pesan = "";

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._product.UpdateStatusActive(model);

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
                result = await this._product.DeleteMasterProduct(id);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    data = ex.Message.IndexOf("foreign key constraint") > -1 ? "Produk ini sudah terpakai untuk transaksi" : ex.Message
                }); ;
            }

            return StatusCode(200, new
            {
                data = result
            });
        }



        [HttpPost]
        public async Task<IActionResult> getAllFavoritProduct([FromBody] List<ParameterSearchModel> param)
        {
            object result = String.Empty;

            try
            {
                result = await this._product.GetAllDataFavoritProductByParams(param);

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
        public async Task<IActionResult> addFavorit([FromBody] FavoritProduct model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._product.AddFavoritProduct(model);

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
        public async Task<IActionResult> deleteFavorit([FromBody] FavoritProduct data)
        {
            object result = String.Empty;

            try
            {
                result = await this._product.DeleteFavoritProduct(data);
            }catch(Exception ex)
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
