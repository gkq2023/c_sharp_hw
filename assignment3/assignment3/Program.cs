using System;

// 定义一个接口，所有形状类都需要实现这个接口
public interface IShape
{
    double CalculateArea();
    bool IsValid();
}

// 长方形类
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double CalculateArea()
    {
        return Width * Height;
    }

    public bool IsValid()
    {
        return Width > 0 && Height > 0;
    }
}

// 正方形类
public class Square : IShape
{
    public double SideLength { get; set; }

    public Square(double sideLength)
    {
        SideLength = sideLength;
    }

    public double CalculateArea()
    {
        return SideLength * SideLength;
    }

    public bool IsValid()
    {
        return SideLength > 0;
    }
}

// 三角形类
public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(double triangleBase, double height)
    {
        Base = triangleBase;
        Height = height;
    }

    public double CalculateArea()
    {
        return 0.5 * Base * Height;
    }

    public bool IsValid()
    {
        return Base > 0 && Height > 0;
    }
}

// 圆形类
public class Circle : IShape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }

    public bool IsValid()
    {
        return Radius > 0;
    }
}

// 简单工厂类
public class ShapeFactory
{
    private static Random random = new Random();

    public static IShape CreateShape()
    {
        int shapeType = random.Next(0, 4); // 范围改为 0 到 3，包含圆形

        switch (shapeType)
        {
            case 0:
                return new Rectangle(random.Next(1, 10), random.Next(1, 10));
            case 1:
                return new Square(random.Next(1, 10));
            case 2:
                return new Triangle(random.Next(1, 10), random.Next(1, 10));
            case 3:
                return new Circle(random.Next(1, 10));
            default:
                throw new InvalidOperationException("Invalid shape type");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        double totalArea = 0;

        for (int i = 0; i < 10; i++)
        {
            IShape shape = ShapeFactory.CreateShape();

            if (shape.IsValid())
            {
                double area = shape.CalculateArea();
                totalArea += area;
                Console.WriteLine($"Shape {i + 1}: 面积 = {area}");
            }
            else
            {
                Console.WriteLine($"Shape {i + 1}: 不合法");
            }
        }

        Console.WriteLine($"总面积: {totalArea}");
    }
}