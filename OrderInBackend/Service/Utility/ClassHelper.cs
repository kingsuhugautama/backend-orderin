using FcmSharp;
using FcmSharp.Requests;
using FcmSharp.Responses;
using FcmSharp.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderInBackend.Model.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OrderInBackend.Service.Utility
{
    public class ClassHelper
    {

        private FcmClient client;

        public ClassHelper()
        {
            string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebaseKey.json");

            var settings = FileBasedFcmClientSettings.CreateFromFile(credentialPath);
            this.client = new FcmClient(settings);
        }


        //convert string to base64
        public string convertStringToBase64(string value)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        public object ConvertListToDictionaryString(object obj)
        {
            var jsonString = JsonConvert.SerializeObject(obj)
                .Replace(":\"",":")
                .Replace("\",\"", ",\"")
                .Replace("\"}", "}")
                .Replace("\":","\":\"")
                .Replace(",\"","\",\"")
                .Replace("}", "\"}")
                .Replace(":\"//","://");

            var result = JsonConvert.DeserializeObject<object>(jsonString);

            return result;
        }

        bool cekkDictionaryKey(Dictionary<string,string> data,string key)
        {
            string value;
            bool keyExists = false;

            if (data != null)
            {
                keyExists = data.TryGetValue(key, out value);
            }

            return keyExists;
        }

        public async Task<object> PushFcmNotification(string topic,NotificationProperty property,dynamic data = null)
        {
            object result = null;
            var empty = "";

            try
            {
                #region Notifikasi
                var message = new FcmMessage()
                {
                    Message = new Message()
                    {
                        //Notification = new FcmSharp.Requests.Notification
                        //{
                        //    Title = property.Title,
                        //    Body = property.Body
                        //},
                        AndroidConfig = new AndroidConfig()
                        {
                            //Notification = new AndroidNotification()
                            //{
                            //    Title = property.Title,
                            //    Body = property.Body,
                            //    Icon = property.Icon,
                            //    ChannelId = property.ChannelId,
                            //    ClickAction = property.ClickAction,
                            //}
                            Data = new Dictionary<string, string>
                            {
                                ["title"] = property.Title,
                                ["body"] = property.Body,
                                ["click_action"] = property.ClickAction,
                                ["android_channel_id"] = property.ChannelId,
                                ["orderheaderid"] = cekkDictionaryKey(data,"orderheaderid") ? data["orderheaderid"]:null,
                                ["statusorder"] = cekkDictionaryKey(data, "statusorder") ? data["statusorder"] : null
                            }
                        },
                        WebpushConfig = new WebpushConfig()
                        {
                            FcmOptions = new WebpushFcmOptions()
                            {
                                Link = "https://google.com"
                            },
                            Notification = new WebpushNotification()
                            {
                                Title = property.Title,
                                Body = property.Body,
                                Icon = property.Icon
                            }
                        },
                        //Data = data, //berupa dictionary string
                        Topic = topic
                    }
                };

                CancellationTokenSource cts = new CancellationTokenSource();
               result = await this.client.SendAsync(message, cts.Token);

                #endregion
            }catch(Exception ex)
            {
                throw ex;
            }

            return result;
            
        }


        //encript data to sha1
        public string encryptSHA1(string Data, string secretKey = "mat_orderin")
        {
            SHA1 sh = new SHA1Managed();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] original = encoder.GetBytes(Data + secretKey + "_data");
            Byte[] encoded = sh.ComputeHash(original);
            Data = BitConverter.ToString(encoded).Replace("-", "");
            var result = Data.ToLowerInvariant();

            return result;
        }
    }
}
