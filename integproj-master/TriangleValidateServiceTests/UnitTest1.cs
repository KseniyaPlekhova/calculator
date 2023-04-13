using Microsoft.VisualStudio.TestTools.UnitTesting;
using integtest;
using Moq;
using System;
using System.Collections.Generic;
using Npgsql;
using integtest.Interfaces;
using integtest.Classes;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Org.BouncyCastle.Asn1.Ocsp;

namespace PTPM_4
{
    [TestClass]
    
    public class TriangleValidateServiceUnitTests
    {
        private Mock<ITriangleProvider> triangleProvider;
        private ITriangleService triangleService;
        private ITriangleValidateService triangleValidateService;

        [TestInitialize]
        public void TestInitialize()
        {
            triangleProvider = new Mock<ITriangleProvider>();
            triangleService = new TriangleService();
            triangleValidateService = new TriangleValidateService(triangleProvider.Object, triangleService);
        }

        [TestMethod]
        public void IsValid_True()
        {

            //triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(1, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            //Assert.AreEqual(true, triangleValidateService.IsValid(1));

            //triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(1, -8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            //Assert.AreEqual(false, triangleValidateService.IsValid(1));

            //triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(1, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            //Assert.AreEqual(true, triangleValidateService.IsValid(1));

            Assert.IsTrue(triangleValidateService.IsAllValid()); :triangleProvider.verify(m => m.Update(triangle));

            triangle.A = -100000000;
            Assert.IsFalse(triangleValidateService.IsAllValidate());
            triangleProvider.Verify(m => m.Update(triangle));
        }
        [TestMethod]
        public void IsValid_False()
        {
            //triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(2, -5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            //Assert.AreEqual(false, triangleValidateService.IsValid(2));
            triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(2, -8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            Assert.AreEqual(false, triangleValidateService.IsValid(2));
            triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(2, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            Assert.AreEqual(true, triangleValidateService.IsValid(2));
            triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle(2, -8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            Assert.AreEqual(false, triangleValidateService.IsValid(2));
        }
        [TestMethod]
        public void IsValid_EmptyTriangle()
        {
            triangleProvider.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Triangle());
            Assert.AreEqual(false, triangleValidateService.IsValid(3));

        }
        [TestMethod]
        public void IsAllValid_True() // Метод IsAllValid должен вернуть true, если все треугольники, полученные от triangleProvider.GettAll(), являются допустимыми согласно критериям валидации
        {
            triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle>{ new Triangle(4, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon), new Triangle(5, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon)});
            Assert.AreEqual(true, triangleValidateService.IsAllValid());

            triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle> { new Triangle(4, 5, 6, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon), new Triangle(5, 8, 8, 10, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon)});
            Assert.AreEqual(false, triangleValidateService.IsAllValid());

            triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle> { new Triangle(4, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon), new Triangle(5, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon) });
            Assert.AreEqual(true, triangleValidateService.IsAllValid());

            //triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle> 
            //{ 
            //    new Triangle(4, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon), 
            //    new Triangle(5, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon) 
            //});
            //Assert.AreEqual(true, triangleValidateService.IsAllValid());

        }
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(Triangle.TriangleType.Scalene | Triangle.TriangleType.Obtuse, triangleService.GetType(3,8,10));
        }
        [TestMethod]
        public void IsAllValid_False() //Этот тест проверяет, что метод IsAllValid возвращает false, если хотя бы один из треугольников в списке является невалидным.
            {
                triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle>
            {
              new Triangle(6, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(7, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(8, 3, 8, 10, 0,Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon)
            });
                Assert.AreEqual(false, triangleValidateService.IsAllValid());
                triangleProvider.Reset();

                triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle>
            {
              new Triangle(6, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(7, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(8, 3, 8, 10, 9.9215674164922147,Triangle.TriangleType.Scalene | Triangle.TriangleType.Obtuse)
            });
                Assert.AreEqual(true, triangleValidateService.IsAllValid());
                triangleProvider.Reset();



                triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle>
            {
              new Triangle(6, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(7, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon),
              new Triangle(8, 3, 8, 10, 0,Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon)
            });
                Assert.AreEqual(false, triangleValidateService.IsAllValid());
            

        }
        [TestMethod]
        public void IsAllValid_EmptyTriangle() //Этот тест проверяет, что метод IsAllValid вернет false, если список, который возвращает провайдер треугольников, содержит пустые треугольники. 
        {
            triangleProvider.Setup(m => m.GettAll()).Returns(new List<Triangle> { new Triangle(), new Triangle() });
            Assert.AreEqual(false, triangleValidateService.IsAllValid());
        }
    }
    [TestClass]
    public class TriangleValidateServiceIntegrationTests
    {
        private ITriangleProvider triangleProvider;
        private ITriangleService triangleService;
        private ITriangleValidateService triangleValidateService;
       

        [TestInitialize]
        public void TestInitialize()
        {
            triangleProvider = new TriangleProvider(); 
            triangleService = new TriangleService();
            triangleValidateService = new TriangleValidateService(triangleProvider, triangleService);
        }
        [TestMethod]
        public void IsValidTrue()
        {
            triangleProvider.Save(new Triangle(7, 5, 5, 8, 12, Triangle.TriangleType.Isosceles | Triangle.TriangleType.Obtuse));
            bool actual = triangleValidateService.IsValid(7);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsAllValid_false()
        {
            triangleProvider.Save(new Triangle(8, -5, 5, 8, 12, Triangle.TriangleType.Isosceles | Triangle.TriangleType.Obtuse));
            bool actual = triangleValidateService.IsAllValid();
            Assert.AreEqual(false, actual);
        }
        
        [TestMethod]
        public void GetTypeTriangle()
        {
            Triangle triangle;
            triangleProvider.Save(triangle = new Triangle(9, 5, 5, 5, 10.8253175473054831, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            Triangle.TriangleType expected = triangle.type;
            Triangle.TriangleType actual = triangleService.GetType(triangle.a, triangle.b, triangle.c);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetAreaTriangle()
        {
            Triangle triangle;
            triangleProvider.Save(triangle = new Triangle(10, 8, 8, 8, 27.71281292110203669643, Triangle.TriangleType.Equilateral | Triangle.TriangleType.Oxygon));
            double expected = triangle.area;
            double actual = triangleService.GetArea(triangle.a, triangle.b, triangle.c);
            Assert.IsTrue(Math.Abs(expected - actual) < 1e-9); //сравнение ожидаемого и фактического значений с учетом погрешности 1e-9
        }
        [TestMethod]
        public void IsValidTriangle()
        {
            triangleProvider.Save(new Triangle(12, 8, 8, 2, 7.9372539331937718, Triangle.TriangleType.Isosceles | Triangle.TriangleType.Oxygon));
            Assert.IsTrue(triangleValidateService.IsValid(12));
        }
    }
}
