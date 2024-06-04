using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Model;

namespace THACO.Web.Service.Interfaces.Repo
{
    public interface ITimeSheetRepo : IBaseRepo
    {
        Task<DAResult> GetTimeline(Type type, FilterTable filterTable);
        Task<DAResult> GetCalculator(Type type, FilterTable filterTable);
    }
}
