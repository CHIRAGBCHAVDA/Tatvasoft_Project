using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class BaseResponseViewModel
    {
        public long StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}


//public class BaseResponseModel
//{
//    public string Msg { get; set; }
//    public bool Status { get; set; }
//    public dynamic Data { get; set; } //instead of int directly use Data send 1 from here
//    //standard to work with API
//}