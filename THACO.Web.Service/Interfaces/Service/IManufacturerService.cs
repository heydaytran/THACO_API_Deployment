using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Model;

namespace THACO.Web.Service.Interfaces.Service
{
    public interface IManufacturerService : ICrudBaseService<Guid, ManufacturerEntity, ManufacturerDtoEdit>
    { 
    }
}
