﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspets.Autofac.Validation
{
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {   
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        // Validation, doğrulama olduğu için metodun başında yapılır. O yüzden OnBefore metodunu eziyoruz.
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // Bu alttaki satır metodun argümanlarını yani parametrelerini gezer.Örneğin Add() metodu.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                // validator = productvalidator , entity = product.
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
