using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomSerializer
{
    public class CustomJsonSerializer
    {
        public static string Serialize<T>(T t)
        {
            if (t is IEnumerable)
            {
                string values = "[";
                foreach (var list in t as IEnumerable)
                    values += GetPropertyValues(list) + ",";
                return values.Remove(values.Length - 1) + "]";
            }
            else
                return GetPropertyValues(t);
        }


        static string GetPropertyValues<T>(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] props = type.GetProperties();
            string str = "{"; 
            foreach (var prop in props)
            {
                if (prop.GetValue(t) is ICollection) str += GetNestedListProps(prop, t) + "}]";
                else if (prop.PropertyType.ToString().Contains("CustomSerializer.")) str += GetNestedClass(prop, t) + ",";
                else
                {
                    var val = prop.GetValue(t) is String ? "\"" + prop.GetValue(t) + "\"" : prop.GetValue(t);
                    str += ("\"" + prop.Name + "\":" + val) + ",";
                }
            }
            return str.Remove(str.Length - 1) + "}";
        }

        private static string GetNestedListProps<T>(PropertyInfo property, T t)
        {
            string str = "\"" + property.Name + "\"" + ":[{";
            foreach (var values in property.GetValue(t) as IEnumerable)
            {
                foreach (var prop in values.GetType().GetProperties())
                {
                    var val = prop.GetValue(values) is String ? "\"" + prop.GetValue(values) + "\"" : prop.GetValue(values);
                    str += ("\"" + prop.Name + "\":" + val);
                }
            }
            return str;
        }

        private static string GetNestedClass<T>(PropertyInfo prop, T t)
        {
            string str = "";
            foreach (var item in prop.GetValue(t).GetType().GetProperties())
            {
                var val = item.GetValue(prop.GetValue(t)) is String ? "\"" + item.GetValue(prop.GetValue(t)) + "\"" : item.GetValue(prop.GetValue(t));
                str += ("\"" + prop.Name + "\":{\"" + item.Name + "\":" + val) + ",";
            }
            return str.Remove(str.Length - 1) + "}";
        }
    }

}
