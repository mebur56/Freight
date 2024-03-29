﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Validators
{
    public class FreightValidator : AbstractValidator<IList<Freight>>
    {
        public FreightValidator()
        {
            RuleFor(c => c.Count)
                .GreaterThan(0)
                .WithMessage("Freights list must be greater than zero");


        }
    }
}
