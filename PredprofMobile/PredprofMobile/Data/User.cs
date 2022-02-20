using System;
using System.Collections.Generic;
using System.Text;

namespace PredprofMobile.Data
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public List<Akes> akeses { get; set; }

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
