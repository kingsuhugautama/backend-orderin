using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Models.SmsManager
{
    public class ApplicationUser : IdentityUser
    {

        public bool Verified { get; set; }

    }
}
