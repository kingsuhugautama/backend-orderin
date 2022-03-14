import { CookieService } from 'ngx-cookie-service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
declare var Swal:any;

import { StorageService } from 'src/app/Services/StorageService/storage.service';
import { AuthenticationService } from './../../../Services/AuthenticationService/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AnimationItem } from 'lottie-web';
import { AnimationOptions } from 'ngx-lottie';
import { Config } from 'protractor';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public fg:FormGroup;

  constructor(private fb:FormBuilder,
    private auth:AuthenticationService,
    private storage:StorageService,
    private cookie:CookieService,
    private router:Router,
    private spinner:NgxSpinnerService) {
      this.fg = this.fb.group({
        "email":["",[]],
        "password":["",[]]
      })
    }

    ngOnInit(): void {
    }

    options: AnimationOptions = {
      path: '/assets/json/lottie/business-strategy.json',
    };

    animationCreated(animationItem: AnimationItem): void {
      // console.log(animationItem);
    }

    login(e){
      this.spinner.show();
      let data = {
        email:this.fg.value.email,
        password:this.storage.encryptSHA1(this.fg.value.password)
      };

      this.auth.login("setupuser/signinmerchant",data).subscribe(res=>{
        // console.log(res)
        let token = res.result.message;

        //uat = user authorization token
        this.cookie.set('uat',token);


        let payload = res.result.data;

        let userData = {
          email:payload.email,
          userid:payload.userid,
          firstname:payload.firstname,
          lastname:payload.lastname,
          avatarurl:payload.avatarurl,
          merchantid:payload.merchantid,
          merchantname:payload.merchantname,

          description: payload.description,
          logoimageurl: payload.logoimageurl,
          coverimageurl: payload.coverimageurl,
          avgratingproduct: payload.avgratingproduct,
          avgratingpackaging: payload.avgratingpackaging,
          avgratingdelivering: payload.avgratingdelivering
        }

        // this.storage.setItem("UserData",JSON.stringify(userData));
        this.storage.setItem("UserData",JSON.stringify(userData));

        this.router.navigate(['dashboard']);
      },
      (err:Config)=>{
        this.spinner.hide();
        // console.log(err);
        this.storage.removeItem("UserData");
        Swal.fire({
          'title':'Error',
          'icon':'error',
          'html':err.error.result
        })
      });
    }

    actionSubmit(){}
  }
