using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidation : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Araç Adı boş olamaz");
            RuleFor(p => p.Name).NotNull().WithMessage("Araç adı boş olamaz.");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("3 karakterden az olamaz.");
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model boş olamaz");
            RuleFor(p => p.Model).NotNull().WithMessage("Model boş olamaz.");
            RuleFor(p => p.Model).MinimumLength(3).WithMessage("3 karakterden az olamaz.");
        }
    }
}
