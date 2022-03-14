using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupMenuDao
    {
        public SQLConn db;


        #region Banner Menu

        public async Task<List<ViewBannerMenu>> GetAllDataBannerMenuByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewBannerMenu>("BannerMenu_GetDataByDynamicFilters",
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

        public async Task<List<ViewBannerMenu>> GetAllDataBannerMenu()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewBannerMenu>("BannerMenu_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddBannerMenu(BannerMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("BannerMenu_insertdata",
                    new
                    {
                        p_bannermenuname = data.bannermenuname,
                        p_bannerimageurl = data.bannerimageurl,
                        p_cityid = data.cityid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateBannerMenu(BannerMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("BannerMenu_UpdateData",
                    new
                    {
                        p_bannermenuid = data.bannermenuid,
                        p_bannermenuname = data.bannermenuname,
                        p_bannerimageurl = data.bannerimageurl,
                        p_cityid = data.cityid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteBannerMenu(int bannermenuid)
        {
            try
            {
                return await this.db.executeScalarSp("BannerMenu_DeleteData",
                    new
                    {
                        p_bannermenuid = bannermenuid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Banner Menu


        #region Menu Kategori

        public async Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenuByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterCategoryMenu>("MasterCategoryMenu_GetDataByDynamicFilters",
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

        public async Task<List<MasterCategoryMenu>> GetAllDataMasterCategoryMenu()
        {
            try
            {
                return await this.db.QuerySPtoList<MasterCategoryMenu>("MasterCategoryMenu_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterCategoryMenu(MasterCategoryMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterCategoryMenu_InsertData",
                    new
                    {
                        p_categorymenuname = data.categorymenuname,
                        p_categoryimageurl = data.categoryimageurl
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterCategoryMenu(MasterCategoryMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterCategoryMenu_UpdateData",
                    new
                    {
                        p_categorymenuid = data.categorymenuid,
                        p_categorymenuname = data.categorymenuname,
                        p_categoryimageurl = data.categoryimageurl
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterCategoryMenu(int categorymenuid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterCategoryMenu_DeleteData",
                    new
                    {
                        p_categorymenuid = categorymenuid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Menu Promo

        public async Task<List<ViewPromoMenu>> GetAllDataPromoMenuByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewPromoMenu>("PromoMenu_GetDataByDynamicFilters",
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

        public async Task<List<ViewPromoMenu>> GetAllDataPromoMenu()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewPromoMenu>("PromoMenu_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddPromoMenu(PromoMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("PromoMenu_InsertData",
                    new
                    {
                        p_productid = data.productid,
                        p_promoanimationurl = data.promoanimationurl,
                        p_cityid = data.cityid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdatePromoMenu(PromoMenu data)
        {
            try
            {
                return await this.db.executeScalarSp("PromoMenu_UpdateData",
                    new
                    {
                        p_promomenuid = data.promomenuid,
                        p_productid = data.productid,
                        p_promoanimationurl = data.promoanimationurl,
                        p_cityid = data.cityid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeletePromoMenu(int promomenuid)
        {
            try
            {
                return await this.db.executeScalarSp("PromoMenu_DeleteData",
                    new
                    {
                        p_promomenuid = promomenuid   // int not null
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
