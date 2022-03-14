import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import {map} from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CrudService } from 'src/app/Services/CrudService/crud.service';
import { StorageService } from 'src/app/Services/StorageService/storage.service';
import { sessionUrlPath,secret_key_session } from '../../Utility/Url/url-constant';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private _router:Router,
    private crud:CrudService,
    private storage:StorageService,
    private cookie:CookieService){
    }

    canActivate(route:ActivatedRouteSnapshot,
      state:RouterStateSnapshot):Observable<boolean>{

        return this.crud.ajaxDataPost(sessionUrlPath,{
          "Prefix":this.storage.encrypt("UserData",secret_key_session)
        }).pipe(
          map((res)=>{
            // console.log(this.cookie.get('.AspNetCore.Session'));
            let data = res.result.data;
            console.log(data);
            if(route.routeConfig.path == ""){
              if(data == false){
                // this._router.navigate(['']);
                return true;
              }else{
                this._router.navigate(['dashboard']);
                return false;
              }
            }else{
              if(data){
                return data;
              }else{
                this._router.navigate(['']);
                return data;
              }
            }
          }));

        }

      }



