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
    public class CreatePaisDtoValidator : AbstractValidator<CreatePaisDto>
    {
        public CreatePaisDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.CodigoISO)
                .NotEmpty().WithMessage("El Codigo ISO es obligatorio.")
                .MaximumLength(3).WithMessage("El Codigo ISO no puede exceder los 3 caracteres.");

            RuleFor(x => x.Moneda)
                .NotEmpty().WithMessage("El Nombre de la moneda es obligatorio.")
                .MinimumLength(3).WithMessage("El Nombre de la moneda debe tener al menos 3 caracteres.");

            RuleFor(x => x.IdiomaOficial)
                .NotEmpty().WithMessage("El Idioma Oficial es obligatoria.");

        }

    }


    public class UpdatePaisDtoValidator : AbstractValidator<UpdatePaisDto>
    {
        public UpdatePaisDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.CodigoISO)
                .NotEmpty().WithMessage("El Codigo ISO es obligatorio.")
                .MaximumLength(3).WithMessage("El Codigo ISO no puede exceder los 3 caracteres.");

            RuleFor(x => x.Moneda)
                .NotEmpty().WithMessage("El Nombre de la moneda es obligatorio.")
                .MinimumLength(3).WithMessage("El Nombre de la moneda debe tener al menos 3 caracteres.");

            RuleFor(x => x.IdiomaOficial)
                .NotEmpty().WithMessage("El Idioma Oficial es obligatoria.");
        }

    }

}
