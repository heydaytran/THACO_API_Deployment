using THACO.Web.Service.Constants;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Interfaces.Repo;
using THACO.Web.Service.Interfaces.Service;
using THACO.Web.Service.Model;
using THACO.Web.Service.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace THACO.Web.Service.Service
{
    public class TimeSheetService : CrudBaseService<ITimeSheetRepo, Guid, TimeSheetEntity, TimeSheetDtoEdit>, ITimeSheetService
    {
        public TimeSheetService(ITimeSheetRepo repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
        {
        }
        

        public async Task<DAResult> GetTimeline(FilterTable filterTable)
        {
            return await _repo.GetTimeline(typeof(TimeSheetEntity), filterTable);
        }
        public async Task<DAResult> GetCalculator(FilterTable filterTable)
        {
            return await _repo.GetCalculator(typeof(TimeSheetEntity), filterTable);
        }

        protected override async Task<TimeSheetDtoEdit> GetEditAsync(IDbConnection cnn, Guid id)
        {
            var model = await base.GetEditAsync(cnn, id);
            if (model.timeSheetTargets?.Count > 0)
            {
                model.timeSheetTargets = model.timeSheetTargets.OrderBy(x => x.sort_order).ThenBy(x => x.deadline_checker_date).ToList();
            }

            return model;
        }
    }
}
