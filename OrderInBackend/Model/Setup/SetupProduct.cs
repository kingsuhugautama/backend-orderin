using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    #region class Param

    #endregion

    public class FavoritProduct
    {

        public int? productid { get; set; } //integer()
        public int? userid { get; set; } //integer()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()

    }

    #region GRAFIK

    public class PeringkatProduct
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? productid { get; set; } //Int32(-1)
        public string productname { get; set; } //String(-1)
        public long? qty { get; set; } //Int64(-1)

    }


    #endregion

    //for merchant
    public class MasterProduct
    {

        public int? productid { get; set; } //integer()
        public int? merchantid { get; set; } //integer()
        public string merchantname { get; set; } //integer()
        public int? categorymenuid { get; set; } //integer()
        public string productname { get; set; } //character varying(60)
        public string productimageurl { get; set; } //character varying()
        public decimal? productprice { get; set; } //numeric()
        public string productdescription { get; set; } //character varying()
        public bool? isbutton { get; set; } //boolean()
        public bool? isactive { get; set; } //boolean()
        public string productsatuan { get; set; } //character varying(15)
        public decimal? kuota { get; set; } //numeric()
        public decimal? kuotamax { get; set; } //numeric()
        public decimal? kuotamin { get; set; } //numeric()
        public decimal? qtymax { get; set; } //numeric()
        public decimal? qtymin { get; set; } //numeric()
        public Boolean ishalal { get; set; } //Boolean(-1)

    }

    //for user
    public class ViewMasterProduct
    {
        public int? productid { get; set; } //integer()
        public int? merchantid { get; set; } //integer()
        public int? categorymenuid { get; set; } //integer()
        public string productname { get; set; } //character varying(60)
        public string productimageurl { get; set; } //character varying()
        public decimal? productprice { get; set; } //numeric()
        public string productdescription { get; set; } //character varying()
        public bool? isbutton { get; set; } //boolean()
        public bool? isactive { get; set; } //boolean()
        public string productsatuan { get; set; } //character varying(15)
        public decimal? kuota { get; set; } //numeric()
        public decimal? kuotamax { get; set; } //numeric()
        public decimal? kuotamin { get; set; } //numeric()
        public decimal? qtymax { get; set; } //numeric()
        public decimal? qtymin { get; set; } //numeric()
        public Boolean ishalal { get; set; } //Boolean(-1)
        public bool isfavorit{ get; set; } //character varying(15)
        public int userid { get; set; } //character varying(15)
    }

}
