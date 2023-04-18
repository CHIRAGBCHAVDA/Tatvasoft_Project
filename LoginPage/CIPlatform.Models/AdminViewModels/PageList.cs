using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class PageList<T> where T : class
    {
        public List<T> Records = new List<T>();
        public int Count;
    }
}
