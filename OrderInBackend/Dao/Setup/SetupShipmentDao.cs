using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupShipmentDao
    {
        public SQLConn db;

        public async Task<List<MasterShipment>> GetAllDataMasterShipmentByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterShipment>("MasterShipment_GetDataByDynamicFilters",
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

        public async Task<List<MasterShipment>> GetAllDataMasterShipment()
        {
            try
            {
                return await this.db.QuerySPtoList<MasterShipment>("MasterShipment_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterShipment(MasterShipment data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterShipment_InsertData",
                    new
                    {
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateMasterShipment(MasterShipment data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterShipment_UpdateData",
                    new
                    {
                        p_shipmentid = data.shipmentid,
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterShipment(int shipmentid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterShipment_DeleteData",
                    new
                    {
                        p_shipmentid = shipmentid // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
