using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupDropshipDao
    {
        public SQLConn db;

        public async Task<List<MasterDropship>> GetAllDataMasterDropshipByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterDropship>("MasterDropship_GetDataByDynamicFilters",
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

        public async Task<List<MasterDropship>> GetAllDataMasterDropship()
        {
            try
            {
                return await this.db.QuerySPtoList<MasterDropship>("MasterDropship_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterDropship(MasterDropship data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterDropship_InsertData",
                    new
                    {
                        p_merchantid = data.merchantid,
                        p_label = data.label,
                        p_longitude = data.longitude,
                        p_latitude = data.latitude,
                        p_dropshipaddress = data.dropshipaddress,
                        p_description = data.description,
                        p_contactname = data.contactname,
                        p_contactphone = data.contactphone,
                        p_radius = data.radius,
                        p_isactive = data.isactive,
                        p_ongkoskirim = data.ongkoskirim,
                        p_iscod = data.iscod
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterDropship(MasterDropship data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterDropship_UpdateData",
                    new
                    {
                        p_dropshipid = data.dropshipid,
                        p_merchantid = data.merchantid,
                        p_label = data.label,
                        p_longitude = data.longitude,
                        p_latitude = data.latitude,
                        p_dropshipaddress = data.dropshipaddress,
                        p_description = data.description,
                        p_contactname = data.contactname,
                        p_contactphone = data.contactphone,
                        p_radius = data.radius,
                        p_isactive = data.isactive,
                        p_ongkoskirim = data.ongkoskirim
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> UpdateStatusActive(UpdateDropshipStatusActive data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterDropship_UpdateStatusActive",
                    new
                    {
                        p_dropshipid = data.dropshipid,
                        p_isactive = data.isactive
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterDropship(int dropshipid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterDropship_DeleteData",
                    new
                    {
                        p_dropshipid = dropshipid,  // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
