﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Account
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
    }
}