// Modules
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

// Services
import { AuthService } from './-services/auth/auth.service';
import { UsersService } from './-services/users/users.service';
import { ServiceInterceptorProvider } from './-services/service.interceptor';

// Components
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({

	imports: [
		FormsModule,
		BrowserModule,
		HttpClientModule
	],
	providers: [
		AuthService,
		UsersService,
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