using integtest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integtest.Classes
{
    public class TriangleValidateService : ITriangleValidateService
    {
        private readonly ITriangleProvider TriangleProvider;
        private readonly ITriangleService TriangleService;
       
        public TriangleValidateService(ITriangleProvider TriangleProvider, ITriangleService TriangleService)
        {
            this.TriangleProvider = TriangleProvider;
            this.TriangleService = TriangleService;
        }

        public bool IsAllValid()
        {
            
            List<Triangle> list = TriangleProvider.GettAll();
            bool b = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (TriangleService.IsValidTriangle(list[i].a, list[i].b, list[i].c) == false || TriangleService.GetType(list[i].a, list[i].b, list[i].c) != list[i].type || Math.Abs(TriangleService.GetArea(list[i].a, list[i].b, list[i].c) - list[i].area) > double.Epsilon) 
                {
                    list[i].isvalid = false;
                    TriangleProvider.Update(list[i]);
                    b = false;

                }
               
                else 
                {
                    list[i].isvalid = true;
                    TriangleProvider.Update(list[i]);
                }
            }
            
            return b;
        }

        public bool IsValid(int id)
        {
            Triangle triangle = TriangleProvider.GetById(id);
            bool b = true;
            if (TriangleService.IsValidTriangle(triangle.a, triangle.b, triangle.c) == false)
            {
                b = false;
            }
            else if (TriangleService.GetType(triangle.a, triangle.b, triangle.c) != triangle.type)
            {
                b = false;
            }
            else if (TriangleService.GetArea(triangle.a, triangle.b, triangle.c) != triangle.area)
            {
                b = false;
            }
            if (b == true)
            {                              
                triangle.isvalid = true;
                TriangleProvider.Update(triangle);               
            }
            return b;
        }
    }
}
