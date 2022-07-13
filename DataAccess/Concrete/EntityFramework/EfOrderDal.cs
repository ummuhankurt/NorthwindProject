using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // IOrderDal şunu eklememizin sebebi, Order için özel işlemlere sahip olabilir.
    public class EfOrderDal : EfEntityRepositoryBase<Order,NorthwindContext> , IOrderDal
    {

    }
}
