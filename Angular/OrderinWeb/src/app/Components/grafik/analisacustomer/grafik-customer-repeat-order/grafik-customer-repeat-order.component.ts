import { UserData } from './../../../../Models/userData';
import { AppService } from './../../../../Services/AppService/app.service';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';

@Component({
  selector: 'app-grafik-customer-repeat-order',
  templateUrl: './grafik-customer-repeat-order.component.html',
  styleUrls: ['./grafik-customer-repeat-order.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class GrafikCustomerRepeatOrderComponent implements OnInit {


  @ViewChild("chart")
  public chart:ChartComponent;

  public orderBaru: Object[];
  public orderLama: Object[];

  public pieData: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

  public title: string = 'Analisa Customer Pembelian Produk';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }

  public margin = { left: 10, right: 10, top: 10, bottom: 10 };

  public headerText: Object = [{ 'text': 'Total Omset','iconCss':'fas fa-chart-bar' }, { 'text': 'Jumlah Omset','iconCss':'fas fa-chart-pie' }];

  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();
  private userData:UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService,
    private app:AppService) {
      this.userData = this.custom.getUserData();
      this.bindDataSource();
      this.app.setTitle('tes')
    }

    ngOnInit(): void {
      this.primaryXAxis = {
        valueType: 'Category',
        title: 'Produk'
      };

    }

    ngAfterViewInit(): void {    }

    setTgl(e,period="start"){
      let value = e.value;
      if(period == "start")
      this.tglStart = this.helper.reformatingDateValue(value) + ' 00:00:00';
      else
      this.tglEnd = this.helper.reformatingDateValue(value) + ' 23:59:59';
    }

    filter(){
      let param = new ParameterSearchModel();
      param.columnName = 't.waktuentry';
      param.filter = 'between';
      param.searchText = this.helper.reformatingDateValue(this.tglStart) + ' 00:00:00';
      param.searchText2 = this.helper.reformatingDateValue(this.tglEnd) + ' 23:59:59';

      this.bindDataSource([param]);
    }

    bindDataSource(param = []){

      if(param.length == 0){
        let paramDate = new ParameterSearchModel();
        paramDate.columnName = 't.waktuentry';
        paramDate.filter = 'between';
        paramDate.searchText = this.tglStart;
        paramDate.searchText2 = this.tglEnd;


        let paramMerchant = new ParameterSearchModel();
        paramMerchant.columnName = 'mm.merchantid';
        paramMerchant.filter = 'equal';
        paramMerchant.searchText = this.userData.merchantid.toString();

        param = [paramDate,paramMerchant];
      }

      this.custom.setAsyncDataSource('Grafik/getAnalisaCustomerRepeatOrder',param)
      .subscribe((res)=>{
        let data = res.data;
        if(data.length > 0){
          this.orderBaru = [];
          this.orderLama = [];

          for(var index in data){

            this.orderBaru.push({
              x:data[index]['productname'],
              y:data[index]['userbaru'],
              text:data[index]['productname'] + ' ( ' + data[index]['userbaru'].toString() + ' )',
              color:'#00CED1'
            });


            this.orderLama.push({
              x:data[index]['productname'],
              y:data[index]['userlama'],
              text:data[index]['productname'] + ' ( ' + data[index]['userlama'].toString() + ' )',
              color:'#FF1493'
            });
          }


          this.primaryYAxis = {
            minimum: 0,
            interval: 2, title: 'Jumlah Customer',
          };
          // console.log(this.chart);
        }
      })
    }

  }
