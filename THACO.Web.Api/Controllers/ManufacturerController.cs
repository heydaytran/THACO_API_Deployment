using Microsoft.AspNetCore.Mvc;
using THACO.Web.Service.Contexts;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Interfaces.Repo;
using THACO.Web.Service.Interfaces.Service;
using THACO.Web.Service.Model;
using THACO.Web.Service.Properties;

namespace THACO.Web.Api.Controllers
{
    public class ManufacturerController : BaseDictionaryApi<IManufacturerService, Guid, ManufacturerEntity, ManufacturerDtoEdit>
    {
        public ManufacturerController(IManufacturerService bookmarkTypeService, IServiceProvider serviceProvider) : base(bookmarkTypeService, serviceProvider)
        {
        }
    }
}
