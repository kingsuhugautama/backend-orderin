import { GrafikOmsetPerDropshipPerTglPoComponent } from './grafik-omset-per-dropship-per-tgl-po/grafik-omset-per-dropship-per-tgl-po.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartComponent } from '@syncfusion/ej2-angular-charts';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';
import { UserData } from 'src/app/Models/userData';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HelperService } from 'src/app/Services/HelperService/helper.service';

@Component({
  selector: 'app-grafik-omset-per-tgl-po',
  templateUrl: './grafik-omset-per-tgl-po.component.html',
  styleUrls: ['./grafik-omset-per-tgl-po.component.css']
})
export class GrafikOmsetPerTglPoComponent implements OnInit {

  @ViewChild("chart")
  public chart:ChartComponent;
  @ViewChild("grafikDetail")
  public grafikDetail:GrafikOmsetPerDropshipPerTglPoComponent;

  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();

  public data: Object[];
  public primaryXAxis: Object;
  public primaryYAxis: Object;

  public title: string = 'Omset Penjualan Per Tanggal Pre Order';

  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public marker: Object = {
    visible: true
  }

  public datalabel = { visible: true, name: 'text', position: 'Outside',font: {
    fontWeight: '600'
  }};

  public userData:UserData = null;
  public tglPo = null;
  public isGrafikHeader:boolean = true;

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
      param.columnName = 'toph.openpodate';
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

      this.custom.setAsyncDataSource('Grafik/getOmsetPerTglPo',param)
      .subscribe((res)=>{
        // console.log(res);
        let data = res.data;
        if(data.length > 0){
          this.data = [];
          for(var index in data){
            let color = this.helper.getRandomColor();

            this.data.push({
              x:this.helper.ddMMMMyyyy(data[index]['tglpo']),
              y:data[index]['total'],
              text:data[index]['tglpo'] + ' ( ' + data[index]['total'].toString() + ' )',
              color:color,
              tglPo:data[index]['tglpo']
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


    showDetail(e){
      let data = e.series.dataSource[e.pointIndex];
      this.tglPo = data['tglPo'];
      //console.log(data);
      setTimeout(() => {
        this.grafikDetail.bindDataSource();
      },200);

      this.isGrafikHeader = false;

    }

  }
