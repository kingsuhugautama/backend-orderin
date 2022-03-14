using FluentValidation;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class MasterStatusTransaksiOrderValidators : AbstractValidator<MasterStatusTransaksiOrder>
    {
        public MasterStatusTransaksiOrderValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.keterangan)
                            .NotNull().WithMessage("Keterangan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Keterangan tidak boleh kosong !");

        }

    }
}
