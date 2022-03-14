import { NgxSpinnerService } from 'ngx-spinner';
import { GrafikPeringkatProdukPerDropshipComponent } from './../grafik-peringkat-produk-per-dropship/grafik-peringkat-produk-per-dropship.component';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';
import { UserData } from 'src/app/Models/userData';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';
import { GrafikOmsetPerProductDropshipComponent } from '../../grafik-omset-per-product-dropship/grafik-omset-per-product-dropship.component';

@Component({
  selector: 'app-grafik-omset-per-dropship-per-tgl-po',
  templateUrl: './grafik-omset-per-dropship-per-tgl-po.component.html',
  styleUrls: ['./grafik-omset-per-dropship-per-tgl-po.component.css']
})
export class GrafikOmsetPerDropshipPerTglPoComponent implements OnInit {

  @ViewChild("chart")
  public chart:ChartComponent;

  @ViewChild("grafikProdukFavorit")
  public grafikProdukFavorit:GrafikPeringkatProdukPerDropshipComponent;


  @Input("tglPo")
  public tglPo = null;

  public tglStart:string;
  public tglEnd:string;

  public isGrafikProduk:boolean = false;
  public data: Object[];
  public pieData: Object[];

  public title: string = 'Omset Penjualan Per Dropship';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }

  public headerText: Object = [{ 'text': 'Total Omset','iconCss':'fas fa-chart-bar' }, { 'text': 'Jumlah Omset','iconCss':'fas fa-chart-pie' }];

  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public animation : Object = {
    enable:true,
    duration:1000
  }

  public dropshipSelectedBarChart = {
    dropshipId : 0,
    label : '',
    tglStart : '',
    tglEnd :''
  };

  public dropshipSelectedPieChart = {
    dropshipId : 0,
    label : ''
  };

  public dropshipId = 0;
  public dropshipName = '';
  public userData:UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService,
    private spinner:NgxSpinnerService) {

      this.userData = this.custom.getUserData();
      this.bindDataSource();
    }



    ngOnInit(): void {

    }

    ngAfterViewInit(): void {
    }

    setTgl(e,period="start"){
      let value = e.value;
      if(period == "start")
      this.tglStart = this.helper.reformatingDateValue(value) + ' 00:00:00';
      else
      this.tglEnd = this.helper.reformatingDateValue(value) + ' 23:59:59';
    }

    bindDataSource(param = []){

      if(this.tglPo){
        this.spinner.show('ajaxSpinner');
        this.tglStart = this.tglPo + ' 00:00:00';
        this.tglEnd = this.tglPo + ' 23:59:59';

        if(param.length == 0){

          let paramDate = new ParameterSearchModel();
          paramDate.columnName = 'toph.openpodate';
          paramDate.filter = 'between';
          paramDate.searchText = this.tglStart;
          paramDate.searchText2 = this.tglEnd;


          let paramMerchant = new ParameterSearchModel();
          paramMerchant.columnName = 'mm.merchantid';
          paramMerchant.filter = 'equal';
          paramMerchant.searchText = this.userData.merchantid.toString();

          param = [paramDate,paramMerchant];
        }

        this.custom.setAsyncDataSource('Grafik/getOmsetPerDropship',param)
        .subscribe((res)=>{
          this.spinner.hide('ajaxSpinner');
          // console.log(res);
          let data = res.data;
          if(data.length > 0){
            this.data = [];
            this.pieData = [];

            for(var index in data){
              let color = this.helper.getRandomColor();

              this.pieData.push({
                x: data[index]['label'],
                y:this.helper.formatRupiah(data[index]['total']),
                dropshipId:data[index]['dropshipid'],
                text: data[index]['total'],
                color:color
              });

            }
            // console.log(this.chart);
          }
        });
      }
    }


    showDetail(e){
      let data = e.series.dataSource[e.pointIndex];
      console.log(data);
      this.dropshipId = data['dropshipId'];
      this.dropshipName = data['x'];

      setTimeout(() => {
        this.grafikProdukFavorit.bindDataSource();
      },200);

      this.isGrafikProduk = true;

    }


  }
