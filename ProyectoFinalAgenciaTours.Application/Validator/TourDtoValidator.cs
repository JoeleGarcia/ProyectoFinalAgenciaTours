using FluentValidation;
using FluentValidation.Validators;
using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Validator
{
    public class CreateTourDtoValidator : AbstractValidator<CreateTourDto>
    {
        public CreateTourDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("la fecha es obligatorio.");

            RuleFor(x => x.Horas)
                .NotEmpty().WithMessage("Las Horas del tour es obligatoria.");

            RuleFor(x => x.TasaImpuesto)
                .NotEmpty().WithMessage("La tasa de impuesto es obligatoria.");

            RuleFor(x => x.Precio)
                .NotEmpty().WithMessage("El precio es obligatoria.");
        }

    }
    public class UpdateTourDtoValidator : AbstractValidator<UpdateTourDto>
    {
        public UpdateTourDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("la fecha es obligatorio.");

            RuleFor(x => x.Horas)
                .NotEmpty().WithMessage("Las Horas del tour es obligatoria.");

            RuleFor(x => x.TasaImpuesto)
                .NotEmpty().WithMessage("La tasa de impuesto es obligatoria.");

            RuleFor(x => x.Precio)
                .NotEmpty().WithMessage("El precio es obligatoria.");

        }

    }

}
