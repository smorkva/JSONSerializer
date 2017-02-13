using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using SerializerJSON;

namespace SerialiserTest
{
    struct testStruct
    {
        public int SomeInt;
        public string SomeString;
    }
    enum Digits
    {
        One = 1,
        Two = 2,
        Tree = 4
    }

    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void SerializeNULL()
        {
            var quote = "{}";
            var serializer = new Serializer();
            var result = serializer.Serialize(null);
            Assert.AreEqual(quote, result);
        }
        [TestMethod]
        public void SerializeStruct()
        {
            var serializedSctruct = "{testStruct:{SomeInt:\"123\",SomeString:\"Test string\"}}";
            var data = new testStruct();
            data.SomeInt = 123;
            data.SomeString = "Test string";

            var serializer = new Serializer();
            var result = serializer.Serialize(data);
            Assert.AreEqual(serializedSctruct, result);
        }
        [TestMethod]
        public void SerializeArrayList()
        {
            var serializedSctruct = "{ArrayList:{0:{\"123\"},1:{\"Test string\"}}}";
            var data = new ArrayList();
            data.Add(123);
            data.Add("Test string");

            var serializer = new Serializer();
            var result = serializer.Serialize(data);
            Assert.AreEqual(serializedSctruct, result);
        }
        [TestMethod]
        public void SerializeEnums()
        {
            var serializedSctruct = "{ArrayList:{0:{Digits:[One,Two,Tree]},1:{Digits:[One,Two,Tree]}}}";
            var data = new ArrayList();

            var one_two = new Digits();
            one_two = Digits.One | Digits.Two;
            data.Add(one_two);

            var one_tree = new Digits();
            one_two = Digits.One | Digits.Tree;
            data.Add(one_tree);

            var serializer = new Serializer();
            var result = serializer.Serialize(data);
            Assert.AreEqual(serializedSctruct, result);
        }
        [TestMethod]
        public void SerializeList()
        {
            var serializedSctruct = "{List`1:{\"123\",\"4563\"}}";
            var data = new List<int>();
            data.Add(123);
            data.Add(4563);

            var serializer = new Serializer();
            var result = serializer.Serialize(data);
            Assert.AreEqual(serializedSctruct, result);
        }

        [TestMethod]
        public void SerializeDictonary()
        {
            var serializedSctruct = "{Dictionary`2:{1:{\"One\"},2:{\"Two\"},3:{\"Tree\"}}}";
            var data = new Dictionary<int, string>();
            data.Add(1, "One");
            data.Add(2, "Two");
            data.Add(3, "Tree");

            var serializer = new Serializer();
            var result = serializer.Serialize(data);
            Assert.AreEqual(serializedSctruct, result);
        }
    }
}
