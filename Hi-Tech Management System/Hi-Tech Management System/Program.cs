using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_Tech_Management_System.GUI;

namespace Hi_Tech_Management_System
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
            Application.Run(new FormLogin());
            //Application.Run(new FormEmployeeUser());
            //Application.Run(new FormClientUser());
            //Application.Run(new FormProductUser());
            //Application.Run(new FormOrderUser());
        }
    }
}
