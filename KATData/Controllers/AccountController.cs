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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            bool status = false;
            var model = new UsersModel()
            {
                userid = info.userid,
                password = info.password
            };
            var response = await  ApiClientFactory.Instance.UserLogin(model); 
            string dataString = response.data.ToString();
            UsersModel[] itm =  JsonConvert.DeserializeObject<UsersModel[]>(dataString);
 
            if(itm.Length >0)
            {
                return Json(new { status = true, message = "Login Successfull!" });
            }
            else
            {
                return Json(new { status = false, message = "Invalid LogIn!" });
            } 
        }
    }
}