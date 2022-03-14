using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OrderInBackend.Model.Setup;

namespace OrderIn.Validators
{
    public class UserLoginValidators: AbstractValidator<UserLogin>
    {
        public UserLoginValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.phone)
                //.EmailAddress().WithMessage("Format email salah !")
                .NotNull().WithMessage("Nomor handphone tidak boleh kosong !")
                .NotEmpty().WithMessage("Nomor handphone tidak boleh kosong !");

            RuleFor(x => x.password)
                .NotNull().WithMessage("Password tidak boleh kosong !")
                .NotEmpty().WithMessage("Password tidak boleh kosong !");
        }
    }

    public class UsersValidators : AbstractValidator<Users>
    {
        public UsersValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.email)
                .NotNull().WithMessage("Email tidak boleh kosong !")
                .NotEmpty().WithMessage("Email tidak boleh kosong !");

            RuleFor(x => x.password)
                .NotNull().WithMessage("Password tidak boleh kosong !")
                .NotEmpty().WithMessage("Password tidak boleh kosong !");


            RuleFor(x => x.firstname)
                .NotNull().WithMessage("Nama tidak boleh kosong !")
                .NotEmpty().WithMessage("Nama tidak boleh kosong !");

            RuleFor(x => x.gender)
                .NotNull().WithMessage("Jenis kelamin tidak boleh kosong !")
                .NotEmpty().WithMessage("Jenis kelamin boleh kosong !");

            RuleFor(x => x.address)
                .NotNull().WithMessage("Alamat tidak boleh kosong !")
                .NotEmpty().WithMessage("Alamat tidak boleh kosong !");

            RuleFor(x => x.cityid)
                .NotNull().WithMessage("Kota tidak boleh kosong !")
                .NotEqual(0).WithMessage("Kota tidak boleh kosong !");

            RuleFor(x => x.phone)
                .NotNull().WithMessage("Nomor handphone tidak boleh kosong !")
                .NotEmpty().WithMessage("Nomor handphone tidak boleh kosong !");


            //RuleFor(x => x.isverified)
            //    .NotNull().WithMessage("Verifikasi tidak boleh kosong !");

            //RuleFor(x => x.ismerchant)
            //    .NotNull().WithMessage("Flag Merchant tidak boleh kosong !");
        }
    }
}
