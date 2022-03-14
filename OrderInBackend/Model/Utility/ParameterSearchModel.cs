using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderInBackend.Model
{
    public class ParameterSearchModel
    {
        public string columnName { get; set; }
        public string filter { get; set; }
        public string searchText { get; set; }
        public string searchText2 { get; set; }
    }

    public class ParameterSearchWithLimit
    {
        public List<ParameterSearchModel> paramSearch { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }
}
