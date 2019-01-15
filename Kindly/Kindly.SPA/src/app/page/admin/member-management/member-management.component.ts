// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';

// services
import { UsersService } from '../../../-services/users/users.service';
import { AlertifyService } from '../../../-services/alertify/alertify.service';

// model
import { User } from '../../../-models/user';
import { Pagination } from '../../../-models/pagination';
import { PaginatedResult } from '../../../-models/paginated-result';
import { UpdateRolesRequest } from '../../../-services/users/users.models';

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
	 * The modal reference.
	 */
	public modalRef: BsModalRef;

	/**
	 * Creates an instance of the member management component.
	 *
	 * @param activatedRoute The activated route.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 * @param modalService The modal service.
	 */
	public constructor
	(
		private readonly activatedRoute: ActivatedRoute,
		private readonly usersApi: UsersService,
		private readonly alertify: AlertifyService,
		private readonly modalService: BsModalService
	)
	{
		// Nothing to do here.
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
		const initialState =
		{
			user: user,
			roles: this.getUserRoleObjects(user),
			closeButtonName: 'Close'
		};

		this.modalRef = this.modalService.show(RolesModalComponent, {initialState});
		this.modalRef.content.updateSelectedRoles.subscribe
		(
			(roles) =>
			{
				const rolesToUpdate: UpdateRolesRequest =
				{
					roles: [...roles.filter(role => role.checked === true).map(role => role.name)]
				};

				if (rolesToUpdate.roles.length > 0)
				{
					this.usersApi.updateRoles(user.id, rolesToUpdate).subscribe
					(
						(body: void) =>
						{
							user.roles = [];

							rolesToUpdate.roles.forEach(roleName =>
							{
								user.roles.push({ id: '', name: roleName });
							});

							this.alertify.success('Updated the users roles.');
						},
						(error: any) =>
						{
							this.alertify.error('Problem updating user roles.');
						}
					);
				}
			}
		);
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

	/**
	 * Returns a users roles in separate objects.
	 *
	 * @param user The user.
	 */
	public getUserRoleObjects(user: User): any
	{
		const roles = [];
		const availableRoles: any[] =
		[
			{ name: 'Administrator', value: 'Administrator' },
			{ name: 'Moderator', value: 'Moderator' },
			{ name: 'Member', value: 'Member' }
		];

		for (let i = 0; i < availableRoles.length; i++)
		{
			let matches = false;

			for (let j = 0; j < user.roles.length; j++)
			{
				if (availableRoles[i].name === user.roles[j].name)
				{
					matches = true;
					availableRoles[i].checked = true;
					roles.push(availableRoles[i]);
					break;
				}
			}

			if (!matches)
			{
				availableRoles[i].checked = false;
				roles.push(availableRoles[i]);
			}
		}

		return roles;
	}
}