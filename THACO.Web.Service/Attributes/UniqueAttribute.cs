using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THACO.Web.Service.Attributes
{
    /// <summary>
    /// Kiểm tra dùng để kiểm tra trùng
    /// </summary>
    public class UniqueAttribute : Attribute
    {
        public UniqueAttribute(string name)
        {

        }
        public UniqueAttribute()
        {

        }
    }
}
