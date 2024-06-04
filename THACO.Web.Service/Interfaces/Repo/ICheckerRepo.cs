using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Model;

namespace THACO.Web.Service.Interfaces.Repo
{
    public interface ICheckerRepo : IBaseRepo
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="model"></param>
        Task<CheckerEntity> Login(LoginModel model);
    }
}
