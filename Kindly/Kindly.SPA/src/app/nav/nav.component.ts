import { Component, OnInit } from '@angular/core';
import { AuthService, LoginWithUserNameRequest } from '../-services/auth/auth.service';

@Component({

	selector: 'app-nav',
	templateUrl: './nav.component.html',
	styleUrls: [ './nav.component.css' ]
})

export class NavComponent implements OnInit
{
	model: LoginWithUserNameRequest = { userName: '', password: '' };

	/**
	 * Creates an instance of the nav component.
	 *
	 * @param auth The auth service.
	 */
	public constructor (private auth: AuthService)
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
	 * Called when the log in button is clicked on.
	 */
	public logIn (): void
	{
		this.auth.loginWithUserName(this.model);
	}

	/**
	 * Called when the log out button is clicked on.
	 */
	public logOut (): void
	{
		localStorage.removeItem('token');
	}

	/**
	 * Checks whether the user is logged in.
	 */
	public loggedIn(): boolean
	{
		const token = localStorage.getItem('token');

		return !!token;
	}

	/**
	 * Checks whether the user is logged out.
	 */
	public loggedOut (): boolean
	{
		return this.loggedIn() === false;
	}
}