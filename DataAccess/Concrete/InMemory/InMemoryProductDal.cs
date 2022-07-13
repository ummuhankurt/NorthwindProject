using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    // Memory kullanacaksan yazdığın kodlar farklıdır, EntitiyFramework kullanacaksan yazdığın kodlar farklıdır.
    // Şimdi burda gerçekten veritabanı varmış gibi simule edicez.
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; // Bu class için global deiğşken.
        public InMemoryProductDal()
        {
            // Oracle'dan veya Sql'den veya Postgres veya MongoDb'den gleiyormuş gibi...
            _products = new List<Product> { 
                new Product {ProductId = 1, CategoryId = 1,ProductName = "Bardak",UnitPrice = 15, UnitsInStock = 15},
                new Product {ProductId = 2, CategoryId = 1,ProductName = "Kamera",UnitPrice = 500, UnitsInStock = 15},
                new Product {ProductId = 3, CategoryId = 2,ProductName = "Telefon",UnitPrice = 1500, UnitsInStock = 2},
                new Product {ProductId = 4, CategoryId = 2,ProductName = "Klavye",UnitPrice = 150, UnitsInStock = 65},
                new Product {ProductId = 5, CategoryId = 2,ProductName = "Fare",UnitPrice = 85, UnitsInStock = 1},
            };
        }
        public void Add(Product product)
        {

            _products.Add(product);
        }

        public void Delete(Product product)
        {

             /*  Product productToDelete = null;
             * _products.Remove(product); -> bu şekilde silemezsin. Çünkü UI'dan new'leyip gönderdiğin product nesnesinin
             * bellekteki yeri ayrı. Buradaki listenin heap'teki adresleri farklı. UI'dan istersen bilgileri birebir aynı
             * olan product'ı gönder. Yine de silmez. Referansları farklı çünkü.
             */

             /* Eğer LINQ bilmeseydim referans numarasını silmek için yazacağım kodlar aşağıdaki gibi olurdu.
             * 
             foreach (var p in _products)
             {
                if(product.ProductId == p.ProductId)
                {
                    productToDelete = p; // referans numarası atıyoruz.
                }
             }
             _products.Remove(productToDelete);
             LINQ -> Language Integrated Query. Dile gömülü sorgulama. Sql'de ki gibi filtreleme yapabilirsin.
             */

             // SingleOrDefault tek bir eleman bulmamızı sağlar.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Product Get(Product Entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            // Datayı business'a vermemiz lazım.
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> getProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            // Gönderdiğim ürün id'sine sahip olan listedeki ürün id'sini bul. Döndü,buldu ve atadı.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        } 
    }
}
