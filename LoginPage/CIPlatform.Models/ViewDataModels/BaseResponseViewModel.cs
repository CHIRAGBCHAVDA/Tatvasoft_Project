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
