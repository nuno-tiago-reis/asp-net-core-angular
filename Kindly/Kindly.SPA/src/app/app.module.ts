// modules
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';

// services
import { AuthService } from './-services/auth/auth.service';
import { UsersService } from './-services/users/users.service';
import { AlertifyService } from './-services/alertify/alertify.service';
import { ServiceInterceptorProvider } from './-services/http.interceptor';

// components
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './members/card/member-card.component';
import { MemberListComponent } from './members/list/member-list.component';

// routes
import { AppRoutes } from './routes';

// guards
import { AuthGuard } from './-guards/auth.guard';

// token
export function tokenGetter()
{
	return localStorage.getItem('token');
}

@NgModule
({
	imports:
	[
		JwtModule.forRoot
		({
			config:
			{
				whitelistedDomains:
				[
					'localhost:44351',
				],
				blacklistedRoutes:
				[
					'localhost:44351/api/auth'
				],
				tokenGetter: tokenGetter
			}
		}),
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
		MessagesComponent,
		MemberCardComponent,
		MemberListComponent
	]
})

export class AppModule { }