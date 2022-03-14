using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupMerchantDao
    {
        public SQLConn db;

        public async Task<List<ViewMasterMerchant>> GetAllDataMasterMerchantByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewMasterMerchant>("MasterMerchant_GetDataByDynamicFilters",
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

        public async Task<object> AddMasterMerchant(MasterMerchant data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterMerchant_InsertData",
                    new
                    {
                        p_userid = data.userid,
                        p_merchantname = data.merchantname,
                        p_description = data.description,
                        p_logoimageurl = data.logoimageurl,
                        p_coverimageurl = data.coverimageurl,
                        p_namabank = data.namabank,
                        p_nomorrekening = data.nomorrekening,
                        p_namapemilikrekening = data.namapemilikrekening
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterMerchant(MasterMerchant data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterMerchant_UpdateData",
                    new
                    {
                        p_merchantid = data.merchantid,
                        p_userid = data.userid,
                        p_merchantname = data.merchantname,
                        p_description = data.description,
                        p_logoimageurl = data.logoimageurl,
                        p_coverimageurl = data.coverimageurl,
                        p_namabank = data.namabank,
                        p_nomorrekening = data.nomorrekening,
                        p_namapemilikrekening = data.namapemilikrekening
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> UpdateRating(int merchantid)
        {
            try
            {
                return await this.db.executeScalarSp("mastermerchant_updaterating",
                    new
                    {
                        p_merchantid = merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterMerchant(int id)
        {
            try
            {
                return await this.db.executeScalarSp("MasterMerchant_DeleteData",
                    new
                    {
                        p_merchantid = id   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
