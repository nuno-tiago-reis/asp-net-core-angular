// components
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';

// services
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';

@Component
({
	selector: 'app-member-edit',
	templateUrl: './member-edit.component.html',
	styleUrls: [ './member-edit.component.css' ]
})

export class MemberEditComponent implements OnInit
{
	/**
	 * The date pipe.
	 */
	public datePipe = new DatePipe('en-US');

	/**
	 * The edit form.
	 */
	@ViewChild('editForm')
	public editForm: NgForm;

	/**
	 * The user.
	 */
	public user: User;

	/**
	 * Creates an instance of the member edit component.
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
	public ngOnInit ()
	{
		this.route.data.subscribe
		(
			(data) =>
			{
				this.user = data['user'];
			}
		);
	}

	/**
	 * Updates the user.
	 */
	public updateUser(): void
	{
		const updateRequest =
		{
			userName: this.user.userName,
			phoneNumber: this.user.phoneNumber,
			emailAddress: this.user.emailAddress,
			knownAs: this.user.knownAs,
			gender: this.user.gender,
			age: this.user.age,
			city: this.user.city,
			country: this.user.country,
			introduction: this.user.introduction,
			lookingFor: this.user.lookingFor,
			interests: this.user.interests
		};

		this.usersApi.update(this.user.id, updateRequest).subscribe
		(
			(next) =>
			{
				this.editForm.reset(this.user);
				this.alertify.success('Profile updated succesfully');
			},
			(error) =>
			{
				this.alertify.error('There was an error updating the profile.');
			},
		);
	}

	/**
	 * Notifies the user when he tries to close the tab while there are unsaved changes.
	 *
	 * @param $event The event.
	 */
	@HostListener('window:beforeunload', ['$event'])
	public unloadNotification($event: any): void
	{
		if (this.editForm.dirty)
		{
			$event.returnValue = true;
		}
	}
}