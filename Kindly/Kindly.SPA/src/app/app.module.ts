// Modules
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap';

// Services
import { AuthService } from './-services/auth/auth.service';
import { UsersService } from './-services/users/users.service';
import { AlertifyService } from './-services/alertify/alertify.service';
import { ServiceInterceptorProvider } from './-services/http.interceptor';

// Components
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({

	imports: [
		FormsModule,
		BrowserModule,
		HttpClientModule,
		BsDropdownModule.forRoot()
	],
	providers: [
		AuthService,
		UsersService,
		AlertifyService,
		ServiceInterceptorProvider
	],
	bootstrap: [
		AppComponent
	],
	declarations: [
		AppComponent,
		NavComponent,
		HomeComponent,
		RegisterComponent
	]
})

export class AppModule { }