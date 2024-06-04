using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Constants;
using THACO.Web.Service.Interfaces.Entities;

namespace THACO.Web.Service.Model
{
    public class CustomerDtoEdit : CustomerEntity, IRecordState, IRecordVersion
    {
        [Description("Trạng thái")]
        public ModelState state { get; set; }
        public long RecordVersion { get; set; }

        public CustomerDtoEdit Clone()
        {
            return (CustomerDtoEdit)MemberwiseClone();
        }
    }
}
