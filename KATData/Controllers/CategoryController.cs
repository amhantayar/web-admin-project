using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspCoreModels;
using KATData.Factory;
using KATData.Models;
using KATData.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KATData.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index(CategoryModel info)
        { 
            ViewData["TreeText"] = await CategoryDataBind();
            var response = await ApiClientFactory.Instance.GetAllCategoryDataByRecordStatus(1);
            string dataString = response.data.ToString();
            CategoryModel[] itmlist = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Parent Category",
                Value = "Parent Category"
            });
            foreach (var itm in itmlist)
            {
                items.Add(new SelectListItem
                {
                    Text = itm.CategoryName,
                    Value = itm.CategoryGUID
                });
            }
            ViewData["CatgoryList"] = items;


            if (!String.IsNullOrEmpty(info.CategoryCode) && !String.IsNullOrEmpty(info.CategoryName))
            {
                string statusText = string.Empty;// = false;

                if (String.IsNullOrEmpty(info.CategoryGUID))
                {
                    info.CategoryGUID = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    if (info.ParentCategoryGUID == "Parent Category")
                        info.ParentCategoryGUID = "0";
                   
                        info.RecordStatus = 1;
                      response = await ApiClientFactory.Instance.GetCategoryByCategoryCode(info.CategoryCode, info.RecordStatus);
                      dataString = response.data.ToString();
                    CategoryModel[] itm = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);

                    if (itm.Length > 0)
                    {
                        statusText = "Duplicate Category Code";
                    }
                    else
                    {
                        var response2 = await ApiClientFactory.Instance.SaveCategory(info);
                        if (response2.success)
                        {
                            info = new CategoryModel();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            statusText = "Error on saving";
                        }
                    }
                }
            }
            return View(info);
        }
        
        public async Task<IActionResult> Update(string id)
        {
            ViewData["TreeText"] = await CategoryDataBind();
            var response = await ApiClientFactory.Instance.GetAllCategoryDataByRecordStatus(1);
            string dataString = response.data.ToString();
            CategoryModel[] itmlist = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Parent Category",
                Value = "Parent Category"
            });
            foreach (var itm in itmlist)
            {
                items.Add(new SelectListItem
                {
                    Text = itm.CategoryName,
                    Value = itm.CategoryGUID
                });
            }
            ViewData["CatgoryList"] = items;
             
            string strValue = id;
             response = await ApiClientFactory.Instance.GetAllCategoryByCategoryGUID(id,1);
            dataString = response.data.ToString();
            CategoryModel[] itmlist2 = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);
            CategoryModel model = new CategoryModel();
            foreach (var itm in itmlist2)
            {
                model.CategoryGUID = itm.CategoryGUID;
                model.CategoryCode = itm.CategoryCode;
                model.CategoryName = itm.CategoryName;
                model.ParentCategoryGUID = itm.ParentCategoryGUID;
                model.RecordStatus = itm.RecordStatus;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update_Post(CategoryModel info)
        {
            if (!String.IsNullOrEmpty(info.CategoryCode) && !String.IsNullOrEmpty(info.CategoryName))
            {
                string statusText = string.Empty;// = false; 
                if (!String.IsNullOrEmpty(info.CategoryGUID))
                { 
                    if (info.ParentCategoryGUID == "Parent Category")
                        info.ParentCategoryGUID = "0";

                    info.RecordStatus = 1;
                   var response = await ApiClientFactory.Instance.GetDuplicateCategoryCodeCheckingForUpdate(info.CategoryGUID,info.CategoryCode, info.RecordStatus);
                   string dataString = response.data.ToString();
                    CategoryModel[] itm = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);

                    if (itm.Length > 0)
                    {
                        statusText = "Duplicate Category Code";
                    }
                    else
                    {
                        var response2 = await ApiClientFactory.Instance.UpdateCategory(info);
                        if (response2.success)
                        {
                           return RedirectToAction("Index");
                        }
                        else
                        {
                            statusText = "Error on saving";
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryModel info)
        {
            if (!String.IsNullOrEmpty(info.CategoryCode) && !String.IsNullOrEmpty(info.CategoryName))
            {
                string statusText = string.Empty;// = false; 
                if (!String.IsNullOrEmpty(info.CategoryGUID))
                {  
                    info.RecordStatus = 0;
                    var response2 = await ApiClientFactory.Instance.DeleteCategory(info);
                    if (response2.success)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        statusText = "Error on saving";
                    } 
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<string> CategoryDataBind()
        { 
            var model = new CategoryModel()
            { 
                RecordStatus = 1
            };
            var response = await ApiClientFactory.Instance.GetAllCategoryDataByRecordStatus(1);
            string dataString = response.data.ToString();
            CategoryModel[] itm = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);

            string treeViewText =  GetTree(itm);
            return treeViewText;
        }

        public string GetTree(CategoryModel[] linkList)
        {
            var sb = new StringBuilder();
            sb.Append("<ul  id='tree1'>"); 
            foreach (var link in linkList)
            {
                if (link.ParentCategoryGUID == "0")
                {
                    sb.Append("<li>");
                    sb.Append("<a href='../../Category/Update/" + link.CategoryGUID + "' data-url='apple - iphone - 5s'>" + link.CategoryName + "</a> ");
                    sb.Append(GetChildren(linkList, link.CategoryGUID));
                    sb.Append("</li>");
                }
            }

            sb.Append("</ul>");
            return sb.ToString();
        }

        private string GetChildren(CategoryModel[] linkList,string parentCategoryGUID)
        {
            var sb = new StringBuilder();

            var childList = from m in linkList
                            where m.ParentCategoryGUID == parentCategoryGUID
                            orderby m.CategoryName
                            select m;

            if (childList.Count() > 0)
            {
                sb.Append("<ul>");
                foreach (var link in childList)
                {
                    sb.Append("<li>"); 
                    //<a asp-action="Update" asp-route-id="@p.Id">Update</a>
                   
                    sb.Append("<a href='../../Category/Update/" + link.CategoryGUID +"' data-url='apple - iphone - 5s'>" + link.CategoryName + "</a> ");
                    sb.Append(GetChildren(linkList, link.CategoryGUID));
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            return sb.ToString();
        }
        public async Task<JsonResult> ParentValidateAndSave(CategoryModel info)
        {
            string statusText = string.Empty;// = false;
             var model = new CategoryModel()
            {
                CategoryGUID = info.CategoryGUID,
                CategoryCode = info.CategoryCode,
                CategoryName = info.CategoryName
            };
            if(String.IsNullOrEmpty(model.CategoryGUID))
            {
                model.CategoryGUID = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                model.ParentCategoryGUID = "0";
                model.RecordStatus = 1;
                var response = await ApiClientFactory.Instance.GetCategoryByCategoryCode(model.CategoryCode, model.RecordStatus);
                string dataString = response.data.ToString();
                CategoryModel[] itm = JsonConvert.DeserializeObject<CategoryModel[]>(dataString);

                if (itm.Length > 0)
                {
                    statusText = "Duplicate Category Code"; 
                }
                else
                {

                    var response2 = await ApiClientFactory.Instance.SaveCategory(model);
                    if (response2.success)
                    {
                        statusText = string.Empty;
                    }
                    else
                    {
                        statusText = "Error on saving";
                    }
                }
            }
            if (String.IsNullOrEmpty(statusText))
            {
                return Json(new { status = true, message = "Successfull Saved!" });
            }
            else
            {
                return Json(new { status = false, message = statusText });
            }
        }
    }
}