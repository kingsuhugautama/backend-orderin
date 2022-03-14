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
    public interface ISetupMenuService
    {

        #region Banner Menu
        Task<List<ViewBannerMenu>> GetAllDataBannerMenuByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewBannerMenu>> GetAllDataBannerMenu();

        Task<object> AddBannerMenu(BannerMenu data);
        Task<object> UpdateBannerMenu(BannerMenu data);
        Task<object> DeleteBannerMenu(int id);
        #endregion

        #region menu Kateogri

        Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenuByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenu();

        Task<object> AddMasterCategoryMenu(MasterCategoryMenu data);
        Task<object> UpdateMasterCategoryMenu(MasterCategoryMenu data);
        Task<object> DeleteMasterCategoryMenu(int id);

        #endregion

        #region menu promo

        Task<List<ViewPromoMenu>> GetAllDataPromoMenuByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewPromoMenu>> GetAllDataPromoMenu();

        Task<object> AddPromoMenu(PromoMenu data);
        Task<object> UpdatePromoMenu(PromoMenu data);
        Task<object> DeletePromoMenu(int id);

        #endregion
    }

    public class SetupMenuService : ISetupMenuService
    {
        private readonly SQLConn _db;
        private readonly SetupMenuDao _dao;

        public SetupMenuService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupMenuDao
            {
                db = this._db
            };
        }

        #region Banner Menu
        public async Task<List<ViewBannerMenu>> GetAllDataBannerMenuByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataBannerMenuByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewBannerMenu>> GetAllDataBannerMenu()
        {
            try
            {
                return await this._dao.GetAllDataBannerMenu();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddBannerMenu(BannerMenu data)
        {
            try
            {
                object hasil = await this._dao.AddBannerMenu(data);

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
        public async Task<object> UpdateBannerMenu(BannerMenu data)
        {
            try
            {
                object hasil = await this._dao.UpdateBannerMenu(data);

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
        public async Task<object> DeleteBannerMenu(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteBannerMenu(id);
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


        #region Menu Kategori
        public async Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenuByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterCategoryMenuByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenu()
        {
            try
            {
                return await this._dao.GetAllDataMasterCategoryMenu();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterCategoryMenu(MasterCategoryMenu data)
        {
            try
            {
                object hasil = await this._dao.AddMasterCategoryMenu(data);

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
        public async Task<object> UpdateMasterCategoryMenu(MasterCategoryMenu data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterCategoryMenu(data);

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
        public async Task<object> DeleteMasterCategoryMenu(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterCategoryMenu(id);
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

        #region menu promo
        public async Task<List<ViewPromoMenu>> GetAllDataPromoMenuByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataPromoMenuByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewPromoMenu>> GetAllDataPromoMenu()
        {
            try
            {
                return await this._dao.GetAllDataPromoMenu();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddPromoMenu(PromoMenu data)
        {
            try
            {
                object hasil = await this._dao.AddPromoMenu(data);

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
        public async Task<object> UpdatePromoMenu(PromoMenu data)
        {
            try
            {
                object hasil = await this._dao.UpdatePromoMenu(data);

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
        public async Task<object> DeletePromoMenu(int id)
        {
            try
            {
                object hasil = await this._dao.DeletePromoMenu(id);
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
