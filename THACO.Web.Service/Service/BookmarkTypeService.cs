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
    public class BookmarkTypeService : CrudBaseService<IBookmarkTypeRepo, Guid, BookmarkTypeEntity, BookmarkTypeDtoEdit>, IBookmarkTypeService
    {
        public BookmarkTypeService(IBookmarkTypeRepo repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
        {
        }
        protected override async Task ValidateDeleteAssync(IDbConnection cnn, DeleteParameter<Guid, BookmarkTypeEntity> parameter, BookmarkTypeEntity model)
        {
            var exist = await _repo.GetAsync<TimeSheetEntity>(nameof(TimeSheetEntity.bookmark_type_id), model.bookmark_type_id);
            if (exist?.Count > 0)
            {
                throw new BusinessException
                {
                    ErrorCode = ErorrCodes.Valdiate,
                    ErrorData = new ValidateResult
                    {
                        Type = ValidateResultType.Generation,
                        Data = new
                        {
                            Code = model.bookmark_type_code,
                            Data = exist.Select(x => x.time_sheet_code).ToList()
                        }
                    }
                };
            }
        }
    }
}
