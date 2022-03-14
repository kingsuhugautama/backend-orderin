using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    public class MasterRating
    {
        public int userentry { get; set; } //integer()
        public int merchantid { get; set; } //integer()

        public int deliveryRating { get; set; }
        public int productRating { get; set; }
        public int packageRating { get; set; }

    }

    public class MasterRatingDelivering
    {

        public int? ratingdeliveringid { get; set; } //integer()
        public int? rating { get; set; } //integer()
        public int? userentry { get; set; } //integer()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public int? merchantid { get; set; } //integer()

    }


    public class MasterRatingPackaging
    {

        public int? ratingpackagingid { get; set; } //integer()
        public int? rating { get; set; } //integer()
        public int? userentry { get; set; } //integer()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public int? merchantid { get; set; } //integer()

    }


    public class MasterRatingProduct
    {

        public int? ratingproductid { get; set; } //integer()
        public int? rating { get; set; } //integer()
        public int? userentry { get; set; } //integer()
        public DateTime? waktuentry { get; set; } //timestamp without time zone()
        public int? merchantid { get; set; } //integer()

    }


}
