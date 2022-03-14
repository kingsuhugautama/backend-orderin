using FluentValidation;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class TransPembayaranValidators : AbstractValidator<TransPembayaran>
    {
        public TransPembayaranValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.orderheaderid)
                            .NotNull().WithMessage("Nomor order belum dipilih!")
                            .NotEqual(0).WithMessage("Nomor order belum dipilih!");
            RuleFor(x => x.idrekening)
                            .NotNull().WithMessage("Rekening tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Rekening tidak boleh kosong!");

        }

    }
}
