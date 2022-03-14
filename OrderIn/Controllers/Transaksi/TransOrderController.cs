using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Helpers;
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
    public class TransOrderController : ControllerBase
    {
        private ITransOrderService _order;
        private ClassHelper _helper;

        public TransOrderController()
        {
            this._order = new TransOrderService();
            this._helper = new ClassHelper();
        }



        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data transorderheader",description: "prefix transorderheader = t," +
            "prefix merchant = mm")]
        public async Task<IActionResult> getAllDataOrderHeader([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataTransOrderHeaderByParams(param);
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
        public async Task<IActionResult> getAllDataOrderDetail([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataTransOrderDetailByParams(param);
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
        public async Task<IActionResult> insertTransOrderHeader([FromBody] TransOrderHeader model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.AddTransOrderHeader(model);

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
        [SwaggerOperation(summary: "untuk update status transaksi", description: "userclosed tidak wajib diisi, hanya diisi ketika status transaksi closed ( 5 )")]
        public async Task<IActionResult> updateStatusTransaksi([FromBody] TransOrderHeaderUpdateStatus model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.UpdateStatusTransaksi(model);

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

        #region TRANS ABSENSI DROPSHIP

        [HttpPost]
        public async Task<IActionResult> getAllDataAbsensi([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataTransAbsensiDropshipByParams(param);
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
        public async Task<IActionResult> insertAbsensiDropship([FromBody] TransAbsensiDropship model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.AddTransAbsensiDropship(model);

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


        #region trans pengiriman

        [HttpPost]
        public async Task<IActionResult> getAllDataPengiriman([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataTransPengirimanByParams(param);
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
        public async Task<IActionResult> updatePenerima([FromBody] TransPengirimanUpdatePenerima model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.UpdatePenerima(model);

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
        public async Task<IActionResult> insertPengiriman([FromBody] TransPengiriman model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.AddTransPengiriman(model);

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

        #region Punishment

        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data Punishment", description: "prefix punishment / merchant = p,prefix user = u")]
        public async Task<IActionResult> getAllDataPunishment([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._order.GetAllDataPunishmentByParams(param);
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
        [SwaggerOperation(summary: "untuk melakukan punishment terhadap user mblenjani")]
        public async Task<IActionResult> insertPunishment([FromBody] Punishment model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._order.AddPunishment(model);

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
        [SwaggerOperation(summary: "untuk membebaskan user mblenjani dari punishment")]
        public async Task<IActionResult> deletePunishment([FromBody] PunishmentRelease model)
        {
            object result;
            try
            {
                result = await this._order.DeletePunishment(model);
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
