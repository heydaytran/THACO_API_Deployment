using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Model;

namespace THACO.Web.Service.Interfaces.Service
{
    public interface ITimeSheetService : ICrudBaseService<Guid, TimeSheetEntity, TimeSheetDtoEdit>
    {
        Task<DAResult> GetTimeline(FilterTable filterTable);
        Task<DAResult> GetCalculator(FilterTable filterTable);
    }
}
