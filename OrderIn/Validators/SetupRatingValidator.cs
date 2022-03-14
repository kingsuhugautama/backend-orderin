using FluentValidation;
using OrderInBackend.Model.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class MasterRatingValidators : AbstractValidator<MasterRating>
    {
        public MasterRatingValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.userentry)
                            .NotNull().WithMessage("User tidak boleh kosong!")
                            .NotEqual(0).WithMessage("User tidak boleh kosong !");
            RuleFor(x => x.merchantid)
                            .NotNull().WithMessage("Merchant tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Merchant tidak boleh kosong !");


            RuleFor(x => x.deliveryRating)
                            .NotNull().WithMessage("Rating Delivery tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Rating Delivery tidak boleh kosong !");

            RuleFor(x => x.packageRating)
                            .NotNull().WithMessage("Rating Packaging tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Rating Packaging tidak boleh kosong !");

            RuleFor(x => x.productRating)
                            .NotNull().WithMessage("Rating Product tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Rating Product tidak boleh kosong !");
        }

    }
}
