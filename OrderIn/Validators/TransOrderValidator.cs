using FluentValidation;
using OrderInBackend.Model.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderIn.Validators
{
    public class TransOrderHeaderValidators : AbstractValidator<TransOrderHeader>
    {
        public TransOrderHeaderValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.openpoheaderid)
                            .NotNull().WithMessage("Transaksi PO tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Transaksi PO tidak boleh kosong !");
            RuleFor(x => x.openpodetaildropshipid)
                            .NotNull().WithMessage("Transaksi PO Dropship tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Transaksi PO Dropship tidak boleh kosong !");
            RuleFor(x => x.userentry)
                            .NotNull().WithMessage("User Entry tidak boleh kosong!")
                            .NotEqual(0).WithMessage("User Entry tidak boleh kosong !");
            RuleFor(x => x.longitude)
                            .NotNull().WithMessage("Longitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Longitude tidak boleh kosong !");
            RuleFor(x => x.latitude)
                            .NotNull().WithMessage("Latitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Latitude tidak boleh kosong !");
            RuleFor(x => x.address)
                            .NotNull().WithMessage("Alamat tidak boleh kosong!")
                            .NotEmpty().WithMessage("Alamat tidak boleh kosong !");

        }

    }

    public class TransOrderDetailValidators : AbstractValidator<TransOrderDetail>
    {
        public TransOrderDetailValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.openpodetailproductid)
                            .NotNull().WithMessage("Transaksi PO Produk tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Transaksi PO Produk tidak boleh kosong !");
            RuleFor(x => x.qty)
                            .NotNull().WithMessage("Jumlah item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah item tidak boleh kosong !");
            RuleFor(x => x.harga)
                            .NotNull().WithMessage("Harga tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga tidak boleh kosong !");
            //RuleFor(x => x.subtotal)
            //                .NotNull().WithMessage("subtotal tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("subtotal tidak boleh kosong !");
        }

    }

    public class TransAbsensiDropshipValidators : AbstractValidator<TransAbsensiDropship>
    {
        public TransAbsensiDropshipValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.openpodetaildropshipid)
                            .NotNull().WithMessage("Open Po Detail Dropship tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Open Po Detail Dropship tidak boleh kosong !");
            RuleFor(x => x.openpoheaderid)
                            .NotNull().WithMessage("Open Po Header tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Open Po Header tidak boleh kosong !");
            RuleFor(x => x.latitude)
                            .NotNull().WithMessage("Latitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Latitude tidak boleh kosong !");
            RuleFor(x => x.longitude)
                            .NotNull().WithMessage("Longitude tidak boleh kosong!")
                            .NotEmpty().WithMessage("Longitude tidak boleh kosong !");
            RuleFor(x => x.address)
                            .NotNull().WithMessage("Alamat tidak boleh kosong!")
                            .NotEmpty().WithMessage("Alamat tidak boleh kosong !");

        }

    }

    public class TransPengirimanValidators : AbstractValidator<TransPengiriman>
    {
        public TransPengirimanValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.orderheaderid)
                            .NotNull().WithMessage("Order tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Order tidak boleh kosong !");
            RuleFor(x => x.shipmentid)
                            .NotNull().WithMessage("Metode Pengiriman tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Metode Pengiriman tidak boleh kosong !");
        }

    }

    public class PunishmentValidators : AbstractValidator<Punishment>
    {
        public PunishmentValidators()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.userid)
                            .NotNull().WithMessage("Pengguna tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Pengguna tidak boleh kosong!");
            RuleFor(x => x.merchantid)
                            .NotNull().WithMessage("Merchant tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Merchant tidak boleh kosong!");
            RuleFor(x => x.orderheaderid)
                            .NotNull().WithMessage("Nomor order tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Nomor order tidak boleh kosong!");

        }

    }
}
