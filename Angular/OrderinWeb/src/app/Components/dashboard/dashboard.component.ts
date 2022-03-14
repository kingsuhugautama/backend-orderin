import { NgxSpinnerService } from 'ngx-spinner';
import { CustomService } from 'src/app/Services/CustomService/custom.service';
import { UserData } from './../../Models/userData';
import { SidebarComponent, TreeViewComponent } from '@syncfusion/ej2-angular-navigations';
import { AppService } from './../../Services/AppService/app.service';
import { NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import 'rxjs/add/operator/filter';
import { Config } from 'protractor';

import { L10n,setCulture,loadCldr,setCurrencyCode } from '@syncfusion/ej2-base';
import indonesia from '../../Locale/syncfusion-locale/src/id.json';

import numberingSystems from '../../Locale/calendar-locale/old/numberingSystems.json';
import gregorian from '../../Locale/calendar-locale/old/id-gregorian.json';
import numbers from '../../Locale/calendar-locale/old/numbers.json';
import detimeZoneNames from '../../Locale/calendar-locale/old/timeZoneNames.json';
import weekData from '../../Locale/calendar-locale/old/weekData.json';
import currenCy from '../../Locale/calendar-locale/old/currencies.json';

loadCldr(numberingSystems, gregorian, numbers, detimeZoneNames,weekData,currenCy);

setCulture('id');
setCurrencyCode('IDR');
L10n.load(indonesia);
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidebarTreeviewInstance')
  public sidebarTreeviewInstance: SidebarComponent;
  @ViewChild('treeviewInstance')
  public treeviewInstance: TreeViewComponent;
  public width: string = '250px';
  public enableDock: boolean = true;
  public dockSize:string ="44px";
  public mediaQuery: string = ('(min-width: 600px)');
  public target: string = '.main-content';
  public title = ''
  public userBarOpen :boolean = false;
  public userData : UserData;

  constructor(private router:Router,
    private app:AppService,
    private custom:CustomService,
    private route:Router,
    private spinner:NgxSpinnerService) {
      this.userData = this.custom.getUserData();
      this.app.getTitle().subscribe((res)=>{
        this.title = res.toString();
      });


      this.router.events
      .filter(e => e instanceof NavigationStart ||
        e instanceof NavigationEnd ||
        e instanceof NavigationError)
        .subscribe((route: Config)=>{
          // console.log(route);
          if(route instanceof NavigationStart){
            this.spinner.show('dashboardSpinner');
          }else if(route instanceof NavigationEnd){
            this.spinner.hide('dashboardSpinner');
          }else if(route instanceof NavigationError){
            console.log('ERROR ROUTING ' + route)
            // window.location.href = route.url;
          }
        });
      }

      ngAfterViewInit(): void {
        //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
        //Add 'implements AfterViewInit' to the class.
        this.spinner.hide();
      }

      ngOnInit(): void {
      }


      //url untuk routing angular, navigateUrl untuk redirect ke url
      public data: Object[] = [
        {
          nodeId: '01', nodeText: 'Beranda', iconCss: 'fas fa-home',
        },
        {
          nodeId: '02', nodeText: 'Akun', iconCss: 'fas fa-user-circle',
        },
        {
          nodeId: '03', nodeText: 'Data', iconCss: 'fas fa-chart-bar',
          nodeChild: [
            {
              nodeId: '03-01', nodeText: 'Omset', iconCss: 'fas fa-file-invoice-dollar',
              nodeChild:[
                {
                  nodeId: '03-01-01', nodeText: 'Omset Per Dropship', iconCss: 'fas fa-hand-point-down',url:'dashboard/grafik/omset-dropship',
                  // nodeChild:[
                  //   {
                  //     nodeId: '03-01-01-01', nodeText: 'Omset Produk Per Dropship ', iconCss: 'fas fa-sitemap',url:'dashboard/grafik/omset-produk-dropship'
                  //   }
                  // ]
                },
                {
                  nodeId: '03-01-02', nodeText: 'Omset Per Tanggal Po', iconCss: 'fas fa-calendar-alt',url:'dashboard/grafik/omset-pertglpo'
                },
                {
                  nodeId: '03-01-03', nodeText: 'Omset Per Produk', iconCss: 'fas fa-gifts',url:'dashboard/grafik/omset-produk'
                },
                {
                  nodeId: '03-01-04', nodeText: 'Omset Per Kategori', iconCss: 'fas fa-object-group',url:'dashboard/grafik/omset-kategori'
                },
                {
                  nodeId: '03-01-05', nodeText: 'Ongkos Kirim', iconCss: 'fas fa-shipping-fast',url:'dashboard/grafik/ongkir-dropship'
                }
              ]
            },
            {
              nodeId: '03-02', nodeText: 'Analisa Customer', iconCss: 'fas fa-clipboard-list',
              nodeChild:[
                {
                  nodeId: '03-02-01', nodeText: 'Jenis Kelamin', iconCss: 'fas fa-transgender',url:'dashboard/grafik/analisa-gender'
                },
                {
                  nodeId: '03-02-02', nodeText: 'Usia', iconCss: 'fas fa-wheelchair',url:'dashboard/grafik/analisa-usia'
                },
                {
                  nodeId: '03-02-03', nodeText: 'Customer Pembelian Produk', iconCss: 'fas fa-reply-all',url:'dashboard/grafik/analisa-repeat-order'
                }
              ]
            },

            {
              nodeId: '03-03', nodeText: 'Analisa Peringkat', iconCss: 'fas fa-gifts',
              nodeChild:[
                { nodeId: '03-03-01', nodeText: 'Analisa Peringkat Produk', iconCss: 'fas fa-gifts',url:'dashboard/grafik/produk-peringkat' },
                { nodeId: '03-03-02', nodeText: 'Analisa Peringkat Pengiriman', iconCss: 'fas fa-shipping-fast',url:'dashboard/grafik/analisa-pengiriman' }
              ]
            }
          ]
        },
        {
          nodeId: '04', nodeText: 'Keluar', iconCss: 'fas fa-sign-out-alt',
        },
      ];
      public field:Object ={ dataSource: this.data, id: 'nodeId', text: 'nodeText', child: 'nodeChild', iconCss: 'iconCss' };

      public onCreated(args: any) {
        this.sidebarTreeviewInstance.element.style.visibility = '';
      }
      public onClose(args: any) {
        this.treeviewInstance.collapseAll();
      }

      toggleSideBar() {
        if(this.sidebarTreeviewInstance.isOpen)
        {
          this.sidebarTreeviewInstance.hide();
          this.treeviewInstance.collapseAll();
        }
        else {
          this.sidebarTreeviewInstance.show();
          this.treeviewInstance.expandAll();
        }
      }

      closeUserBar(){
        this.userBarOpen = false;
      }

      showUserBar(){
        this.userBarOpen = !this.userBarOpen;
      }

      navigateRoute(e){
        let data:any = this.treeviewInstance.getTreeData(e.node);
        this.router.navigate([<string>data[0].url]);
      }
    }
