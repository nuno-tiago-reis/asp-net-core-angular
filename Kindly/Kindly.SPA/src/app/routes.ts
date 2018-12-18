import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './-guards/auth.guard';

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
			{ path: 'members', component: MembersComponent },
			{ path: 'messages', component: MessagesComponent }
		]
	},
	{ path: '**', redirectTo: '', pathMatch: 'full' }
];