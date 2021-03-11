using CaseStudy.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CaseStudy.Business.ValidationRules
{
    public class UserTaskValidator:AbstractValidator<UserTask>
    {
        public UserTaskValidator()
        {
            RuleFor(u => u.Priority).GreaterThanOrEqualTo(0);
            RuleFor(u => u.Priority).LessThanOrEqualTo(100);

            RuleFor(u => u.StartingDate).NotEmpty();

            RuleFor(u=> u.DueDate).GreaterThanOrEqualTo(u => u.StartingDate);

            RuleFor(u => u.ExpectedDueDate).GreaterThanOrEqualTo(u => u.StartingDate);
            
            RuleFor(u=> u.AppointedId).NotEmpty();
            RuleFor(u=> u.AssignedId).NotEmpty();

            

        }
    }
}
