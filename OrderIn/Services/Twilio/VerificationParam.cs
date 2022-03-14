using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Services.Twilio
{
    public class VerificationParam
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int userid { get; set; }
    }
}
