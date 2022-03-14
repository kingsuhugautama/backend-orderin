using FluentValidation;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class SetupShipmentValidator : AbstractValidator<MasterShipment>
    {
        public SetupShipmentValidator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.keterangan)
                            .NotNull().WithMessage("Keterangan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Keterangan tidak boleh kosong !");

        }

    }
}
