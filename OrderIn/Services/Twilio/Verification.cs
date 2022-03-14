using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Base;
using Twilio.Exceptions;
using Twilio.Rest.Serverless.V1.Service.Environment;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Rest.Verify.V2.Service.RateLimit;

namespace OrderIn.Services.Twilio
{
    public interface IVerification
    {
        Task<VerificationResult> StartVerificationAsync(string phoneNumber, string channel);

        Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code);
    }

    public class Verification : IVerification
    {

        private ClassHelper _helper;
        private string _phoneNumber = "";
        private readonly ConfigurationTwilio _config;

        public Verification(ConfigurationTwilio configuration)
        {
            this._helper = new ClassHelper();
            _config = configuration;
            TwilioClient.Init(_config.AccountSid, _config.AuthToken);


            //var limitCek = RateLimitResource.Fetch(
            //    pathServiceSid: _config.VerificationSid,
            //    pathSid = ""
            //);


        }


        public async Task<VerificationResult> StartVerificationAsync(string phoneNumber, string channel)
        {
            try
            {
                string rateLimitSid = "";

                phoneNumber = this._helper.convertPhone(phoneNumber);
                this._phoneNumber = phoneNumber;

                DateTime tgl = DateTime.Now;

                //cek rate limit apakah existing
                var limitCek = RateLimitResource.Read(
                    pathServiceSid: _config.VerificationSid
                ).Where(x => x.UniqueName == phoneNumber.Replace("+", "") + "_" + tgl.ToString("ddMMyyyy")).ToList();

                if (limitCek.Count == 0)
                {
                    //membuat rate limit berdasarkan hp_ddMMyyyyHHmmss
                    var rateLimit = RateLimitResource.Create(
                           description: "Limit on end user " + phoneNumber + " on " + tgl.ToString("yyyy-MM-dd"),
                           uniqueName: phoneNumber.Replace("+", "") + "_" + tgl.ToString("ddMMyyyy"),
                           pathServiceSid: _config.VerificationSid
                       );

                    rateLimitSid = rateLimit.Sid;

                    //set dalam 1 hari maksimal 3x request
                    var bucket = BucketResource.Create(
                        max: 3,
                        interval: 86400,
                        pathServiceSid: _config.VerificationSid,
                        pathRateLimitSid: rateLimitSid
                    );

                }
                else
                {
                    rateLimitSid = limitCek[0].Sid;
                }


                var rateLimits = new Dictionary<string, Object>()
        {
            { phoneNumber.Replace("+", "") + "_" + tgl.ToString("ddMMyyyy"), phoneNumber }
        };

                var verificationResource = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: channel,
                    pathServiceSid: _config.VerificationSid,
                    rateLimits: rateLimits
                );



                return new VerificationResult(verificationResource.Sid);
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }

        public async Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            try
            {
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: _config.VerificationSid
                );
                return verificationCheckResource.Status.Equals("approved") ?
                    new VerificationResult(verificationCheckResource.Sid) :
                    new VerificationResult(new List<string> { "Wrong code. Try again." });
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }


        //public async Task<VerificationResult> GetLogs(string phoneNumber, string code)
        //{
        //    try
        //    {
        //        var log = LogResource.Fetch(
        //            pathServiceSid: _config.VerificationSid,
        //            pathEnvironmentSid: "ZEXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
        //            pathSid: "VEb6d596053a5c60c0a6d55e8dcae46c48"
        //        );

        //        return verificationCheckResource.Status.Equals("approved") ?
        //            new VerificationResult(verificationCheckResource.Sid) :
        //            new VerificationResult(new List<string> { "Wrong code. Try again." });
        //    }
        //    catch (TwilioException e)
        //    {
        //        return new VerificationResult(new List<string> { e.Message });
        //    }
        //}
    }
}