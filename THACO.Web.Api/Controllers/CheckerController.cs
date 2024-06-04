using Microsoft.AspNetCore.Mvc;
using THACO.Web.Service.Contexts;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Interfaces.Repo;
using THACO.Web.Service.Interfaces.Service;
using THACO.Web.Service.Model;
using THACO.Web.Service.Properties;
using THACO.Web.Service.Constants;
using THACO.Web.Service.Helpers;

namespace THACO.Web.Api.Controllers
{
    public class CheckerController : BaseDictionaryApi<ICheckerService, Guid, CheckerEntity, CheckerDtoEdit>
    {
        ICheckerService _checkerService;
        public CheckerController(ICheckerService service, IServiceProvider serviceProvider) : base(service, serviceProvider)
        {
            _checkerService = service;
        }
        /// <summary>
        /// Đăng nhập
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var res = await _checkerService.Login(model);
                if (res != null)
                {
                    var actionResult = new DAResult(200, Resources.getDataSuccess, "", res);
                    return Ok(actionResult);
                }
                else
                {
                    var actionResult = new DAResult(204, Resources.noReturnData, "", null);
                    return Ok(actionResult);
                }
            }
            catch (ValidateException exception)
            {
                var actionResult = new DAResult(exception.resultCode, exception.Message, "", exception.DataErr);
                return Ok(actionResult);

            }
            catch (Exception exception)
            {
                //var actionResult = new DAResult(500, Resources.error, exception.Message, null);
                var actionResult = new DAResult(500, exception.StackTrace, exception.Message, null);
                Console.WriteLine(exception.StackTrace);
                return Ok(actionResult);
            }
        }

    }
}
