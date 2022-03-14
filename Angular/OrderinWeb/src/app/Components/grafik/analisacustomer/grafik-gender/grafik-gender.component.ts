import { UserData } from './../../../../Models/userData';
import { HelperService } from './../../../../Services/HelperService/helper.service';
import { CustomService } from './../../../../Services/CustomService/custom.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';

@Component({
  selector: 'app-grafik-gender',
  templateUrl: './grafik-gender.component.html',
  styleUrls: ['./grafik-gender.component.css']
})
export class GrafikGenderComponent implements OnInit {
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
  public userData : UserData = null;

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
        visible: false
      };
      this.datalabel = { visible: true, name: 'text', position: 'Inside',font: {
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

        param = [paramDate,paramMerchant];
      }

      this.custom.setAsyncDataSource('Grafik/getAnalisaCustomerPerGender',param)
      .subscribe((res)=>{
        // console.log(res);
        let data = res.data;
        if(data.length > 0){


          this.piedata = []
          for(var index in data){
            this.piedata.push({
              x:data[index]['gender'],
              y:data[index]['jumlah'],
              text:data[index]['gender'] + ' ( ' + data[index]['jumlah'].toString() + ' )',
              color:data[index]['gender'] == 'Pria' ? '#008080':'#FF1493'
            });
          }
        }
      })
    }

  }
