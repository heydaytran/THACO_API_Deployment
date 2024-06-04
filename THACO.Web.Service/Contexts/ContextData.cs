using THACO.Web.Service.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THACO.Web.Service.Contexts
{
    public class ContextData
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        public Guid CheckerId { get; set; }
        public DateTime TokenExpired { get; set; }
        public Role Role { get; set; }
        public string CheckerName { get; set; }
        public string CheckerCode { get; set; }
        public string Email { get; set; }
    }
}
