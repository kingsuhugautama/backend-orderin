using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class TransPembayaranController : ControllerBase
    {
        private ITransPembayaranService _bayar;

        public TransPembayaranController()
        {
            this._bayar = new TransPembayaranService();
        }

        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data TransPembayaran", description: "")]
        public async Task<IActionResult> getAllData([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._bayar.GetAllDataTransPembayaranByParams(param);
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
        public async Task<IActionResult> insertPembayaran([FromBody] TransPembayaranInsertPembayaran model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._bayar.AddTransPembayaran(model);

                    //.ToString().StartsWith("SUCCESS")
                    if (result.status.StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }


                    return StatusCode(code, new
                    {
                        data = result
                    });

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
        public async Task<IActionResult> updatePembayaran([FromBody] TransPembayaranUpdatePembayaran model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._bayar.UpdateTransPembayaran(model);

                    if (result.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }


                    return StatusCode(code, new
                    {
                        data = result
                    });

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
        [SwaggerOperation(summary: "untuk merubah status pembayaran", description: "")]
        public async Task<IActionResult> updateStatusPembayaran([FromBody] TransPembayaranVerifikasi model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this._bayar.UpdateStatusPembayaran(model);

                    if (result.ToString().StartsWith("SUCCESS"))
                    {
                        code = 200;
                    }
                    else
                    {
                        code = 501;
                    }


                    return StatusCode(code, new
                    {
                        data = result
                    });

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
                result = await this._bayar.DeleteTransPembayaran(id);
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



        #region Status Pembayaran

        [HttpPost]
        [SwaggerOperation(summary: "untuk mendapatkan data Status Pembayaran", description: "")]
        public async Task<IActionResult> getAllDataStatusPembayaran([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._bayar.GetAllDataMasterStatusPembayaranByParams(param);
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
        public async Task<IActionResult> insertUpdateStatusPembayaran([FromBody] MasterStatusPembayaran model)
        {
            string message = "";
            int code = 200;

            if (ModelState.IsValid)
            {
                try
                {
                    var result = model.statuspembayaranid == 0 ? await this._bayar.AddMasterStatusPembayaran(model) : await this._bayar.UpdateMasterStatusPembayaran(model);

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
        [SwaggerOperation(summary: "untuk menghapus Pembayaran by orderheaderid", description: "hapus by orderheaderid")]
        public async Task<IActionResult> deleteStatusPembayaran([FromRoute] int id)
        {
            object result;
            try
            {
                result = await this._bayar.DeleteMasterStatusPembayaran(id);
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
