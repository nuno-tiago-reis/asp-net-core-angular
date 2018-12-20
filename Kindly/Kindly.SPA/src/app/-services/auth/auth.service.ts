// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { User } from '../../-models/user';
import { Token, DecodedToken } from '../../-models/token';
import
{
	RegisterRequest ,
	AddPasswordRequest,
	ChangePasswordRequest,
	LoginWithUserNameRequest,
	LoginWithPhoneNumberRequest,
	LoginWithEmailAddressRequest
}
from './auth.models';

// environment
import { environment } from '../../../environments/environment.development';

@Injectable
({
	providedIn: 'root'
})

export class AuthService
{
	/**
	 * The auth API base url.
	 */
	private baseURL = environment.apiUrl + 'auth/';

	/**
	 * The jwt helper service.
	 */
	private jtwHelper = new JwtHelperService();

	/**
	 * The encoded auth token.
	 */
	public encodedToken: string = null;
	/**
	 * The decoded auth token.
	 */
	public decodedToken: DecodedToken = null;

	/**
	 * Creates an instance of the auth service.
	 *
	 * @param http The http client.
	 */
	public constructor (protected readonly http: HttpClient)
	{
		const storedToken = localStorage.getItem('token');

		if (storedToken)
		{
			this.encodedToken = storedToken;
			this.decodedToken = this.jtwHelper.decodeToken(this.encodedToken);
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
	public logInWithUserName (model: LoginWithUserNameRequest): Observable<Token>
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
	public logInWithPhoneNumber (model: LoginWithPhoneNumberRequest): Observable<Token>
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
	public logInWithEmailAddress (model: LoginWithEmailAddressRequest): Observable<Token>
	{
		return this.logIn(this.baseURL + 'login/email-address', model);
	}

	/**
	 * Logs in a user using the given url and model.
	 *
	 * @param url The url.
	 * @param model The model.
	 */
	private logIn (url: string, model: any): Observable<Token>
	{
		const observable = this.http.post<Token>(url, model).pipe(map
		(
			(body: Token) =>
			{
				if (body.token)
				{
					this.encodedToken = body.token;
					this.decodedToken = this.jtwHelper.decodeToken(this.encodedToken);

					localStorage.setItem('token', this.encodedToken);
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
		this.encodedToken = null;
		this.decodedToken = null;

		localStorage.removeItem('token');
	}

	/**
	 * Checks whether the user is logged in.
	 */
	public isLoggedIn (): boolean
	{
		return this.jtwHelper.isTokenExpired(this.encodedToken) === false;
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
	public register (model: RegisterRequest): Observable<User>
	{
		const observable = this.http.post<User>(this.baseURL + 'register', model);

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