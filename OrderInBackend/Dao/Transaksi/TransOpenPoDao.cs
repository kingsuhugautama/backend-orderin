using DapperPostgreeLib;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Transaksi
{
    public class TransOpenPoDao
    {
        public SQLConn db;

        #region TRANS PO HEADER
        public async Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeaderByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewTransOpenPoHeader>("TransOpenPoHeader_GetDataByDynamicFilters",
                    new
                    {
                        filters    // not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeader()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewTransOpenPoHeader>("TransOpenPoHeader_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddTransOpenPoHeader(TransOpenPoHeader data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoHeader_InsertData",
                    new
                    {
                        p_openpodate = data.openpodate,
                        p_closepodate = data.closepodate,
                        p_kota = data.kota,
                        p_statustransaksi = data.statustransaksi,
                        p_merchantid = data.merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateTransOpenPoHeader(TransOpenPoHeader data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoHeader_UpdateData",
                    new
                    {
                        p_openpoheaderid = data.openpoheaderid,
                        p_openpodate = data.openpodate,
                        p_closepodate = data.closepodate,
                        p_kota = data.kota,
                        p_statustransaksi = data.statustransaksi
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransOpenPoHeader(int openpoheaderid)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoHeader_DeleteData",
                    new
                    {
                        p_openpoheaderid = openpoheaderid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region TRANS PO DETAIL PRODUK

        public async Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProductByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewTransOpenPoDetailProduct>("TransOpenPoDetailProduct_GetDataByDynamicFilters",
                    new
                    {
                        filters    // not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProduct()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewTransOpenPoDetailProduct>("TransOpenPoDetailProduct_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddTransOpenPoDetailProduct(TransOpenPoDetailProduct data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailProduct_InsertData",
                    new
                    {
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_openpoheaderid = data.openpoheaderid,
                        p_productid = data.productid,
                        p_productprice = data.productprice,
                        p_kuotamin = data.kuotamin,
                        p_kuotamax = data.kuotamax,
                        p_qtymax = data.qtymax,
                        p_qtymin = data.qtymin
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateTransOpenPoDetailProduct(TransOpenPoDetailProduct data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailProduct_UpdateData",
                    new
                    {
                        p_openpodetailproductid = data.openpodetailproductid,
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_openpoheaderid = data.openpoheaderid,
                        p_productid = data.productid,
                        p_productprice = data.productprice,
                        p_kuotamin = data.kuotamin,
                        p_kuotamax = data.kuotamax,
                        p_qtymax = data.qtymax,
                        p_qtymin = data.qtymin
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransOpenPoDetailProduct(int p_openpodetailproductid)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailProduct_DeleteData",
                    new
                    {
                        p_openpodetailproductid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region TRANS PO DETAIL DROPSHIP

        public async Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewTransOpenPoDetailDropship>("TransOpenPoDetailDropship_GetDataByDynamicFilters",
                    new
                    {
                        filters    // not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropship()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewTransOpenPoDetailDropship>("TransOpenPoDetailDropship_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ViewExistingDropship>> GetExistingDropshipByParams(CekExistingDropshipParams data)
        {
            try
            {

                return await this.db.QuerySPtoList<ViewExistingDropship>("transopenpodetaildropship_getExistingDropship",
                    new
                    {
                        p_dropshipid = data.dropshipid,
                        p_openpodate = data.openpodate// not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddTransOpenPoDetailDropship(TransOpenPoDetailDropship data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailDropship_InsertData",
                    new
                    {
                        p_openpoheaderid = data.openpoheaderid,
                        p_dropshipid = data.dropshipid,
                        p_starttime = data.starttime,
                        p_endtime = data.endtime,
                        p_tolerance = data.tolerance,
                        p_keterangan = data.keterangan,
                        p_ongkoskirim = data.ongkoskirim,
                        p_iscod = data.iscod
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddTransOpenPoDetailDropshipKategori(TransOpenPoDetailDropshipKategori data)
        {
            try
            {
                return await this.db.executeScalarSp("transopenpodetaildropshipkategori_InsertData",
                    new
                    {
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_categorymenuid = data.categorymenuid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateTransOpenPoDetailDropship(TransOpenPoDetailDropship data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailDropship_UpdateData",
                    new
                    {
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_openpoheaderid = data.openpoheaderid,
                        p_dropshipid = data.dropshipid,
                        p_starttime = data.starttime,
                        p_endtime = data.endtime,
                        p_tolerance = data.tolerance,
                        p_keterangan = data.keterangan,
                        p_ongkoskirim = data.ongkoskirim
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransOpenPoDetailDropship(int p_openpodetaildropshipid)
        {
            try
            {
                return await this.db.executeScalarSp("TransOpenPoDetailDropship_DeleteData",
                    new
                    {
                        p_openpodetaildropshipid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
