import { catchError, map } from 'rxjs/operators';
import { UserData } from './../../Models/userData';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable, throwError } from 'rxjs';
import { secret_key_http_request, secret_key_jwt } from '../Url/url-constant';
import { StorageService } from '../../Services/StorageService/storage.service';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class RequestHttpInterceptor implements HttpInterceptor {
  private token:string = "";
  constructor(private storage:StorageService,
    private cookie:CookieService,
    private custom:CustomService) {
      // this.notif.requestPermission();
    }

    intercept( request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      // action interceptor
      let userData = this.custom.getUserData();

      //jwt token pertama
      this.token = this.cookie.get(this.storage.encryptSHA1('uat'));
      // console.log(userData);

      let header : HttpHeaders;
      header = request.headers;
      if(this.token) header=header.set('uat', this.token ?? ''); //jwt
      if(userData) header=header.set('auth', btoa(userData.userid.toString()));

      //bug di interceptor, request yg sudah di enkrip di enkrip lagi, jadi harus di normalize dlu.
      let pureData = this.storage.decrypt(request.body,secret_key_http_request);

      // console.log(request.body,pureData)
      let dataBody = environment.isEncriptionStorage ?  this.storage.encrypt(pureData ? pureData:request.body,secret_key_http_request) : request.body

      //sample token
      request = request.clone({
        withCredentials:true,
        headers: header,
        body:dataBody
      });

      // console.log(request);
      return next.handle(request).pipe(
        map(res => {
          if(res instanceof HttpResponse){
            this.custom.sessionIdle = 0;
            // console.log(res.body.result);
            let data = res.body.result;
            data = this.modifyEncriptedJson(data);
            res.body.result = data;
            // console.log(res.body.result);
            return res;
          }
        }),
        catchError(
          err=>{
            console.log(err)
            if(err instanceof HttpErrorResponse){
              this.custom.sessionIdle = 0;

              err.error.result = this.modifyEncriptedJson(err.error.result);
              //refreshed token
              let newToken = err.error.result.refreshToken;
              // console.log(err);

              //jika error authentication dan mendapat refreshToken => refresh token dan ulangi call api
              if(err.status == 401){
                if(newToken) header=header.set('uat', newToken ?? '');

                this.custom.refreshJwtToken(err);

                //jika token di refresh maka ulangin request http nya dengan token baru
                // with refreshed token
                request = request.clone({
                  withCredentials:true,
                  headers: header,
                  body:request.body
                });

                return next.handle(request).pipe(
                  map(res => {
                    if(res instanceof HttpResponse){
                      // console.log(res);
                      this.custom.sessionIdle = 0;
                      let data = res.body.result;
                      data = this.modifyEncriptedJson(data);
                      res.body.result = data;
                      // console.log(res.body.result);
                      return res;
                    }
                  }));
                }else{
                  this.custom.sessionIdle = 0;
                  return throwError(err);
                }

              }
            })
            );
          }

          modifyEncriptedJson(body:any):any{
            //bug di interceptor, request yg sudah di enkrip di enkrip lagi, jadi harus di normalize dlu.
            let pureData = this.storage.decrypt(body,secret_key_http_request);

            // console.log(body,pureData)
           body = environment.isEncriptionStorage ?  (pureData ? pureData:JSON.parse(body)) : body
            // console.log(body)
            return body;
          }
        }
