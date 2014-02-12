using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    [Serializable]
    public class TestClass
    {
        public string text;
        public TestClass(string str)
        {
            text = str;
        }
    }
}
