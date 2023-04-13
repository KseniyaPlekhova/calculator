using integtest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Triangle;

namespace integtest.Classes
{
    public class TriangleService : ITriangleService
    {
        public bool IsValidTriangle(int a, int b, int c)
        {
            bool x = true;
            if (a <= 0 || b <= 0 || c <= 0)
            {
                x = false;
            }
            if (a + b < c || b + c < a || c + a < b)
            {
                x = false;
            }
            return x;
        }

        public TriangleType GetType(int a, int b, int c)
        {
            TriangleType type = 0;
            if (a == b & a != c || b == c & b != a || c == a & c != b)
            {
                type = TriangleType.Isosceles; 
            }
            else if (a == b && b == c)
            {
                type = TriangleType.Equilateral;
            }
            else
            {
                type = TriangleType.Scalene;
            }

            int[] v = { a, b, c };  
            Array.Sort(v);

            if (Math.Pow(c, 2) < Math.Pow(a, 2) + Math.Pow(b, 2))
            {
                type |= TriangleType.Oxygon;
            }
            else if (Math.Pow(c, 2) > Math.Pow(a, 2) + Math.Pow(b, 2))
            {
                type |= TriangleType.Obtuse;
            }
            else
            {
                type |= TriangleType.Right;
            }
            return type;
        }
        public double GetArea(double a, double b, double c)
        {
            return Math.Round(Math.Sqrt(((a + b + c) / 2) * (((a + b + c) / 2) - a) * (((a + b + c) / 2) - b) * (((a + b + c) / 2) - c)), 5);
            
        }

    }
}
