using System;
using System.IO;
using System.Net;

namespace Printer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            var fileName = Path.GetFileName(path);
            File.Create(fileName);
        }
    }
}