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
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            Console.WriteLine(productManager.GetAll().Message);
            if(productManager.GetAll().Success)
            {
                foreach (var item in productManager.GetAll().Data)
                {
                    Console.WriteLine(item.ProductName);
                }
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var item in categoryManager.GetAll().Data)
            {
                Console.WriteLine(item.CategoryName);
            }

            Console.WriteLine("3 numaralı kategori : ");
            Console.WriteLine(categoryManager.GetById(3).Data);
        }
    }
}
