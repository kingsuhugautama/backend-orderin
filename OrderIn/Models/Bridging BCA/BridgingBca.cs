using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Models.Bridging_BCA
{

    #region Response

    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }


    public partial class FundTransferResponse
    {
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceId { get; set; }
        public string Status { get; set; }
    }

    public partial class ErrorMessageResponse
    {
        public string ErrorCode { get; set; }
        public ErrorMessage ErrorMessage { get; set; }
    }

    public partial class ErrorMessage
    {
        public string Indonesian { get; set; }
        public string English { get; set; }
    }

    #endregion


    #region Request

    public class SignatureParams
    {
        public string UrlPath { get; set; } //e.g /general/xxx/xx
        public string HttpMethod { get; set; } //GET/POST/PUT
        public dynamic RequestBody { get; set; } //json string

        internal string UrlGenerate = "https://sandbox.bca.co.id:443/utilities/signature";
        internal string AccessToken { get; set; } //alphanumeric e.g s1w1e12-sa4343ss-dsd23232
        internal string ApiSecret { get; set; } //secret api key
        
        internal string Timestamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            }
        } //yyyy-MM-ddTHH:mm:ss.SSSTZD
    }


    //untuk transfer uang
    public partial class FundTransferParams
    {
        public string CorporateID { get; set; }
        public string SourceAccountNumber { get; set; }
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public string ReferenceID { get; set; }
        public string CurrencyCode { get; set; }
        public string Amount { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
    }

    #endregion
}
