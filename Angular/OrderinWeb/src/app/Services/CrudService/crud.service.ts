declare var Swal:any;

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { base_url_api  } from '../../Utility/Url/url-constant';
import { NgxSpinnerService } from "ngx-spinner";

@Injectable({
  providedIn: 'root'
})
export class CrudService {
  private header : HttpHeaders;
  constructor(private http:HttpClient,
    private spinner: NgxSpinnerService) {
    this.header = new HttpHeaders();
    this.header=this.header.set('Content-Type', 'application/json;');
    // this.header=this.header.set('AuthorizationToken', localStorage.getItem('authorizationToken'));
  }

  // post method

  ajaxDataPost(url:string,data:any = [],showLoading:boolean=false):Observable<any>{
    if(showLoading)
    // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
    this.spinner.show();
    return this.http.post(base_url_api  + url,JSON.stringify(data),{
      headers : this.header
    });
  }

  // put method
  ajaxDataPut(url:string,data:any = [],showLoading:boolean=false):Observable<any>{
    if(showLoading)
    // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
    this.spinner.show();

    return this.http.put(base_url_api  + url,JSON.stringify(data),
    {
      headers:this.header
    });
  }

  // delete method
  ajaxDataDelete(url:string,data:any = [],showLoading:boolean=false):Observable<any>{
    if(showLoading)
    // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
    this.spinner.show();

    return this.http.delete(base_url_api  + url + '/' + JSON.stringify(data),{
      headers : this.header
    });
  }

  // get method
  ajaxDataGet(url:string,data:any = [],showLoading:boolean=false):Observable<any>{
    if(showLoading)
    // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
    this.spinner.show();

    return this.http.get(base_url_api  + url,{
      headers : this.header
    });
  }

}
