using System;
using System.Diagnostics;

namespace photo_printing_dispatcher
{
    public class PrintManager
    {
        private readonly string _pathToPrinterExe;
        
        public PrintManager(string pathToPrinterExe)
        {
            _pathToPrinterExe = pathToPrinterExe;
        }
        
        public void Print(string pathToPhoto)
        {
            Console.WriteLine($"[{DateTime.Now}] Running printer exe with path {pathToPhoto}");
            Process.Start(_pathToPrinterExe,pathToPhoto);
        }
    }
}