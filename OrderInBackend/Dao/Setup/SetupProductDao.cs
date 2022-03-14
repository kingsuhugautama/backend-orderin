using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupProductDao
    {
        public SQLConn db;

        public async Task<List<FavoritProduct>> GetAllDataFavoritProductByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<FavoritProduct>("favoritproduct_GetDataByDynamicFilters",
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

        public async Task<List<PeringkatProduct>> GetAllDataPeringkatProductByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<PeringkatProduct>("product_getdataperingkatproductbydynamicfilters",
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

        public async Task<List<MasterProduct>> GetAllDataMasterProductForMarketByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterProduct>("MasterProduct_GetDataForMarketByDynamicFilters",
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

        public async Task<List<ViewMasterProduct>> GetAllDataMasterProductForUserByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewMasterProduct>("MasterProduct_GetDataForUserByDynamicFilters",
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
        public async Task<List<ViewMasterProduct>> GetAllDataMasterProduct()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewMasterProduct>("MasterProduct_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterProduct(MasterProduct data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterProduct_InsertData",
                    new
                    {
                        p_merchantid = data.merchantid,
                        p_categorymenuid = data.categorymenuid,
                        p_productname = data.productname,
                        p_productimageurl = data.productimageurl,
                        p_productprice = data.productprice,
                        p_productdescription = data.productdescription,
                        p_isbutton = data.isbutton,
                        p_isactive = data.isactive,
                        p_productsatuan = data.productsatuan,
                        p_kuota = data.kuota,
                        p_kuotamax = data.kuotamax,
                        p_qtymax = data.qtymax,
                        p_qtymin = data.qtymin,
                        p_kuotamin = data.kuotamin,
                        p_ishalal = data.ishalal
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddFavoritProduct(FavoritProduct data)
        {
            try
            {
                return await this.db.executeScalarSp("favoritproduct_InsertData",
                           new
                           {
                               p_productid = data.productid,
                               p_userid = data.userid
                           });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterProduct(MasterProduct data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterProduct_UpdateData",
                    new
                    {
                        p_productid = data.productid,
                        p_merchantid = data.merchantid,
                        p_categorymenuid = data.categorymenuid,
                        p_productname = data.productname,
                        p_productimageurl = data.productimageurl,
                        p_productprice = data.productprice,
                        p_productdescription = data.productdescription,
                        p_isbutton = data.isbutton,
                        p_isactive = data.isactive,
                        p_productsatuan = data.productsatuan,
                        p_kuota = data.kuota,
                        p_kuotamax = data.kuotamax,
                        p_qtymax = data.qtymax,
                        p_qtymin = data.qtymin,
                        p_kuotamin = data.kuotamin,
                        p_ishalal = data.ishalal
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateStatusActive(UpdateProductStatusActive data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterProduct_UpdateStatusActive",
                    new
                    {
                        p_productid = data.productid,
                        p_isactive = data.isactive
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> DeleteMasterProduct(int id)
        {
            try
            {
                return await this.db.executeScalarSp("MasterProduct_DeleteData",
                    new
                    {
                        p_productid = id   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteFavoritProduct(FavoritProduct data)
        {
            try
            {

                return await this.db.executeScalarSp("favoritproduct_DeleteData",
                    new
                    {
                        p_productid = data.productid,// int not null
                        p_userid = data.userid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
