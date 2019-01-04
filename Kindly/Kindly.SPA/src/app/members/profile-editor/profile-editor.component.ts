// components
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { TabsetComponent } from 'ngx-bootstrap';

// services
import { AuthService } from '../../-services/auth/auth.service';
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';

@Component
({
	selector: 'app-profile-editor',
	templateUrl: './profile-editor.component.html',
	styleUrls: [ './profile-editor.component.css' ]
})

export class ProfileEditorComponent implements OnInit
{
	/**
	 * The contacts form.
	 */
	@ViewChild('contactsForm')
	public contactsForm: NgForm;

	/**
	 * The profile form.
	 */
	@ViewChild('profileForm')
	public profileForm: NgForm;

	/**
	 * The profile tabs component.
	 */
	@ViewChild('profileTabs')
	public profileTabs: TabsetComponent;

	/**
	 * The user.
	 */
	public user: User;

	/**
	 * The profile picture url.
	 */
	public profilePictureUrl: string;

	/**
	 * Creates an instance of the member edit component.
	 *
	 * @param router The router.
	 * @param activatedRoute The activated route.
	 * @param authApi The auth service.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private activatedRoute: ActivatedRoute, private router: Router,
		private authApi: AuthService, private usersApi: UsersService, private alertify: AlertifyService
	)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit ()
	{
		this.activatedRoute.data.subscribe
		(
			(data) =>
			{
				this.user = data['user'];
			}
		);

		this.activatedRoute.queryParams.subscribe
		(
			parameters =>
			{
				const tab = parameters['tab'];

				switch (tab)
				{
					case 'contacts':
						this.selectTab(0);
						break;
					case 'profile':
						this.selectTab(1);
						break;
					case 'pictures':
						this.selectTab(2);
						break;
				}
			}
		);

		this.authApi.profilePictureUrlObservable.subscribe(p => this.profilePictureUrl = p);
	}

	/**
	 * Invoked when a tab is selected
	 *
	 * @param tabID The tab selected.
	 */
	public onSelectTab(tab: string)
	{
		this.router.navigate
		(
			[],
			{
				relativeTo: this.activatedRoute,
				queryParams: { tab: tab },
				queryParamsHandling: 'merge'
			}
		);
	}

	/**
	 * Selects a tab using the given id.
	 *
	 * @param tabID The tab to select.
	 */
	public selectTab(tabID: number)
	{
		this.profileTabs.tabs[tabID].active = true;
	}

	/**
	 * Updates the user.
	 */
	public updateUser(): void
	{
		if (this.profileForm.dirty && this.profileForm.touched || this.contactsForm.valid && this.contactsForm.dirty)
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
					// emit the known as change
					this.authApi.setKnownAs(this.user.knownAs);

					this.profileForm.reset(this.user);
					this.contactsForm.reset(this.user);

					this.alertify.success('Profile updated succesfully');
				},
				(error) =>
				{
					this.alertify.error(error);
				},
			);
		}
	}

	/**
	 * Notifies the user when he tries to close the tab while there are unsaved changes.
	 *
	 * @param $event The event.
	 */
	@HostListener('window:beforeunload', ['$event'])
	public unloadNotification($event: any): void
	{
		if (this.profileForm.dirty || this.contactsForm.dirty)
		{
			$event.returnValue = true;
		}
	}
}