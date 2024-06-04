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
using THACO.Web.Service.Cruds;

namespace THACO.Web.Service.Service
{
    public class TaiLieuGocService : CrudBaseService<ITaiLieuGocRepo, Guid, TaiLieuGocEntity, TaiLieuGocDtoEdit>, ITaiLieuGocService
    {
        public TaiLieuGocService(ITaiLieuGocRepo repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
        {
        }
    }
}
