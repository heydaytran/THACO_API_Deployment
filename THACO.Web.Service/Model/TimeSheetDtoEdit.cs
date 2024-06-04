using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Attributes;
using THACO.Web.Service.Constants;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Interfaces.Entities;

namespace THACO.Web.Service.Model
{
    public class TimeSheetDtoEdit : TimeSheetEntity, IRecordState, IRecordVersion
    {
        public ModelState state { get; set; }
        [Detail(nameof(TimeSheetTargetDtoEdit.time_sheet_id), typeof(TimeSheetTargetEntity))]
        public List<TimeSheetTargetDtoEdit> timeSheetTargets { get; set; } = new List<TimeSheetTargetDtoEdit>();
        public long RecordVersion { get; set; }

        public TimeSheetDtoEdit Clone()
        {
            return (TimeSheetDtoEdit)MemberwiseClone();
        }
    }
}
