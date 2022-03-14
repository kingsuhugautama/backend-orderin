using DapperPostgreeLib;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Transaksi
{
    public class TransPembayaranDao
    {
        public SQLConn db;



        #region Status Pembayaran

        public async Task<List<MasterStatusPembayaran>> GetAllDataMasterStatusPembayaranByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterStatusPembayaran>("MasterStatusPembayaran_GetDataByDynamicFilters",
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

        public async Task<object> AddMasterStatusPembayaran(MasterStatusPembayaran data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusPembayaran_InsertData",
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

        public async Task<object> UpdateMasterStatusPembayaran(MasterStatusPembayaran data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusPembayaran_UpdateData",
                    new
                    {
                        p_statuspembayaranid = data.statuspembayaranid,
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterStatusPembayaran(int statuspembayaranid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterStatusPembayaran_DeleteData",
                    new
                    {
                        p_statuspembayaranid = statuspembayaranid // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Trans Pembayaran

        public async Task<List<TransPembayaran>> GetAllDataTransPembayaranByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<TransPembayaran>("TransPembayaran_GetDataByDynamicFilters",
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

        public async Task<TransPembayaranInsertPembayaranResult> AddTransPembayaran(TransPembayaranInsertPembayaran data)
        {
            try
            {
                return await this.db.QuerySPtoSingle<TransPembayaranInsertPembayaranResult>("TransPembayaran_InsertData",
                    new
                    {
                        p_orderheaderid = data.orderheaderid,
                        p_idrekening = data.idrekening
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> UpdateTransPembayaran(TransPembayaranUpdatePembayaran data)
        {
            try
            {
                return await this.db.executeScalarSp("TransPembayaran_UpdatePembayaran",
                    new
                    {
                        p_pembayaranid = data.pembayaranid,
                        p_orderheaderid = data.orderheaderid,
                        p_noref = data.noref,
                        p_buktibayarurl = data.buktibayarurl,
                        p_statuspembayaranid = data.statuspembayaranid,
                        p_waktubayar = data.waktubayar
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateStatusPembayaran(TransPembayaranVerifikasi data)
        {
            try
            {
                return await this.db.executeScalarSp("TransPembayaran_VerifikasiPembayaran",
                    new
                    {
                        p_pembayaranid = data.pembayaranid,
                        p_statuspembayaranid = data.statuspembayaranid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteTransPembayaran(int orderheaderid)
        {
            try
            {
                return await this.db.executeScalarSp("TransPembayaran_DeleteData",
                    new
                    {
                        p_orderheaderid = orderheaderid // int not null
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
