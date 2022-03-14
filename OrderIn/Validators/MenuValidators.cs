using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OrderInBackend.Model.Setup;

namespace OrderIn.Validators
{
    public class PromoMenuValidators : AbstractValidator<PromoMenu>
    {
        public PromoMenuValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.productid)
                .NotNull().WithMessage("Produk tidak boleh kosong")
                .NotEqual(0).WithMessage("Produk tidak boleh kosong");

            RuleFor(x => x.promoanimationurl)
                .NotNull().WithMessage("Gambar promo tidak boleh kosong")
                .NotEmpty().WithMessage("Gambar promo tidak boleh kosong");
        }
    }

    public class BannerMenuValidators : AbstractValidator<BannerMenu>
    {
        public BannerMenuValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.bannermenuname)
                .NotNull().WithMessage("Nama banner tidak boleh kosong")
                .NotEmpty().WithMessage("Nama banner tidak boleh kosong");

            RuleFor(x => x.bannerimageurl)
                .NotNull().WithMessage("Gambar banner tidak boleh kosong")
                .NotEmpty().WithMessage("Gambar banner tidak boleh kosong");
        }
    }

    public class MasterCategoryMenuValidators : AbstractValidator<MasterCategoryMenu>
    {
        public MasterCategoryMenuValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.categorymenuname)
                .NotNull().WithMessage("Nama kategori tidak boleh kosong")
                .NotEmpty().WithMessage("Nama kategori tidak boleh kosong");

            RuleFor(x => x.categoryimageurl)
                .NotNull().WithMessage("Gambar kategori tidak boleh kosong")
                .NotEmpty().WithMessage("Gambar kategori tidak boleh kosong");
        }
    }
}
