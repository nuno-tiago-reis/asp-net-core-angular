// angular modules
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MATERIAL_SANITY_CHECKS } from '@angular/material';
import { MatTooltipModule } from '@angular/material/tooltip';

import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// ngx modules
import { TabsModule } from 'ngx-bootstrap';
import { ButtonsModule } from 'ngx-bootstrap';
import { PaginationModule } from 'ngx-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { BsDatepickerModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';

// ng2 modules
import { FileUploadModule } from 'ng2-file-upload';

// auth0 modules
import { JwtModule } from '@auth0/angular-jwt';

// pipes
import { TimeAgoPipe } from 'time-ago-pipe';

// services
import { AuthService } from './-services/auth/auth.service';
import { UsersService } from './-services/users/users.service';
import { LikesService } from './-services/likes/likes.service';
import { PicturesService } from './-services/pictures/pictures.service';
import { AlertifyService } from './-services/alertify/alertify.service';
import { ServiceInterceptorProvider } from './-services/http.interceptor';

// guards
import { AuthGuard } from './-guards/auth.guard';
import { PreventUnsavedChangesGuard } from './-guards/prevent-unsaved-changes.guard';

// resolvers
import { ListsResolver } from './-resolvers/lists.resolver';
import { MemberListResolver } from './-resolvers/member-list.resolver';
import { MemberDetailResolver } from './-resolvers/member-detail.resolver';
import { ProfileEditorResolver } from './-resolvers/profile-editor.resolver';

// components
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { PictureEditorComponent } from './members/picture-editor/picture-editor.component';
import { ProfileEditorComponent } from './members/profile-editor/profile-editor.component';

// routes
import { AppRoutes } from './routes';

// jwt token
export function getJwtToken()
{
	return localStorage.getItem('token');
}

// datepicker config
export function getDatepickerConfig(): BsDatepickerConfig
{
	return Object.assign(new BsDatepickerConfig(),
	{
		containerClass: 'theme-orange',
		dateInputFormat: 'DD-MM-YYYY'
	});
}

@NgModule
({
	imports:
	[
		// auth0
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
				tokenGetter: getJwtToken
			}
		}),

		// angular
		RouterModule.forRoot(AppRoutes),
		BrowserModule,
		HttpClientModule,

		// forms
		FormsModule,
		ReactiveFormsModule,

		// angular material
		MatTooltipModule,
		BrowserAnimationsModule,

		// ngx
		TabsModule.forRoot(),
		ButtonsModule.forRoot(),
		PaginationModule.forRoot(),
		BsDropdownModule.forRoot(),
		BsDatepickerModule.forRoot(),
		NgxGalleryModule,

		// ng2
		FileUploadModule
	],
	providers:
	[
		// guards
		AuthGuard,
		PreventUnsavedChangesGuard,

		// services
		AuthService,
		UsersService,
		LikesService,
		PicturesService,
		AlertifyService,

		// resolvers
		ListsResolver,
		MemberListResolver,
		MemberDetailResolver,
		ProfileEditorResolver,

		// interceptors
		ServiceInterceptorProvider,

		// material
		{ provide: MATERIAL_SANITY_CHECKS, useValue: false },

		// datepicker
		{ provide: BsDatepickerConfig, useFactory: getDatepickerConfig }
	],
	bootstrap:
	[
		// components
		AppComponent
	],
	declarations:
	[
		// pipes
		TimeAgoPipe,

		// components
		AppComponent,
		NavComponent,
		HomeComponent,
		FooterComponent,
		RegisterComponent,
		ListsComponent,
		MessagesComponent,
		MemberCardComponent,
		MemberListComponent,
		MemberDetailComponent,
		PictureEditorComponent,
		ProfileEditorComponent
	]
})

export class AppModule
{
}