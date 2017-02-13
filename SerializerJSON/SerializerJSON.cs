using System;
using System.Collections;

namespace SerializerJSON
{
    public class Serializer
    {
        private string parseType(object _object) {
            if (_object == null)
                return "";
            string result = "";
            string fields = "";

            var currType = _object.GetType();

            if (currType == typeof(int) ||
                currType == typeof(string) ||
                currType == typeof(bool) ||
                currType == typeof(float) ||
                currType == typeof(double) ||
                currType == typeof(UInt32) ||
                currType == typeof(UInt64) ||
                currType == typeof(System.DateTime))
            {
                result += "\"" + _object + "\"";
            }
            else if (currType.IsEnum)
            {
                Array enumValues = System.Enum.GetValues(currType);
                result += currType.Name + ":[";
                for (var i = 0; i < enumValues.Length; i++)
                {
                    result += enumValues.GetValue(i).ToString();
                    if (i < enumValues.Length - 1)
                        result += ',';
                }
                result += "]";

            }
            else if (currType.IsGenericParameter) {
              
            }
            else if (currType.IsGenericType) {
                if (_object is IEnumerable)
                {
                    var enumerable = (_object as IEnumerable);
                    foreach (object o in enumerable)
                    {
                        fields += this.parseType(o) + ",";
                    }
                    fields = fields.Remove(fields.Length - 1);
                    result += "{" + fields + "}";
                }
                else {
                    var key = currType.GetProperty("Key")
                        .GetValue(_object, null);
                    object o = currType.GetProperty("Value")
                        .GetValue(_object, null);
                    result += key + ":{" + this.parseType(o) + "}";
                }
                
            }
            else if (currType == typeof(ArrayList))
            {
                var aList = _object as ArrayList;
                var i = 0;
                foreach (var item in aList)
                {
                    fields += i++.ToString() + ":{" + this.parseType(item) + "},";
                }
                fields = fields.Remove(fields.Length - 1);
                result += "{" + fields + "}";
            }
            else
            {
                foreach (var field in currType.GetFields())
                {
                    fields += field.Name + ":" + this.parseType(field.GetValue(_object)) + ",";
                }
                fields = fields.Remove(fields.Length - 1);
                result += "{" + fields + "}";
            }
            
            
            return result;
        }
        public string Serialize(object obj) {
            if (obj == null) {
                return "{}";
            }
            var type = obj.GetType();

            string fields = this.parseType(obj);
            return "{" + type.Name + ":" + fields + "}";
        }
    }
}
