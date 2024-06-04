﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THACO.Web.Service.Helpers
{
    public class TypeHelpers
    {
        public static Type GetAnyElementType(Type type)
        {
            // Type is Array
            // short-circuit if you expect lots of arrays
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces()
                                    .Where(t => t.IsGenericType &&
                                           t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
            return enumType ?? type;
        }

        public static string FixJsonData(string jsonData)
        {
            // Replace single quotes with double quotes
            jsonData = jsonData.Replace("'", "\"");

            // Add double quotes around keys and string values
            jsonData = jsonData.Replace("{", "{\"").Replace(",", "\",\"").Replace(":", "\":\"").Replace("}", "\"}");

            return jsonData;
        }
    }
}
