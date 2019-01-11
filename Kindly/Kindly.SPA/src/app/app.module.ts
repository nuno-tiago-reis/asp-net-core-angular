// angular modules
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MATERIAL_SANITY_CHECKS } from '@angular/material';

import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// ngx modules
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipConfig } from 'ngx-bootstrap/tooltip';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
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
import { MessagesService } from './-services/messages/messages.service';
import { PicturesService } from './-services/pictures/pictures.service';
import { AlertifyService } from './-services/alertify/alertify.service';
import { ServiceInterceptorProvider } from './-services/http.interceptor';

// guards
import { AuthGuard } from './-guards/auth.guard';
import { PreventUnsavedChangesGuard } from './-guards/prevent-unsaved-changes.guard';

// resolvers
import { MatchesResolver } from './-resolvers/matches.resolver';
import { MessagesResolver } from './-resolvers/messages.resolver';
import { MemberListResolver } from './-resolvers/member-list.resolver';
import { MemberDetailResolver } from './-resolvers/member-detail.resolver';
import { ProfileEditorResolver } from './-resolvers/profile-editor.resolver';

// directives
import { HasRoleDirective } from './-directives/has-role.directive';

// components
import { AppComponent } from './app.component';
import { NavBarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './page/index/home/home.component';
import { RegisterComponent } from './page/index/register/register.component';
import { MatchesComponent } from './page/matches/matches.component';
import { MessagesComponent } from './page/messages/messages.component';
import { MemberCardComponent } from './page/members/member-card/member-card.component';
import { MemberListComponent } from './page/members/member-list/member-list.component';
import { MemberDetailComponent } from './page/members/member-detail/member-detail.component';
import { MemberMessagesComponent } from './page/members/member-messages/member-messages.component';
import { ProfileEditorComponent } from './page/profile/profile-editor/profile-editor.component';
import { PictureEditorComponent } from './page/profile/picture-editor/picture-editor.component';
import { AdminPanelComponent } from './page/admin/admin-panel/admin-panel.component';
import { MemberManagementComponent } from './page/admin/member-management/member-management.component';
import { PictureManagementComponent } from './page/admin/picture-management/picture-management.component';
import { RolesModalComponent } from './page/admin/roles-modal/roles-modal.component';

// routes
import { AppRoutes } from './routes';
import { MemberManagementResolver } from './-resolvers/member-management.resolver';

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

// tooltip config
export function getAlertConfig(): TooltipConfig
{
	return Object.assign(new TooltipConfig(),
	{
		placement: 'bottom',
		container: 'body'
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
		BrowserAnimationsModule,

		// ngx
		TabsModule.forRoot(),
		TooltipModule.forRoot(),
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
		MessagesService,
		PicturesService,
		AlertifyService,

		// resolvers
		MatchesResolver,
		MessagesResolver,
		MemberListResolver,
		MemberDetailResolver,
		MemberManagementResolver,
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
		NavBarComponent,
		FooterComponent,

		// home components
		HomeComponent,
		RegisterComponent,

		// matches components
		MatchesComponent,

		// messages components
		MessagesComponent,

		// members components
		MemberCardComponent,
		MemberListComponent,
		MemberDetailComponent,
		MemberMessagesComponent,

		// profile components
		PictureEditorComponent,
		ProfileEditorComponent,

		// admin components
		AdminPanelComponent,
		MemberManagementComponent,
		PictureManagementComponent,
		RolesModalComponent,

		// directives
		HasRoleDirective,
	]
})

export class AppModule
{
}