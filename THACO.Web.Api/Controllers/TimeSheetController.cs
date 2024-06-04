using Microsoft.AspNetCore.Mvc;
using THACO.Web.Service.Contexts;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Helpers;
using THACO.Web.Service.Interfaces.Repo;
using THACO.Web.Service.Interfaces.Service;
using THACO.Web.Service.Model;
using THACO.Web.Service.Properties;

namespace THACO.Web.Api.Controllers
{
    public class TimeSheetController : BaseDictionaryApi<ITimeSheetService, Guid, TimeSheetEntity, TimeSheetDtoEdit>
    {
        public TimeSheetController(ITimeSheetService service, IServiceProvider serviceProvider) : base(service, serviceProvider)
        {

        }
        /// <summary>
        /// Lấy dữ liệu timeLine
        /// </summary>
        [HttpPost("timeline")]
        public async Task<IActionResult> GetTimeline([FromBody] FilterTable param)
        {
            var result = await _service.GetTimeline(param);
            return Ok(result);
        }
        /// <summary>
        /// Lấy dữ liệu calculator
        /// </summary>
        [HttpPost("calculator")]
        public async Task<IActionResult> GetCalculator([FromBody] FilterTable param)
        {
            var result = await _service.GetCalculator(param);
            return Ok(result);
        }

        [HttpGet("Export")]
        public override async Task<IActionResult> Export(string? sort)
        {
            var resultData = await _service.GetListAsync(sort);
            if (resultData == null || resultData.Count == 0)
            {
                throw new Exception("nothing to export!!!");
            }
            var streamData = ExportLib<TimeSheetEntity>.ToTemplateExcel((List<TimeSheetEntity>)resultData, typeof(TimeSheetEntity).GetProperties().Select(x => x.Name).ToList(),"/ExcelTemplate/Template_TimeSheetEntity.xlsx");
            return File(streamData, "application/octet-stream", "TimeSheetEntity.xlsx");
        }


    }
}
