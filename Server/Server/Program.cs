using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MonteCarloMethod;
using SymplexMetod;

namespace Server
{   
    class Program
    {
        static int port = 8005;
        static double[] result = new double[4];

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(5);

                Console.WriteLine("Сервер запущен. Ожидайте подключений...");

                int iteration = 0;
                int minKol = 0;
                while (true)
                {
                    Socket handler = listenSocket.Accept();

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    bytes = handler.Receive(data);
                    builder = new StringBuilder();
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    string method = builder.ToString();
                    if (method == "3")
                    {
                        Console.WriteLine("\nТеневая цена\n");
                    }
                    if (method == "1")
                    {
                        double[,] matrix = new double[8, 5];
                        Console.WriteLine("\nСимплекс метод\n");
                        bytes = 0;
                        data = new byte[256];
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                bytes = handler.Receive(data);
                                builder = new StringBuilder();
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                                matrix[i, j] = Convert.ToDouble((builder.ToString()));
                            }
                        }
                        Symplex symplex = new Symplex();

                        Console.WriteLine("Первоначальная матрица");
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                Console.Write(matrix[i, j].ToString().PadRight(9));
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
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
                            Console.WriteLine("Столбец " + symplex.column + "; строка " + symplex.row + ";");
                            if (symplex.position[symplex.row] == symplex.column)
                            {
                                symplex.position[symplex.row] = 0;
                            }
                            else
                            {
                                symplex.position[symplex.row] = symplex.column;
                            }
                            matrix = symplex.ChangeMatrix(matrix);
                            matrix = symplex.MethodGausa(matrix, secondaryMatrix);
                            for (int i = 0; i < matrix.GetLength(0); i++)
                            {
                                for (int j = 0; j < matrix.GetLength(1); j++)
                                {
                                    Console.Write(Math.Round(matrix[i, j], 2).ToString().PadRight(9));
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine();

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
                        Console.WriteLine("Максимальная оптимальная прибыль с костюмов будет равна " + Math.Round(matrix[matrix.GetLength(0) - 1, 0]));
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
                                result[symplex.position[i] - 1] = Math.Round(matrix[i, 0]);
                            }
                        }
                        for (int i = 0; i < result.Length - 1; i++)
                        {
                            Console.WriteLine("M" + (i + 1) + " вид костюма должены выпустить в размере " + result[i]);
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            System.Threading.Thread.Sleep(50);
                            data = Encoding.Unicode.GetBytes(result[i].ToString());
                            handler.Send(data);
                        }

                        continue;
                    }
                    else if (method == "2")
                    {
                        double[,] matrix = new double[4, 5];
                        Console.WriteLine("\nМетод Монте-Карло\n");
                        bytes = 0;
                        data = new byte[256];

                        bytes = handler.Receive(data);
                        builder = new StringBuilder();
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                        iteration = int.Parse(builder.ToString());

                        bytes = handler.Receive(data);
                        builder = new StringBuilder();
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                        minKol = int.Parse(builder.ToString());


                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                bytes = handler.Receive(data);
                                builder = new StringBuilder();
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                                matrix[i, j] = Convert.ToDouble((builder.ToString()));
                            }
                        }

                        MonteCarlo monteCarlo = new MonteCarlo();
                        double[] result = new double[matrix.GetLength(1)];
                        result = monteCarlo.MetodMonteCarlo(matrix, iteration, minKol);

                        for (int i = 0; i < 5; i++)
                        {
                            System.Threading.Thread.Sleep(50);
                            data = Encoding.Unicode.GetBytes(result[i].ToString());
                            handler.Send(data);
                        }

                        continue;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
