using DapperPostgreeLib;
using FcmSharp;
using FcmSharp.Requests;
using FcmSharp.Settings;
using OrderIn.Service;
using OrderInBackend.Dao.Setup;
using OrderInBackend.Dao.Transaksi;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Model.Utility;
using OrderInBackend.Service.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Transaksi
{
    public interface ITransOpenPoService
    {

        #region TRANS PO HEADER
        Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeaderByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeader();

        Task<object> AddTransOpenPoHeader(TransOpenPoHeader data);
        Task<object> UpdateTransOpenPoHeader(TransOpenPoHeader data);
        Task<object> DeleteTransOpenPoHeader(int id);

        #endregion

        #region TRANS PO DETAIL PRODUK

        Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProductByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProduct();

        Task<object> AddTransOpenPoDetailProduct(TransOpenPoDetailProduct data);
        Task<object> UpdateTransOpenPoDetailProduct(TransOpenPoDetailProduct data);
        Task<object> DeleteTransOpenPoDetailProduct(int id);

        #endregion

        #region TRANS PO DETAIL DROPSHIP

        Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropshipByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropship();
        Task<List<ViewExistingDropship>> GetExistingDropshipByParams(CekExistingDropshipParams model);

        Task<object> AddTransOpenPoDetailDropship(TransOpenPoDetailDropship data);
        Task<object> UpdateTransOpenPoDetailDropship(TransOpenPoDetailDropship data);
        Task<object> DeleteTransOpenPoDetailDropship(int id);
        #endregion

    }

    public class TransOpenPoService : ITransOpenPoService
    {
        private ClassHelper _helper;
        private readonly SQLConn _db;
        private readonly TransOpenPoDao _dao;
        private readonly SetupProductDao _productDao;

        public TransOpenPoService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new TransOpenPoDao
            {
                db = this._db
            };
            this._productDao = new SetupProductDao
            {
                db = this._db
            };

            this._helper = new ClassHelper();
        }

        #region TRANS PO HEADER

        public async Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeaderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoHeaderByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewTransOpenPoHeader>> GetAllDataTransOpenPoHeader()
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoHeader();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddTransOpenPoHeader(TransOpenPoHeader data)
        {

            String messages = string.Empty;

            object detailProductId = string.Empty;
            object detailDropshipId = string.Empty;

            this._db.beginTransaction();
            try
            {
                object headerId = await this._dao.AddTransOpenPoHeader(data);

                if ((Int32)headerId > 0)
                {
                    if (data.dropships.Count > 0)
                    {

                        //if (data.dropships.Count > 1)
                        //{
                        //    throw new Exception("FAIL : Jumlah dropship tidak boleh lebih dari 1");
                        //}
                        //else
                        //{
                        foreach (var dropship in data.dropships)
                        {
                            dropship.openpoheaderid = (Int32)headerId;
                            detailDropshipId = await this._dao.AddTransOpenPoDetailDropship(dropship);


                            if ((Int32)detailDropshipId > 0)
                            {

                                if (dropship.kategoris.Count > 0)
                                {
                                    foreach (var kategori in dropship.kategoris)
                                    {
                                        kategori.openpodetaildropshipid = (Int32)detailDropshipId;
                                        object inputKategori = await this._dao.AddTransOpenPoDetailDropshipKategori(kategori);

                                        if ((Int32)inputKategori <= 0)
                                        {
                                            throw new Exception("FAIL : Gagal input detail dropship kategori");
                                        }

                                    }
                                }


                                if (dropship.products.Count > 0)
                                {
                                    foreach (var produk in dropship.products)
                                    {
                                        produk.openpoheaderid = (Int32)headerId;
                                        produk.openpodetaildropshipid = (Int32)detailDropshipId;
                                        detailProductId = await this._dao.AddTransOpenPoDetailProduct(produk);

                                        if ((Int32)detailProductId <= 0)
                                        {
                                            throw new Exception("FAIL : Gagal input detail product");
                                        }
                                        else
                                        {
                                            var param = new List<ParameterSearchModel>
                                            {
                                               new ParameterSearchModel
                                               {
                                                   columnName = "productid",
                                                   filter = "equal",
                                                   searchText = produk.productid.ToString(),
                                                   searchText2 = ""
                                               }
                                            };

                                            var productFavorit = await this._productDao.GetAllDataFavoritProductByParams(param);

                                            //notifikasi ketika open po baru dan pemberitahuan bahwa produk favorit telah dibuka open PO. (channel : userid )
                                            if (productFavorit.Count > 0)
                                            {
                                                foreach (var favorit in productFavorit)
                                                {
                                                    #region Notifikasi

                                                    string topic = favorit.userid.ToString();
                                                    var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                                                    {

                                                        Title = "Pre Order baru telah ditambahkan",
                                                        Body = "Produk favorit anda baru saja ditambahkan dalam Pre Order"
                                                    },
                                                    data: new Dictionary<string, string>
                                                    {
                                                        ["openpoheaderid"]= headerId.ToString(),
                                                        ["merchantid"] = data.merchantid.ToString(),
                                                        ["idnotifikasi"]="1"
                                                    });

                                                    #endregion
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        //}
                    }


                    if ((Int32)headerId > 0 && (Int32)detailProductId > 0 && (Int32)detailDropshipId > 0)
                    {
                        messages = "SUCCESS : Data berhasil disimpan";
                    }
                    else
                    {
                        if ((Int32)detailProductId == 0)
                        {
                            messages = "FAIL : Gagal insert ke tabel detail produk";
                        }

                        if ((Int32)detailDropshipId == 0)
                        {
                            messages = "FAIL : Gagal insert ke tabel detail dropship";
                        }
                    }
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel header";
                }

                this._db.commitTrans();
                return (object)messages;
            }
            catch (Exception ex)
            {
                this._db.rollBackTrans();
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateTransOpenPoHeader(TransOpenPoHeader data)
        {
            try
            {
                object hasil = await this._dao.UpdateTransOpenPoHeader(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteTransOpenPoHeader(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteTransOpenPoHeader(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion

        #region TRANS PO DETAIL PRODUK

        public async Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProductByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoDetailProductByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewTransOpenPoDetailProduct>> GetAllDataTransOpenPoDetailProduct()
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoDetailProduct();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddTransOpenPoDetailProduct(TransOpenPoDetailProduct data)
        {
            try
            {
                object hasil = await this._dao.AddTransOpenPoDetailProduct(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil disimpan";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateTransOpenPoDetailProduct(TransOpenPoDetailProduct data)
        {
            try
            {
                object hasil = await this._dao.UpdateTransOpenPoDetailProduct(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteTransOpenPoDetailProduct(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteTransOpenPoDetailProduct(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion

        #region TRANSPO DETAIL DROPSHIP

        public async Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoDetailDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<ViewTransOpenPoDetailDropship>> GetAllDataTransOpenPoDetailDropship()
        {
            try
            {
                return await this._dao.GetAllDataTransOpenPoDetailDropship();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<ViewExistingDropship>> GetExistingDropshipByParams(CekExistingDropshipParams model)
        {
            try
            {
                return await this._dao.GetExistingDropshipByParams(model);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> AddTransOpenPoDetailDropship(TransOpenPoDetailDropship data)
        {
            try
            {
                object hasil = await this._dao.AddTransOpenPoDetailDropship(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil disimpan";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> UpdateTransOpenPoDetailDropship(TransOpenPoDetailDropship data)
        {
            try
            {
                object hasil = await this._dao.UpdateTransOpenPoDetailDropship(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    messages = "SUCCESS : Data berhasil diupdate";
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
                }

                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> DeleteTransOpenPoDetailDropship(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteTransOpenPoDetailDropship(id);
                String messages = (Convert.ToBoolean(hasil) == true) ? "SUCCESS : Data berhasil dihapus" : "FAIL : Gagal Hapus ke tabel";
                return (object)messages;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion




    }
}
