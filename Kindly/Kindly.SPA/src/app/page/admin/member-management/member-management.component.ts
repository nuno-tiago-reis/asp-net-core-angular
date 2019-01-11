// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { UsersService } from 'src/app/-services/users/users.service';
import { AlertifyService } from 'src/app/-services/alertify/alertify.service';

// model
import { User } from 'src/app/-models/user';
import { Pagination } from 'src/app/-models/pagination';
import { PaginatedResult } from 'src/app/-models/paginated-result';

@Component
({
	selector: 'app-member-management',
	templateUrl: './member-management.component.html',
	styleUrls: ['./member-management.component.css']
})

export class MemberManagementComponent implements OnInit
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
	 * The JSON interface.
	 */
	public JSON: JSON;

	/**
	 * Creates an instance of the member management component.
	 *
	 * @param activatedRoute The activated route.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor (private activatedRoute: ActivatedRoute, private usersApi: UsersService, private alertify: AlertifyService)
	{
		this.JSON = JSON;
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.activatedRoute.data.subscribe(data =>
		{
			this.users = data['users'].results;
			this.pagination = data['users'].pagination;
		});
	}

	/**
	 * Invoked when the page is changed.
	 */
	public changePage(event: any): void
	{
		this.pagination.pageNumber = event.page;

		this.getUsers();
	}

	/**
	 * Gets the users.
	 */
	public getUsers(): void
	{
		this.usersApi.getAllWithRoles(this.pagination.pageNumber, this.pagination.pageSize).subscribe
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

	/**
	 * Shows the edit user roles modal.
	 *
	 * @param user The user.
	 */
	public showEditRolesModal(user: User): void
	{
		// Nothing to do here.
	}

	/**
	 * Returns a users roles separated by commas.
	 *
	 * @param user The user.
	 */
	public getUserRoles(user: User): string
	{
		let roles = '';

		user.roles.forEach
		(
			(role) =>
			{
				roles += `${role.name}, `;
			}
		);

		return roles.slice(0, roles.length - 2);
	}
}