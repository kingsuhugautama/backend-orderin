using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    #region PARAMETER UPDATE DATA
    public class UserLogin
    {
        public string phone { get; set; }
        public string password { get; set; }
    }

    public class UserUpdateStatus
    {
        public int? userid { get; set; }
        public string status { get; set; }
    }

    public class UserUpdateKtp
    {
        public int? userid { get; set; }
        public string identitycardurl { get; set; }
    }

    public class UserUpdateBiometric
    {
        public int? userid { get; set; }
        public string biometric { get; set; }
    }

    #endregion
    

    public class Users
    {
        public int? userid { get; set; } //integer()
        public string email { get; set; } //character varying(60)
        public string password { get; set; } //character varying(60)
        public string firstname { get; set; } //character varying(30)
        public string lastname { get; set; } //character varying(30)
        public DateTime? birthdate { get; set; } //date()
        public string region { get; set; } //character varying(10)
        public string gender { get; set; } //character varying(10)
        public string address { get; set; } //character varying()
        public string postalcode { get; set; } //character varying(8)
        public int? cityid { get; set; } //integer()
        public string phone { get; set; } //character varying(18)
        public string job { get; set; } //character varying(20)
        public string avatarurl { get; set; } //character varying()
        public string identitycardurl { get; set; } //character varying()
        public DateTime? createddate { get; set; } //timestamp without time zone()
        public DateTime? lastlogin { get; set; } //timestamp without time zone()
        public DateTime? lastlogout { get; set; } //timestamp without time zone()

        //internal bool isverified { get; set; } //bit(1)
        //internal bool ismerchant { get; set; } //bit(1)
        //internal bool isverifiedmerchant { get; set; } //bit(1)
        
        public string biometric { get; set; } //character varying()
    }

    public class ViewUsers
    {
        public int? userid { get; set; } //integer()
        public string email { get; set; } //character varying(60)
        public string password { get; set; } //character varying(60)
        public string firstname { get; set; } //character varying(30)
        public string lastname { get; set; } //character varying(30)
        public DateTime? birthdate { get; set; } //date()
        public string region { get; set; } //character varying(10)
        public string gender { get; set; } //character varying(10)
        public string address { get; set; } //character varying()
        public string postalcode { get; set; } //character varying(8)
        public int? cityid { get; set; } //integer()
        public string phone { get; set; } //character varying(18)
        public string job { get; set; } //character varying(20)
        public string avatarurl { get; set; } //character varying()
        public string identitycardurl { get; set; } //character varying()
        public DateTime? createddate { get; set; } //timestamp without time zone()
        public DateTime? lastlogin { get; set; } //timestamp without time zone()
        public DateTime? lastlogout { get; set; } //timestamp without time zone()
        public bool isverified { get; set; } //bit(1)
        public bool ismerchant { get; set; } //bit(1)
        public bool isverifiedmerchant { get; set; } //bit(1)
        public string biometric { get; set; } //String(-1)

        #region  informasi merchant
        public int? merchantid { get; set; } //Int32(-1) 
        public string merchantname { get; set; } //String(-1)
        public string description { get; set; } //String(-1)
        public string logoimageurl { get; set; } //String(-1)
        public string coverimageurl { get; set; } //String(-1)
        public decimal? avgratingproduct { get; set; } //Decimal(-1)
        public decimal? avgratingpackaging { get; set; } //Decimal(-1)
        public decimal? avgratingdelivering { get; set; } //Decimal(-1)
        #endregion
    }


}
