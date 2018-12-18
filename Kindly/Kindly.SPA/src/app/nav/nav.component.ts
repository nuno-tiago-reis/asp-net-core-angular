import { Component, OnInit } from '@angular/core';
import { AuthService, LoginWithUserNameRequest } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

@Component({

	selector: 'app-nav',
	templateUrl: './nav.component.html',
	styleUrls: [ './nav.component.css' ]
})

export class NavComponent implements OnInit
{
	/**
	 * The login model.
	 */
	model: LoginWithUserNameRequest = { userName: '', password: '' };

	/**
	 * The current users name.
	 */
	userName: string;
	/**
	 * The current users phone number.
	 */
	phoneNumber: string;
	/**
	 * The current users email address.
	 */
	emailAddress: string;

	/**
	 * Creates an instance of the nav component.
	 *
	 * @param auth The auth service.
	 */
	public constructor (private auth: AuthService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		if (this.isLoggedIn() === true)
		{
			this.getUserInformation();
		}
	}

	/**
	 * Called when the log in button is clicked on.
	 */
	public logIn (): void
	{
		this.auth.logInWithUserName(this.model).subscribe(

			(next: any) =>
			{
				this.getUserInformation();

				this.alertify.success('Logged in successfully.');
			},
			(error: any) =>
			{
				this.alertify.error(error);
			}
		);
	}

	/**
	 * Called when the log out button is clicked on.
	 */
	public logOut (): void
	{
		this.auth.logOut();

		this.alertify.success('Logged out successfully.');
	}

	/**
	 * Checks whether the user is logged in.
	 */
	public isLoggedIn(): boolean
	{
		return this.auth.isLoggedIn();
	}

	/**
	 * Checks whether the user is logged out.
	 */
	public isLoggedOut (): boolean
	{
		return this.auth.isLoggedOut();
	}

	/**
	 * Gets the users information from the auth service.
	 */
	private getUserInformation(): void
	{
		const decodedToken = this.auth.decodedToken;

		this.userName = decodedToken.userName;
		this.phoneNumber = decodedToken.phoneNumber;
		this.emailAddress = decodedToken.emailAddress;
	}
}