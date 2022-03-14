using DapperPostgreeLib;
using OrderIn.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FcmSharp;
using FcmSharp.Settings;
using FcmSharp.Requests;
using OrderInBackend.Model;
using System.Threading;

namespace OrderInBackend.Service.Utility
{
    public interface INotificationService
    {

    }

    public class NotificationService : INotificationService
    {
        private FcmClient client;
        private readonly SQLConn _db;

        public NotificationService()
        {
            this._db = new SQLConn(new AppConfiguration("DefaultConnection").ConnectionString);

            string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebaseKey.json");


            var settings = FileBasedFcmClientSettings.CreateFromFile(credentialPath);
            this.client = new FcmClient(settings);
        }


    }
}
