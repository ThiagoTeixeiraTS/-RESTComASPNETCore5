using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Minutes { get; set; }
        public string DaysToExpiry { get; set; }
       

    }
}
