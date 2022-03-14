import { UserData } from './../../../../Models/userData';
import { Component, OnInit } from '@angular/core';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';

@Component({
  selector: 'app-grafik-peringkat-produk',
  templateUrl: './grafik-peringkat-produk.component.html',
  styleUrls: ['./grafik-peringkat-produk.component.css']
})
export class GrafikPeringkatProdukComponent implements OnInit {

  public piedata: Object[];
  public legendSettings: Object;
  public startAngle: number;
  public endAngle: number;
  public center: Object ;
  public explode: boolean ;
  public datalabel: Object;
  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };
  public userData:UserData = null;

  constructor(private custom:CustomService) {
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

  bindDataSource(){

    let paramMerchant = new ParameterSearchModel();
    paramMerchant.columnName = 'mm.merchantid';
    paramMerchant.filter = 'equal';
    paramMerchant.searchText = this.userData.merchantid.toString();

    let param = [paramMerchant];

    this.custom.setAsyncDataSource('Grafik/getAllDataPeringkatProduk',param)
    .subscribe((res)=>{
      // console.log(res);
      let data = res.data;
      if(data.length > 0){


        this.piedata = []
        for(var index in data){
          if(data[index]['qty'] != 0)
          this.piedata.push({
            x:data[index]['productname'],
            y:data[index]['qty'],
            text:data[index]['productname'] + ' ( ' + data[index]['qty'].toString() + ' )',
            // color:parseInt(index) == 0 ? 'pink':'blue'
          });
        }
      }
    })
  }

}
