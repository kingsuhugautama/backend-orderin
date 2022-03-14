using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupPaymentDao
    {
        public SQLConn db;

        public async Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethodByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterPaymentMethod>("MasterPaymentMethod_GetDataByDynamicFilters",
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

        public async Task<List<MasterPaymentMethod>> GetAllDataMasterPaymentMethod()
        {
            try
            {
                return await this.db.QuerySPtoList<MasterPaymentMethod>("MasterPaymentMethod_GetAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterPaymentMethod(MasterPaymentMethod data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterPaymentMethod_InsertData",
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

        public async Task<object> UpdateMasterPaymentMethod(MasterPaymentMethod data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterPaymentMethod_UpdateData",
                    new
                    {
                        p_paymentmethodid = data.paymentmethodid,
                        p_keterangan = data.keterangan
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteMasterPaymentMethod(int paymentmethodid)
        {
            try
            {
                return await this.db.executeScalarSp("MasterPaymentMethod_DeleteData",
                    new
                    {
                        p_paymentmethodid = paymentmethodid // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
