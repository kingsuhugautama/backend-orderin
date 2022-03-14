using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrderIn.Models.Bridging_BCA;
using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderIn.Helpers
{
    public class BridgingBcaHelper
    {
        private string tglSekarang = "";

        private string apiKey = "79c729d0-f508-463c-9d49-aa9cf3a24d12";
        private string apiSecKey = "002d42a8-797d-4df0-9c04-280ea9268caf"; //api secret key
        private string clientId = "fbeed5f6-0c65-42ec-b3a1-17dc0127de37";
        private string clientSecKey = "f32e7373-4eb7-47b9-ae93-d2ab703c8fa7"; //client secret key
        private string aksesToken = "";

        public ClassHelper _helper;
        public CryptoHelper _crypto;
        private readonly HttpHelper _http;

        public BridgingBcaHelper(IHttpContextAccessor context)
        {
            this._helper = new ClassHelper(context);
            this._crypto = new CryptoHelper();
            this._http = new HttpHelper();
        }


        public async Task<string> getAccessToken(string clientId = "", string clientSecretId = "", string url = "https://sandbox.bca.co.id/api/oauth/token")
        {
            string result = "";

            try
            {

                //credentials = 
                //{
                //    "grant_type":"client_credentials"
                //}

                var item = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string,string>("grant_type","client_credentials")
                    });


                string authKey = this._helper.convertStringToBase64(clientId + ":" + clientSecretId);

                var headers = new List<RequestHeaderData>
                {
                    new RequestHeaderData
                    {
                        name = "Authorization",
                        value = "Basic " + authKey
                    }
                };


                var response = await this._http.PostData(url, item, headers);


                if (response.IsSuccessStatusCode)
                {
                    var responseStream = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<AccessTokenResponse>(responseStream);
                    result = data.AccessToken;
                    this.aksesToken = data.AccessToken;
                    //this.generateSignature
                }
                else
                {
                    throw new Exception("Gagal fetching ke API BCA");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                result = ex.Message;
            }

            return result;
        }

        public async Task<string> generateSignature(SignatureParams model)
        {
            this.tglSekarang = model.Timestamp;
            model.ApiSecret = this.apiSecKey;

            string Signature = "";
            try
            {
                model.AccessToken = await getAccessToken(clientId, clientSecKey);

                string jsonStringReqBody = this._helper.jsonSerializationCannonicalize(model.RequestBody);

                string hexReqBody = this._helper.encryptHexSHA256(jsonStringReqBody == "\"\"" ? "" : jsonStringReqBody).ToLowerInvariant();
                string StringToSign = model.HttpMethod.ToUpper() + ":" + model.UrlPath + ":" + model.AccessToken + ":" + hexReqBody + ":" + this.tglSekarang;
                Signature = this._helper.hmacSha256Digest(StringToSign, model.ApiSecret);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Signature;
        }

        public async Task<dynamic> TransferUang(SignatureParams model)
        {
            object message = null;

            try
            {
                string signature = "";

                if (model is not null)
                {
                    model.HttpMethod = "POST";
                    model.UrlPath = "/banking/corporates/transfers";

                    signature = await generateSignature(model);

                    FundTransferParams ftp = this._helper.jsonDynamicToObject<FundTransferParams>(model.RequestBody);

                    if (ftp is not null)
                    {
                        var headers = new List<RequestHeaderData> {
                                    new RequestHeaderData
                                    {
                                        name = "Authorization",
                                        value = "Bearer " + this.aksesToken
                                    },new RequestHeaderData
                                    {
                                        name = "X-BCA-Key",
                                        value = this.apiKey
                                    },new RequestHeaderData
                                    {
                                        name = "X-BCA-Timestamp",
                                        value = this.tglSekarang
                                    },new RequestHeaderData
                                    {
                                        name = "X-BCA-Signature",
                                        value = signature
                                    },
                                };

                        string json1 = this._helper.jsonSerializationCannonicalize(model.RequestBody);

                        var content = new StringContent(json1,
                            Encoding.UTF8, "application/json");

                        var response = await this._http.PostData(Properties.Resources.base_url_bca + model.UrlPath,
                            content,
                            headers: headers);

                        message = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            message = JsonConvert.DeserializeObject<FundTransferResponse>(message.ToString());
                        }
                        else
                        {
                            message = JsonConvert.DeserializeObject<ErrorMessageResponse>(message.ToString());
                        }

                        return message;
                    }

                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }
    }
}
