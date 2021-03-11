using CaseStudy.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Business.ValidationRules
{
    public class StatusValidator: AbstractValidator<Status>
    {
        public StatusValidator()
        {
            RuleFor(s => s.StatusDesc).NotEmpty();
        }
    }
}
