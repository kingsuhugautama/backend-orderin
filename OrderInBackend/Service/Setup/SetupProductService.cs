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
    public interface ISetupProductService
    {

        Task<List<ViewMasterProduct>> GetAllDataMasterProductForUserByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterProduct>> GetAllDataMasterProductForMarketByParams(List<Model.ParameterSearchModel> param);
        Task<List<PeringkatProduct>> GetAllDataPeringkatProductByParams(List<Model.ParameterSearchModel> param);
        Task<List<FavoritProduct>> GetAllDataFavoritProductByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewMasterProduct>> GetAllDataMasterProduct();

        Task<object> AddMasterProduct(MasterProduct data);
        Task<object> AddFavoritProduct(FavoritProduct data);
        Task<object> UpdateMasterProduct(MasterProduct data);
        Task<object> UpdateStatusActive(UpdateProductStatusActive data);
        Task<object> DeleteMasterProduct(int id);
        Task<object> DeleteFavoritProduct(FavoritProduct data);
    }

    public class SetupProductService : ISetupProductService
    {
        private readonly SQLConn _db;
        private readonly SetupProductDao _dao;

        public SetupProductService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupProductDao
            {
                db = this._db
            };
        }


        public async Task<List<FavoritProduct>> GetAllDataFavoritProductByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataFavoritProductByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<PeringkatProduct>> GetAllDataPeringkatProductByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataPeringkatProductByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<ViewMasterProduct>> GetAllDataMasterProductForUserByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterProductForUserByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<MasterProduct>> GetAllDataMasterProductForMarketByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterProductForMarketByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewMasterProduct>> GetAllDataMasterProduct()
        {
            try
            {
                return await this._dao.GetAllDataMasterProduct();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterProduct(MasterProduct data)
        {
            try
            {
                object hasil = await this._dao.AddMasterProduct(data);

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

        public async Task<object> UpdateMasterProduct(MasterProduct data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterProduct(data);

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

        public async Task<object> UpdateStatusActive(UpdateProductStatusActive data)
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
        public async Task<object> DeleteMasterProduct(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterProduct(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> AddFavoritProduct(FavoritProduct data)
        {
            try
            {
                object hasil = await this._dao.AddFavoritProduct(data);

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

        public async Task<object> DeleteFavoritProduct(FavoritProduct data)
        {
            try
            {
                object hasil = await this._dao.DeleteFavoritProduct(data);
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
