// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { AuthService } from '../../-services/auth/auth.service';
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User, Gender } from '../../-models/user';
import { Pagination } from '../../-models/pagination';
import { PaginatedResult } from '../../-models/paginated-result';
import { UserParameters } from '../../-services/users/users.models';

@Component
({
	selector: 'app-member-list',
	templateUrl: './member-list.component.html',
	styleUrls: [ './member-list.component.css' ]
})

export class MemberListComponent implements OnInit
{
	/**
	 * The logged user.
	 */
	public user: User;

	/**
	 * The array of users.
	 */
	public users: User[];

	/**
	 * The pagination options.
	 */
	public pagination: Pagination;

	/**
	 * The filter parameters.
	 */
	public filterParameters: UserParameters;

	/**
	 * The gender labels.
	 */
	public readonly genderLabels =
	[
		{ value: Gender.male, display: 'Males' },
		{ value: Gender.female, display: 'Females' },
		{ value: Gender.undefined, display: 'Everything' }
	];

	/**
	 * Creates an instance of the member list component.
	 *
	 * @param route The activated route.
	 * @param authApi The auth service.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor (private route: ActivatedRoute, private authApi: AuthService, private usersApi: UsersService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.user = this.authApi.user;

		this.route.data.subscribe(data =>
		{
			this.users = data['users'].results;
			this.pagination = data['users'].pagination;
		});

		this.resetFilter(false);
	}

	/**
	 * Invoked when the filtering parameters are reset.
	 *
	 * @param resetUsers Whether the users should be reset aswell.
	 */
	public resetFilter(resetUsers: boolean)
	{
		this.filterParameters =
		{
			gender: 'undefined',
			minimumAge: 18,
			maximumAge: 100,
			orderBy: 'lastActiveAt'
		};

		switch (this.user.gender)
		{
			case 'male':
				this.filterParameters.gender = 'female';
				break;
			case 'female':
				this.filterParameters.gender = 'male';
				break;
		}

		if(resetUsers)
			this.getUsers();
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
		this.usersApi.getAll(this.pagination.pageNumber, this.pagination.pageSize, this.filterParameters).subscribe
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