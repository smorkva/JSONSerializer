using System;
using System.Collections;
using System.Collections.Generic;

using System.Web.Script.Serialization;

namespace prg
{
    public enum someEnum {
        one,
        two,
        tree
    }
    struct infoItem {
        public string name;
        public someEnum value;
        public infoItem(string name, someEnum value)
        {
            this.name = name;
            this.value = value;
        }
    }
    [Serializable]
    struct someData {
        public string firstField;
        public int intValue;
        public Dictionary<int, infoItem> someCollect;
        public ArrayList arrayList;
        public List<string> list;
        public string _data { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new JSONSerializer();
            var target = new someData();
            
            target.firstField = "I am first";
            target.intValue = 2;
            target.someCollect = new Dictionary<int, infoItem>();
                target.someCollect.Add(4, new infoItem("one and tree", someEnum.one | someEnum.tree));
                target.someCollect.Add(3, new infoItem("one and two", someEnum.one | someEnum.two));
                target.someCollect.Add(5, new infoItem("two and tree", someEnum.two | someEnum.two));
            target.list = new List<string>();
                target.list.Add("First item in list");
            target.arrayList = new ArrayList();
                target.arrayList.Add(123);
                target.arrayList.Add("some string");
                target.arrayList.Add(new infoItem("one two tree",
                        someEnum.one | someEnum.two | someEnum.two));

            Console.WriteLine(serializer.serialize(target));
            Console.ReadLine();
        }
    }
}
