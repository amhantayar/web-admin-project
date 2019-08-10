using AspCoreModels; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UltimateTruth.Api.Admin.Communications;

namespace CoreApiClient
{
    public partial class ApiClient
    { 
       public async Task<BaseResponse> GetAllCategoryByCategoryGUID(string categoryGUID,int recordstatus)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "categories/GetAllCategoryByCategoryGUID/" + categoryGUID + "/" + recordstatus));

            return await GetAsync<BaseResponse>(requestUrl);
        }
        public async Task<BaseResponse> GetParentCategoryData(string parentcategoryguid,int recordstatus)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "categories/GetAllParentCategoryData/" + parentcategoryguid + "/" + recordstatus));

            return await GetAsync<BaseResponse>(requestUrl);
        }
        public async Task<BaseResponse>GetAllCategoryDataByRecordStatus(int recordstatus)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "categories/GetAllCategoryByRecordStatus/" + recordstatus));

            return await GetAsync<BaseResponse>(requestUrl);
        }
        public async Task<BaseResponse> GetCategoryByCategoryCode(string categorycode,int recordstatus)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "categories/GetCategoryByCategoryCode/" + categorycode + "/"+recordstatus));

            return await GetAsync<BaseResponse>(requestUrl);
        }
       public async Task<BaseResponse> GetDuplicateCategoryCodeCheckingForUpdate(string categoryguid,string categorycode,int recordstatus)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "categories/GetDuplicateCategoryCodeCheckingForUpdate/" + categoryguid + "/"+ categorycode + "/" + recordstatus)); 
            return await GetAsync<BaseResponse>(requestUrl);
        }
        
        public async Task<BaseResponse> SaveCategory(CategoryModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "categories/PostAsync"));
            return await PostAsync<CategoryModel>(requestUrl, model);
        }
        public async Task<BaseResponse> UpdateCategory(CategoryModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "categories/UpdateAsync"));
            return await PutAsync<CategoryModel>(requestUrl, model);
        }
        public async Task<BaseResponse> DeleteCategory(CategoryModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "categories/DeleteAsync"));
            return await PutAsync<CategoryModel>(requestUrl, model);
        }
    }
}
