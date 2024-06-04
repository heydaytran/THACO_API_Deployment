using THACO.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using THACO.Web.Service.Model;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Properties;
using Dapper;
using System.Collections;

namespace THACO.Web.Repo.Repo
{
    public class ManufacturerRepo : BaseRepo, IManufacturerRepo
    {
        public ManufacturerRepo(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration, serviceProvider)
        {
        }

    }
}
