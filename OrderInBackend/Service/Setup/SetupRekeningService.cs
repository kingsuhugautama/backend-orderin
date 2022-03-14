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
    public interface ISetupRekeningService
    {

        Task<List<MasterRekening>> GetAllDataMasterRekeningByParams(List<Model.ParameterSearchModel> param);
    }

    public class SetupRekeningService : ISetupRekeningService
    {
        private readonly SQLConn _db;
        private readonly SetupRekeningDao _dao;

        public SetupRekeningService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupRekeningDao
            {
                db = this._db
            };
        }


        public async Task<List<MasterRekening>> GetAllDataMasterRekeningByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterRekeningByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
    }
}
