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
    public class CreateDestinoDtoValidator : AbstractValidator<CreateDestinoDto>
    {
        public CreateDestinoDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.CapacidadMaxima)
                .NotEmpty().WithMessage("La capacidad es obligatoria.");

        }

        private bool SoloNumeros(string value)
        {
            if (value != null && value.All(char.IsDigit))
            {
                return true;
            }

            return false;
        }

    }


    public class UpdateDestinoDtoValidator : AbstractValidator<UpdateDestinoDto>
    {
        public UpdateDestinoDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.CapacidadMaxima)
                .NotEmpty().WithMessage("La capacidad es obligatoria.");

        }

        private bool SoloNumeros(string value)
        {
            if (value != null && value.All(char.IsDigit))
            {
                return true;
            }

            return false;
        }

    }

}
