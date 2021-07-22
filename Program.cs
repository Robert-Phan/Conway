using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conway
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var t = new GUI.GUI(@"3bo15b$2b3o14b$b2obo5bo8b$b3o5b3o7b$2b2o4bo2b2o3b3o$8b3o4bo2bo$18bo$
18bo$18bo$2b3o12bob$2bo2bo13b$2bo16b$2bo16b$3bo15b7$3o16b$o2bo11bo3b$o
13b3o2b$o12b2obo2b$o12b3o3b$bo12b2o!", speed: 50);
            Application.Run(t);
        }
    }
}
