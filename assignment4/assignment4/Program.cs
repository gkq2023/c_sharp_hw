using System;

// 链表节点
public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Data { get; set; }

    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}

// 泛型链表类
public class GenericList<T>
{
    private Node<T> head;
    private Node<T> tail;

    public GenericList()
    {
        tail = head = null;
    }

    public Node<T> Head
    {
        get => head;
    }

    public void Add(T t)
    {
        Node<T> n = new Node<T>(t);
        if (tail == null)
        {
            head = tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
    }


    public void ForEach(Action<T> action)
    {
        Node<T> current = head;
        while (current != null)
        {
            action(current.Data);
            current = current.Next;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 整型List
        GenericList<int> intlist = new GenericList<int>();
        for (int x = 0; x < 10; x++)
        {
            intlist.Add(x);
        }


        Console.WriteLine("打印整型链表：");
        intlist.ForEach(x => Console.WriteLine(x));

        int sum = 0;
        int max = int.MinValue;
        int min = int.MaxValue;
        intlist.ForEach(x =>
        {
            sum += x;
            if (x > max) max = x;
            if (x < min) min = x;
        });
        Console.WriteLine($"总和：{sum} 最大值：{max} 最小值：{min}");

        // 字符串型List
        GenericList<string> strList = new GenericList<string>();
        for (int x = 0; x < 10; x++)
        {
            strList.Add("str" + x);
        }

        Console.WriteLine("\n打印字符串链表：");
        strList.ForEach(s => Console.WriteLine(s));
    }
}