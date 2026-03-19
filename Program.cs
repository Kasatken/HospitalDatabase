using System;
using System.Windows.Forms;

namespace PolyclinicApp
{
    internal static class Program
    {
        // Строка подключения к PostgreSQL 
        public static string ConnectionString =
            "Host=localhost;Port=5432;Database=polyclinic_db;Username=postgres;Password=ПОМЕНЯТЬ ТУТА;";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
