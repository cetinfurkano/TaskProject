﻿using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Core.Validation
{
   public static class ValidationTool
    {
        public static ValidationResult Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            return result;
        }
    }
}
