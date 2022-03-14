
import { AuthenticationGuard } from './Guard/AuthenticationGuard/authentication.guard';
import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './Components/Authentication/login/login.component';
import { LogoutComponent } from './Components/Authentication/logout/logout.component';

const routes: Routes = [
  {
    path:'',
    component:LoginComponent,
    canActivate:[AuthenticationGuard]
  },
  {
    path:'logout',
    component:LogoutComponent
  },
  {
    path:'dashboard',
    canActivate:[AuthenticationGuard],
    loadChildren:()=>import('./Modules/dashboard/dashboard.module').then(e=>e.DashboardModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
