using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{
    #region Model update data

    public class UpdateProductStatusActive
    {
        public int? productid { get; set; } //integer()
        public bool? isactive { get; set; } //boolean()
    }

    #endregion

    public class ViewMasterMerchant
    {

        public int? merchantid { get; set; } //integer()
        public int userid { get; set; } //integer()
        public string merchantname { get; set; } //character varying(30)
        public string description { get; set; } //character varying()
        public string logoimageurl { get; set; } //character varying()
        public string coverimageurl { get; set; } //character varying()
        public decimal? avgratingproduct { get; set; } //numeric()
        public decimal? avgratingpackaging { get; set; } //numeric()
        public decimal? avgratingdelivering { get; set; } //numeric()
        public string namabank { get; set; } //character varying(30)
        public string nomorrekening { get; set; } //character varying(50)
        public string namapemilikrekening { get; set; } //character varying(50)
        public string identitycardurl { get; set; } //character varying()

    }

    public class MasterMerchant
    {

        public int? merchantid { get; set; } //integer()
        public int userid { get; set; } //integer()
        public string merchantname { get; set; } //character varying(30)
        public string description { get; set; } //character varying()
        public string logoimageurl { get; set; } //character varying()
        public string coverimageurl { get; set; } //character varying()
        public string namabank { get; set; } //character varying(30)
        public string nomorrekening { get; set; } //character varying(50)
        public string namapemilikrekening { get; set; } //character varying(50)
        public string identitycardurl { get; set; } //character varying()

    }

}
