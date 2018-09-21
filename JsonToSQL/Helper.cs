using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Helper
    {
        public static Dictionary<String, Object> Dyn2Dict(dynamic dynObj)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj))
            {
                object obj = propertyDescriptor.GetValue(dynObj);
                dictionary.Add(propertyDescriptor.Name, obj);
            }
            return dictionary;
        }
    }
}
