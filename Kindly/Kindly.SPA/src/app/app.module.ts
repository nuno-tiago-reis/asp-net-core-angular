// Modules
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
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
import { ListsComponent } from './lists/lists.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';

// Routes
import { AppRoutes } from './routes';

// Guards
import { AuthGuard } from './-guards/auth.guard';

@NgModule
({
	imports:
	[
		FormsModule,
		RouterModule.forRoot(AppRoutes),
		BrowserModule,
		HttpClientModule,
		BsDropdownModule.forRoot()
	],
	providers:
	[
		AuthGuard,
		AuthService,
		UsersService,
		AlertifyService,
		ServiceInterceptorProvider
	],
	bootstrap:
	[
		AppComponent
	],
	declarations:
	[
		AppComponent,
		NavComponent,
		RegisterComponent,
		HomeComponent,
		ListsComponent,
		MembersComponent,
		MessagesComponent
	]
})

export class AppModule { }