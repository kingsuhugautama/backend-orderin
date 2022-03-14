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
    public interface ISetupUsersService
    {
        Task<List<ViewUsers>> GetAllDataUsersByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewUsers>> GetAllDataUsersByParamsWithLimit(ParameterSearchWithLimit param);
        Task<ViewUsers> GetAllDataUsersByPhoneAndPassword(UserLogin user);
        Task<List<ViewUsers>> GetAllDataUsers();

        Task<object> VerifyUsers(int userid);
        Task<object> AddUsers(Users data);
        Task<object> UpdateStatusLogin(UserUpdateStatus data);
        Task<object> UpdateUsers(Users data);
        Task<object> UpdateBiometric(UserUpdateBiometric data);
        Task<object> UpdatePunishment(int userid);
        Task<object> DeleteUsers(int id);

    }
    public class SetupUserservice : ISetupUsersService
    {
        private readonly SQLConn _db;
        private readonly SetupUserDao _dao;


        public SetupUserservice()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new SetupUserDao
            {
                db = this._db
            };
        }

        public async Task<List<ViewUsers>> GetAllDataUsers()
        {
            try
            {
                return await this._dao.GetAllDataUsers();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<ViewUsers>> GetAllDataUsersByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataUsersByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<ViewUsers>> GetAllDataUsersByParamsWithLimit(ParameterSearchWithLimit param)
        {
            try
            {
                return await this._dao.GetAllDataUsersByParamsWithLimit(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<ViewUsers> GetAllDataUsersByPhoneAndPassword(UserLogin user)
        {
            try
            {
                return await this._dao.GetAllDataUsersByPhoneAndPassword(user);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> VerifyUsers(int userid)
        {
            try
            {
                object hasil = await this._dao.VerifyUser(userid);

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

        public async Task<object> AddUsers(Users data)
        {
            try
            {
                object hasil = await this._dao.AddUsers(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil disimpan";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateUsers(Users data)
        {
            try
            {
                object hasil = await this._dao.UpdateUsers(data);

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

        public async Task<object> DeleteUsers(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteUsers(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public Task<object> UpdateStatusLogin(UserUpdateStatus data)
        {
            try
            {
                return this._dao.UpdateStatusLogin(data);


            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateBiometric(UserUpdateBiometric data)
        {

            try
            {
                object hasil = await this._dao.UpdateBiometric(data);

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
        
        public async Task<object> UpdatePunishment(int userid)
        {

            try
            {
                object hasil = await this._dao.UpdatePunishment(userid);

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
    }
}
