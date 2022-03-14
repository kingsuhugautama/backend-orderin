using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Transaksi
{

    #region Parameter
    public class PunishmentRelease{
        public int? userid { get; set; } //integer()
        public int? merchantid { get; set; } //integer()
    }

    public class TransPembayaranVerifikasi
    {

        public int? pembayaranid { get; set; } //integer()
        public int? statuspembayaranid { get; set; } //integer()
    }

    public class TransPembayaranInsertPembayaran
    {

        public int? orderheaderid { get; set; } //integer()
        public int? idrekening { get; set; } //integer()
    }

    public class TransPembayaranUpdatePembayaran
    {

        public int? pembayaranid { get; set; } //integer()
        public int orderheaderid { get; set; } //integer()
        public string noref { get; set; } //character varying()
        public string buktibayarurl { get; set; } //character varying()
        public int? statuspembayaranid { get; set; } //integer()
        public DateTime? waktubayar { get; set; } //timestamp without time zone()

    }


    //response dari insert pembayaran ke tabel
    public class TransPembayaranInsertPembayaranResult
    {

        public int orderheaderid { get; set; } //integer()
        public decimal jumlahbayar { get; set; } //numeric()
        public string rekening { get; set; } //character varying()
        public DateTime waktuentry { get; set; } //timestamp without time zone()
        public DateTime closepodate { get; set; } //timestamp without time zone()
        public int status { get; set; } //integer()
    }


    //new response insert pembayaran modify dari TransPembayaranInsertPembayaranResult
    public class TransPembayaranInsertPembayaranNewResult
    {

        public int orderheaderid { get; set; } //integer()
        public decimal jumlahbayar { get; set; } //numeric()
        public string rekening { get; set; } //character varying()
        public DateTime waktuentry { get; set; } //timestamp without time zone()
        public DateTime closepodate { get; set; } //timestamp without time zone()
        public int pembayaranid { get; set; } //integer()
        public string status { get; set; } //integer()
    }
    #endregion

    public class TransPembayaran
    {

        public int? pembayaranid { get; set; } //integer()
        public int? orderheaderid { get; set; } //integer()
        public decimal? jumlahbayar { get; set; } //numeric()
        public int? digitbayar { get; set; } //integer()
        public int? idrekening { get; set; } //integer()
        public string noinvoice { get; set; } //character varying()
        public string noref { get; set; } //character varying()
        public string buktibayarurl { get; set; } //character varying()
        public int? statuspembayaranid { get; set; } //integer()
        public DateTime? waktubayar { get; set; } //timestamp without time zone()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()

    }

    public class MasterStatusPembayaran
    {

        public int? statuspembayaranid { get; set; } //integer()
        public string keterangan { get; set; } //character varying()

    }


}
