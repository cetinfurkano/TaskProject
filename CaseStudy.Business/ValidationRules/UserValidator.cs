using CaseStudy.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CaseStudy.Business.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).Cascade(CascadeMode.Stop).
                Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"));
           
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).MaximumLength(30);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).MaximumLength(30);
         


        }
    }
}
