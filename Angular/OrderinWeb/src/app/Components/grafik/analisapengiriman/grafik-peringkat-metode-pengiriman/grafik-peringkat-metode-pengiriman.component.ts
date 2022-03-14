import { UserData } from './../../../../Models/userData';
import { ParameterSearchModel } from './../../../../Models/parameterSearchModel';
import { HelperService } from 'src/app/Services/HelperService/helper.service';
import { Component, OnInit } from '@angular/core';
import { CustomService } from 'src/app/Services/CustomService/custom.service';

@Component({
  selector: 'app-grafik-peringkat-metode-pengiriman',
  templateUrl: './grafik-peringkat-metode-pengiriman.component.html',
  styleUrls: ['./grafik-peringkat-metode-pengiriman.component.css']
})
export class GrafikPeringkatMetodePengirimanComponent implements OnInit {


  public piedata: Object[];
  public legendSettings: Object;
  public startAngle: number;
  public endAngle: number;
  public center: Object ;
  public explode: boolean ;
  public datalabel: Object;
  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };

  public tglStart:string = this.helper.getFirstDateOfMonth(this.helper.getCurrentDateISO());
  public tglEnd:string = this.helper.getCurrentDateISO();
  public userData:UserData = null;

  constructor(private custom:CustomService,
    private helper:HelperService) {
      this.userData = this.custom.getUserData();
      this.bindDataSource();
    }

    ngOnInit(): void {
      this.startAngle = 0;
      this.endAngle = 360;
      this.explode = true;

      this.piedata = []
      // { x: 'Jan', y: 13, text: 'Jan: 3' }, { x: 'Feb', y: 3.5, text: 'Feb: 3.5' }

      this.legendSettings = {
        visible: true
      };
      this.datalabel = { visible: true, name: 'text', position: 'Outside',font: {
        fontWeight: '600'
      } };

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
      param.columnName = 'waktukirim';
      param.filter = 'between';
      param.searchText = this.helper.reformatingDateValue(this.tglStart) + ' 00:00:00';
      param.searchText2 = this.helper.reformatingDateValue(this.tglEnd) + ' 23:59:59';

      this.bindDataSource([param]);
    }

    bindDataSource(param = []){

      if(param.length == 0){

        let paramDate = new ParameterSearchModel();
        paramDate.columnName = 'waktukirim';
        paramDate.filter = 'between';
        paramDate.searchText = this.tglStart;
        paramDate.searchText2 = this.tglEnd;


        let paramMerchant = new ParameterSearchModel();
        paramMerchant.columnName = 'mm.merchantid';
        paramMerchant.filter = 'equal';
        paramMerchant.searchText = this.userData.merchantid.toString();

        param = [paramDate,paramMerchant];
      }

      this.custom.setAsyncDataSource('Grafik/getAnalisaCustomerPeringkatMetodePengiriman',param)
      .subscribe((res)=>{
        // console.log(res);
        let data = res.data;
        if(data.length > 0){


          this.piedata = []
          for(var index in data){
            this.piedata.push({
              x:data[index]['keterangan'],
              y:data[index]['jumlah'],
              text:data[index]['keterangan'] + ' ( ' + data[index]['jumlah'].toString() + ' )',
              // color:parseInt(index) == 0 ? 'pink':'blue'
            });
          }
        }
      })
    }
  }
