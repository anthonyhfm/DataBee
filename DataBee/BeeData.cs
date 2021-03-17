using System;
using System.Collections.Generic;
using System.Text;

namespace Bee
{
    public class BeeSingle
    {
        public string localName { get; set; }

        public object localData { get; set; }

        public BeeSingle(string name, object data)
        {
            localName = name;
            localData = data;
        }
    }

    public class BeeArray
    {
        public string localName { get; set; }

        public object[] localData { get; set; }

        public BeeArray(string name, object[] data)
        {
            localName = name;
            localData = data;
        }
    }
}
