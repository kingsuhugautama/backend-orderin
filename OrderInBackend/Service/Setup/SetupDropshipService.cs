using DapperPostgreeLib;
using OrderIn.Service;
using OrderInBackend.Dao.Setup;
using OrderInBackend.Model;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Setup
{
    public interface ISetupDropshipService
    {

        Task<List<MasterDropship>> GetAllDataMasterDropshipByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterDropship>> GetAllDataMasterDropship();

        Task<object> AddMasterDropship(MasterDropship data);
        Task<object> UpdateMasterDropship(MasterDropship data);
        Task<object> UpdateStatusActive(UpdateDropshipStatusActive data);
        Task<object> DeleteMasterDropship(int id);
    }

    public class SetupDropshipService : ISetupDropshipService
    {
        private readonly SQLConn _db;
        private readonly SetupDropshipDao _dao;

        public SetupDropshipService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupDropshipDao
            {
                db = this._db
            };
        }


        public async Task<List<MasterDropship>> GetAllDataMasterDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<MasterDropship>> GetAllDataMasterDropship()
        {
            try
            {
                return await this._dao.GetAllDataMasterDropship();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterDropship(MasterDropship data)
        {
            try
            {
                object hasil = await this._dao.AddMasterDropship(data);

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

        public async Task<object> UpdateMasterDropship(MasterDropship data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterDropship(data);

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

        public async Task<object> UpdateStatusActive(UpdateDropshipStatusActive data)
        {
            try
            {
                object hasil = await this._dao.UpdateStatusActive(data);

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
        public async Task<object> DeleteMasterDropship(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterDropship(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
    }
}
