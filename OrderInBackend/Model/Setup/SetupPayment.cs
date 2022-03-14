using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    public class MasterPaymentMethod
    {

        public int? paymentmethodid { get; set; } //integer()
        public string keterangan { get; set; } //character varying(20)
    }
}
