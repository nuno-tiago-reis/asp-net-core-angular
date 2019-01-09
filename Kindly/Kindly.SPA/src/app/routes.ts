// components
import { Routes } from '@angular/router';
import { HomeComponent } from './page/index/home/home.component';
import { MatchesComponent } from './page/matches/matches.component';
import { MessagesComponent } from './page/messages/messages.component';
import { MemberListComponent } from './page/members/member-list/member-list.component';
import { MemberDetailComponent } from './page/members/member-detail/member-detail.component';
import { ProfileEditorComponent } from './page/profile/profile-editor/profile-editor.component';

// guards
import { AuthGuard } from './-guards/auth.guard';
import { PreventUnsavedChangesGuard } from './-guards/prevent-unsaved-changes.guard';

// resolvers
import { MatchesResolver } from './-resolvers/matches.resolver';
import { MessagesResolver } from './-resolvers/messages.resolver';
import { MemberListResolver } from './-resolvers/member-list.resolver';
import { MemberDetailResolver } from './-resolvers/member-detail.resolver';
import { ProfileEditorResolver } from './-resolvers/profile-editor.resolver';

export const AppRoutes: Routes =
[
	{ path: '', component: HomeComponent },
	{
		path: '',
		runGuardsAndResolvers: 'always',
		canActivate: [AuthGuard],
		children:
		[
			{ path: 'matches', component: MatchesComponent, resolve: { likes: MatchesResolver } },
			{ path: 'messages', component: MessagesComponent, resolve: { messages: MessagesResolver } },
			{ path: 'members', component: MemberListComponent, resolve: { users: MemberListResolver } },
			{ path: 'members/:id', component: MemberDetailComponent, resolve: { user: MemberDetailResolver } },
			{ path: 'profile', component: ProfileEditorComponent, resolve: { user: ProfileEditorResolver }, canDeactivate: [PreventUnsavedChangesGuard] }
		]
	},
	{ path: '**', redirectTo: '', pathMatch: 'full' }
];