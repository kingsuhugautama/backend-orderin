using DapperPostgreeLib;
using Newtonsoft.Json;
using OrderIn.Service;
using OrderInBackend.Dao.Setup;
using OrderInBackend.Dao.Transaksi;
using OrderInBackend.Model;
using OrderInBackend.Model.Transaksi;
using OrderInBackend.Model.Utility;
using OrderInBackend.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Transaksi
{
    public interface ITransOrderService
    {

        Task<List<ViewTransOrderBelumBayar>> GetAllDataUserBelumBayarByOpenPoHeaderId(int orderheaderid);
        Task<List<ViewTransOrderHeader>> GetAllDataTransOrderHeaderByParams(List<Model.ParameterSearchModel> param);
        Task<List<ViewTransOrderDetail>> GetAllDataTransOrderDetailByParams(List<Model.ParameterSearchModel> param);

        Task<object> AddTransOrderHeader(TransOrderHeader data);
        Task<object> UpdateStatusTransaksi(TransOrderHeaderUpdateStatus data);
        Task<object> DeleteTransOrderHeader(int id);


        #region Status Transaksi

        Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrderByParams(List<Model.ParameterSearchModel> param);
        Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrder();

        Task<object> AddMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data);
        Task<object> UpdateMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data);
        Task<object> DeleteMasterStatusTransaksiOrder(int id);
        #endregion

        #region TRANS ABSENSI DROPSIP
        Task<List<TransAbsensiDropship>> GetAllDataTransAbsensiDropshipByParams(List<Model.ParameterSearchModel> param);
        Task<object> AddTransAbsensiDropship(TransAbsensiDropship data);
        #endregion

        #region TRANS PENGIRIMAN

        Task<List<TransPengiriman>> GetAllDataTransPengirimanByParams(List<Model.ParameterSearchModel> param);
        Task<object> AddTransPengiriman(TransPengiriman data);
        Task<object> UpdatePenerima(TransPengirimanUpdatePenerima data);
        #endregion


        #region Punishment
        Task<List<Punishment>> GetAllDataPunishmentByParams(List<Model.ParameterSearchModel> param);

        Task<object> AddPunishment(Punishment data);
        Task<object> DeletePunishment(PunishmentRelease data);
        #endregion

        #region GRAFIK
        #region OMSET

        Task<List<OmsetPerProduct>> GetOmsetPerProductByParams(List<Model.ParameterSearchModel> param);
        Task<List<OmsetPerCategory>> GetOmsetPerCategoryByParams(List<Model.ParameterSearchModel> param);
        Task<List<OmsetPerDropship>> GetOmsetPerDropshipByParams(List<Model.ParameterSearchModel> param);
        Task<List<OngkosKirimPerDropship>> GetOngkirPerDropshipByParams(List<Model.ParameterSearchModel> param);
        Task<List<OmsetPerDropshipProduct>> GetOmsetPerDropshipProductByParams(List<Model.ParameterSearchModel> param);
        Task<List<OmsetPerTanggalPo>> GetOmsetPerTanggalPoByParams(List<Model.ParameterSearchModel> param);

        Task<List<AnalisaCustomerGender>> GetAnalisaCustomerPerGenderByParams(List<Model.ParameterSearchModel> param);
        Task<List<AnalisaCustomerUsia>> GetAnalisaCustomerPerUsiaByParams(List<ParameterSearchModel> param);
        Task<List<AnalisaCustomerRepeatOrder>> GetAnalisaCustomerRepeatOrderByParams(List<ParameterSearchModel> param);
        Task<List<AnalisaCustomerMetodePengirimanPerDropship>> GetAnalisaCustomerMetodePengirimanPerDropshipByParams(List<ParameterSearchModel> param);

        #endregion

        #endregion
    }

    public class TransOrderService : ITransOrderService
    {
        private readonly SQLConn _db;
        private readonly SetupUserDao _userDao;
        private readonly TransOrderDao _dao;
        private readonly TransOpenPoDao _poDao;
        private ClassHelper _helper;

        public TransOrderService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);
            this._dao = new TransOrderDao
            {
                db = this._db
            };

            this._poDao = new TransOpenPoDao
            {
                db = this._db
            };

            this._userDao = new SetupUserDao
            {
                db = this._db
            };

            this._helper = new ClassHelper();
        }

        #region HEADER

        public async Task<List<ViewTransOrderBelumBayar>> GetAllDataUserBelumBayarByOpenPoHeaderId(int orderheaderid)
        {
            try
            {
                return await this._dao.GetAllDataUserBelumBayarByOpenPoHeaderId(orderheaderid);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<ViewTransOrderHeader>> GetAllDataTransOrderHeaderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransOrderHeaderByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddTransOrderHeader(TransOrderHeader data)
        {
            try
            {
                this._db.beginTransaction();
                data.total = 0;

                foreach (var item in data.details)
                {
                    data.total += Convert.ToDecimal(item.qty * item.harga);
                }

                data.statustransorderid = 1; //OPEN
                object hasil = await this._dao.AddTransOrderHeader(data);

                String messages = string.Empty;
                object detailId = String.Empty;

                if ((Int32)hasil > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.orderheaderid = (Int32)hasil;
                            detailId = await this._dao.AddTransOrderDetail(detail);

                            if ((Int32)detailId == 0)
                                throw new Exception("FAIL : Gagal insert ke tabel detail");
                            else if ((Int32)detailId == -1)
                                throw new Exception("FAIL : Jumlah item tidak boleh melebihi kuota maksimum");
                        }
                    }

                    if ((Int32)hasil > 0 && (Int32)detailId > 0)
                    {
                        var dataPo = await this._poDao.GetAllDataTransOpenPoHeaderByParams(new List<ParameterSearchModel>
                                {
                                    new ParameterSearchModel
                                    {
                                        columnName = "t.openpoheaderid",
                                        filter="equal",
                                        searchText=data.openpoheaderid.ToString(),
                                        searchText2=""
                                    }
                                });

                        if (dataPo.Count > 0)
                        {
                            //push notifikasi ke merchant. (channel : useridmerchant )
                            #region Notifikasi
                            string topic = dataPo[0].useridmerchant.ToString();
                            var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                            {

                                Title = "Pesanan baru telah ditambahkan",
                                Body = "Cek daftar pesanan baru"
                            },
                            data: new Dictionary<string, string>
                            {
                                ["orderheaderid"] = hasil.ToString(),
                                ["openpoheaderid"]=data.openpoheaderid.ToString(),
                                ["merchantid"]= dataPo[0].merchantid.ToString(),
                                ["statusorder"] = "1",
                                ["idnotifikasi"] = "2"
                            });
                        }
                        else
                        {
                            //kondisi ketika tidak dapat push notifikasi karena merchant yang bersangkutan dgn PO tidak ditemukan
                            throw new Exception("FAIL : Merchant tidak ditemukan");
                        }

                        #endregion

                        messages = "SUCCESS : Data berhasil disimpan";
                    }
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Pre Order sudah berakhir";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
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


        public async Task<object> UpdateStatusTransaksi(TransOrderHeaderUpdateStatus data)
        {
            try
            {
                object hasil = await this._dao.UpdateStatusTransaksi(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    if (data.statustransorderid == 5)
                    {
                        //notifikasi ketika status order dirubah menjadi closed. (channel : userid)
                        #region Notifikasi
                        var param = new List<ParameterSearchModel>
                                            {
                                               new ParameterSearchModel
                                               {
                                                   columnName = "t.orderheaderid",
                                                   filter = "equal",
                                                   searchText = data.orderheaderid.ToString(),
                                                   searchText2 = ""
                                               }
                                            };

                        var dataOrder = await this._dao.GetAllDataTransOrderHeaderByParams(param);

                        if (dataOrder.Count > 0)
                        {
                            string topic = dataOrder[0].userentry.ToString();
                            var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                            {

                                Title = "Pesanan selesai",
                                Body = "Berikan rating untuk penjual"
                            },
                            data: new Dictionary<string, string>
                            {
                                ["orderheaderid"] = data.orderheaderid.ToString(),
                                ["statusorder"] = "5",
                                ["idnotifikasi"] = "6"
                            });
                        }
                        #endregion
                    }

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
        public async Task<object> DeleteTransOrderHeader(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteTransOrderHeader(id);
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

        #region DETAIL
        public async Task<List<ViewTransOrderDetail>> GetAllDataTransOrderDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransOrderDetailByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        #endregion

        #region STATUS TRANSAKSI
        public async Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataMasterStatusTransaksiOrderByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<MasterStatusTransaksiOrder>> GetAllDataMasterStatusTransaksiOrder()
        {
            try
            {
                return await this._dao.GetAllDataMasterStatusTransaksiOrder();
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<object> AddMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data)
        {
            try
            {
                object hasil = await this._dao.AddMasterStatusTransaksiOrder(data);

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

        public async Task<object> UpdateMasterStatusTransaksiOrder(MasterStatusTransaksiOrder data)
        {
            try
            {
                object hasil = await this._dao.UpdateMasterStatusTransaksiOrder(data);

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

        public async Task<object> DeleteMasterStatusTransaksiOrder(int id)
        {
            try
            {
                object hasil = await this._dao.DeleteMasterStatusTransaksiOrder(id);
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
        #region TRANS ABSENSI DROPSIP
        public async Task<object> AddTransAbsensiDropship(TransAbsensiDropship data)
        {
            try
            {
                this._db.beginTransaction();
                object hasil = await this._dao.AddTransAbsensiDropship(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    var param = new List<ParameterSearchModel>
                    {
                        new ParameterSearchModel
                        {
                            columnName = "t.openpoheaderid",
                            filter = "equal",
                            searchText = data.openpoheaderid.ToString(),
                            searchText2 = ""
                        },
                        new ParameterSearchModel
                        {
                            columnName = "t.openpodetaildropshipid",
                            filter = "equal",
                            searchText = data.openpodetaildropshipid.ToString(),
                            searchText2 = ""
                        }
                    };

                    var blmBayar = await this._dao.GetAllDataUserBelumBayarByOpenPoHeaderId((Int32)data.openpoheaderid);


                    //lakukan punishment terhadap user yang belum bayar
                    if (blmBayar.Count > 0)
                    {
                        foreach (var item in blmBayar)
                        {

                            var punishment = await this._dao.AddPunishment(new Punishment
                            {
                                merchantid = item.merchantid,
                                orderheaderid = item.orderheaderid,
                                userid = item.userid,
                                punishmentid = 0
                            });

                            if ((Int32)punishment <= 0)
                            {
                                throw new Exception("FAIL : Gagal melakukan punishment");
                            }
                            else
                            {
                                var updatePunishmentCount = await this._userDao.UpdatePunishment((Int32)item.userid);

                                if ((Int32)updatePunishmentCount <= 0)
                                {
                                    throw new Exception("FAIL : Gagal update jumlah punishment");
                                }
                            }


                        }
                    }


                    var daftarOrderan = await this._dao.GetAllDataTransOrderHeaderByParams(param);


                    object statusTrans = String.Empty;

                    if (daftarOrderan.Count > 0)
                    {

                        foreach (var orderan in daftarOrderan)
                        {
                            var order = new TransOrderHeaderUpdateStatus
                            {
                                orderheaderid = (Int32)orderan.orderheaderid,
                                statustransorderid = 2 //CONFIRMED
                            };
                            statusTrans = await this._dao.UpdateStatusTransaksi(order);

                            if ((Int32)statusTrans > 0)
                            {
                                //notif ke customer yang sudah bayar ketika absensi dropship (channel : userid user yang telah order)
                                #region Notifikasi

                                var sdhBayar = await this._dao.GetAllDataUserSudahBayarByOpenPoHeaderId((Int32)data.openpoheaderid); ;

                                if (sdhBayar.Count > 0)
                                {
                                    foreach (var user in sdhBayar)
                                    {
                                        string topic = user.userid.ToString();
                                        var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                                        {

                                            Title = "Pesanan dikonfirmasi",
                                            Body = "Penjual telah sampai di titik dropship"
                                        },
                            data: new Dictionary<string, string>
                            {
                                ["orderheaderid"] = "0",
                                ["statusorder"] = "2",
                                ["idnotifikasi"] = "3"
                            });

                                    }
                                }

                                #endregion
                            }
                        }
                    }

                    if ((Int32)hasil > 0 && (Int32)statusTrans > 0)
                    {
                        messages = "SUCCESS : Data berhasil disimpan";
                    }
                    else
                        throw new Exception("FAIL : Gagal update status transaksi");
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Tidak dapat melakukan absensi karena open po belum selesai";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
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

        public async Task<List<TransAbsensiDropship>> GetAllDataTransAbsensiDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransAbsensiDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion

        #region TRANS PENGIRIMAN
        public async Task<List<TransPengiriman>> GetAllDataTransPengirimanByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataTransPengirimanByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> AddTransPengiriman(TransPengiriman data)
        {
            try
            {
                this._db.beginTransaction();
                object hasil = await this._dao.AddTransPengiriman(data);

                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    var updateStatus = new TransOrderHeaderUpdateStatus
                    {
                        orderheaderid = (Int32)data.orderheaderid,
                        statustransorderid = data.shipmentid == 1 ? 5 : 3 // 5 = CLOSED / 3 = ON PROCESS
                    };

                    object pengiriman = await this._dao.UpdateStatusTransaksi(updateStatus);

                    if ((Int32)pengiriman > 0 && data.shipmentid != 1) //jika bukan cod
                    {
                        //notifikasi ketika merchant mengirim pesanan. (channel : userid pelanggan order)
                        #region Notifikasi

                        var param = new List<ParameterSearchModel>
                                            {
                                               new ParameterSearchModel
                                               {
                                                   columnName = "t.orderheaderid",
                                                   filter = "equal",
                                                   searchText = data.orderheaderid.ToString(),
                                                   searchText2 = ""
                                               }
                                            };

                        var dataOrder = await this._dao.GetAllDataTransOrderHeaderByParams(param);

                        if (dataOrder.Count > 0)
                        {
                            string topic = dataOrder[0].userentry.ToString();
                            var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                            {

                                Title = "Pesanan dikirim",
                                Body = "Pesanan sedang diantar ke lokasi tujuan"
                            },
                            data: new Dictionary<string, string>
                            {
                                ["orderheaderid"] = data.orderheaderid.ToString(),
                                ["statusorder"] = data.shipmentid == 1 ? "5" : "3", // 5 = CLOSED / 3 = ON PROCESS,
                                ["idnotifikasi"] = "4"
                            });
                        }

                        #endregion

                        messages = "SUCCESS : Data berhasil disimpan";
                    }
                    else
                        throw new Exception("FAIL : Gagal update status transaksi");
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal insert ke tabel";
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

        //ketika pesanan tiba ke customer
        public async Task<object> UpdatePenerima(TransPengirimanUpdatePenerima data)
        {
            try
            {
                this._db.beginTransaction();
                object hasil = await this._dao.UpdatePenerima(data);


                String messages = string.Empty;
                if ((Int32)hasil > 0)
                {
                    var updateStatus = new TransOrderHeaderUpdateStatus
                    {
                        orderheaderid = (Int32)data.orderheaderid,
                        statustransorderid = 4 //DELIVERED
                    };

                    object statusTrans = await this._dao.UpdateStatusTransaksi(updateStatus);

                    if ((Int32)statusTrans > 0)
                    {
                        //notifikasi ketika pesanan sudah tiba tapi belum dikonfirmasi (channel : userid yang order)
                        #region Notifikasi

                        var param = new List<ParameterSearchModel>
                                            {
                                               new ParameterSearchModel
                                               {
                                                   columnName = "t.orderheaderid",
                                                   filter = "equal",
                                                   searchText = data.orderheaderid.ToString(),
                                                   searchText2 = ""
                                               }
                                            };

                        var dataOrder = await this._dao.GetAllDataTransOrderHeaderByParams(param);

                        if (dataOrder.Count > 0)
                        {
                            string topic = dataOrder[0].userentry.ToString();
                            var notif = await this._helper.PushFcmNotification(topic, new NotificationProperty()
                            {

                                Title = "Pesanan telah tiba",
                                Body = "Mohon lakukan konfirmasi pesanan telah diterima",
                                ChannelId = "4",
                                ClickAction = data.orderheaderid.ToString()
                            },
                            data: new Dictionary<string, string>
                            {
                                ["orderheaderid"] = data.orderheaderid.ToString(),
                                ["statusorder"] = "4",
                                ["idnotifikasi"] = "5"
                            });
                        }

                        #endregion

                        messages = "SUCCESS : Data berhasil diupdate";
                    }
                    else
                        throw new Exception("FAIL : Gagal update status transaksi");
                }
                else if ((Int32)hasil == -1)
                {
                    messages = "FAIL : Data ini sudah ada dalam database";
                }
                else
                {
                    messages = "FAIL : Gagal update ke tabel";
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
        #endregion




        #region Punishment

        public async Task<List<Punishment>> GetAllDataPunishmentByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllDataPunishmentByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<object> AddPunishment(Punishment data)
        {
            try
            {
                object hasil = await this._dao.AddPunishment(data);

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


        public async Task<object> DeletePunishment(PunishmentRelease data)
        {
            try
            {
                object hasil = await this._dao.DeletePunishment(data);
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



        #region GRAFIK

        #region OMSET


        public async Task<List<OmsetPerProduct>> GetOmsetPerProductByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOmsetPerProductByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<OmsetPerCategory>> GetOmsetPerCategoryByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOmsetPerCategoryByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<OmsetPerDropship>> GetOmsetPerDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOmsetPerDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }
        public async Task<List<OmsetPerDropshipProduct>> GetOmsetPerDropshipProductByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOmsetPerDropshipProductByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<OngkosKirimPerDropship>> GetOngkirPerDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOngkirPerDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion

        #region ANALISA CUSTOMER


        public async Task<List<AnalisaCustomerGender>> GetAnalisaCustomerPerGenderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAnalisaCustomerPerGenderByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<AnalisaCustomerUsia>> GetAnalisaCustomerPerUsiaByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAnalisaCustomerPerUsiaByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<AnalisaCustomerRepeatOrder>> GetAnalisaCustomerRepeatOrderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAnalisaCustomerRepeatOrderByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }


        public async Task<List<AnalisaCustomerMetodePengirimanPerDropship>> GetAnalisaCustomerMetodePengirimanPerDropshipByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAnalisaCustomerMetodePengirimanPerDropshipByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        public async Task<List<OmsetPerTanggalPo>> GetOmsetPerTanggalPoByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetOmsetPerTanggalPoByParams(param);
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO : log error
            }
        }

        #endregion

        #endregion
    }
}
