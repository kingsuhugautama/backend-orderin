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
    public interface ISetupMerchantService
    {

        Task<List<ViewMasterMerchant>> GetAllDataMasterMerchantByParams(List<Model.ParameterSearchModel> param);

        Task<object> VerifyMerchant(MasterMerchant data);
        Task<object> AddMasterMerchant(MasterMerchant data);
        Task<object> UpdateMasterMerchant(MasterMerchant data);
        Task<object> UpdateRating(int id);
        Task<object> DeleteMasterMerchant(int id);
    }


    public class SetupMerchantService : ISetupMerchantService
    {

        private SQLConn _db;
        private SetupMerchantDao _dao;
        private SetupUserDao _userDao;

        public SetupMerchantService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupMerchantDao
            {
                db = this._db
            };

            this._userDao = new SetupUserDao
            {
                db = this._db
            };
        }

        public async Task<List<ViewMasterMerchant>> GetAllDataMasterMerchantByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterMerchantByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> VerifyMerchant(MasterMerchant data)
        {
            try
            {
                object hasil = await this._userDao.VerifyMerchant(data.userid);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {

                    messages = "SUCCESS : Data berhasil diverifikasi";

                    //object updateMerchant = await this._userDao.UpdateStatusMerchant(data);

                    //if ((Int32)updateMerchant > 0)
                    //{
                    //    messages = "SUCCESS : Data berhasil diverifikasi";
                    //}
                    //else
                    //{
                    //    throw new Exception("FAIL : Gagal melakukan verifikasi merchant " + data.merchantname);
                    //}
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> AddMasterMerchant(MasterMerchant data)
        {
            this._db.beginTransaction();

            try
            {
                object hasil = await this._dao.AddMasterMerchant(data);

                String messages = string.Empty;

                if ((Int32)hasil > 0)
                {
                    //update foto ktp
                    object updateUser = await this._userDao.UpdateKtp(new UserUpdateKtp
                    {
                        userid = data.userid,
                        identitycardurl = data.identitycardurl
                    });

                    if ((Int32)updateUser > 0)
                    {
                        messages = "SUCCESS : Data berhasil disimpan";
                    }
                    else
                    {
                        throw new Exception("FAIL : Gagal mengubah foto ktp");
                    }

                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
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

        public async Task<object> UpdateMasterMerchant(MasterMerchant data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterMerchant(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateRating(int id)
        {
            try
            {
                object hasil = await this._dao.UpdateRating(id);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteMasterMerchant(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterMerchant(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
    }
}
