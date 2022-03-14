using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{
    public class ViewBannerMenu
    {
        public int? bannermenuid { get; set; } //integer()
        public string bannermenuname { get; set; } //character varying(30)
        public string bannerimageurl { get; set; } //character varying()
        public int? cityid { get; set; } //integer()
        public string cityname { get; set; } //character varying()

    }

    public class BannerMenu
    {
        public int? bannermenuid { get; set; } //integer()
        public string bannermenuname { get; set; } //character varying(30)
        public string bannerimageurl { get; set; } //character varying()
        public int? cityid { get; set; } //integer()

    }


    public class MasterCategoryMenu
    {

        public int? categorymenuid { get; set; } //integer()
        public string categorymenuname { get; set; } //character varying(30)
        public string categoryimageurl { get; set; } //character varying
    }


    public class ViewPromoMenu
    {
        public int? promomenuid { get; set; } //integer()
        public int? productid { get; set; } //integer()
        public string productname { get; set; } //character varying()
        public string productimageurl { get; set; } //character varying()
        public string promoanimationurl { get; set; } //character varying()
        public int? cityid { get; set; } //integer()
        public string cityname { get; set; } //character varying()
    }

    public class PromoMenu
    {
        public int? promomenuid { get; set; } //integer()
        public int? productid { get; set; } //integer()
        public string promoanimationurl { get; set; } //character varying()
        public int? cityid { get; set; } //integer()
    }


}
