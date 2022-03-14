using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    #region Model update data

    public class UpdateDropshipStatusActive
    {
        public int? dropshipid { get; set; } //integer()
        public bool? isactive { get; set; } //boolean()
    }

    #endregion

    public class MasterDropship
    {

        public int? dropshipid { get; set; } //integer()
        public int? merchantid { get; set; } //integer()
        public string label { get; set; } //character varying()
        public string longitude { get; set; } //character varying(30)
        public string latitude { get; set; } //character varying(30)
        public string dropshipaddress { get; set; } //character varying()
        public string description { get; set; } //character varying()
        public string contactname { get; set; } //character varying(30)
        public string contactphone { get; set; } //character varying(20)
        public decimal? radius { get; set; } //numeric()
        public bool? isactive { get; set; } //boolean()
        public decimal? ongkoskirim { get; set; } //Decimal(-1)
        public bool? iscod { get; set; } //boolean()
    }


}
