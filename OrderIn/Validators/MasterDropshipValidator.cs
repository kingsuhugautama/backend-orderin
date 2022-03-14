using FluentValidation;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class MasterDropshipValidators : AbstractValidator<MasterDropship>
    {
        public MasterDropshipValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.merchantid)
                            .NotNull().WithMessage("Merchant tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Merchant tidak boleh kosong !");
            RuleFor(x => x.label)
                            .NotNull().WithMessage("Label tidak boleh kosong!")
                            .NotEmpty().WithMessage("Label tidak boleh kosong !");
            RuleFor(x => x.longitude)
                            .NotNull().WithMessage("Longitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Longitude tidak boleh kosong !");
            RuleFor(x => x.latitude)
                            .NotNull().WithMessage("Latitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Latitude tidak boleh kosong !");
            RuleFor(x => x.dropshipaddress)
                            .NotNull().WithMessage("Alamat Dropship tidak boleh kosong!")
                            .NotEmpty().WithMessage("Alamat Dropship tidak boleh kosong !");
            RuleFor(x => x.description)
                            .NotNull().WithMessage("Description tidak boleh kosong!")
                            .NotEmpty().WithMessage("Description tidak boleh kosong !");
            RuleFor(x => x.contactname)
                            .NotNull().WithMessage("Nama Kontak tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Kontak tidak boleh kosong !");
            RuleFor(x => x.contactphone)
                            .NotNull().WithMessage("Telepon tidak boleh kosong!")
                            .NotEmpty().WithMessage("Telepon tidak boleh kosong !");
            RuleFor(x => x.radius)
                            .NotNull().WithMessage("Radius tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Radius tidak boleh kosong !");

        }

    }
}
