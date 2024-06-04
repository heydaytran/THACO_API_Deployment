using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Constants;

namespace THACO.Web.Service.Interfaces.Entities
{
    /// <summary>
    /// Trạng thái bản ghi
    /// Sử dụng cho form chi tiết để xác định bản ghi là thêm/sửa/xóa
    /// </summary>
    public interface IRecordState
    {
        /// <summary>
        /// Trạng thái của bản ghi
        /// </summary>
        ModelState state { get; set; }
    }
}
