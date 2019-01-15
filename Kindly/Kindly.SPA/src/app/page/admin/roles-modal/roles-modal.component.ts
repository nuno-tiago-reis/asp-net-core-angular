// components
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

// models
import { User } from 'src/app/-models/user';

@Component
({
	selector: 'app-roles-modal',
	templateUrl: './roles-modal.component.html',
	styleUrls: ['./roles-modal.component.css']
})

export class RolesModalComponent implements OnInit
{
	/**
	 * The update selected roles event.
	 */
	@Output()
	public updateSelectedRoles = new EventEmitter();

	/**
	 * The user.
	 */
	public user: User;

	/**
	 * The role objects.
	 */
	public roles: any[];

	/**
	 * The close button name.
	 */
	public closeButtonName: string;

	/**
	 * Creates an instance of the roles modal component.
	 *
	 * @param modalRef The modal reference.
	 */
	public constructor (public modalRef: BsModalRef)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		// Nothing to do here.
	}

	/**
	 * Updates the users roles.
	 */
	public updateRoles(): void
	{
		this.updateSelectedRoles.emit(this.roles);
		this.modalRef.hide();
	}
}