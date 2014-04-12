using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Buttons
{
    public enum Language
    {
        French,
        English
    }

    static class Strings
    {
        static Language language;
        static string file;

        static public Language Language
        {
            get { return language; }
            set
            {
                language = value;
                file = value.ToString() + ".txt";
            }
        }

        public static string stringForKey(string key)
        {
            StreamReader reader = new StreamReader(file);
            string str;
            do
            {
                str = reader.ReadLine();
            } while (!str.Contains(key));

            while (str[0] != '\"')
            {
                str = str.Substring(1);
            }
            str = str.Substring(1);
            
            reader.Close();

            return str.Substring(0, str.Length - 1);
        }



    }
}
