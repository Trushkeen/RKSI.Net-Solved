using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab6
{
    static class Logger
    {
        public static void WriteLog(string path, string text)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToShortTimeString() + " " + text);
            }
        }
    }
}
