using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplexMetod
{
    class Symplex
    {
        public int column = 0, row = 0;
        public int[] position = new int[10];
        public double[,] FoundPermissionElement(double[,] matrix)
        {            
            double min = double.PositiveInfinity;
            for (int i = 1; i < matrix.GetLength(1); i++)
            {
                if (min > matrix[matrix.GetLength(0)-1, i])
                {
                    min = matrix[matrix.GetLength(0)-1, i];
                    column = i;                    
                }
            }
            min = double.PositiveInfinity;
            double[] auxiliaryMatrix = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0)-1; i++)
            {
                auxiliaryMatrix[i] = (matrix[i, 0] / matrix[i, column]);
                if (auxiliaryMatrix[i] < min && auxiliaryMatrix[i] > 0)
                {
                    min = auxiliaryMatrix[i];
                    row = i;
                }
            }
            return matrix;
        }
        public double[,] ChangeMatrix(double[,] matrix)
        {            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i != row)
                {
                    matrix[i, column] = (matrix[i, column] / matrix[row,column]) * -1d; 
                }
            }
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (i != column)
                {
                    matrix[row, i] = (matrix[row, i] / matrix[row, column]);
                }
            }
            matrix[row, column] = 1 / matrix[row, column];
            return matrix;
        }
        public double[,] MethodGausa(double[,] matrix, double[,] secondarymatrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i != row && j != column)
                    {
                        matrix[i, j] =((secondarymatrix[row,column]*secondarymatrix[i,j]) - (secondarymatrix[row,j]*secondarymatrix[i,column])) / (secondarymatrix[row, column]);
                    }
                }                
            }
            
            return matrix;
        }
    }
}
