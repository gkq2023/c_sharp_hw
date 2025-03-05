using System;
using System.Numerics;

namespace Prime
{
    class Erato
    {
        static void Main()
        {

            List<int> list = new List<int>();
            for (int i = 2; i < 100; i++) { list.Add(i); }
            list = Eratosthenes(list);
            for (int i = 0; i < list.Count; i++) { Console.WriteLine(list[i].ToString()); }

        }
        static List<int> Eratosthenes(List<int> list)
        {
            if (list.Count == 0) return list;
            List<int> newlist= new List<int>();
            bool flag = false;
            newlist.Add(list[0]);
            for(int i = 1; i < list.Count; i++)
            {
                if (list[i] % list[0] != 0)
                {
                    newlist.Add(list[i]);
                }
                else
                {
                    flag = true;
                }
            }
            return flag ? Eratosthenes(newlist) : list;
        }
    }
}
