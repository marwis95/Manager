using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists("config.ini"))
            {
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("Plik 'config.ini' nie istnieje!");
            }
        }
    }
}
