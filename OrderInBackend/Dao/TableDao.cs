using DapperPostgreeLib;
using OrderInBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Dao
{
    public class TableDao
    {
        public SQLConn db;

        public async Task<List<Tables>> GetAllTable()
        {
            try
            {
                return await this.db.QuerySQLtoList<Tables>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE = 'BASE TABLE' and table_schema = 'public'");
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
