using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Color lightBackColor = SystemColors.Control;
        private readonly Color lightForeColor = SystemColors.ControlText;
        private readonly Color darkBackColor = Color.FromArgb(31, 31, 31);
        private readonly Color darkForeColor = SystemColors.ControlLightLight;

        private bool isDarkTheme = false;

        bool isShifr = true;

        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";
        private const string RussianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public Form1()
        {
            InitializeComponent();
            ApplyTheme();
            shiftDirectionComboBox.SelectedIndex = 0;
            encryptButton.Click += EncryptButton_Click;
        }


        public void ApplyTheme()
        {
            // Применение темы к форме и текстовым полям
            if (isDarkTheme)
            {
                BackColor = darkBackColor;
                ForeColor = darkForeColor;
                inputTextBox.BackColor = outputTextBox.BackColor = darkBackColor;
                inputTextBox.ForeColor = outputTextBox.ForeColor = darkForeColor;
            }
            else
            {
                BackColor = lightBackColor;
                ForeColor = lightForeColor;
                inputTextBox.BackColor = outputTextBox.BackColor = lightBackColor;
                inputTextBox.ForeColor = outputTextBox.ForeColor = lightForeColor;
            }

            // Применение темы к кнопкам, комбобоксу и NumericUpdown
            Color buttonBackColor = isDarkTheme ? darkBackColor : lightBackColor;
            Color buttonForeColor = isDarkTheme ? darkForeColor : lightForeColor;

            encryptButton.BackColor = ChangeButton.BackColor = shiftDirectionComboBox.BackColor = shiftNumericUpDown.BackColor = buttonBackColor;
            encryptButton.ForeColor = ChangeButton.ForeColor = shiftDirectionComboBox.ForeColor = shiftNumericUpDown.ForeColor = buttonForeColor;

            // Применение темы к элементам меню
            menuStrip1.BackColor = buttonBackColor;
            menuStrip1.ForeColor = buttonForeColor;

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.BackColor = buttonBackColor;
                item.ForeColor = buttonForeColor;
                ApplyToolStripItemsTheme(item.DropDownItems, buttonBackColor, buttonForeColor);
            }
        }

        private void ApplyToolStripItemsTheme(ToolStripItemCollection items, Color backColor, Color foreColor)
        {
            foreach (ToolStripItem item in items)
            {
                item.BackColor = backColor;
                item.ForeColor = foreColor;

                if (item is ToolStripMenuItem subMenuItem)
                {
                    ApplyToolStripItemsTheme(subMenuItem.DropDownItems, backColor, foreColor);
                }
            }
        }

        private void DarkModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDarkTheme = darkModeCheckBox.Checked;
            ApplyTheme();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Viperr files (*.viperr)|*.viperr";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inputTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Viperr files (*.viperr)|*.viperr";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, outputTextBox.Text);
                }
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            int shift = Convert.ToInt32(shiftNumericUpDown.Value);
            if (shiftDirectionComboBox.SelectedItem.ToString() == "Влево")
            {
                shift = -shift;
            }
            string plainText = inputTextBox.Text;
            string encryptedText = Encrypt(plainText, shift);
            outputTextBox.Text = encryptedText;
        }



        private void DecryptButton_Click(object sender, EventArgs e)
        {
            int shift = Convert.ToInt32(shiftNumericUpDown.Value);
            if (shiftDirectionComboBox.SelectedItem.ToString() == "Вправо")
            {
                shift = -shift;
            }
            string encryptedText = inputTextBox.Text;
            string decryptedText = Decrypt(encryptedText, shift);
            outputTextBox.Text = decryptedText;
        }

        private string Encrypt(string input, int shift)
        {
            return Transform(input, shift, true);
        }

        private string Decrypt(string input, int shift)
        {
            return Transform(input, shift, false);
        }

        private string Transform(string input, int shift, bool encrypt)
        {
            string alphabet = GetAlphabet(input);
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (char.IsLetter(ch))
                {
                    char baseLetter = char.IsUpper(ch) ? char.ToUpper(alphabet[0]) : char.ToLower(alphabet[0]);
                    int alphabetIndex = alphabet.IndexOf(char.ToLower(ch));
                    int newIndex = (alphabetIndex + (encrypt ? shift : -shift) + alphabet.Length) % alphabet.Length;
                    char newChar = char.IsUpper(ch) ? char.ToUpper(alphabet[newIndex]) : char.ToLower(alphabet[newIndex]);
                    result += newChar;
                }
                else
                {
                    result += ch;
                }
            }
            return result;
        }

        private string GetAlphabet(string text)
        {
            if (text.Any(c => RussianAlphabet.Contains(char.ToLower(c))))
            {
                return RussianAlphabet;
            }
            else
            {
                return EnglishAlphabet;
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            string textBotTextTmp = inputTextBox.Text;
            inputTextBox.Text = outputTextBox.Text;
            outputTextBox.Text = textBotTextTmp;
            
            if (isShifr)
            {
                isShifr = false;
                encryptButton.Click += DecryptButton_Click;
                encryptButton.Text = "Дешифровать";
            }
            else
            {
                isShifr = true;
                encryptButton.Click += EncryptButton_Click;
                encryptButton.Text = "Шифровать";
            }

            
            
        }
    }
}






