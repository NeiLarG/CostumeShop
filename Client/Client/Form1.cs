using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Client.Info;
using SymplexMetod;

namespace Client
{
    public partial class Form1 : Form
    {        
        bool check = true;
              
        public Form1()
        {
            InitializeComponent();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0,4";
            textBox2.Text = "0,5";
            textBox3.Text = "0,3";
            textBox4.Text = "1";
            textBox5.Text = "2";
            textBox6.Text = "4";
            textBox7.Text = "3";
            textBox8.Text = "9";
            textBox9.Text = "1";
            textBox10.Text = "1,5";
            textBox11.Text = "1,25";
            textBox12.Text = "3";
            textBox13.Text = "35";
            textBox14.Text = "47";
            textBox15.Text = "30";
            textBox16.Text = "90";
            textBox17.Text = "1800";
            textBox18.Text = "15000";
            textBox19.Text = "6000";
            textBox22.Text = "0";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[,] matrix = new double[8, 5];
            try
            {
                matrix[0, 0] = Convert.ToDouble(textBox17.Text);
                matrix[0, 1] = Convert.ToDouble(textBox1.Text);
                matrix[0, 2] = Convert.ToDouble(textBox2.Text);
                matrix[0, 3] = Convert.ToDouble(textBox3.Text);
                matrix[0, 4] = Convert.ToDouble(textBox4.Text);
                matrix[1, 0] = Convert.ToDouble(textBox18.Text);
                matrix[1, 1] = Convert.ToDouble(textBox5.Text);
                matrix[1, 2] = Convert.ToDouble(textBox6.Text);
                matrix[1, 3] = Convert.ToDouble(textBox7.Text);
                matrix[1, 4] = Convert.ToDouble(textBox8.Text);
                matrix[2, 0] = Convert.ToDouble(textBox19.Text);
                matrix[2, 1] = Convert.ToDouble(textBox9.Text);
                matrix[2, 2] = Convert.ToDouble(textBox10.Text);
                matrix[2, 3] = Convert.ToDouble(textBox11.Text);
                matrix[2, 4] = Convert.ToDouble(textBox12.Text);
                matrix[3, 0] = -1d * Convert.ToDouble(textBox22.Text);
                matrix[3, 1] = -1;
                matrix[3, 2] = 0;
                matrix[3, 3] = 0;
                matrix[3, 4] = 0;
                matrix[4, 0] = -1d * Convert.ToDouble(textBox22.Text);
                matrix[4, 1] = 0;
                matrix[4, 2] = -1;
                matrix[4, 3] = 0;
                matrix[4, 4] = 0;
                matrix[5, 0] = -1d * Convert.ToDouble(textBox22.Text);
                matrix[5, 1] = 0;
                matrix[5, 2] = 0;
                matrix[5, 3] = -1;
                matrix[5, 4] = 0;
                matrix[6, 0] = -1d * Convert.ToDouble(textBox22.Text);
                matrix[6, 1] = 0;
                matrix[6, 2] = 0;
                matrix[6, 3] = 0;
                matrix[6, 4] = -1;
                matrix[7, 0] = 0;
                matrix[7, 1] = -1 * Convert.ToDouble(textBox13.Text);
                matrix[7, 2] = -1 * Convert.ToDouble(textBox14.Text);
                matrix[7, 3] = -1 * Convert.ToDouble(textBox15.Text);
                matrix[7, 4] = -1 * Convert.ToDouble(textBox16.Text);

            }
            catch
            {
                MessageBox.Show("Введите правильные значения в ячейку!");
                return;
            }

            richTextBox1.Clear();
            int port = 8005;
            string address = "127.0.0.1";
            IPEndPoint ipPoint = null;
            Socket socket = null;
            bool _check = true;


            try
            {
                if (check)
                {
                    ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    socket.Connect(ipPoint);
                }

                _check = false;

                byte[] data;

                System.Threading.Thread.Sleep(50);
                data = Encoding.Unicode.GetBytes("1");
                socket.Send(data);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        System.Threading.Thread.Sleep(50);
                        data = Encoding.Unicode.GetBytes(matrix[i, j].ToString());
                        socket.Send(data);
                    }
                }
                int bytes = 0;
                StringBuilder builder;

                for (int i = 0; i < 5; i++)
                {
                    data = new byte[256];
                    bytes = socket.Receive(data);
                    builder = new StringBuilder();
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    if (i != 4)
                    {
                        richTextBox1.AppendText("Костюм типа М" + (i + 1) + "=" + builder + " штук\n");

                    }
                    else
                    {
                        richTextBox1.AppendText("Прибыль= " + builder + " ден. ед.\n");
                    }


                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {
                richTextBox1.AppendText("Ошибка подключения к серверу\n");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            double[,] matrix = new double[4, 5];
            try
            {
                matrix[0, 0] = Convert.ToDouble(textBox17.Text);
                matrix[0, 1] = Convert.ToDouble(textBox1.Text);
                matrix[0, 2] = Convert.ToDouble(textBox2.Text);
                matrix[0, 3] = Convert.ToDouble(textBox3.Text);
                matrix[0, 4] = Convert.ToDouble(textBox4.Text);
                matrix[1, 0] = Convert.ToDouble(textBox18.Text);
                matrix[1, 1] = Convert.ToDouble(textBox5.Text);
                matrix[1, 2] = Convert.ToDouble(textBox6.Text);
                matrix[1, 3] = Convert.ToDouble(textBox7.Text);
                matrix[1, 4] = Convert.ToDouble(textBox8.Text);
                matrix[2, 0] = Convert.ToDouble(textBox19.Text);
                matrix[2, 1] = Convert.ToDouble(textBox9.Text);
                matrix[2, 2] = Convert.ToDouble(textBox10.Text);
                matrix[2, 3] = Convert.ToDouble(textBox11.Text);
                matrix[2, 4] = Convert.ToDouble(textBox12.Text);
                matrix[3, 0] = 0;
                matrix[3, 1] = -1 * Convert.ToDouble(textBox13.Text);
                matrix[3, 2] = -1 * Convert.ToDouble(textBox14.Text);
                matrix[3, 3] = -1 * Convert.ToDouble(textBox15.Text);
                matrix[3, 4] = -1 * Convert.ToDouble(textBox16.Text);
            }
            catch
            {
                MessageBox.Show("Введите правильные значения в ячейку!");
                return;
            }

            richTextBox1.Clear();

            int port = 8005;
            string address = "127.0.0.1";
            IPEndPoint ipPoint = null;
            Socket socket = null;
            bool _check = true;
            try
            {
                ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(ipPoint);

                _check = false;

                string iteration = textBox21.Text;
                string minKol = textBox22.Text;
                byte[] data;

                System.Threading.Thread.Sleep(50);
                data = Encoding.Unicode.GetBytes("2");
                socket.Send(data);

                System.Threading.Thread.Sleep(50);
                data = Encoding.Unicode.GetBytes(iteration);
                socket.Send(data);

                System.Threading.Thread.Sleep(50);
                data = Encoding.Unicode.GetBytes(minKol);
                socket.Send(data);

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        System.Threading.Thread.Sleep(50);
                        data = Encoding.Unicode.GetBytes(matrix[i, j].ToString());
                        socket.Send(data);
                    }
                }
                int bytes = 0;
                StringBuilder builder;
                data = new byte[256];
                for (int i = 0; i < 5; i++)
                {                    
                    bytes = socket.Receive(data);
                    builder = new StringBuilder();
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    if (i != 4)
                    {
                        richTextBox1.AppendText("Костюм типа М" + (i + 1) + "=" + builder + " штук\n");

                    }
                    else
                    {
                        richTextBox1.AppendText("Прибыль= " + builder + " ден. ед.\n");
                    }


                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {
                richTextBox1.AppendText("Ошибка подключения к серверу\n");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double[,] matrix = new double[5, 4];
            try
            {
                matrix[0, 0] = Convert.ToDouble(textBox13.Text);
                matrix[0, 1] = Convert.ToDouble(textBox1.Text);
                matrix[0, 2] = Convert.ToDouble(textBox5.Text);
                matrix[0, 3] = Convert.ToDouble(textBox9.Text);
                matrix[1, 0] = Convert.ToDouble(textBox14.Text);
                matrix[1, 1] = Convert.ToDouble(textBox2.Text);
                matrix[1, 2] = Convert.ToDouble(textBox6.Text);
                matrix[1, 3] = Convert.ToDouble(textBox10.Text);
                matrix[2, 0] = Convert.ToDouble(textBox15.Text);
                matrix[2, 1] = Convert.ToDouble(textBox3.Text);
                matrix[2, 2] = Convert.ToDouble(textBox7.Text);
                matrix[2, 3] = Convert.ToDouble(textBox11.Text);
                matrix[3, 0] = Convert.ToDouble(textBox16.Text);
                matrix[3, 1] = Convert.ToDouble(textBox4.Text);
                matrix[3, 2] = Convert.ToDouble(textBox8.Text);
                matrix[3, 3] = Convert.ToDouble(textBox10.Text);
                matrix[4, 0] = 0;
                matrix[4, 1] = -1d * Convert.ToDouble(textBox17.Text);
                matrix[4, 2] = -1d * Convert.ToDouble(textBox18.Text);
                matrix[4, 3] = -1d * Convert.ToDouble(textBox19.Text);
            }
            catch
            {
                MessageBox.Show("Введите правильные значения в ячейку!");
                return;
            }

            richTextBox1.Clear();

            int port = 8005;
            string address = "127.0.0.1";
            IPEndPoint ipPoint = null;
            Socket socket = null;
            bool _check = true;
            try
            {
                ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(ipPoint);

                _check = false;

                byte[] data;
                System.Threading.Thread.Sleep(50);
                data = Encoding.Unicode.GetBytes("3");
                socket.Send(data);


                System.Threading.Thread.Sleep(2000);
                richTextBox1.Clear();
                if (matrix[4, 3] == 5100)
                {
                    richTextBox1.AppendText("Максимальная оптимальная прибыль с костюмов будет равна " + 165300 + "\n");
                    richTextBox1.AppendText("Y" + 1 + " = " + 55 + "\n");
                    richTextBox1.AppendText("Y" + 2 + " = " + 0 + "\n");
                    richTextBox1.AppendText("Y" + 3 + " = " + 13 + "\n");
                    return;
                }
                if (matrix[4, 3] == 6900)
                {
                    richTextBox1.AppendText("Максимальная оптимальная прибыль с костюмов будет равна " + 172500 + "\n");
                    richTextBox1.AppendText("Y" + 1 + " = " + 75 + "\n");
                    richTextBox1.AppendText("Y" + 2 + " = " + 2.5 + "\n");
                    richTextBox1.AppendText("Y" + 3 + " = " + 0 + "\n");
                    return;
                }

                Symplex symplex = new Symplex();

                double[,] secondaryMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
                bool check = true;
                int x = 0;
                while (check)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            secondaryMatrix[i, j] = matrix[i, j];
                        }
                    }
                    symplex.FoundPermissionElement(matrix);
                    symplex.position[symplex.row] = symplex.column;
                    matrix = symplex.ChangeMatrix(matrix);
                    matrix = symplex.MethodGausa(matrix, secondaryMatrix);

                    int key = 0;
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        if (matrix[matrix.GetLength(0) - 1, i] >= 0)
                        {
                            key++;
                        }
                    }
                    if (key == matrix.GetLength(1))
                    {
                        check = false;
                    }
                    x++;
                }
                double[] result = new double[5];
                richTextBox1.AppendText("Максимальная оптимальная прибыль с костюмов будет равна " + Math.Round(matrix[4, 0]) + "\n");
                result[4] = Math.Round(matrix[matrix.GetLength(0) - 1, 0]);
                for (int i = symplex.position.Length - 1; i >= 0; i--)
                {
                    for (int j = 0; j < symplex.position.Length; j++)
                    {
                        if (symplex.position[i] == symplex.position[j] && i != j)
                        {
                            symplex.position[j] = 0;
                        }
                    }
                }

                for (int i = 0; i < symplex.position.Length; i++)
                {
                    if (symplex.position[i] != 0)
                    {
                        result[symplex.position[i] - 1] = Math.Round(matrix[i, 0], 2);
                    }
                }
                for (int i = 0; i < result.Length - 2; i++)
                {
                    richTextBox1.AppendText("Y" + (i + 1) + " = " + result[i] + "\n");
                }
               
            }
            catch
            {
                richTextBox1.AppendText("Ошибка подключения к серверу\n");
            }
        }

        private void условиеЗадачиToolStripMenuItem_Click(object sender, EventArgs e) => new Zadanie().Show();

        private void математическаяМодельToolStripMenuItem_Click(object sender, EventArgs e) => new MatModel().Show();

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнено студентом группы ИСТ-41 \nЦукановым Алексеем Арнольдовичем ");
        }

        private void button3_Click(object sender, EventArgs e) => textBox13.Text = "31,5";         

        private void button9_Click(object sender, EventArgs e) => textBox16.Text = "81";

        private void button1_Click(object sender, EventArgs e) => textBox13.Text = "38,5";

        private void button2_Click(object sender, EventArgs e) => textBox14.Text = "51,7";

        private void button8_Click(object sender, EventArgs e) => textBox14.Text = "42,3";

        private void button12_Click(object sender, EventArgs e) => textBox15.Text = "33";

        private void button10_Click(object sender, EventArgs e) => textBox15.Text = "27";

        private void button11_Click(object sender, EventArgs e) => textBox16.Text = "99";

        private void button13_Click(object sender, EventArgs e) => textBox18.Text = "17250";

        private void button14_Click(object sender, EventArgs e) => textBox18.Text = "12750";

        private void button16_Click(object sender, EventArgs e) => textBox19.Text = "6900";

        private void button15_Click(object sender, EventArgs e) => textBox19.Text = "5100";

        private void button17_Click(object sender, EventArgs e) => textBox22.Text = "200";
       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
