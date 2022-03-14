using DapperPostgreeLib;
using OrderIn.Service;
using OrderInBackend.Dao.Transaksi;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Transaksi
{
    public interface ITransPembayaranService
    {
        Task<List<TransPembayaran>> GetAllDataTransPembayaranByParams(List<Model.ParameterSearchModel> param);

        Task<TransPembayaranInsertPembayaranNewResult> AddTransPembayaran(TransPembayaranInsertPembayaran data);
        Task<object> UpdateTransPembayaran(TransPembayaranUpdatePembayaran data);
        Task<object> UpdateStatusPembayaran(TransPembayaranVerifikasi data);
        Task<object> DeleteTransPembayaran(int id);

        #region Status Pembayaran


        Task<List<MasterStatusPembayaran>> GetAllDataMasterStatusPembayaranByParams(List<Model.ParameterSearchModel> param);

        Task<object> AddMasterStatusPembayaran(MasterStatusPembayaran data);
        Task<object> UpdateMasterStatusPembayaran(MasterStatusPembayaran data);
        Task<object> DeleteMasterStatusPembayaran(int id);
        #endregion
    }

    public class TransPembayaranService : ITransPembayaranService
    {
        private readonly SQLConn _db;
        private readonly TransPembayaranDao _dao;

        public TransPembayaranService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new TransPembayaranDao
            {
                db = this._db
            };
        }


        public async Task<List<TransPembayaran>> GetAllDataTransPembayaranByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransPembayaranByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        
        public async Task<TransPembayaranInsertPembayaranNewResult> AddTransPembayaran(TransPembayaranInsertPembayaran data)
        {
            try
            {
                var hasil = await this._dao.AddTransPembayaran(data);

                var result = hasil;
                int status = hasil.status;

                var newResult = new TransPembayaranInsertPembayaranNewResult
                {
                    orderheaderid = result.orderheaderid,
                    jumlahbayar = result.jumlahbayar,
                    waktuentry = result.waktuentry,
                    closepodate = result.closepodate,
                    rekening = result.rekening,
                    pembayaranid = result.status, //status = pembayaran id atau 0 jika gagal
                    status = result.status > 0 ? "SUCCESS : Berhasil menyiapkan pembayaran" : (result.status == -1 ? "FAIL : Pre order sudah ditutup" : result.status == -2 ? "FAIL : Gagal menambahkan nomor invoice" : "FAIL : Gagal insert ke tabel") 
                };


                return newResult;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateTransPembayaran(TransPembayaranUpdatePembayaran data)
        {
            try
            {
                object hasil = await this._dao.UpdateTransPembayaran(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Pembayaran telah kami terima mohon tunggu sampai kami melakukan verifikasi pembayaran anda";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Pre order sudah ditutup";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateStatusPembayaran(TransPembayaranVerifikasi data)
        {
            try
            {
                object hasil = await this._dao.UpdateStatusPembayaran(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Pembayaran telah diverifikasi";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteTransPembayaran(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteTransPembayaran(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }


        #region Status Pembayaran

        public async Task<List<MasterStatusPembayaran>> GetAllDataMasterStatusPembayaranByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterStatusPembayaranByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
       
        public async Task<object> AddMasterStatusPembayaran(MasterStatusPembayaran data)
        {
            try
            {
                object hasil = await this._dao.AddMasterStatusPembayaran(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil disimpan";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateMasterStatusPembayaran(MasterStatusPembayaran data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterStatusPembayaran(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteMasterStatusPembayaran(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterStatusPembayaran(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion


    }
}
