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
    public interface ISetupPaymentService
    {

        Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethodByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethod();

        Task<object> AddMasterPaymentMethod(MasterPaymentMethod data);
        Task<object> UpdateMasterPaymentMethod(MasterPaymentMethod data);
        Task<object> DeleteMasterPaymentMethod(int id);
    }

    public class SetupPaymentService : ISetupPaymentService
    {
        private readonly SQLConn _db;
        private readonly SetupPaymentDao _dao;

        public SetupPaymentService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupPaymentDao
            {
                db = this._db
            };
        }


        public async Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethodByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterPaymentMethodByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethod()
        {
            try
            {
                return await this._dao.GetAllDataMasterPaymentMethod();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterPaymentMethod(MasterPaymentMethod data)
        {
            try
            {
                object hasil = await this._dao.AddMasterPaymentMethod(data);

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

        public async Task<object> UpdateMasterPaymentMethod(MasterPaymentMethod data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterPaymentMethod(data);

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

        public async Task<object> DeleteMasterPaymentMethod(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterPaymentMethod(id);
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
