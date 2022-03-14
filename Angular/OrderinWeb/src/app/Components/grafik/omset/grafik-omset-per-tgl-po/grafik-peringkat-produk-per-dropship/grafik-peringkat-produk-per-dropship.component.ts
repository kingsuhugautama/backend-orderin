import { NgxSpinnerService } from 'ngx-spinner';
import { Component, Input, OnInit } from '@angular/core';
import { ParameterSearchModel } from 'src/app/Models/parameterSearchModel';
import { UserData } from 'src/app/Models/userData';
import { CustomService } from 'src/app/Services/CustomService/custom.service';

@Component({
  selector: 'app-grafik-peringkat-produk-per-dropship',
  templateUrl: './grafik-peringkat-produk-per-dropship.component.html',
  styleUrls: ['./grafik-peringkat-produk-per-dropship.component.css']
})
export class GrafikPeringkatProdukPerDropshipComponent implements OnInit {

  @Input("dropshipId")
  public dropshipId:number = 0;
  @Input("dropshipName")
  public dropshipName:string = '';

  public title = '';
  public piedata: Object[];
  public legendSettings: Object;
  public startAngle: number;
  public endAngle: number;
  public center: Object ;
  public explode: boolean ;
  public datalabel: Object;
  public tooltip: Object = { enable: true, format: '${point.x} : <b>${point.y}</b>' };
  public userData:UserData = null;

  constructor(private custom:CustomService,
    private spinner:NgxSpinnerService) {
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
    this.spinner.show('ajaxSpinner');
    this.title = 'Peringkat Produk Favorit Dropship ( ' + this.dropshipName + ' )'
    let param = [];

    let paramMerchant = new ParameterSearchModel();
    paramMerchant.columnName = 'mm.merchantid';
    paramMerchant.filter = 'equal';
    paramMerchant.searchText = this.userData.merchantid.toString();

    if(this.dropshipId != 0){
      let paramDropship = new ParameterSearchModel();
      paramDropship.columnName = 'topdd.dropshipid';
      paramDropship.filter = 'equal';
      paramDropship.searchText = this.dropshipId.toString();

      param = [paramMerchant,paramDropship];
    }else{
      param = [paramMerchant];
    }

    this.custom.setAsyncDataSource('Grafik/getAllDataPeringkatProduk',param)
    .subscribe((res)=>{
      this.spinner.hide('ajaxSpinner');
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
    });

  }

}
