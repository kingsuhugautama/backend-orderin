using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupRekeningDao
    {
        public SQLConn db;

        public async Task<List<MasterRekening>> GetAllDataMasterRekeningByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<MasterRekening>("MasterRekening_GetDataByDynamicFilters",
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
    }
}
