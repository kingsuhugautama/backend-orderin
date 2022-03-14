import { NgxSpinnerService } from 'ngx-spinner';
import { UserData } from './../../../../Models/userData';
import { ParameterSearchModel } from './../../../../Models/parameterSearchModel';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';

@Component({
  selector: 'app-grafik-omset-per-product-dropship',
  templateUrl: './grafik-omset-per-product-dropship.component.html',
  styleUrls: ['./grafik-omset-per-product-dropship.component.css']
})
export class GrafikOmsetPerProductDropshipComponent implements OnInit {

  @Input()
  public chartType : string = '';
  @Input()
  public dropshipId : number = 0;
  @Input()
  public dropshipLabel : string = '';
  @Input()
  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  @Input()
  public tglEnd : string = '';

  @ViewChild("chart")
  public chart:ChartComponent;

  public data: Object[];
  public pieData: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

  public title: string = '';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }

  public headerText: Object = [{ 'text': 'Total Omset','iconCss':'fas fa-chart-bar' }, { 'text': 'Jumlah Omset','iconCss':'fas fa-chart-pie' }];

  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public userData : UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService,
    private spinner:NgxSpinnerService) {
      this.userData = this.custom.getUserData();
    }

    ngOnInit(): void {
      this.primaryXAxis = {
        valueType: 'Category',
        title: 'Produk'
      };

      // this.bindDataSource();
    }

    ngAfterViewInit(): void {
    }

    bindDataSource(){
      if(this.dropshipId > 0){
        this.spinner.show('ajaxSpinner');
        this.title = 'Omset Penjualan Product ( ' + this.dropshipLabel + ' ) ';

        let paramDropship : ParameterSearchModel = new ParameterSearchModel();
        paramDropship.columnName = 'md.dropshipid';
        paramDropship.filter = 'equal'
        paramDropship.searchText = this.dropshipId.toString();

        let paramMerchant : ParameterSearchModel = new ParameterSearchModel();
        paramMerchant.columnName = 'mm.merchantid';
        paramMerchant.filter = 'equal'
        paramMerchant.searchText = this.userData.merchantid.toString();


        let paramTgl : ParameterSearchModel = new ParameterSearchModel();
        paramTgl.columnName = 't.waktuentry';
        paramTgl.filter = 'between'
        paramTgl.searchText = this.tglStart;
        paramTgl.searchText2 = this.tglEnd;

        this.custom.setAsyncDataSource('Grafik/getOmsetPerDropshipProduct',[
          paramDropship,
          paramMerchant,
          paramTgl
        ])
        .subscribe((res)=>{
          this.spinner.hide('ajaxSpinner');
          console.log(res);
          let data = res.data;
          if(data.length > 0){
            this.data = [];
            this.pieData = [];

            for(var index in data){
              let color = this.helper.getRandomColor();

              this.data.push({
                x:data[index]['productname'],
                y:data[index]['total'],
                text:data[index]['productname'] + ' ( ' + data[index]['total'].toString() + ' )',
                color:color
              });


              this.pieData.push({
                x:data[index]['productname'],
                y:data[index]['qty'],
                text:data[index]['productname'] + ' ( ' + data[index]['qty'].toString() + ' )',
                color:color
              });

            }


            this.primaryYAxis = {
              minimum: 0, maximum: Math.max.apply(Math,data.map((o)=>{
                return o['total'];
              })) * 1.2,
              interval: 200000, title: 'Total',
            };
            // console.log(this.chart);
          }
        });
      }
    }

  }
