using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTruth.Api.Admin.Communications
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public object error { get; set; }
        
    }

    public class ResponseError
    {        
        public string error_message { get; set; }
    }
}
