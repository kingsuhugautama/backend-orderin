import { GrafikOmsetPerDropshipPerTglPoComponent } from './../../Components/grafik/omset/grafik-omset-per-tgl-po/grafik-omset-per-dropship-per-tgl-po/grafik-omset-per-dropship-per-tgl-po.component';
import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GrafikRoutingModule } from './grafik-routing.module';
import { AccumulationAnnotationService, AccumulationDataLabelService, AccumulationLegendService, AccumulationTooltipService, CategoryService, ChartAnnotationService, ColumnSeriesService, DateTimeService, LegendService, LineSeriesService, PieSeriesService, PyramidSeriesService, RangeColumnSeriesService, ScrollBarService, StackingColumnSeriesService, TooltipService } from '@syncfusion/ej2-angular-charts';

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
import { GrafikPeringkatProdukPerDropshipComponent } from '../../Components/grafik/omset/grafik-omset-per-tgl-po/grafik-peringkat-produk-per-dropship/grafik-peringkat-produk-per-dropship.component';


@NgModule({
  declarations: [
    GrafikGenderComponent,
    GrafikUsiaComponent,
    GrafikCustomerRepeatOrderComponent,
    GrafikPeringkatMetodePengirimanComponent,
    GrafikOmsetPerDropshipComponent,
    GrafikOmsetPerProductDropshipComponent,
    GrafikOmsetPerProductComponent,
    GrafikOmsetPerKategoriComponent,
    GrafikOngkirComponent,
    GrafikPeringkatProdukComponent,
    GrafikOmsetPerTglPoComponent,
    GrafikOmsetPerDropshipPerTglPoComponent,
    GrafikPeringkatProdukPerDropshipComponent
  ],
  imports: [
    CommonModule,
    GrafikRoutingModule,
    SharedModule
  ],
  providers: [
    CategoryService,DateTimeService, ScrollBarService,
    LineSeriesService,PieSeriesService,ColumnSeriesService,
    AccumulationLegendService, AccumulationTooltipService,
    AccumulationDataLabelService,AccumulationAnnotationService,
    ChartAnnotationService, RangeColumnSeriesService, StackingColumnSeriesService,
    LegendService, TooltipService
  ]
})
export class GrafikModule { }
