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
    public interface ISetupDaerahService
    {

        Task<List<Kota>> GetAllDataKotaByParams(List<Model.ParameterSearchModel> param);

        Task<List<Provinsi>> GetAllDataProvinsiByParams(List<Model.ParameterSearchModel> param);

    }

    public class SetupDaerahService :ISetupDaerahService
    {


        private readonly SQLConn _db;
        private readonly SetupDaerahDao _dao;

        public SetupDaerahService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupDaerahDao
            {
                db = this._db
            };
        }


        public async Task<List<Provinsi>> GetAllDataProvinsiByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataProvinsiByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<Kota>> GetAllDataKotaByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataKotaByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
    }
}
