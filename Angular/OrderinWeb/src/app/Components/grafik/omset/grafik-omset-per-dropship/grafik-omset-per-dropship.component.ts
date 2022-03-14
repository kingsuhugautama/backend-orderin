import { UserData } from './../../../../Models/userData';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';
import { GrafikOmsetPerProductDropshipComponent } from '../grafik-omset-per-product-dropship/grafik-omset-per-product-dropship.component';

@Component({
  selector: 'app-grafik-omset-per-dropship',
  templateUrl: './grafik-omset-per-dropship.component.html',
  styleUrls: ['./grafik-omset-per-dropship.component.css']
})
export class GrafikOmsetPerDropshipComponent implements OnInit {

  @ViewChild("chart")
  public chart:ChartComponent;

  @ViewChild("grafikDetailBar")
  public grafikDetailBar:GrafikOmsetPerProductDropshipComponent;
  @ViewChild("grafikDetailPie")
  public grafikDetailPie:GrafikOmsetPerProductDropshipComponent;

  public isGrafikBarHeader:boolean = true;
  public isGrafikPieHeader:boolean = true;

  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();
  public data: Object[];
  public pieData: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

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

  public userData:UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService) {
      this.userData = this.custom.getUserData();
      this.bindDataSource();
    }



    ngOnInit(): void {
      this.primaryXAxis = {
        valueType: 'Category',
        title: 'Dropship'
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

      this.custom.setAsyncDataSource('Grafik/getOmsetPerDropship',param)
      .subscribe((res)=>{
        // console.log(res);
        let data = res.data;
        if(data.length > 0){
          this.data = [];
          this.pieData = [];

          for(var index in data){
            let color = this.helper.getRandomColor();

            this.data.push({
              x:data[index]['label'],
              y:data[index]['total'],
              dropshipId:data[index]['dropshipid'],
              text:data[index]['label'] + ' ( ' + data[index]['total'].toString() + ' )',
              color:color
            });


            this.pieData.push({
              x:data[index]['label'],
              y:data[index]['qty'],
              dropshipId:data[index]['dropshipid'],
              text:data[index]['label'] + ' ( ' + data[index]['qty'].toString() + ' )',
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

    showDetail(e,chartType){
      let data = e.series.dataSource[e.pointIndex];

      // console.log(data);

      if(chartType == 'bar'){

        this.dropshipSelectedBarChart['dropshipId'] = data['dropshipId'];
        this.dropshipSelectedBarChart['label'] = data['x'];

        setTimeout(() => {
          this.grafikDetailBar.bindDataSource();
        },200);

        $('#filterBar').hide();
        this.isGrafikBarHeader = false;

      }else{
        this.dropshipSelectedPieChart['dropshipId'] = data['dropshipId'];
        this.dropshipSelectedPieChart['label'] = data['x'];
        setTimeout(() => {
          this.grafikDetailPie.bindDataSource();
        }, 200);

        $('#filterPie').hide();
        this.isGrafikPieHeader = false;
      }
    }

  }
