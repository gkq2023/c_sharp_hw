using System;

namespace Calculator
{
    class CalculatorProgram
    {
        static void Main()
        {
            int num1 = GetValidInteger("请输入第一个整数：");
            int num2 = GetValidInteger("请输入第二个整数：");

            Console.Write("请选择运算符：\n" +
                          "1: 加 (+)\n" +
                          "2: 减 (-)\n" +
                          "3: 乘 (*)\n" +
                          "4: 除 (/)\n" +
                          "请输入选项编号（1-4）：");

            int operation;
            while (!int.TryParse(Console.ReadLine(), out operation) || operation < 1 || operation > 4)
            {
                Console.Write("输入无效，请重新输入选项编号（1-4）：");
            }

            try
            {
                double result = Calculate(num1, num2, operation);
                Console.WriteLine("运算结果为：{0:F2}", result); // 保留两位小数
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("错误：除数不能为零！");
            }
        }

        static int GetValidInteger(string prompt)
        {
            int number;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("输入无效，请重新输入整数：");
            }
            return number;
        }

        static double Calculate(int a, int b, int operation)
        {
            switch (operation)
            {
                case 1:
                    return a + b;
                case 2:
                    return a - b;
                case 3:
                    return a * b;
                case 4:
                    if (b == 0) throw new DivideByZeroException();
                    return (double)a / b; 
                default:
                    throw new InvalidOperationException(); 
            }
        }
    }
}