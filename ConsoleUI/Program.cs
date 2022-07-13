using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    // SOLID. Open Princliple : Yaptığın yazılıma yeni bir özellik ekliyorsan, mevcuttaki hiçbir koduna dokunamazsın.
    class Program
    {
        static void Main(string[] args)
        {
            // ProductManager productManager = new ProductManager(new EfProductDal());
            //CategoryTest();
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var item in productManager.GetProductDetails())
            {
                Console.WriteLine("Product Name  : " + item.ProductName);
                Console.WriteLine("Category : " + item.CategoryName );
                Console.WriteLine("Units in Stock : " + item.UnitsInStock);
                Console.WriteLine("-------------------------------");
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var item in categoryManager.GetAll())
            {
                Console.WriteLine(item.CategoryName);
            }

            Console.WriteLine("3 numaralı kategori : ");
            Console.WriteLine(categoryManager.GetById(3).CategoryName);
        }
    }
}
