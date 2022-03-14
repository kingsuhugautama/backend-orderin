using DapperPostgreeLib;
using OrderIn.Service;
using OrderInBackend.Dao.Setup;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Setup
{
    public interface ISetupRatingService
    {

        Task<object> AddRating(MasterRating data);
    }

    public class SetupRatingService : ISetupRatingService
    {

        private readonly SQLConn _db;
        private readonly SetupRatingDao _dao;
        private readonly SetupMerchantDao _merchantDao;

        public SetupRatingService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupRatingDao
            {
                db = this._db
            };

            this._merchantDao = new SetupMerchantDao
            {
                db = this._db
            };
        }

        public async Task<object> AddRating(MasterRating data)
        {
            try
            {
                this._db.beginTransaction();

                object deliver = await this._dao.AddMasterRatingDelivering(data);
                object product = await this._dao.AddMasterRatingProduct(data);
                object package = await this._dao.AddMasterRatingPackaging(data);
                object avgRating = await this._merchantDao.UpdateRating(data.merchantid);

                String messages = string.Empty;
                if ((Int32)deliver > 0 && (Int32)product > 0 && (Int32)package > 0 && (Int32)avgRating > 0)
                {
                    messages = "SUCCESS : Data berhasil disimpan";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
                }

                this._db.commitTrans();
                return (object)messages;
            }
            catch (Exception ex)
            {
                this._db.rollBackTrans();
                throw ex;
                //TODO : log error
            }
        }
    }
}
