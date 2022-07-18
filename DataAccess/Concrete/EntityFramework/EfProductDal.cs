using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // IProductDal, product nesnesi için her işleme sahip(IEntityRepository kullanarak tabiiki)
    // EfProductDal ise IProductDal'dan inherit edilerek Product nesnesi için her işleme sahip.
    // EfEntityRepository ise product için(başka bir nesne de olabilir), EntityFramework (NorthWindContext)altyapısını oluşturuyor.
    // Northwind context olmak zorunda değil başka bir şey de olabilir.
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        // Ekstra istediğim join işlemi.
        public List<ProductDetailDto> getProductDetails()
        {
            using (NorthwindContext contex = new NorthwindContext())
            {
                var result = from p in contex.Products
                             join c in contex.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto { ProductId = p.ProductId , ProductName = p.ProductName , 
                                 CategoryName = c.CategoryName , UnitsInStock = p.UnitsInStock};
                return result.ToList();
            }
            
        }
    }
}
