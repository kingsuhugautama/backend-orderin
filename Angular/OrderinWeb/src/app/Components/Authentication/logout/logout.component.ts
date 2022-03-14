import { UserData } from './../../../Models/userData';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CrudService } from 'src/app/Services/CrudService/crud.service';
import { StorageService } from 'src/app/Services/StorageService/storage.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  private userData:UserData;

  constructor(private router:Router,
    private cookie:CookieService,
    private crud:CrudService,
    private custom:CustomService,
    private storage:StorageService) {

      this.userData = this.custom.getUserData();

      this.crud.ajaxDataPost('setupuser/signOut',this.userData.userid).subscribe(res=>{

        this.cookie.deleteAll();
        this.router.navigate(['']);
      });

    }

    ngOnInit(): void {
    }

  }
