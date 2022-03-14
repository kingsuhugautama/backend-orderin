using FluentValidation;
using OrderInBackend.Model.Setup;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class TransOpenPoHeaderValidators : AbstractValidator<TransOpenPoHeader>
    {
        public TransOpenPoHeaderValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.openpodate)
                .NotNull().WithMessage("Tanggal dibuka tidak boleh kosong!")
                .NotEmpty().WithMessage("Tanggal dibuka tidak boleh kosong !");

            RuleFor(x => x.closepodate)
                            .NotNull().WithMessage("Tanggal ditutup tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tanggal ditutup tidak boleh kosong !");
            RuleFor(x => x.kota)
                            .NotNull().WithMessage("Kota tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kota tidak boleh kosong !");
            RuleFor(x => x.statustransaksi)
                            .NotNull().WithMessage("Status Transaksi tidak boleh kosong!")
                            .NotEmpty().WithMessage("Status Transaksi tidak boleh kosong !");


        }

    }

    public class TransOpenPoDetailProductValidators : AbstractValidator<TransOpenPoDetailProduct>
    {
        public TransOpenPoDetailProductValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.productid)
                .NotNull().WithMessage("Produk tidak boleh kosong!")
                .NotEqual(0).WithMessage("Produk tidak boleh kosong !");
            RuleFor(x => x.productprice)
                            .NotNull().WithMessage("Harga Produk tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Produk tidak boleh kosong !");

        }

    }

    public class TransOpenPoDetailDropshipKategoriValidators : AbstractValidator<TransOpenPoDetailDropshipKategori>
    {
        public TransOpenPoDetailDropshipKategoriValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.categorymenuid)
                            .NotNull().WithMessage("Kategori tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Kategori tidak boleh kosong !");

        }

    }

    public class TransOpenPoDetailDropshipValidators : AbstractValidator<TransOpenPoDetailDropship>
    {
        public TransOpenPoDetailDropshipValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.dropshipid)
                            .NotNull().WithMessage("Titik Dropship tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Titik Dropship tidak boleh kosong !");
            RuleFor(x => x.keterangan)
                            .NotNull().WithMessage("Keterangan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Keterangan tidak boleh kosong !");

        }

    }
}
