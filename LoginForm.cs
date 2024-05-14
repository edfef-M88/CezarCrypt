using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        private const string Username = "admin";
        private const string Password = "admin";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = usernameTextBox.Text;
            string enteredPassword = passwordTextBox.Text;

            if (enteredUsername == Username && enteredPassword == Password)
            {
                this.DialogResult = DialogResult.OK; // Устанавливаем результат диалога как OK
                this.Close(); // Закрываем форму авторизации
            }
            else
            {
                MessageBox.Show("Неверные учетные данные!");
            }
        }

    }
}

