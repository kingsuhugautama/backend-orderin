import { CookieService } from 'ngx-cookie-service';
import { UserData } from './../../Models/userData';

import { Tooltip } from '@syncfusion/ej2-popups';
declare var setNumericValueNoInstance,Swal,convertBase64ToFile:any;

import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { Config } from 'protractor';
import { base_url_api } from '../../Utility/Url/url-constant';
import { StorageService } from '../StorageService/storage.service';
import { NgxSpinnerService } from "ngx-spinner";
import { map, catchError } from 'rxjs/operators';
import { FormGroup } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
import { isBoolean } from 'util';
import { HelperService } from '../HelperService/helper.service';
import { environment } from 'src/environments/environment.prod';

var dataCallback:any;
var setDataCallback = (data:any)=>{
  dataCallback = data;
}

interface JQuery{
  popUpGrid():any;
}

@Injectable({
  providedIn: "root"
})

export class CustomService  {
  private customFunction: (data:any) => void;
  private header:HttpHeaders;
  public sessionIdle = 0;


  constructor(private http:HttpClient,
    private router:Router,
    private cookie:CookieService,
    private storage:StorageService,
    private spinner: NgxSpinnerService,
    private helper:HelperService) {
      this.header = new HttpHeaders();
      this.header = this.header.set('Content-Type', 'application/json;');

      // this.header=this.header.set('AuthorizationToken', this.storage.getItem('authorizationToken'));
    }

    //untuk mengecek apakah user idle selama waktu yg ditentukan
    filterSession(){

      setInterval(()=>{
        //setial 30 detik interval akan menambahkan session idle time 30 s
        this.sessionIdle += 300; //perbandingan 1/1000 dengan interval
        // console.log(this.sessionIdle)

        let timeout = this.sessionIdle

        // let jam = timeout/3600;
        timeout = timeout/60;

        console.log('Idle time : ' + timeout.toString() + ' of ' + environment.sessionTimeout.toString());

        if(timeout === environment.sessionTimeout){
          Swal.fire({
            icon:'error',
            title:'Perhatian',
            html:"Waktu habis, silahkan login kembali!"
          }).then(()=>{
            window.location.href = "/";
          });
        }
      },300000);
    }


    refreshJwtToken(err){

      // console.log(err);
      if (err.status == 401) {

        let token = err.error.result.refreshToken;
        // console.log('new Token :' + token);

        if(token){
          this.cookie.delete(this.storage.encryptSHA1('uat'));

          setTimeout(() => {
            this.cookie.set(this.storage.encryptSHA1('uat'),token);
          }, 100);

          // location.reload();
        }else{
          Swal.fire({
            icon:'error',
            title:'Perhatian',
            html:"Token Expired!"
          }).then(()=>{
            window.location.href = "/";
          });

        }
      } else {
        // alert(err.error.result);
        this.helper.swalMsg(err.error.result.data,'Perhatian','error');
      }
    }

    getUserData():UserData{
      let data = this.storage.getItem('UserData');
      let userData : UserData= JSON.parse(data);

      return userData;
    }

    // region processing callback
    getDataCallback(){
      return dataCallback;
    }
    // endregion processing callback


    connectFunction(fn: (data:any) => void) {
      // console.log(fn);
      this.customFunction = fn;
      // from now on, call myFunc wherever you want inside this service
    }

    callLookupServer(event, url, title, headers, filters = null, callbackPassId = [],formGroup : FormGroup = null, defaultFilter = []) {
      //headers = untuk header title datatable
      //filters = untuk pencarian datatable by checkbox berisi title (ditampilkan di label checkbox)
      //dan field(nama field yg di passing ke value checkbox untuk pencarian)
      //callbackid = id html yang akan diberi nilai dari hasil callback (primary key)
      //callbackvalueid = id html yang akan diberi nilai dari hasil callback (display mask)
      //console.log(url);

      let buttonId: string = event.target.id == "" ? event.target.parentElement.parentElement.id : event.target.id;
      // console.log(event);

      (<any>$('#' + buttonId)).popUpGrid({
        'httpHeaders':this.header,
        'httpClient':this.http,
        'title': title,
        'url': base_url_api + url,
        'data': {},
        'filter': filters,
        'defaultFilter': defaultFilter,
        'columns': headers,
        callback: function (res) {
          //console.log(res);
          setDataCallback(res);
          if (callbackPassId.length > 0) {
            for (let i = 0; i < callbackPassId.length; i++) {


              if (res[callbackPassId[i].field]) {
                try {
                  $('#' + callbackPassId[i].htmlid).val(res[callbackPassId[i].field]);
                  $('#' + callbackPassId[i].htmlid).html(res[callbackPassId[i].field]);
                  $('#' + callbackPassId[i].htmlid).text(res[callbackPassId[i].field]);
                  setNumericValueNoInstance(callbackPassId[i].htmlid, res[callbackPassId[i].field]);

                  // console.log(formGroup)
                  if(formGroup){
                    formGroup.get(callbackPassId[i].htmlid).setValue(res[callbackPassId[i].field]);
                  }

                  //trigger change setelah set callback
                  const customEvent = document.createEvent('Event');
                  customEvent.initEvent('change', true, true);
                  $('#' + callbackPassId[i].htmlid)[0].dispatchEvent(customEvent);
                } catch (ex) {

                }
              }
            }
          } else {
            //alert('Response tidak pass kemanapun');
          }

          // result = res;

        },
        tutup: function (res) {
          // console.log(res)
          // result = res;
        }
      });


      $('#' + buttonId).trigger('click');
    }

    bindGrid(url, params:any = [], grid,showLoading = false){
      // console.log(grid);
      if(showLoading){
        // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
        this.spinner.show('ajaxSpinner');
      }

      this.http.post(base_url_api + url,JSON.stringify(params),{
        headers:this.header
      }).subscribe((data:Config)=>{

        // console.log(data);
        // Swal.close();
        this.spinner.hide('ajaxSpinner');

        if(showLoading){
          if(data.message){
            setTimeout(()=>{
              Swal.fire({
                icon: 'success',
                html: data.message
              });
            },300);
          }
        }

        grid.dataSource = data.result;

        // console.log(grid.dataSource);
      },
      error =>{
        console.log(error);
        this.spinner.hide('ajaxSpinner');
      })
    }


    swalConfirmationWithParams(params,confirmText = "menyimpan"){
      Swal.fire({
        title: 'Perhatian',
        icon: 'question',
        text: 'Anda yakin ingin ' + confirmText + "?",
        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        focusCancel: true,
        confirmButtonText:
        'Ya',
        showLoaderOnConfirm: true,
        preConfirm: () => {
          setDataCallback(params);
          this.customFunction(params);
        },
        //confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText:
        'Tidak'
        //cancelButtonAriaLabel: 'Thumbs down'
      })
    }

    async restrictPageAsync(api_url,data,url=''){
      // console.log(data);
      return await this.http.post(base_url_api + api_url,JSON.stringify(data),{
        headers:this.header
      }).toPromise();
    }


    //pembatasan menu url
    restrictPage(api_url,data,url=''){
      // console.log(data);
      this.http.post(base_url_api + api_url,JSON.stringify(data),{
        headers:this.header
      }).subscribe((res:Config)=>{

        // console.log(res);
        let dataMenu = res.result.data;

        if (dataMenu.length > 0) {
          let currentUrl = url == '' ? document.location.href : url;
          //console.log(currentUrl);
          let fixUrl = currentUrl.toLowerCase().replace(location.origin,'').replace(/[/"]+/g, '').replace(/[#"]+/g, '');
          // console.log(fixUrl,location.origin);
          let filter = dataMenu.filter((df) => {

            if (currentUrl.indexOf("matchmaker") > -1) {
              fixUrl = currentUrl.substring(0, currentUrl.indexOf("matchmaker") + 10);
            }

            // console.log(fixUrl.toLowerCase(),location.origin);
            return df["Url"].replace(/[/"]+/g, '').replace(/[#"]+/g, '').toLowerCase() == fixUrl;
          });

          let urlException = ["dashboard","unknown"]
          //console.log(fixUrl, urlException.includes(fixUrl),filter.length)

          if(filter.length == 0 ){
            if(urlException.includes(fixUrl) == false)
            this.router.navigate(['unknown']);
          }else{
            this.storage.removeItem('lastMenuActive');
            this.storage.setItem('lastMenuActive',filter[0].MenuSideBarId);
            let dataJson = {
              "RoleId": data,
              "MenuSideBarId": parseInt(filter[0].MenuSideBarId)
            };

            this.http.post(base_url_api + 'menu/button',dataJson,{
              headers:this.header
            }).subscribe((btnConf:Config)=>{

              this.storage.removeItem("buttons");
              this.storage.setItem("buttons", JSON.stringify(btnConf));
              this.renderButton(JSON.stringify(btnConf));
            },
            error=>{
              console.log(error);
            })
          }
        } else {
          this.router.navigate(['unknown']);
        }
      },
      (err)=>{
        //this.refreshJwtToken(err);
      })
    }


    //pembatasan tombol
    renderButton(data) {
      // console.log(data);
      var json = JSON.parse(data);
      // console.log(json.result.length);
      $('#contentRendered').find('.btn').each(function () {
        // console.log($(this).text());
        var button = $(this).text().trim().toLowerCase();
        // console.log($(this));
        if (button) {
          if (json != null) {
            for (let i = 0; i < json.result.length; i++) {
              //console.log(json.result[i]);
              if (json.result[i].statusAkses == false) {
                if (button == json.result[i].caption.toLowerCase()) {
                  $(this).detach();
                }
                //$(this).prop('disabled', true);
              }
            }
          }else{

          }
        }
        //console.log($(this).text().trim().toLowerCase());

      });
    }

    navigateToSidebar(url,data,action:(data)=>void){
      // console.log(service,url,data);
      let menu = [];
      this.http.post(base_url_api + url,JSON.stringify(data),
      {
        headers:this.header
      })
      .pipe(
        map((res:Config)=>{
          return JSON.parse(res.result.data)
        })
        )
        .subscribe((res:Config)=>{

          // console.log(res,res.length);
          // Swal.close();
          this.spinner.hide('ajaxSpinner');

          if(res.length > 0){

            for(let i=0;i < res.length;i++){
              menu.push({
                text:res[i].text,
                iconCss:res[i].iconCss,
                url:res[i].items?.length == 0 ? res[i].url:null,
                items:res[i].items?.length == 0 ? null:res[i].items,
                MenuSideBarId: res[i].MenuSideBarId,
                roleId:res[i].RoleId
              })
            }

            // console.log(menu);

            this.storage.removeItem("sidebar");
            this.storage.setItem("sidebar", JSON.stringify(menu));

            if ($('#sidebar-treeview').hasClass('e-open')) {
              $('#hamburger').trigger('click');
            }

            action(menu);
          }

          // jeda sebelum set sidebar menu
          // setTimeout(() => {
          //     this.menuItems = menu;
          // }, 100);
        },
        (err)=>{

          //this.refreshJwtToken(err);
        });
      }


      readNotification(url_api,redirect_url,notifId,userId){

        this.http.post(url_api,JSON.stringify({
          'IdNotification':notifId,
          'UserId':userId
        }),
        {
          headers:this.header
        }).subscribe((data)=>{

          // console.log(data);
          location.href = redirect_url;
        });
      }

      getComboboxValue(cbbComponent){
        return cbbComponent.itemData?.value == undefined ? "":cbbComponent.itemData?.value;
      }

      //setting datasource pada komponent syncfusion yg support datasource
      setAsyncDataSource(url,data:any = []):Observable<any>{
        this.spinner.show("ajaxSpinner");
        return this.http.post(base_url_api + url,JSON.stringify(data),
        {headers:this.header})
        .pipe(
          map((res:Config)=>{
            // console.log(res);

            setTimeout(() => {
              this.spinner.hide("ajaxSpinner");
            }, 100);

            return res.result.data;
          }),
          catchError((err)=>{
            this.spinner.hide("ajaxSpinner");
            // console.log(error);

            //this.refreshJwtToken(err);
            return null;
          })
          );

        }


        setComboboxValueNoForm(component,value){

          let filter = [];
          if(component.dataSource?.length > 0){

            filter = (component.dataSource).filter((df)=>{
              // console.log(df.value,value);
              return df.value == value;
            });

            // console.log(component.value)
            if(filter.length > 0){

              if(component.value == null ||
                component.value == ""){
                  component.value = filter[0].text;
                }
              }
            }
          }

          setComboboxValue(formGroup:FormGroup,component,value){
            // console.log(component);
            try{

              let id = component.element?.id;
              // console.log(component.element.id);
              // console.log(id,component.dataSource)
              if(id != undefined){
                formGroup.get(id).setValue(value);

                let filter = [];

                // console.log(component.dataSource)

                let interval = null;

                interval = setInterval(()=>{

                  if(component.dataSource?.length > 0){

                    filter = (component.dataSource).filter((df)=>{
                      // console.log(df.value,value);
                      return df.value == value;
                    });

                    // console.log(component.value)
                    if(filter.length > 0){

                      if(component.value == null ||
                        component.value == ""){
                          component.value = filter[0].text;
                        }
                      }

                      if(component.value != null){
                        clearInterval(interval);
                      }
                    }

                  },10);

                }
              }catch(err){
                // console.log(err)
              }
            }

            //untuk get form control value
            getFormControlValue(formGroup:FormGroup,controlName):any{
              return formGroup.get(controlName).value;
            }

            //untuk set form control value
            setFormControlValue(formGroup:FormGroup,controlName,value){
              if(isBoolean(value)){
                $('#' + controlName).prop('checked',value);
              }

              // if(formGroup.get(controlName)?.value == null ||
              // formGroup.get(controlName)?.value == "" ||
              // formGroup.get(controlName)?.value == "0"){
              formGroup.get(controlName)?.setValue(value);
              // }
            }

            setFormControlValueWithTrigger(formGroup:FormGroup,controlName,value,actionTrigger:(data)=>void){
              if(isBoolean(value)){
                $('#' + controlName).prop('checked',value);
              }
              formGroup.get(controlName)?.setValue(value);

              actionTrigger(value);
            }

            isArray(what) {
              return Object.prototype.toString.call(what) === '[object Array]';
            }

            setFormControlValueByAjax(api_url,data, formGroup:FormGroup){
              this.http.post( base_url_api + api_url,
                JSON.stringify(data),
                {headers:this.header})
                .pipe(
                  map((res:Config)=>res.result.data)
                  )
                  .subscribe((res:Config)=>{


                    if(this.isArray(res)){
                      for(let index in res){
                        let data = res[index];
                        for(let item in data){

                          // console.log(item,data[item])

                          if($('#' + item).val() == null ||
                          $('#' + item).val() == "" ||
                          $('#' + item).val() == "0"){

                            $('#' + item).val(data[item]);

                          }

                          this.setFormControlValue(formGroup,item,data[item]);
                          this.setComboboxValue(formGroup,item,data[item]);
                        }
                      }
                    }else{
                      // console.table(res);
                      for(let item in res){
                        // console.log(item,res[item])

                        if($('#' + item).val() == null ||
                        $('#' + item).val() == "" ||
                        $('#' + item).val() == "0"){

                          $('#' + item).val(res[item]);

                        }

                        this.setFormControlValue(formGroup,item,res[item]);
                        this.setComboboxValue(formGroup,item,res[item]);
                      }
                    }
                  },
                  (err)=>{

                    //this.refreshJwtToken(err);
                  });
                }


                setFormControlValueByAjaxWithTrigger(api_url,data, formGroup:FormGroup,actionTrigger:(data)=>void){
                  this.http.post( base_url_api + api_url,
                    JSON.stringify(data),
                    {headers:this.header})
                    .pipe(
                      map((res:Config)=>res.result.data)
                      )
                      .subscribe((res:Config)=>{


                        if(this.isArray(res)){
                          for(let index in res){
                            let data = res[index];
                            for(let item in data){
                              // console.log(item,data[item])

                              if($('#' + item).val() == null ||
                              $('#' + item).val() == "" ||
                              $('#' + item).val() == "0"){

                                $('#' + item).val(data[item]);

                              }

                              this.setFormControlValue(formGroup,item,data[item]);
                              this.setComboboxValue(formGroup,item,data[item]);
                            }
                          }
                        }else{
                          // console.table(res);
                          for(let item in res){

                            if($('#' + item).val() == null ||
                            $('#' + item).val() == "" ||
                            $('#' + item).val() == "0"){

                              $('#' + item).val(res[item]);

                            }

                            this.setFormControlValue(formGroup,item,res[item]);
                            this.setComboboxValue(formGroup,item,res[item]);
                          }
                        }

                        actionTrigger(res);
                      },
                      (err)=>{

                        //this.refreshJwtToken(err);
                      });
                    }


                    setNumericValue(formGroup:FormGroup,component,value){
                      // console.log(component);
                      try{
                        component.value = value;
                        formGroup.get(component.cloneElement.id).setValue(value);
                      }catch(err){
                        // console.log(err);
                      }
                    }

                    setFormControlValueByEvent(formGroup: FormGroup,event){
                      // console.log(event)
                      try{
                        if(formGroup){
                          let value;

                          // console.log(event.element.id,event.itemData);

                          //setting value
                          if(event.itemData == undefined){

                            // console.log(event.value)
                            if(event.value != undefined){
                              let isDate = (event.value instanceof Date);
                              // console.log(isDate)

                              if(isDate){
                                value = event.value;//this.helper.reformatingDateValue(event.value);
                              }else{
                                value = event.value;
                              }
                            }else{
                              value = event.target.checked;
                            }
                          }else{
                            //dropdownlist / combobox
                            value = event.itemData.value;
                          }

                          //formControlName untuk combobox,numeric,dan checkbox diganti dengan id
                          let controlName = ""
                          // console.log(event.element);
                          // controlName = event.element.id;

                          if(event.element != undefined){
                            //untuk dropdownlist,combobox,dan datepicker
                            controlName = event.element.id;
                          }else{
                            //untuk numerictextbox
                            if(event.event != undefined)
                            controlName = event.event.srcElement.offsetParent.parentElement.id
                            else
                            //untuk element html default / bukan syncfusion
                            controlName = event.target.id;
                          }

                          // console.log(controlName,value)
                          formGroup.get(controlName).setValue(value);
                        }
                      }catch(err){
                        console.log(err)
                      }
                    }

                    submitFormSummaryErrors(e,api_url,data){
                      e.preventDefault();
                      // console.log(data);
                      this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      })
                      .subscribe((res:Config)=>{

                        // console.log(res.result.data);
                        Swal.fire({
                          title:'Perhatian',
                          icon:'success',
                          html:res.result.data
                        });

                        location.reload();
                      },
                      (err)=>{
                        // console.log(err);
                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;
                          let errorMessages = "";

                          for(var index in errors){

                            // this.errorData.push(
                            //   errors[index][0]
                            //   );
                            errorMessages += errors[index][0] + "<br>";
                          }

                          Swal.fire({
                            title:'ERROR',
                            icon:'error',
                            html:errorMessages
                          });
                        }

                      });
                    }

                    submitFormSingleError(e,api_url,data){
                      e.preventDefault();
                      // console.log(data);
                      this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      })
                      .subscribe((res:Config)=>{

                        // console.log(res.result.data);
                        Swal.fire({
                          title:'Perhatian',
                          icon:'success',
                          html:res.result.data
                        });

                        location.reload();
                      },
                      (err)=>{
                        // console.log(err);

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;
                          let errorMessages = "";

                          for(var index in errors){

                            // this.errorData.push(
                            //   errors[index][0]
                            //   );
                            errorMessages = errors[index][0] + "<br>";
                          }

                          Swal.fire({
                            title:'ERROR',
                            icon:'error',
                            html:errorMessages
                          });
                        }
                      });
                    }

                    //merubah data undefined menjadi null,
                    // dan merubah empty value menjadi integer (masukkan ke parameter column)
                    updateFormDataType(form,column:any = []){
                      let data = form.value;
                      if(column.length > 0){
                        for(let item in column){
                          // console.log(item);
                          data[column[item]] = parseInt(data[column[item]] == undefined ? 0:data[column[item]]);
                        }
                      }


                      for(let index in data){
                        if(data[index] == undefined){
                          data[index] = null;
                        }

                        let boolean = data[index] == 'true'? true: data[index] == 'false' ? false: data[index];

                        if(typeof(boolean)){
                          data[index] = boolean;
                        }
                      }
                    }


                    //menampilkan tooltip teks untuk grid cell / column
                    showGridTooltips(args){
                      let value = args.data[args.column.field];
                      if(value){
                        const tooltip: Tooltip = new Tooltip({
                          content: args.data[args.column.field].toString()
                        }, args.cell as HTMLTableCellElement);
                      }
                    }

                    //update grid data dari hasil lookup
                    updateGridDataByLookupCallback(grid,index,lookupData){

                      for(let item in lookupData){
                        $('#' + grid.id + item).val(lookupData[item]);
                        grid.dataSource[index][item] = lookupData[item];
                      }

                      // grid.grid.endEdit();
                      // grid.refresh();
                    }

                    //merubah data undefined menjadi null,
                    // dan merubah empty value menjadi integer (masukkan ke parameter column)
                    updateGridDataType(grid,column:any = []){
                      let gridData = grid.dataSource;

                      for(let i=0; i < gridData.length;i++){
                        if(column.length > 0){
                          for(let item in column){
                            // console.log(item);
                            gridData[i][column[item]] = parseInt(gridData[i][column[item]] == undefined ? 0:gridData[i][column[item]]);
                          }
                        }


                        for(let item in gridData[i]){
                          if(gridData[i][item] == undefined)
                          gridData[i][item] = null;
                        }
                      }
                    }

                    updateGridDataClientSide(args, grid,data = null) {
                      // console.log(args);
                      if (args.rowIndex != undefined) {
                        data = data == null ? args.data:data;
                        grid.dataSource[args.rowIndex] = data;
                      }
                    }

                    disableExpandingAccordion(args,toggleStatus = false){
                      if(toggleStatus == false){
                        args.cancel = true;
                      }
                    }

                    //untuk menghapus namun harus pakai method HTTP_DELETE
                    deleteData(api_url,data,actionAfterDelete:(data)=>void){
                      this.http.delete(base_url_api + api_url + '/' + data.toString(), {
                        headers:this.header
                      }).subscribe((res:Config)=>{


                        Swal.fire({
                          title:'Perhatian',
                          icon:res.result.data.startsWith('SUCCESS') ? 'success':'error',
                          html:res.result.data.substring(res.result.data.indexOf(':') + 2)
                        })

                        actionAfterDelete(data)
                      },
                      err=>{

                        // console.log(err)
                        Swal.fire({
                          title:'Perhatian',
                          icon:'error',
                          html:err.error.message
                        })
                      });
                    }

                    //bisa untuk semua akses api, tidak harus untuk menyimpan tapi juga untuk hapus
                    submitFormNoFormGroup(api_url,data,actionAfterSubmit:()=>void){
                      this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      })
                      .subscribe((res:Config)=>{
                        // console.log(res.result.data);
                        Swal.fire({
                          title:'Perhatian',
                          icon:res.result.data.startsWith('SUCCESS') ? 'success':'error',
                          html:res.result.data.substring(res.result.data.indexOf(':') + 2)
                        }).then(()=>{
                          // location.reload();
                          actionAfterSubmit();
                        });
                      },
                      (err)=>{
                        console.log(err);

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;

                          if(errors != undefined){
                            // console.log(errors);

                            for(let item in errors){
                              this.helper.swalMsg( ' ( ' + errors[item] + ' )','Perhatian','error');
                              break;
                            }
                            // this.helper.swalMsg(errors,'Perhatian','error');
                          }else{
                            // console.log(err)
                            let messageError = err.error.result.data.split(".")[1] == undefined ? err.error.result.data.split(".")[0]:err.error.result.data.split(".")[1];
                            messageError = messageError.startsWith('FAIL') ? messageError.substring(messageError.indexOf(':') + 2) : messageError;
                            this.helper.swalMsg(messageError,'Perhatian','error');
                          }
                        }
                      });
                    }

                    //khusus untuk menyimpan
                    submitForm(e,api_url,form:FormGroup,data,actionAfterSubmit:()=>void){
                      e.preventDefault();
                      // console.log(data);
                      this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      })
                      .subscribe((res:Config)=>{
                        // console.log(res.result.data);

                        Swal.fire({
                          title:'Perhatian',
                          icon:res.result.data.startsWith('SUCCESS') ? 'success':'error',
                          html:res.result.data.substring(res.result.data.indexOf(':') + 2)
                        }).then(()=>{
                          // location.reload();
                          actionAfterSubmit();
                        });
                      },
                      (err)=>{

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;

                          if(errors != undefined){
                            // console.log(errors);
                            for(var index in errors){

                              let formControlName = index.replace('$.','').split(".")[index.replace('$.','').split(".").length -1];
                              // console.log(formControlName)
                              if(form != null){
                                // console.log(form.get(formControlName));
                                let formControl = form.get(formControlName);

                                if(formControl != null){
                                  formControl.setErrors({
                                    serverError:(errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                    "Format salah : " + errors[index][0].split(" ")[8]
                                    :errors[index][0])
                                  });

                                  Swal.fire({
                                    title:'Perhatian',
                                    icon:'error',
                                    html:err.error.result.title + "<br>" + " (" +
                                    (errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                    "Format salah : " + errors[index][0].split(" ")[8]
                                    :errors[index][0]) + ")"
                                  });
                                }else{

                                  Swal.fire({
                                    title:'Perhatian',
                                    icon:'error',
                                    html:err.error.result.title + "<br>" + " (" + formControlName + " : " +
                                    (errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                    "Format salah : " + errors[index][0].split(" ")[8]
                                    :errors[index][0]) + ")"
                                  });
                                }
                              }
                            }
                            // errorMessages = errors[index][0] + "<br>";
                          }else{

                            Swal.fire({
                              title:'Perhatian',
                              icon:'error',
                              html:err.error.result
                            });
                          }

                          // Swal.fire({
                          //   title:'ERROR',
                          //   icon:'error',
                          //   html:errorMessages
                          // });
                        }
                      });
                    }

                    //tanpa add new row di grid syncfusion, keluar swal message
                    validateGridDialogNoAddRecordWithMessage(args,grid,api_url,data,actionAfterSave:()=>void){
                      // console.log(data);
                      let action = args.action;

                      for(let index in data){
                        data[index] = this.helper.removeTagsHTML(data[index]);
                      }
                      // console.log(data);
                      return this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      }).subscribe((res:Config)=>{

                        grid.closeEdit();
                        grid.refresh();

                        Swal.fire({
                          title:'Perhatian',
                          icon:res.result.data.startsWith('SUCCESS') ? 'success':'error',
                          html:res.result.data.substring(res.result.data.indexOf(':') + 2)
                        }).then((val)=>{

                          // console.log(grid.dataSource);
                          actionAfterSave();
                        });
                      },
                      (err)=>{
                        console.log(err);

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;

                          // console.log(errors);

                          if(errors){
                            for(var index in errors){

                              let formControlName = index.replace('$.','').split(".")[index.replace('$.','').split(".").length -1];
                              // console.log(formControlName)

                              Swal.fire({
                                title:'Perhatian',
                                icon:'error',
                                html:err.error.result.title + "<br>" + " (" + formControlName + " : " +
                                (errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                "Format salah : " + errors[index][0].split(" ")[8]
                                :errors[index][0]) + ")"
                              });
                            }
                          }else{

                            let messageError = err.error.result.data.split(".")[0].toString();

                            // console.log(messageError);
                            if(messageError.startsWith("SUCCESS") || messageError.startsWith("FAIL")){
                              messageError = messageError.substring(messageError.indexOf(':') + 2)
                            }

                            Swal.fire({
                              title:'Perhatian',
                              icon:'error',
                              html:messageError
                            });
                          }
                        }
                      });
                    }


                    //tanpa input new row di grid syncfusion, tapi input ke database
                    validateGridDialogNoAddRecord(args,grid,api_url,data,actionAfterSave:()=>void){
                      // console.log(data);
                      let action = args.action;

                      for(let index in data){
                        data[index] = this.helper.removeTagsHTML(data[index]);
                      }

                      return this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      }).subscribe((res:Config)=>{
                        grid.closeEdit();
                        grid.refresh();

                        // console.log(grid.dataSource);
                        actionAfterSave();
                      },
                      (err)=>{

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          console.log(err);

                          let errors = err.error.result.errors;

                          // console.log(errors);

                          if(errors){
                            for(var index in errors){

                              let formControlName = index.replace('$.','').split(".")[index.replace('$.','').split(".").length -1];
                              // console.log(formControlName)

                              Swal.fire({
                                title:'Perhatian',
                                icon:'error',
                                html:err.error.result.title + "<br>" + " (" + formControlName + " : " +
                                (errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                "Format salah : " + errors[index][0].split(" ")[8]
                                :errors[index][0]) + ")"
                              });
                            }
                          }else{

                            let messageError = err.error.result.data.split(".")[0].toString();

                            // console.log(messageError);
                            if(messageError.startsWith("SUCCESS") || messageError.startsWith("FAIL")){
                              messageError = messageError.substring(messageError.indexOf(':') + 2)
                            }

                            Swal.fire({
                              title:'Perhatian',
                              icon:'error',
                              html:messageError
                            });
                          }
                        }
                      });
                    }


                    //diikuti insert new row di grid syncfusion, tapi tanpa message swal
                    validateGridDialog(args,grid,api_url,data,actionAfterSave:()=>void){
                      // console.log(data);
                      let action = args.action;

                      for(let index in data){
                        data[index] = this.helper.removeTagsHTML(data[index]);
                      }

                      return this.http.post(base_url_api + api_url,JSON.stringify(data),{
                        headers:this.header
                      }).subscribe((res:Config)=>{

                        if(action === "add"){
                          // grid.dataSource.push(data);
                          grid.addNewRecord(data);
                        }else if(action === "edit"){
                          this.updateGridDataClientSide(args,grid,data);
                        }

                        grid.closeEdit();
                        grid.refresh();

                        // console.log(grid.dataSource);
                        actionAfterSave();
                      },
                      (err)=>{

                        console.log(err);

                        let status = err.status;
                        if(status == 401){

                          //this.refreshJwtToken(err);
                        }else{
                          let errors = err.error.result.errors;

                          // console.log(errors);

                          if(errors){
                            for(var index in errors){

                              let formControlName = index.replace('$.','').split(".")[index.replace('$.','').split(".").length -1];
                              // console.log(formControlName)

                              Swal.fire({
                                title:'Perhatian',
                                icon:'error',
                                html:err.error.result.title + "<br>" + " (" + formControlName + " : " +
                                (errors[index][0].indexOf("The JSON value could not be converted to") > -1 ?
                                "Format salah : " + errors[index][0].split(" ")[8]
                                :errors[index][0]) + ")"
                              });
                            }
                          }else{

                            let messageError = err.error.result.data.split(".")[0].toString();

                            // console.log(messageError);
                            if(messageError.startsWith("SUCCESS") || messageError.startsWith("FAIL")){
                              messageError = messageError.substring(messageError.indexOf(':') + 2)
                            }

                            Swal.fire({
                              title:'Perhatian',
                              icon:'error',
                              html:messageError
                            });
                          }
                        }
                      });
                    }

                    printReport(api_url, data) {
                      // console.log(api_url,JSON.stringify(data));
                      this.spinner.show('ajaxSpinner');
                      this.http.post(base_url_api +  api_url,JSON.stringify(data),{
                        headers:this.header
                      }).subscribe(
                        (res:Config)=> {
                          let typePdf = "data:application/pdf;base64,";//.replace(/['"]+/g, '');
                          var url = typePdf + res.result.data;

                          // console.log(url);
                          var params = [
                            'height=' + screen.height,
                            'width=' + screen.width,
                            'fullscreen=yes' // only works in IE, but here for completeness
                          ].join(',');

                          var content = '<embed width=100% height=100% id="viewer" '
                          + ' type="application/pdf"'
                          + ' src="' + url + '"></embed>';

                          var printwindow = window.open("", "pdf", params);
                          printwindow.moveTo(0, 0);
                          printwindow.document.write(content);
                          this.spinner.hide('ajaxSpinner');
                        },
                        (err) => {
                          this.spinner.hide('ajaxSpinner');
                          // console.log(err);

                          //this.refreshJwtToken(err);
                        }
                        );
                      }

                      exportReport(api_url, data,format = "pdf") {

                        this.header = this.header.set("ReportFormat",format);

                        switch(format){
                          case "pdf":format="pdf";break;
                          case "doc":format="msword";break;
                          case "word":format="msword";break;
                          case "xls":format="vnd.ms-excel";break;
                          case "excel":format="vnd.ms-excel";break;
                          default:format="pdf";break;
                        }

                        //format pdf / xls / doc
                        // console.log(api_url,JSON.stringify(data));
                        this.spinner.show('ajaxSpinner');
                        this.http.post(api_url,JSON.stringify(data),{
                          headers:this.header
                        }).subscribe(
                          (res:Config)=> {

                            convertBase64ToFile(res.result.data,format);

                            this.spinner.hide('ajaxSpinner');
                          },
                          (err) => {
                            this.spinner.hide('ajaxSpinner');
                            // console.log(err);

                            //this.refreshJwtToken(err);
                          }
                          );
                        }


                      }

