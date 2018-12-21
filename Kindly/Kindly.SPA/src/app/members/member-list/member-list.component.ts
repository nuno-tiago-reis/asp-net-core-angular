// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';

@Component
({
	selector: 'app-member-list',
	templateUrl: './member-list.component.html',
	styleUrls: [ './member-list.component.css' ]
})

export class MemberListComponent implements OnInit
{
	/**
	 * The array of users.
	 */
	public users: User[];

	/**
	 * Creates an instance of the member list component.
	 *
	 * @param route The activated route.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor (private route: ActivatedRoute, private usersApi: UsersService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.route.data.subscribe(data => { this.users = data['users']; });
	}
}