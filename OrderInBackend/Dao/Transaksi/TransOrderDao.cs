using DapperPostgreeLib;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Transaksi
{
    public class TransOrderDao
    {
        public SQLConn db;
        private CryptoHelper _helper;

        public TransOrderDao()
        {
            this._helper = new CryptoHelper();
        }


        #region HEADER

        public async Task<List<ViewTransOrderBelumBayar>> GetAllDataUserBelumBayarByOpenPoHeaderId(int p_openpoheaderid)
        {
            try
            {
                return await this.db.QuerySPtoList<ViewTransOrderBelumBayar>("transorderheader_GetAllDataUserBelumBayarByOpenPoHeaderId",
                    new
                    {
                        p_openpoheaderid    // not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewTransOrderBelumBayar>> GetAllDataUserSudahBayarByOpenPoHeaderId(int p_openpoheaderid)
        {
            try
            {
                return await this.db.QuerySPtoList<ViewTransOrderBelumBayar>("transorderheader_getalldatausersudahbayarbyopenpoheaderid",
                    new
                    {
                        p_openpoheaderid    // not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewTransOrderHeader>> GetAllDataTransOrderHeaderByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                if(param.Count > 0)
                {
                    foreach(var item in param)
                    {
                        item.searchText2 =  item.columnName.IndexOf("notransaksi") > -1 ?  this._helper.CryptoDecrypt(item.searchText2,"mat_notransaksi") : item.searchText2;
                    }
                }

                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);
                

                return await this.db.QuerySPtoList<ViewTransOrderHeader>("TransOrderHeader_GetDataByDynamicFilters",
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

        public async Task<object> AddTransOrderHeader(TransOrderHeader data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOrderHeader_InsertData",
                    new
                    {
                        p_openpoheaderid = data.openpoheaderid,
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_paymentmethodid = data.paymentmethodid,
                        p_userentry = data.userentry,
                        p_longitude = data.longitude,
                        p_latitude = data.latitude,
                        p_address = data.address,
                        p_total = data.total,
                        p_notesaddress = data.notesaddress,
                        p_statustransorderid = data.statustransorderid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateStatusTransaksi(TransOrderHeaderUpdateStatus data)
        {
            try
            {
                return await this.db.executeScalarSp("transorderheader_UpdateStatusTransaksi",
                    new
                    {
                        p_orderheaderid = data.orderheaderid,
                        p_statustransorderid = data.statustransorderid,
                        p_userclosed = data.userclosed
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransOrderHeader(int orderheaderid)
        {
            try
            {
                return await this.db.executeScalarSp("TransOrderHeader_DeleteData",
                    new
                    {
                        p_orderheaderid = orderheaderid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DETAIL

        public async Task<List<ViewTransOrderDetail>> GetAllDataTransOrderDetailByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewTransOrderDetail>("TransOrderDetail_GetDataByDynamicFilters",
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

        public async Task<object> AddTransOrderDetail(TransOrderDetail data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOrderDetail_InsertData",
                    new
                    {
                        p_orderheaderid = data.orderheaderid,
                        p_openpodetailproductid = data.openpodetailproductid,
                        p_qty = data.qty,
                        p_harga = data.harga,
                        p_notes = data.notes
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateTransOrderDetail(TransOrderDetail data)
        {
            try
            {
                return await this.db.executeScalarSp("TransOrderDetail_UpdateData",
                    new
                    {
                        p_orderdetailid = data.orderdetailid,
                        p_orderheaderid = data.orderheaderid,
                        p_openpodetailproductid = data.openpodetailproductid,
                        p_qty = data.qty,
                        p_notes = data.notes
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransOrderDetail(int orderdetailid)
        {
            try
            {
                return await this.db.executeScalarSp("TransOrderDetail_DeleteData",
                    new
                    {
                        p_orderdetailid = orderdetailid // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region STATUS TRANSAKSI
        public async Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrderByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterStatusTransaksiOrder>("MasterStatusTransaksiOrder_GetDataByDynamicFilters",
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

        public async Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrder()
        {
            try
            {
                return await this.db.QuerySPtoList<MasterStatusTransaksiOrder>("MasterStatusTransaksiOrder_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusTransaksiOrder_InsertData",
                    new
                    {
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusTransaksiOrder_UpdateData",
                    new
                    {
                        p_statustransorderid = data.statustransorderid,
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterStatusTransaksiOrder(int statustransorderid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusTransaksiOrder_DeleteData",
                    new
                    {
                        p_statustransorderid = statustransorderid // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region TRANS ABSENSI DROPSHIP
        public async Task<List<TransAbsensiDropship>> GetAllDataTransAbsensiDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<TransAbsensiDropship>("TransAbsensiDropship_GetDataByDynamicFilters",
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

        public async Task<object> AddTransAbsensiDropship(TransAbsensiDropship data)
        {
            try
            {
                return await this.db.executeScalarSp("TransAbsensiDropship_InsertData",
                    new
                    {
                        p_openpodetaildropshipid = data.openpodetaildropshipid,
                        p_openpoheaderid = data.openpoheaderid,
                        p_latitude = data.latitude,
                        p_longitude = data.longitude,
                        p_address = data.address,
                        p_foto = data.foto
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region PENGIRIMAN
        public async Task<List<TransPengiriman>> GetAllDataTransPengirimanByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<TransPengiriman>("TransPengiriman_GetDataByDynamicFilters",
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

        public async Task<object> AddTransPengiriman(TransPengiriman data)
        {
            try
            {
                return await this.db.executeScalarSp("TransPengiriman_InsertData",
                    new
                    {
                        p_orderheaderid = data.orderheaderid,
                        p_shipmentid = data.shipmentid,
                        p_foto = data.foto,
                        p_waktukirim = data.waktukirim
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdatePenerima(TransPengirimanUpdatePenerima data)
        {
            try
            {
                return await this.db.executeScalarSp("TransPengiriman_UpdatePenerima",
                    new
                    {
                        p_orderheaderid = data.orderheaderid,
                        p_namapenerima = data.namapenerima,
                        p_fotobuktiterima = data.fotobuktiterima,
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region Punishment

        public async Task<List<Punishment>> GetAllDataPunishmentByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<Punishment>("Punishment_GetDataByDynamicFilters",
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

        public async Task<object> AddPunishment(Punishment data)
        {
            try
            {
                return await this.db.executeScalarSp("Punishment_InsertData",
                    new
                    {
                        p_userid = data.userid,
                        p_merchantid = data.merchantid,
                        p_orderheaderid = data.orderheaderid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<object> UpdatePunishment(Punishment data)
        //{
        //    try
        //    {
        //        return await this.db.executeScalarSp("Punishment_UpdateData",
        //            new
        //            {
        //                p_punishmentid = data.punishmentid,
        //                p_userid = data.userid,
        //                p_merchantid = data.merchantid,
        //                p_orderheaderid = data.orderheaderid
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //untuk release oleh merchant, membebaskan customer dari hukuman
        public async Task<object> DeletePunishment(PunishmentRelease data)
        {
            try
            {
                return await this.db.executeScalarSp("Punishment_DeleteData",
                    new
                    {
                        p_userid = data.userid,
                        p_merchantid = data.merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region GRAFIK


        #region OMSET

        public async Task<List<OmsetPerProduct>> GetOmsetPerProductByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OmsetPerProduct>("grafik_getdataomsetperproductbydynamicfilters",
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


        public async Task<List<OmsetPerCategory>> GetOmsetPerCategoryByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OmsetPerCategory>("grafik_getdataomsetpercategorybydynamicfilters",
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

        public async Task<List<OmsetPerDropship>> GetOmsetPerDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OmsetPerDropship>("grafik_getdataomsetperdropshipbydynamicfilters",
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

        public async Task<List<OmsetPerDropshipProduct>> GetOmsetPerDropshipProductByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OmsetPerDropshipProduct>("grafik_getdataomsetperdropshipproductbydynamicfilters",
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

        public async Task<List<OngkosKirimPerDropship>> GetOngkirPerDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OngkosKirimPerDropship>("grafik_getongkoskirimperdropshipbydynamicfilters",
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

        public async Task<List<OmsetPerTanggalPo>> GetOmsetPerTanggalPoByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<OmsetPerTanggalPo>("grafik_getomsetpertanggalpoByDynamicFilters",
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
        #endregion

        #region analisa customer

        public async Task<List<AnalisaCustomerGender>> GetAnalisaCustomerPerGenderByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<AnalisaCustomerGender>("grafik_getalldataanalisacustomerpergenderByDynamicFilters",
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

        public async Task<List<AnalisaCustomerUsia>> GetAnalisaCustomerPerUsiaByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<AnalisaCustomerUsia>("grafik_getalldataanalisacustomerperusiaByDynamicFilters",
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

        public async Task<List<AnalisaCustomerRepeatOrder>> GetAnalisaCustomerRepeatOrderByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<AnalisaCustomerRepeatOrder>("grafik_getdataanalisacustomerrepeatorderByDynamicFilters",
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

        public async Task<List<AnalisaCustomerMetodePengirimanPerDropship>> GetAnalisaCustomerMetodePengirimanPerDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<AnalisaCustomerMetodePengirimanPerDropship>("grafik_GetDatapengirimanperdropshipByDynamicFilters",
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

        #endregion


        #endregion
    }
}
