using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Triangle
{
    [Flags]
    public enum TriangleType
    {
        Oxygon = 1, 
        Obtuse = 2, 
        Right = 4,
        Scalene = 8, 
        Isosceles = 16, 
        Equilateral = 32, 
    }
    public int id { get; set; }
    public int a { get; set; }
    public int b { get; set; }
    public int c { get; set; }
    public double area { get; set; }
    public TriangleType type { get; set; }
    public bool isvalid { get; set; }

    public Triangle(int id, int a, int b, int c, double area, TriangleType type)
    {
        this.id = id;
        this.a = a;
        this.b = b;
        this.c = c;
        this.area = Math.Round(area, 5);
        this.type = type;
        isvalid = false;
    }
    public Triangle()
    {

    }
}
