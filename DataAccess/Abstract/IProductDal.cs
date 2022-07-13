using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // Product ile ilgili veritabanında operasyonları içeren interface.
    public interface IProductDal : IEntityRepository<Product>
    {
        // Tamam, IEntityRepository belirli işlemleri yapıyor ama, burada sadece product'a özel işlemler de yapabiliriz. Bu yüzden burası bize lazım.
        List<ProductDetailDto> getProductDetails();


    }
}
// code refactoring. Kodun iyileştirilmesi. IEntityRepository'i core katmanına taşıdık. Core katmanı, evrensel kodların olduğu katman.