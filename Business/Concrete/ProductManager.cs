using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspets.Autofac.Caching;
using Core.Aspets.Autofac.Performance;
using Core.Aspets.Autofac.Transaction;
using Core.Aspets.Autofac.Validation;
using Core.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Business.Concrete  
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))] 
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product) 
        {
            var result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), ChecekIfCategoryCount());
            if (result!= null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
            
        public IResult Delete(Product product) 
        {

            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        [CacheAspect] // Key,value
        public IDataResult<List<Product>> GetAll()
        {
            if(DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //return new DataResult<List<Product>>(_productDal.GetAll(),true);
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }
        [CacheAspect]
        [PerformanceAspect(5)] // Bu metodun çalışması 5 saniyeyi geçerse beni uyar.Sistemde yavaşlık var.
        public IDataResult<Product> GetById(int id)
        {
            return new DataResult<Product>(_productDal.Get(p => p.ProductId == id),true);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), true);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new DataResult<List<ProductDetailDto>>(_productDal.getProductDetails(),false);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] // Bellekte, içerisinde Get olan tüm key'leri iptal et.
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                if (CheckIfProductNameExists(product.ProductName).Success)
                {
                    _productDal.Update(product);
                    return new SuccessResult(Messages.ProductUpdated);
                }
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult(); 
        }

        private IResult CheckIfProductNameExists(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult ChecekIfCategoryCount()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
                {
                    throw new Exception("");
                }
            Add(product);   
            return null;
        }
    } 
}
