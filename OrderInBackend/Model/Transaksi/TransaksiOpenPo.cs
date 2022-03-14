using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Transaksi
{

    #region PARAMETER SP

    public class CekExistingDropshipParams{

        public int? dropshipid { get; set; } //integer()
        public string openpodate { get; set; } //DateTime(-1)
    }

    #endregion


    public class TransOpenPoDetailDropshipKategori
    {

        public int? openpodetaildropshipid { get; set; } //integer()
        public int? categorymenuid { get; set; } //integer()
    }


    public class TransOpenPoDetailDropship
    {

        public int? openpodetaildropshipid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public int? dropshipid { get; set; } //integer()
        public string starttime { get; set; } //character varying(10)
        public string endtime { get; set; } //character varying(10)
        public string tolerance { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
        public decimal? ongkoskirim { get; set; } //numeric()
        public bool? iscod { get; set; } //boolean()
        public List<TransOpenPoDetailProduct> products { get; set; }
        public List<TransOpenPoDetailDropshipKategori> kategoris { get; set; }

        public TransOpenPoDetailDropship()
        {
            products = new List<TransOpenPoDetailProduct>();
            kategoris = new List<TransOpenPoDetailDropshipKategori>();
        }
    }


    public class ViewExistingDropship
    {

        public int? openpoheaderid { get; set; } //Int32(-1)
        public DateTime? openpodate { get; set; } //DateTime(-1)
        public DateTime? closepodate { get; set; } //DateTime(-1)
        public string statustransaksi { get; set; } //String(-1)
        public DateTime? waktuentry { get; set; } //DateTime(-1)
        public int? dropshipid { get; set; } //Int32(-1)
        public string starttime { get; set; } //String(-1)
        public string endtime { get; set; } //String(-1)

    }



    public class ViewTransOpenPoDetailDropship
    {

        public int? openpodetaildropshipid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public string starttime { get; set; } //character varying(10)
        public string endtime { get; set; } //character varying(10)
        public string tolerance { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
        public decimal? ongkoskirim { get; set; } //numeric()
        public bool? iscod { get; set; } //boolean()
        public int? dropshipid { get; set; } //integer()
        public string dropshipname { get; set; } //character varying()
        public string dropshipaddress { get; set; } //character varying()
        public string latitude { get; set; } //character varying()
        public string longitude { get; set; } //character varying()
        public decimal? radius { get; set; } //character varying()
        public string contactname { get; set; } //character varying()
        public string contactphone { get; set; } //character varying()
        public string dropshipdesc { get; set; } //character varying()
        public int? merchantid { get; set; } //character varying()
        public string merchantname { get; set; } //character varying()
        public string merchantdesc { get; set; } //character varying()
        public string logoimageurl { get; set; } //character varying()
        public string coverimageurl { get; set; } //character varying()
        public int? categorymenuid { get; set; } //character varying()
        public DateTime? openpodate { get; set; } //timestamp()
        public DateTime? closepodate { get; set; } //timestamp()
        public string statustransaksi { get; set; } //character varying()
    }



    public class ViewTransOpenPoDetailProduct
    {

        public int? openpodetailproductid { get; set; } //integer()
        public int? openpodetaildropshipid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public int? productid { get; set; } //integer()
        public string productname { get; set; } //integer()
        public string productimageurl { get; set; } //integer()
        public decimal? productprice { get; set; } //numeric()
        public decimal? kuotamin { get; set; } //numeric()
        public decimal? kuotamax { get; set; } //numeric()
        public decimal? qtymin { get; set; } //numeric()
        public decimal? qtymax { get; set; } //numeric()
        public decimal? kuota { get; set; } //numeric()
        public int? categorymenuid { get; set; } //integer()
        public bool? isactive { get; set; } //integer()
        public bool? isbutton { get; set; } //integer()
        public decimal? qtyorder { get; set; } //character varying(15)
        public string statustransaksi { get; set; } //character varying()

    }

    public class TransOpenPoDetailProduct
    {

        public int? openpodetailproductid { get; set; } //integer()
        public int? openpodetaildropshipid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public int? productid { get; set; } //integer()
        public decimal? productprice { get; set; } //numeric()
        public decimal? kuotamin { get; set; } //numeric()
        public decimal? kuotamax { get; set; } //numeric
        public decimal? qtymin { get; set; } //numeric()
        public decimal? qtymax { get; set; } //numeric()
        //public bool? isfavorit { get; set; } //integer() tambahan untuk notifikasi firebase

    }

    public class ViewTransOpenPoHeader
    {

        public int? openpoheaderid { get; set; } //integer()
        public DateTime? openpodate { get; set; } //timestamp without time zone()
        public DateTime? closepodate { get; set; } //timestamp without time zone()
        public string kota { get; set; } //character varying(25)
        public string namakota { get; set; } //character varying(25)
        public string statustransaksi { get; set; } //character varying(10)
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public int? merchantid { get; set; } //integer()
        public int? useridmerchant { get; set; } //integer()

    }

    public class TransOpenPoHeader
    {

        public int? openpoheaderid { get; set; } //integer()
        public DateTime? openpodate { get; set; } //timestamp without time zone()
        public DateTime? closepodate { get; set; } //timestamp without time zone()
        public string kota { get; set; } //character varying(25)
        public string statustransaksi { get; set; } //character varying(10)
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public int? merchantid { get; set; } //integer()

        public List<TransOpenPoDetailDropship> dropships { get; set; }

        public TransOpenPoHeader()
        {
            dropships = new List<TransOpenPoDetailDropship>();
        }
    }


    public class TransAbsensiDropship
    {

        public int? openpodetaildropshipid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public string latitude { get; set; } //character varying(20)
        public string longitude { get; set; } //character varying(20)
        public string address { get; set; } //character varying()
        public string foto { get; set; } //character varying()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()

    }




}
