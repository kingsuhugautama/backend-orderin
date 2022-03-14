using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    public class MasterRekening
    {

        public int? idrekening { get; set; } //integer()
        public string norekening { get; set; } //character varying()
        public string namapemilikrekening { get; set; } //character varying()
        public string namabank { get; set; } //character varying()


    }


}
