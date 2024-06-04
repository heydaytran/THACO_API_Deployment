using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THACO.Web.Service.Attributes;
using THACO.Web.Service.Constants;
using THACO.Web.Service.Interfaces.Entities;

namespace THACO.Web.Service.Model
{
    public class BookmarkTypeDtoEdit : BookmarkTypeEntity, IRecordState, IRecordVersion
    {
        public ModelState state { get; set; }
        [EditVersion]
        public long RecordVersion { get; set; }

        public BookmarkTypeDtoEdit Clone()
        {
            return (BookmarkTypeDtoEdit)MemberwiseClone();
        }
    }
}
