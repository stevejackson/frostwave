using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace F2D.Core
{
    public static class Log
    {
        const string filename = "Log.txt";
        static StreamWriter stream;

        public static void Write(string content)
        {
            content = "Error: " + content;
            using (stream = File.CreateText(filename))
            {
                stream.WriteLine(content);
            }

        }

        public static void Close()
        {
            stream.Close();
        }

    }
}
