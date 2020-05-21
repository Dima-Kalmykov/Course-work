using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс для начала работы программы.
    /// </summary>
    internal static class PointOfStartProgram
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}