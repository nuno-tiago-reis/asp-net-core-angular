// components
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './members/list/member-list.component';
import { MemberDetailComponent } from './members/detail/member-detail.component';

// guards
import { AuthGuard } from './-guards/auth.guard';

// resolvers
import { MemberListResolver } from './-resolvers/member-list.resolver';
import { MemberDetailResolver } from './-resolvers/member-detail.resolver';

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
			{ path: 'members', component: MemberListComponent, resolve: { users: MemberListResolver } },
			{ path: 'members/:id', component: MemberDetailComponent, resolve: { user: MemberDetailResolver } },
			{ path: 'messages', component: MessagesComponent }
		]
	},
	{ path: '**', redirectTo: '', pathMatch: 'full' }
];