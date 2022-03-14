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
    public interface ISetupShipmentService
    {

        Task<List<MasterShipment>> GetAllDataMasterShipmentByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterShipment>> GetAllDataMasterShipment();

        Task<object> AddMasterShipment(MasterShipment data);
        Task<object> UpdateMasterShipment(MasterShipment data);
        Task<object> DeleteMasterShipment(int id);

    }

    public class SetupShipmentService : ISetupShipmentService
    {
        private readonly SQLConn _db;
        private readonly SetupShipmentDao _dao;

        public SetupShipmentService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupShipmentDao
            {
                db = this._db
            };
        }


        public async Task<List<MasterShipment>> GetAllDataMasterShipmentByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterShipmentByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<MasterShipment>> GetAllDataMasterShipment()
        {
            try
            {
                return await this._dao.GetAllDataMasterShipment();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterShipment(MasterShipment data)
        {
            try
            {
                object hasil = await this._dao.AddMasterShipment(data);

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

        public async Task<object> UpdateMasterShipment(MasterShipment data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterShipment(data);

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

        public async Task<object> DeleteMasterShipment(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterShipment(id);
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
