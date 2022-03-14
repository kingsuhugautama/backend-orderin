import { UserData } from './../../../../Models/userData';
import { ParameterSearchModel } from './../../../../Models/parameterSearchModel';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { HelperService } from './../../../../Services/HelperService/helper.service';
import { CustomService } from './../../../../Services/CustomService/custom.service';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-grafik-omset-per-product',
  templateUrl: './grafik-omset-per-product.component.html',
  styleUrls: ['./grafik-omset-per-product.component.css']
})
export class GrafikOmsetPerProductComponent implements OnInit {

  @ViewChild("chart")
  public chart:ChartComponent;
  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();

  public data: Object[];
  public pieData: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

  public title: string = 'Omset Penjualan Per Product';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }

  public headerText: Object = [{ 'text': 'Total Omset','iconCss':'fas fa-chart-bar' }, { 'text': 'Jumlah Omset','iconCss':'fas fa-chart-pie' }];

  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public userData:UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService) {
      this.userData = this.custom.getUserData();
      this.bindDataSource();
    }

    ngOnInit(): void {
      this.primaryXAxis = {
        valueType: 'Category',
        title: 'Produk'
      };

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

      this.custom.setAsyncDataSource('Grafik/getOmsetPerProduct',param)
      .subscribe((res)=>{
        // console.log(res);
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
      })
    }
  }
