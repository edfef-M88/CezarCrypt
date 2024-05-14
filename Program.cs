using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm(); // Создаем форму авторизации
            if (loginForm.ShowDialog() == DialogResult.OK) // Показываем форму и проверяем результат
            {
                Form1 mainForm = new Form1(); // Создаем основную форму программы
                mainForm.ApplyTheme(); // Применяем тему (если необходимо)
                Application.Run(mainForm); // Показываем основную форму
            }
            else
            {
                Application.Exit(); // Если авторизация не прошла успешно, завершаем приложение
            }
        }
    }
}
