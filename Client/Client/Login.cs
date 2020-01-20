using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty);

            if (File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Shop\UserInfo.md5") == encoded)
            {
                Form form = new Form1();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный пароль!");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Shop\UserInfo.md5";
            if (File.Exists(dir))
            {
                this.Text = "Авторизация";
                label1.Visible = true;
                textBox1.Visible = true;
                button1.Visible = true;
            }
            else
            {
                this.Text = "Создание пароля!";
                label1.Visible = true;
                textBox1.Visible = true;
                button2.Visible = true;

                MessageBox.Show("Приветсвую, Вас, в программе по управлению прибылью костюмов \nДля большей безопасности установите пароль, состоящий более чем из 6 символов. ", "Внимание!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 6)
            {
                string password = textBox1.Text;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string encoded = BitConverter.ToString(hash).Replace("-", string.Empty);
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Shop");
                System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Shop/UserInfo.md5", encoded);
                DialogResult result = MessageBox.Show("Пароль успешно установлен", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    Form form = new Form1();
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Пароль должен быть больше 6 символов!");
            }
        }
    }
}
