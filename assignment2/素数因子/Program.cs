using System;

namespace PrimeFactor
{
    class Factor
    {
        static void Main()
        {
            Console.Write("请输入要分解的数字：");
            string input = Console.ReadLine();


            if(!int.TryParse(input, out int result))
            {
                Console.WriteLine("请输入整数");
            }
            else
            {
                Factorize(result);
            }

        }

        static void Factorize(int N)
        {
            for (int i = 2; i * i <= N; i++)
            {
                if (N % i == 0)
                {  
                    while (N % i == 0) N /= i;
                    Console.Write(i.ToString()+" ");
                }
            }
            if (N != 1)
            {  
                Console.Write(N.ToString());
            }
        }
    }
}