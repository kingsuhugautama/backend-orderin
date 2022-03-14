using DapperPostgreeLib;
using OrderIn.Service;
using OrderInBackend.Dao;
using OrderInBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Service
{
    public interface ITableService
    {
        Task<List<Tables>> GetAllTables();
    }

    public class TableService : ITableService
    {
        private SQLConn _db;
        private TableDao _dao;

        public TableService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new TableDao
            {
                db = this._db
            };
        }

        public async Task<List<Tables>> GetAllTables()
        {
            try
            {
                return await this._dao.GetAllTable();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
