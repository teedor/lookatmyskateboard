using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Account
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}