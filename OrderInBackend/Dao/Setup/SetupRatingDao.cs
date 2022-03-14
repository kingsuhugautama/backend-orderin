using DapperPostgreeLib;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao.Setup
{
    public class SetupRatingDao
    {
        public SQLConn db;

        public async Task<object> AddMasterRatingDelivering(MasterRating data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterRatingDelivering_InsertData",
                    new
                    {
                        p_rating = data.deliveryRating,
                        p_userentry = data.userentry,
                        p_merchantid = data.merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterRatingPackaging(MasterRating data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterRatingPackaging_InsertData",
                    new
                    {
                        p_rating = data.packageRating,
                        p_userentry = data.userentry,
                        p_merchantid = data.merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> AddMasterRatingProduct(MasterRating data)
        {
            try
            {
                return await this.db.executeScalarSp("MasterRatingProduct_InsertData",
                    new
                    {
                        p_rating = data.productRating,
                        p_userentry = data.userentry,
                        p_merchantid = data.merchantid
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
