using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THACO.Web.Service.Interfaces.Service
{
    public interface ISerializerService
    {
        string SerializeObject(object o);
        T DeserializeObject<T>(string s);
        object DeserializeObject(string s, Type type);
    }
}
