﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Ef yani entity framework. O yüzden NorthwindContex ti gönderiyoruz.
    public class EfCategoryDal : EfEntityRepositoryBase<Category,NorthwindContext> , ICategoryDal
    {

    }
}
