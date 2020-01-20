using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloMethod
{
    class MonteCarlo
    {
        public double[] MetodMonteCarlo(double[,] matrix, int iterations, int minKol)
        {
            double[] maxRand = new double[matrix.GetLength(1)];
            int[] function = new int[matrix.GetLength(1)];
            double[] maxFunction = new double[matrix.GetLength(1)];
            int max = 0;
            for (int i = 1; i < matrix.GetLength(1); i++)
            {
                double min = double.PositiveInfinity;
                for (int j = 0; j < matrix.GetLength(0) - 1; j++)
                {
                    if (min > (matrix[j, 0] / matrix[j, i]))
                    {
                        min = matrix[j, 0] / matrix[j, i];
                    }

                }
                maxRand[i - 1] = min;
            }
            Random rand = new Random();
            for (int i = 0; i < iterations + 1; i++)
            {
                int sum = 0;
                double time = 0, reklam = 0, area = 0;
                //double limit = 1;
                for (int j = 0; j < maxRand.Length - 1; j++)
                {
                    /*double percent = maxRand[j] * limit;
                    function[j] = rand.Next(0, (int)percent);
                    limit -= function[j] / maxRand[j];*/
                    function[j] = rand.Next(minKol, (int)maxRand[j]);
                }
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    sum += function[j] * (int)matrix[3, j + 1] * -1;
                    time += function[j] * matrix[0, j + 1];
                    reklam += function[j] * matrix[1, j + 1];
                    area += function[j] * matrix[2, j + 1];

                }
                if (time < matrix[0, 0] && reklam < matrix[1, 0] && area < matrix[2, 0])
                {
                    if (sum > max)
                    {
                        Console.WriteLine("Максиальная прибыль увеличена");
                        max = sum;
                        for (int j = 0; j < matrix.GetLength(1)-1; j++)
                        {
                            maxFunction[j] = function[j];
                            Console.WriteLine("Костюм типа М" + (j + 1) + "=" + maxFunction[j] + " штук");
                        }
                        maxFunction[matrix.GetLength(1) - 1] = max;
                        Console.WriteLine("Прибыль= " + maxFunction[matrix.GetLength(1) - 1] + " ден. ед.");
                        Console.WriteLine();
                    }
                }
                else { i--; }
            }
            return maxFunction;
        }
    }
}
