using System;
using System.Collections.Generic;
using System.Text;

namespace PredprofMobile.Data
{
    public class LoginResponse
    {
        public bool Successfully { get; set; }
        public User user { get; set; }
    }
}
