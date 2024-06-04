using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Constants;
using THACO.Web.Service.Contexts;
using THACO.Web.Service.Interfaces.Entities;

namespace THACO.Web.Service.Model
{
    public class CheckerDtoEdit : CheckerEntity, IRecordState, IRecordVersion //, IImportModel
    {
        public ModelState state { get; set; }
        public long RecordVersion { get; set; }
        //public List<ErrorDetail> ErrorDetails { get; set; }
        //public int ImportValidateStatus { get; set; }
        //public int? RowImportIndex { get; set; }
        //public bool IsUpdateRedudant { get; set; }
        //public IDictionary<string, object> OldData { get; set; }
        //public int? HorizontalDetailId { get; set; }

        public CheckerDtoEdit Clone()
        {
            return (CheckerDtoEdit)MemberwiseClone();
        }
    }
}
