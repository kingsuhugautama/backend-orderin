import { UserData } from './../../../../Models/userData';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';

@Component({
  selector: 'app-grafik-ongkir',
  templateUrl: './grafik-ongkir.component.html',
  styleUrls: ['./grafik-ongkir.component.css']
})
export class GrafikOngkirComponent implements OnInit {

  @ViewChild("chart")
  public chart:ChartComponent;


  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();
  public data: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

  public title: string = 'Ongkos Kirim Per Dropship';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }
  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public userData : UserData = null;

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

    reset(){
      this.data = [];

      setTimeout(() => {
        this.bindDataSource();
        this.chart.refresh();
      }, 200);
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

      this.custom.setAsyncDataSource('Grafik/getOngkirPerDropship',param)
      .subscribe((res)=>{
        let apiData = res.data;
        if(apiData.length > 0){
          this.data = [];

          for(var index in apiData){
            let color = this.helper.getRandomColor();

            this.data.push({
              x:apiData[index]['label'],
              y:apiData[index]['totalongkoskirim'],
              text:apiData[index]['label'] + ' ( ' + apiData[index]['totalongkoskirim'].toString() + ' )',
              color:color
            });
          }


          this.primaryYAxis = {
            minimum: 0, maximum: Math.max.apply(Math,apiData.map((o)=>{
              return o['totalongkoskirim'];
            })) * 1.2,
            interval: 5000, title: 'Total',
          };
          // console.log(this.chart);
        }
      })
    }

}
