// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';
import { Pagination } from 'src/app/-models/pagination';
import { PaginatedResult } from 'src/app/-models/paginated-result';

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
	 * The pagination options.
	 */
	public pagination: Pagination;

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
		this.route.data.subscribe(data =>
		{
			this.users = data['users'].results;
			this.pagination = data['users'].pagination;
		});
	}

	/**
	 * Invoked when the page is changed.
	 */
	public pageChanged(event: any): void
	{
		this.pagination.pageNumber = event.page;
		this.getUsers();
	}

	/**
	 * Gets the users.
	 */
	public getUsers(): void
	{
		this.usersApi.getAll(this.pagination.pageNumber, this.pagination.pageSize).subscribe
		(
			(body: PaginatedResult<User>) =>
			{
				this.users = body.results;
				this.pagination = body.pagination;
			},
			(error: any) =>
			{
				this.alertify.error('Problem retrieving members data.');
			}
		);
	}
}