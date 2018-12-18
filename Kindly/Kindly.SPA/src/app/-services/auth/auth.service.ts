import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({

	providedIn: 'root'
})

export class AuthService
{
	/**
	 * The auth API base url.
	 */
	protected baseURL = 'https://localhost:44351/api/auth/';

	/**
	 * Creates an instance of the auth service.
	 *
	 * @param http The http client.
	 */
	public constructor (protected readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Logs in using the username as an identifier.
	 * The username and password are mandatory (sample model below).
	 *
	 * {
	 * 	"userName" : "<placeholder>",
	 * 	"password" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public loginWithUserName (model: LoginWithUserNameRequest): Observable<LoginResponse>
	{
		return this.login(this.baseURL + 'login/user-name', model);
	}

	/**
	 * Logs in using the phone number as an identifier.
	 * The phone number and password are mandatory (sample model below).
	 *
	 * {
	 * 	"phoneNumber" : "<placeholder>",
	 * 	"password" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public loginWithPhoneNumber (model: LoginWithPhoneNumberRequest): Observable<LoginResponse>
	{
		return this.login(this.baseURL + 'login/phone-number', model);
	}

	/**
	 * Logs in using the email address as an identifier.
	 * The email address and password are mandatory (sample model below).
	 *
	 * {
	 * 	"emailAddress" : "<placeholder>",
	 * 	"password" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public loginWithEmailAddress (model: LoginWithEmailAddressRequest): Observable<LoginResponse>
	{
		return this.login(this.baseURL + 'login/email-address', model);
	}

	/**
	 * Logs in using the given url and model.
	 *
	 * @param url The url.
	 * @param model The model.
	 */
	private login (url: string, model: any): Observable<LoginResponse>
	{
		const observable = this.http.post<LoginResponse>(url, model);
		observable.pipe(map(

			(body: LoginResponse) =>
			{
				const token = body.token;

				if (token)
				{
					localStorage.setItem('token', token);
				}
			}
		));

		return observable;
	}

	/**
	 * Registers a user.
	 * The username, phone number, email address and password are mandatory (sample model below).
	 *
	 * {
	 * 	"userName" : "<placeholder>",
	 * 	"phoneNumber" : "<placeholder>",
	 * 	"emailAddress" : "<placeholder>",
	 * 	"password" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public register (model: RegisterRequest): Observable<RegisterResponse>
	{
		const observable = this.http.post<RegisterResponse>(this.baseURL + 'register', model);

		return observable;
	}

	/**
	 * Adds a password to the user.
	 * The user id and password are mandatory (sample model below).
	 *
	 * {
	 * 	"id" : "<placeholder>",
	 * 	"password" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public addPassword (model: AddPasswordRequest): Observable<void>
	{
		const observable = this.http.post<void>(this.baseURL + 'password', model);

		return observable;
	}

	/**
	 * Changes a users password.
	 * The user id, old password and new password are mandatory (sample model below).
	 *
	 * {
	 * 	"id" : "<placeholder>",
	 * 	"oldPassword" : "<placeholder>",
	 * 	"newPassword" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public changePassword (model: ChangePasswordRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL + 'password', model);

		return observable;
	}
}

export interface LoginWithUserNameRequest
{
	userName: string;
	password: string;
}

export interface LoginWithPhoneNumberRequest
{
	phoneNumber: string;
	password: string;
}

export interface LoginWithEmailAddressRequest
{
	emailAddress: string;
	password: string;
}

export interface LoginResponse
{
	token: string;
}

export interface RegisterRequest
{
	userName: string;
	phoneNumber: string;
	emailAddress: string;
	password: string;
}

export interface RegisterResponse
{
	id: string;
	userName: string;
	phoneNumber: string;
	emailAddress: string;
}

export interface AddPasswordRequest
{
	id: string;
	password: string;
}

export interface ChangePasswordRequest
{
	id: string;
	oldPassword: string;
	newPassword: string;
}