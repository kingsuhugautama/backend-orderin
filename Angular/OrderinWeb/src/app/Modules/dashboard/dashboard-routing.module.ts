import { DashboardModule } from './dashboard.module';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from 'src/app/Components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path:'',component:DashboardComponent,
    children:[
      {
        path:'grafik',loadChildren:()=>import('../grafik/grafik.module').then(e=>e.GrafikModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
