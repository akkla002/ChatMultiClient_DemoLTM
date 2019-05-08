using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Client
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
            //ClientSite.Instance.Connect();
            //ClientSite.Instance.ListFormShow.Add(new fChat());
            Application.Run(new fLogin());
            //Application.Run(ClientSite.Instance.ListFormShow[0]);
        }
    }
}
