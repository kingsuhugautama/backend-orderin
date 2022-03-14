import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GrafikCustomerRepeatOrderComponent } from 'src/app/Components/grafik/analisacustomer/grafik-customer-repeat-order/grafik-customer-repeat-order.component';
import { GrafikGenderComponent } from 'src/app/Components/grafik/analisacustomer/grafik-gender/grafik-gender.component';
import { GrafikUsiaComponent } from 'src/app/Components/grafik/analisacustomer/grafik-usia/grafik-usia.component';
import { GrafikPeringkatMetodePengirimanComponent } from 'src/app/Components/grafik/analisapengiriman/grafik-peringkat-metode-pengiriman/grafik-peringkat-metode-pengiriman.component';
import { GrafikOmsetPerDropshipComponent } from 'src/app/Components/grafik/omset/grafik-omset-per-dropship/grafik-omset-per-dropship.component';
import { GrafikOmsetPerKategoriComponent } from 'src/app/Components/grafik/omset/grafik-omset-per-kategori/grafik-omset-per-kategori.component';
import { GrafikOmsetPerProductDropshipComponent } from 'src/app/Components/grafik/omset/grafik-omset-per-product-dropship/grafik-omset-per-product-dropship.component';
import { GrafikOmsetPerProductComponent } from 'src/app/Components/grafik/omset/grafik-omset-per-product/grafik-omset-per-product.component';
import { GrafikOmsetPerTglPoComponent } from 'src/app/Components/grafik/omset/grafik-omset-per-tgl-po/grafik-omset-per-tgl-po.component';
import { GrafikOngkirComponent } from 'src/app/Components/grafik/omset/grafik-ongkir/grafik-ongkir.component';
import { GrafikPeringkatProdukComponent } from 'src/app/Components/grafik/produk/grafik-peringkat-produk/grafik-peringkat-produk.component';

const routes: Routes = [
  {
    path:'omset-dropship',component:GrafikOmsetPerDropshipComponent
  },
  {
    path:'omset-produk-dropship',component:GrafikOmsetPerProductDropshipComponent
  },
  {
    path:'omset-produk',component:GrafikOmsetPerProductComponent
  },
  {
    path:'omset-kategori',component:GrafikOmsetPerKategoriComponent
  },
  {
    path:'omset-pertglpo',component:GrafikOmsetPerTglPoComponent
  },
  {
    path:'produk-peringkat',component:GrafikPeringkatProdukComponent
  },
  {
    path:'ongkir-dropship',component:GrafikOngkirComponent
  },
  {
    path:'analisa-gender',component:GrafikGenderComponent
  },
  {
    path:'analisa-usia',component:GrafikUsiaComponent
  },
  {
    path:'analisa-repeat-order',component:GrafikCustomerRepeatOrderComponent
  },
  {
    path:'analisa-pengiriman',component:GrafikPeringkatMetodePengirimanComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GrafikRoutingModule { }
