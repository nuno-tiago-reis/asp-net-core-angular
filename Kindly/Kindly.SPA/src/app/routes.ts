// components
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ProfileEditorComponent } from './members/profile-editor/profile-editor.component';

// guards
import { AuthGuard } from './-guards/auth.guard';
import { PreventUnsavedChangesGuard } from './-guards/prevent-unsaved-changes.guard';

// resolvers
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
			{ path: 'lists', component: ListsComponent },
			{ path: 'profile', component: ProfileEditorComponent, resolve: { user: ProfileEditorResolver }, canDeactivate: [PreventUnsavedChangesGuard] },
			{ path: 'members', component: MemberListComponent, resolve: { users: MemberListResolver } },
			{ path: 'members/:id', component: MemberDetailComponent, resolve: { user: MemberDetailResolver } },
			{ path: 'messages', component: MessagesComponent }
		]
	},
	{ path: '**', redirectTo: '', pathMatch: 'full' }
];