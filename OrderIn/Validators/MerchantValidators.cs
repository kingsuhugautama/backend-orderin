using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OrderInBackend.Model.Setup;

namespace OrderIn.Validators
{
    public class MerchantValidators : AbstractValidator<MasterMerchant>
    {
        public MerchantValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.userid)
                .NotNull().WithMessage("User tidak boleh kosong !")
                .NotEqual(0).WithMessage("User tidak boleh kosong !");

            RuleFor(x => x.merchantname)
                .NotNull().WithMessage("Nama merchant tidak boleh kosong !")
                .NotEmpty().WithMessage("Nama merchant tidak boleh kosong !");

            RuleFor(x => x.logoimageurl)
                .NotNull().WithMessage("Logo merchant tidak boleh kosong !")
                .NotEmpty().WithMessage("Logo merchant tidak boleh kosong !");

        }
    }

    public class MasterProductValidators : AbstractValidator<MasterProduct>
    {
        public MasterProductValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.merchantid)
                .NotNull().WithMessage("Merchant tidak boleh kosong !")
                .NotEqual(0).WithMessage("Merchant tidak boleh kosong !");

            RuleFor(x => x.categorymenuid)
                .NotNull().WithMessage("Kategori tidak boleh kosong !")
                .NotEqual(0).WithMessage("Kategori tidak boleh kosong !");

            RuleFor(x => x.productname)
                .NotNull().WithMessage("Nama produk tidak boleh kosong !")
                .NotEmpty().WithMessage("Nama produk tidak boleh kosong !");

            RuleFor(x => x.productimageurl)
                .NotNull().WithMessage("Gambar produk tidak boleh kosong !")
                .NotEmpty().WithMessage("Gambar produk tidak boleh kosong !");

            RuleFor(x => x.productprice)
                .NotNull().WithMessage("Harga produk tidak boleh kosong !")
                .NotEqual(0).WithMessage("Harga produk tidak boleh kosong !");

            RuleFor(x => x.productdescription)
                .NotNull().WithMessage("Deskripsi produk tidak boleh kosong !")
                .NotEmpty().WithMessage("Deskripsi produk tidak boleh kosong !");

            RuleFor(x => x.isbutton)
                .NotNull().WithMessage("Status tombol tidak boleh kosong !")
                .NotEmpty().WithMessage("Status tombol tidak boleh kosong !");

            RuleFor(x => x.isactive)
                .NotNull().WithMessage("Status aktif tidak boleh kosong !")
                .NotEmpty().WithMessage("Status aktif tidak boleh kosong !");

        }
    }
}
