import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './Modules/shared/shared.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RequestHttpInterceptor } from './Utility/Interceptor/RequestHttpInterceptor';
import { LoginComponent } from './Components/Authentication/login/login.component';
import { LottieModule } from 'ngx-lottie';
import player from 'lottie-web';
import { LogoutComponent } from './Components/Authentication/logout/logout.component';
import { BrowserAnimationsModule,NoopAnimationsModule } from '@angular/platform-browser/animations';


// Note we need a separate function as it's required
// by the AOT compiler.
export function playerFactory() {
  return player;
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    AppRoutingModule,
    SharedModule,
    LottieModule.forRoot({ player: playerFactory })
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,
      useClass:RequestHttpInterceptor,
      multi:true
    }
  ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA,NO_ERRORS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
