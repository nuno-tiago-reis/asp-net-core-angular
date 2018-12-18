import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

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
	private baseURL = 'https://localhost:44351/api/auth/';

	/**
	 * The jwt helper service.
	 */
	private jtwHelper = new JwtHelperService();

	/**
	 * The decoded auth token.
	 */
	public decodedToken: DecodedToken;

	/**
	 * Creates an instance of the auth service.
	 *
	 * @param http The http client.
	 */
	public constructor (protected readonly http: HttpClient)
	{
		const encodedToken = localStorage.getItem('token');

		if (encodedToken)
		{
			this.decodedToken = this.jtwHelper.decodeToken(encodedToken);
		}
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
	public logInWithUserName (model: LoginWithUserNameRequest): Observable<LoginResponse>
	{
		return this.logIn(this.baseURL + 'login/user-name', model);
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
	public logInWithPhoneNumber (model: LoginWithPhoneNumberRequest): Observable<LoginResponse>
	{
		return this.logIn(this.baseURL + 'login/phone-number', model);
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
	public logInWithEmailAddress (model: LoginWithEmailAddressRequest): Observable<LoginResponse>
	{
		return this.logIn(this.baseURL + 'login/email-address', model);
	}

	/**
	 * Logs in a user using the given url and model.
	 *
	 * @param url The url.
	 * @param model The model.
	 */
	private logIn (url: string, model: any): Observable<LoginResponse>
	{
		const observable = this.http.post<LoginResponse>(url, model).pipe(map(

			(body: LoginResponse) =>
			{
				const encodedToken = body.token;

				if (encodedToken)
				{
					localStorage.setItem('token', encodedToken);
					this.decodedToken = this.jtwHelper.decodeToken(encodedToken);
				}

				return body;
			}
		));

		return observable;
	}

	/**
	 * Logs out a user.
	 */
	public logOut (): void
	{
		localStorage.removeItem('token');

		this.decodedToken = null;
	}

	/**
	 * Checks whether the user is logged in.
	 */
	public isLoggedIn (): boolean
	{
		const encodedToken = localStorage.getItem('token');

		return this.jtwHelper.isTokenExpired(encodedToken) === false;
	}

	/**
	 * Checks whether the user is logged out.
	 */
	public isLoggedOut (): boolean
	{
		return this.isLoggedIn() === false;
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

export interface DecodedToken
{
	id: string;
	userName: string;
	phoneNumber: string;
	emailAddress: string;
	nbf: number;
	exp: number;
	iat: number;
}