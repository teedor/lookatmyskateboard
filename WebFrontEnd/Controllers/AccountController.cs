﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using WebFrontEnd.Models.Account;

namespace WebFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var cs = "data source=.;initial catalog=lookatmyskateboard;integrated security=True;";
            using (var cn = new SqlConnection(cs))
            {
                const int usernameColumnIndex = 1;
                const int isAdminColumnIndex = 3;
                cn.Open();
                var sql = "SELECT TOP 1 * FROM Users WHERE username = '" + model.Username + "' AND password = '" + model.Password + "'";
                using (var cmd = new SqlCommand(sql, cn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var userAccount = new UserAccount
                            {
                                Username = reader.GetString(usernameColumnIndex),
                                IsAdmin = reader.GetBoolean(isAdminColumnIndex)
                            };

                            Session["User"] = userAccount;
                        }

                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect username and/or password");
                        return View(model);
                    }
                }
            }

            return View(model);
        }
    }
}