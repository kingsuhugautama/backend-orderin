declare var Swal:any;
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { base_url_api  } from '../../Utility/Url/url-constant';
import { NgxSpinnerService } from "ngx-spinner";


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private header;
  constructor(private http:HttpClient,
    private spinner: NgxSpinnerService) {
    this.header = new HttpHeaders();
    this.header=this.header.set('Content-Type', 'application/json;');
  }

  login(url,data:any):Observable<any>{
    // Swal.fire({ title: 'Loading..', onOpen: () => { Swal.showLoading() } });
    this.spinner.show();
    // sessionStorage.setItem('HttpClient',JSON.stringify(this.http));
    // console.log(this.http);
    return this.http.post(base_url_api  + url,JSON.stringify(data),
      {
        headers:this.header
      });
    }

  }

