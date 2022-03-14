using DapperPostgreeLib;
using OrderInBackend.Model;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupUserDao
    {
        public SQLConn db;

        public async Task<List<ViewUsers>> GetAllDataUsers()
        {
            try
            {
                return await this.db.QuerySPtoList<ViewUsers>("Users_getAllData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewUsers>> GetAllDataUsersByParamsWithLimit(ParameterSearchWithLimit param)
        {
            var filters = string.Empty;
            var limit = Model.Utility.ParameterQuery.GetLimitOffset(param.page,param.limit);

            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param.paramSearch);

                return await this.db.QuerySPtoList<ViewUsers>("users_getdatabydynamicfilterswithlimit",
                    new
                    {
                        filters,    // not null
                        limit.p_limit,
                        limit.p_offset
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewUsers>> GetAllDataUsersByParams(List<Model.ParameterSearchModel> param)
        {
            var filters = string.Empty;
            try
            {
                filters = Model.Utility.ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<ViewUsers>("Users_GetDataByDynamicFilters",
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

        public async Task<ViewUsers> GetAllDataUsersByPhoneAndPassword(UserLogin data)
        {
            try
            {

                return await this.db.QuerySPtoSingle<ViewUsers>("users_getalldatabyphoneandpassword",
                    new
                    {
                        p_phone = data.phone,
                        p_password = data.password
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddUsers(Users data)
        {
            try
            {
                return await this.db.executeScalarSp("users_insertdata",
                    new
                    {
                        p_email = data.email,
                        p_password = data.password,
                        p_firstname = data.firstname,
                        p_lastname = data.lastname,
                        p_birthdate = data.birthdate,
                        p_region = data.region,
                        p_gender = data.gender,
                        p_address = data.address,
                        p_postalcode = data.postalcode,
                        p_cityid = data.cityid,
                        p_phone = data.phone,
                        p_job = data.job,
                        p_avatarurl = data.avatarurl,
                        //p_identitycardurl = data.identitycardurl,
                        //p_isverified = data.isverified,
                        //p_ismerchant = data.ismerchant,
                        //p_isverifiedmerchant = data.isverifiedmerchant,
                        p_biometric = data.biometric
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateUsers(Users data)
        {
            try
            {
                return await this.db.executeScalarSp("Users_UpdateData",
                    new
                    {
                        p_userid = data.userid,
                        p_email = data.email,
                        p_password = data.password,
                        p_firstname = data.firstname,
                        p_lastname = data.lastname,
                        p_birthdate = data.birthdate,
                        p_region = data.region,
                        p_gender = data.gender,
                        p_address = data.address,
                        p_postalcode = data.postalcode,
                        p_cityid = data.cityid,
                        p_phone = data.phone,
                        p_job = data.job,
                        p_avatarurl = data.avatarurl,
                        p_identitycardurl = data.identitycardurl
                        //p_createddate = data.createddate,
                        //p_lastlogin = data.lastlogin,
                        //p_lastlogout = data.lastlogout,
                        //p_isverified = data.isverified,
                        //p_ismerchant = data.ismerchant,
                        //p_isverifiedmerchant = data.isverifiedmerchant,
                        //p_biometric = data.biometric
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateBiometric(UserUpdateBiometric data)
        {
            try
            {
                return await this.db.executeScalarSp("Users_UpdateBiometric",
                    new
                    {
                        p_userid = data.userid,
                        p_biometric = data.biometric
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdatePunishment(int p_userid)
        {
            try
            {
                return await this.db.executeScalarSp("users_UpdatePunishmentCount",
                    new
                    {
                        p_userid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> VerifyUser(int userid)
        {
            try
            {
                return await this.db.executeScalarSp("users_VerifyUser",
                    new
                    {
                        p_userid = userid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> UpdateKtp(UserUpdateKtp data)
        {
            try
            {
                return await this.db.executeScalarSp("users_updatektp",
                    new
                    {
                        p_userid = data.userid,
                        p_identitycardurl = data.identitycardurl
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> VerifyMerchant(int userid)
        {
            try
            {
                return await this.db.executeScalarSp("users_VerifyMerchant",
                    new
                    {
                        p_userid = userid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateStatusMerchant(MasterMerchant data)
        {
            try
            {
                return await this.db.executeScalarSp("users_UpdateStatusMerchant",
                    new
                    {
                        p_userid = data.userid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> DeleteUsers(int userid)
        {
            try
            {
                return await this.db.executeScalarSp("Users_DeleteData",
                    new
                    {
                        p_userid = userid   // int not null
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<object> UpdateStatusLogin(UserUpdateStatus data)
        {
            try
            {
                return await this.db.executeScalarSp("Users_UpdateStatusLogin",
                    new
                    {
                        p_userid = data.userid, // int not null
                        p_status = data.status
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
