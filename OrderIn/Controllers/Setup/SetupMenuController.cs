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
    [JwtFilter]
    [ApiController]
    public class SetupMenuController : ControllerBase
    {
        private ISetupMenuService _menu;

        public SetupMenuController()
        {
            this._menu = new SetupMenuService();
        }


        #region Banner
        [HttpPost]
        public async Task<IActionResult> getAllBanner([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._menu.GetAllDataBannerMenuByParams(param);
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
        public async Task<IActionResult> insertUpdateBanner([FromBody] BannerMenu model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.bannermenuid == 0 ? await this._menu.AddBannerMenu(model) : await this._menu.UpdateBannerMenu(model);

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
                    throw ex;
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
        public async Task<IActionResult> deleteBanner([FromRoute] int id)
        {
            object result;
            try
            {
                result = await this._menu.DeleteBannerMenu(id);
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
        #endregion

        #region Kategori

        [HttpPost]
        public async Task<IActionResult> getAllKategori([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._menu.GetAllDataMasterCategoryMenuByParams(param);
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
        public async Task<IActionResult> insertUpdateKategori([FromBody] MasterCategoryMenu model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.categorymenuid == 0 ? await this._menu.AddMasterCategoryMenu(model) : await this._menu.UpdateMasterCategoryMenu(model);

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
        public async Task<IActionResult> deleteKategori([FromRoute] int id)
        {
            object result;
            try
            {
                result = await this._menu.DeleteMasterCategoryMenu(id);
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

        #endregion



        #region Promo

        [HttpPost]
        public async Task<IActionResult> getAllPromo([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._menu.GetAllDataPromoMenuByParams(param);
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
        public async Task<IActionResult> insertUpdatePromo([FromBody] PromoMenu model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.promomenuid == 0 ? await this._menu.AddPromoMenu(model) : await this._menu.UpdatePromoMenu(model);

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
        public async Task<IActionResult> deletePromo([FromRoute] int id)
        {
            object result;
            try
            {
                result = await this._menu.DeletePromoMenu(id);
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

        #endregion
    }
}
