using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Setup
{

    public class Kota
    {
        public int? id { get; set; } //integer()
        public int? idprovinsi { get; set; } //integer()
        public string nama { get; set; } //character varying()
    }


    public class Provinsi
    {
        public int? id { get; set; } //integer()
        public string nama { get; set; } //character varying()
    }
}


