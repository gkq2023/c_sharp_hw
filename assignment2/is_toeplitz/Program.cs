using System;

namespace toe
{
    class Sol
    {
        static bool IsToeplitzMatrix(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;

            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (matrix[i][j] != matrix[i + 1][j + 1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            int[][] matrix = new int[][]
            {
                new int[] {1, 2, 3, 4},
                new int[] {5, 1, 2, 3},
                new int[] {9, 5, 1, 2}
            };


            bool result = IsToeplitzMatrix(matrix);
            Console.WriteLine(""+result);
        }
    }
}