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
            var t = new GUI.GUI(new int[,] {
                {11, 20},
                {10, 21},
                {11, 21},
                {12, 22},
                {10, 22}
            }, speed: 50);
            Application.Run(t);
        }
    }
}
