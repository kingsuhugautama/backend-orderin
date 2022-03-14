using OrderInBackend.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Transaksi
{

    #region UPDATE PARAMETER

    public class TransOrderHeaderUpdateStatus
    {
        public int orderheaderid { get; set; } //integer(),
        public int? statustransorderid { get; set; } //integer()
        public int? userclosed { get; set; } //integer()
    }

    public class TransPengirimanUpdatePenerima
    {
        public int? orderheaderid { get; set; } //integer()
        public string namapenerima { get; set; } //character varying()
        public string fotobuktiterima { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
    }

    #endregion



    #region GRAFIK

    #region OMSET

    public class OmsetPerProduct
    {
        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? productid { get; set; } //Int32(-1)
        public string productname { get; set; } //String(-1)
        public decimal? qty { get; set; } //Decimal(-1)
        public decimal? total { get; set; } //Decimal(-1)
    }


    public class OmsetPerDropship
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? dropshipid { get; set; } //Int32(-1)
        public string label { get; set; } //String(-1)
        public decimal? qty { get; set; } //Decimal(-1)
        public decimal? total { get; set; } //Decimal(-1)

    }

    public class OmsetPerDropshipProduct
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? dropshipid { get; set; } //Int32(-1)
        public string label { get; set; } //String(-1)
        public int? productid { get; set; } //Int32(-1)
        public string productname { get; set; } //String(-1)
        public decimal? qty { get; set; } //Decimal(-1)
        public decimal? total { get; set; } //Decimal(-1)

    }

    public class OmsetPerTanggalPo
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public string tglpo { get; set; } //String(-1)
        public decimal? total { get; set; } //Decimal(-1)

    }

    public class OmsetPerCategory
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? categorymenuid { get; set; } //Int32(-1)
        public string categorymenuname { get; set; } //String(-1)
        public decimal? qty { get; set; } //Decimal(-1)
        public decimal? total { get; set; } //Decimal(-1)

    }


    public class OngkosKirimPerDropship
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? dropshipid { get; set; } //Int32(-1)
        public string label { get; set; } //String(-1)
        public decimal? totalongkoskirim { get; set; } //Decimal(-1)

    }



    public class AnalisaCustomerGender
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public string gender { get; set; } //String(-1)
        public long? jumlah { get; set; } //Int64(-1)

    }


    public class AnalisaCustomerUsia
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public string usia { get; set; } //String(-1)
        public long? jumlah { get; set; } //Int64(-1)

    }


    public class AnalisaCustomerRepeatOrder
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? productid { get; set; } //Int32(-1)
        public string productname { get; set; } //String(-1)
        public long? userbaru { get; set; } //Int64(-1)
        public long? userlama { get; set; } //Int64(-1)

    }


    public class AnalisaCustomerMetodePengirimanPerDropship
    {

        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public int? dropshipid { get; set; } //Int32(-1)
        public string label { get; set; } //String(-1)
        public int? shipmentid { get; set; } //Int32(-1)
        public string keterangan { get; set; } //String(-1)
        public long? jumlah { get; set; } //Int64(-1)

    }



    #endregion


    #endregion


    public class Punishment
    {

        public int? punishmentid { get; set; } //integer()
        public int? userid { get; set; } //integer()
        public string firstname { get; set; } //character varying()
        public string email { get; set; } //character varying()
        public string avatarurl { get; set; } //character varying()
        public int? merchantid { get; set; } //integer()
        public int? orderheaderid { get; set; } //integer()

    }


    public class ViewTransOrderDetail
    {

        public int? orderdetailid { get; set; } //Int32(-1)
        public int? orderheaderid { get; set; } //Int32(-1)
        public int? openpodetailproductid { get; set; } //Int32(-1)
        public decimal? qty { get; set; } //Decimal(-1)
        public string notes { get; set; } //String(-1)
        public int? productid { get; set; } //Int32(-1)
        public string productname { get; set; } //String(-1)
        public string productsatuan { get; set; } //String(-1)
        public string productimageurl { get; set; } //String(-1)
        public string productdescription { get; set; } //String(-1)
        public decimal? productprice { get; set; } //Decimal(-1)
    }

    public class TransOrderDetail
    {

        public int? orderdetailid { get; set; } //integer()
        public int? orderheaderid { get; set; } //integer()
        public int? openpodetailproductid { get; set; } //integer()
        public decimal? qty { get; set; } //numeric()
        public string notes { get; set; } //character varying()
        public decimal? harga { get; set; } //numeric()
        //public decimal? subtotal { get; set; } //numeric()
    }

    public class MasterStatusTransaksiOrder
    {

        public int? statustransorderid { get; set; } //integer()
        public string keterangan { get; set; } //character varying(30)

    }

    public class ViewTransOrderBelumBayar
    {
        public int? orderheaderid { get; set; } //integer()
        public int? userid { get; set; } //integer()
        public int? merchantid { get; set; } //integer()
    }


    public class ViewTransOrderHeader
    {
        public int? orderheaderid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public int? openpodetaildropshipid { get; set; } //integer()
        public int? paymentmethodid { get; set; } //integer()
        public string paymentmethod { get; set; } //string()
        private string NoTransaksi;
        public string notransaksi
        {
            get => NoTransaksi;
            set => NoTransaksi = this._helper.CryptoEncrypt(value, "mat_notransaksi");
        } //character varying(30)
        public int? userentry { get; set; } //integer()
        public string avatarurl { get; set; } //string()
        public string username { get; set; } //string()
        public string email { get; set; } //string()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public string longitude { get; set; } //character varying(20)
        public string latitude { get; set; } //character varying(20)
        public string address { get; set; } //character varying()
        public decimal? total { get; set; } //numeric()
        public string notesaddress { get; set; } //character varying()
        public int? statustransorderid { get; set; } //integer()
        public string statustransaksi { get; set; } //string()
        public int? userclosed { get; set; } //integer()
        public string usernameclosed { get; set; } //string()
        public int statuspembayaranid { get; set; } //int()
        public string statuspembayaran { get; set; } //string()
        public int? merchantid { get; set; } //Int32(-1)
        public string merchantname { get; set; } //String(-1)
        public string coverimageurl { get; set; } //String(-1)
        public string logoimageurl { get; set; } //String(-1)
        public string description { get; set; } //String(-1)

        private CryptoHelper _helper;
        public ViewTransOrderHeader()
        {
            this._helper = new CryptoHelper();
        }

    }

    public class TransOrderHeader
    {

        public int? orderheaderid { get; set; } //integer()
        public int? openpoheaderid { get; set; } //integer()
        public int? openpodetaildropshipid { get; set; } //integer()
        public int? paymentmethodid { get; set; } //integer()
        public string notransaksi { get; set; } //character varying(30)
        public int? userentry { get; set; } //integer()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public string longitude { get; set; } //character varying(20)
        public string latitude { get; set; } //character varying(20)
        public string address { get; set; } //character varying()
        internal decimal? total { get; set; } //numeric()
        public string notesaddress { get; set; } //character varying()
        public int? statustransorderid { get; set; } //integer()

        public List<TransOrderDetail> details { get; set; }

        public TransOrderHeader()
        {
            details = new List<TransOrderDetail>();
        }
    }


    public class TransPengiriman
    {

        public int? pengirimanid { get; set; } //integer()
        public int? orderheaderid { get; set; } //integer()
        public int? shipmentid { get; set; } //integer()
        public string foto { get; set; } //character varying()
        public DateTime? waktukirim { get; set; } //timestamp without time zone()
        public DateTime? waktuterima { get; set; } //timestamp without time zone()
        public string namapenerima { get; set; } //character varying()
        public string fotobuktiterima { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()

    }




}
