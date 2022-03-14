using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderIn.Filters;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Service.Setup;
using OrderInBackend.Service.Transaksi;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Controllers.Grafik
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [JwtFilter]
    public class GrafikController : ControllerBase
    {
        private ITransOrderService _trans;
        private ISetupProductService _product;

        public GrafikController()
        {
            this._trans = new TransOrderService();
            this._product = new SetupProductService();
        }


        #region OMSET
        [SwaggerOperation(summary: "untuk mendapatkan data omset per dropship",
            description: "prefix transaksi = t,prefix trans open po = toph,prefix merchant = mm,prefix dropship = md")]
        [HttpPost]
        //[Route("/api/grafik/omset/getDataDropship")]
        public async Task<IActionResult> getOmsetPerDropship([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOmsetPerDropshipByParams(param);
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


        [SwaggerOperation(summary: "untuk mendapatkan data omset per product dari dropship",
            description: "prefix merchant = mm,prefix dropship = md,prefix product = mp,prefix trans order = t," +
            "prefix trans po = toph")]
        [HttpPost]
        //[Route("/api/grafik/omset/getDataDropshipProduct")]
        public async Task<IActionResult> getOmsetPerDropshipProduct([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOmsetPerDropshipProductByParams(param);
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


        [SwaggerOperation(summary: "untuk mendapatkan data omset per produk", description: "prefix merchant = mm,prefix produk = mp,filter tanggal = waktuentry")]
        [HttpPost]
        //[Route("/api/grafik/omset/getDataProduct")]
        public async Task<IActionResult> getOmsetPerProduct([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOmsetPerProductByParams(param);
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


        [SwaggerOperation(summary: "untuk mendapatkan data omset per kategori", description: "prefix merchant = mm,prefix kategori = mcm")]
        [HttpPost]
        //[Route("/api/grafik/produk/getDataCategory")]
        public async Task<IActionResult> getOmsetPerCategory([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOmsetPerCategoryByParams(param);
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


        [SwaggerOperation(summary: "untuk mendapatkan data omset per tanggal po", description: "prefix transaksi order = t,prefix trans po = toph,prefix merchant = mm")]
        [HttpPost]
        //[Route("/api/grafik/produk/getDataCategory")]
        public async Task<IActionResult> getOmsetPerTglPo([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOmsetPerTanggalPoByParams(param);
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

        #region PRODUK
        [HttpPost]
        //[Route("/api/grafik/produk/getDataPeringkatProduk")]
        [SwaggerOperation(summary: "untuk mendapatkan data peringkat produk berdasarkan favorit", description: "prefix masterproduct = m,prefix merchant = mm")]
        public async Task<IActionResult> getAllDataPeringkatProduk([FromBody] List<ParameterSearchModel> param)
        {
            object result = String.Empty;

            try
            {
                result = await this._product.GetAllDataPeringkatProductByParams(param);

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


        #region Ongkir
        [SwaggerOperation(summary: "untuk mendapatkan data ongkir per dropship", 
            description: "prefix merchant = mm,prefix trans order = t,prefix dropship = md" +
            "prefix trans po = toph")]
        [HttpPost]
        //[Route("/api/grafik/ongkir/getDataPerDropship")]
        public async Task<IActionResult> getOngkirPerDropship([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetOngkirPerDropshipByParams(param);
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


        #region ANALISA CUSTOMER
        [SwaggerOperation(summary: "untuk mendapatkan data analisa customer per gender",
            description: "prefix merchant = mm,prefix trans order = t")]
        [HttpPost]
        //[Route("/api/grafik/analisacustomer/getDataGender")]
        public async Task<IActionResult> getAnalisaCustomerPerGender([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetAnalisaCustomerPerGenderByParams(param);
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

        [SwaggerOperation(summary: "untuk mendapatkan data analisa customer per usia",
            description: "prefix merchant = mm,prefix trans order = t")]
        [HttpPost]
        //[Route("/api/grafik/analisacustomer/getDataUsia")]
        public async Task<IActionResult> getAnalisaCustomerPerUsia([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetAnalisaCustomerPerUsiaByParams(param);
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

        [SwaggerOperation(summary: "untuk mendapatkan data analisa customer repeat order per product user baru / user lama",
            description: "prefix trans order = t,prefix merchant = mm,prefix product = mp,prefix user = u")]
        [HttpPost]
        //[Route("/api/grafik/analisacustomer/getDataRepeatOrder")]
        public async Task<IActionResult> getAnalisaCustomerRepeatOrder([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetAnalisaCustomerRepeatOrderByParams(param);
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


        [SwaggerOperation(summary: "untuk mendapatkan data analisa customer peringkat metode pengiriman per dropship",
            description: "prefix trans order = toh,prefix merchant = mm,prefix shipment = ms,prefix dropship = md")]
        [HttpPost]
        //[Route("/api/grafik/analisacustomer/getDataPeringkatMetodePengiriman")]
        public async Task<IActionResult> getAnalisaCustomerPeringkatMetodePengiriman([FromBody] List<ParameterSearchModel> param)
        {
            object result;

            try
            {
                result = await this._trans.GetAnalisaCustomerMetodePengirimanPerDropshipByParams(param);
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
