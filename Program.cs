using OlymmpicManagementSystem.Util;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace E520._47标定
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SqlHelper.ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

}




