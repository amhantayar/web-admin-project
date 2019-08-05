using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreModels;
using KATData.Factory;
using KATData.Models;
using KATData.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KATData.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        public AccountController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<JsonResult>  Validate(UsersModel info )
        {
            var model = new UsersModel()
            {
                userid = info.userid,
                password = info.password
            };
            var response =   ApiClientFactory.Instance.UserLogin(model);
            return Json(response);

            //bool status = false;
            //if (status)
            //{
            //    return Json(new { status = true, message = "Login Successfull!" });

            //   // return Json(new { status = false, message = "Invalid Password!" });
            //}
            //else
            //{
            //    return Json(new { status = false, message = "Invalid Email!" });
            //}
            //var _admin = db.Admins.Where(s => s.Email == admin.Email);
            //if (_admin.Any())
            //{
            //    if (_admin.Where(s => s.Password == admin.Password).Any())
            //    {

            //        return Json(new { status = true, message = "Login Successfull!" });
            //    }
            //    else
            //    {
            //        return Json(new { status = false, message = "Invalid Password!" });
            //    }
            //}
            //else
            //{
            //    return Json(new { status = false, message = "Invalid Email!" });
            //}
        }
    }
}