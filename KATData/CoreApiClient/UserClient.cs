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
        public async Task<List<UsersModel>> GetUsers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "User/GetAllUsers"));
            return await GetAsync<List<UsersModel>>(requestUrl);
        }

        public async Task<BaseResponse> UserLogin(UsersModel model)
        { 
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "users/"+model.userid+"/"+model.password));

            return await GetAsync<BaseResponse>(requestUrl);
        }
        
    }
}
