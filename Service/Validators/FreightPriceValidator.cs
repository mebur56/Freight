﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Validators
{
    public class FreightPriceValidator : AbstractValidator<IList<FreightPrice>>
    {
        public FreightPriceValidator()
        {
            //RuleFor(c => c.Name)
            //    .NotEmpty().WithMessage("Please enter the name.")
            //    .NotNull().WithMessage("Please enter the name.");


        }
    }
}
