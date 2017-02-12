using System;
using System.Collections;

namespace prg
{
    class JSONSerializer
    {
        public string parseType(object obj, int depth) {
            if (obj == null)
                return "";
            string res = "";
            string prefix = "".PadLeft(depth, '\t');
            string fields = "";

            var currType = obj.GetType();

            if (currType == typeof(int) ||
                currType == typeof(string) ||
                currType == typeof(bool) ||
                currType == typeof(float) ||
                currType == typeof(double) ||
                currType == typeof(UInt32) ||
                currType == typeof(UInt64) ||
                currType == typeof(System.DateTime))
            {
                res += prefix + currType.Name + ": \"" + obj + "\",\r\n";
            }
            else if (currType.IsEnum)
            {
                Array enumValues = System.Enum.GetValues(currType);
                res += prefix + currType.Name + ": [";
                for (var i = 0; i < enumValues.Length; i++)
                {
                    res += enumValues.GetValue(i).ToString();
                    if (i < enumValues.Length - 1)
                        res += ',';
                }
                res += "],\r\n";

            }
            else if (currType.IsGenericParameter) {
              
            }
            else if (currType.IsGenericType) {
                if (obj is IEnumerable)
                {
                    foreach (object o in (obj as IEnumerable))
                    {
                        fields += this.parseType(o, depth + 1);
                    }
                    res += prefix + currType.Name + ": {\r\n" + fields + prefix + "},\r\n";
                }
                else {
                    var key = currType.GetProperty("Key")
                        .GetValue(obj, null);
                    object o = currType.GetProperty("Value")
                        .GetValue(obj, null);
                    res += prefix + key + ": {\r\n" + this.parseType(o, depth + 1) + prefix + "},\r\n";
                }
                
            }
            else if (currType == typeof(ArrayList))
            {
                var aList = obj as ArrayList;

                foreach (var item in aList)
                {
                    fields += this.parseType(item, depth + 1);
                }
                res += prefix + currType.Name + ": {\r\n" + fields + prefix + "},\r\n";
            }
            else
            {
                foreach (var field in currType.GetFields())
                {
                    fields += this.parseType(field.GetValue(obj), depth + 1);
                }
                res += prefix + currType.Name + ": {\r\n" + fields + prefix + "},\r\n";
            }
            
            
            return res;
        }
        public string serialize(object obj) {
            if (obj == null) {
                return "{}";
            }
            var type = obj.GetType();

            string fields = this.parseType(obj, 1);
            return "{" + string.Format("\r\n\tname:{0}, \r\n{1}", type.Name, fields) + "}";
        }
    }
}
