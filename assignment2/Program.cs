using System;

namespace Arr
{
    class MaxMinAvg
    {
        static void Main()
        {
            Console.Write("请输入要一行用空格分隔的数字：");
            string input = Console.ReadLine();
            if (input == null || input.Length == 0) { Console.WriteLine("请按正确格式输入"); }
            string[] inputs = input.Split(' ');
            double max = double.NegativeInfinity, min = double.PositiveInfinity, sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if(!double.TryParse(inputs[i],out double num))
                {
                    Console.WriteLine("请按正确格式输入");
                }
                else
                {
                    max = double.Max(max, num);
                    min = double.Min(min, num);
                    sum += num;
                }
            }
            Console.WriteLine($"Max:{max},Min:{min},avg:{sum / inputs.Length}");
        }

    }
}