// modules
import { DEFAULT_PICTURE, DEFAULT_KNOWN_AS } from '../../app.constants';

// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { User } from '../../-models/user';
import { LoginToken, DecodedToken } from '../../-models/token';
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
import { environment } from '../../../environments/environment';

@Injectable
({
	providedIn: 'root'
})

export class AuthService
{
	/**
	 * The auth API base url.
	 */
	private readonly baseURL = environment.apiUrl + 'auth/';

	/**
	 * The jwt helper service.
	 */
	private readonly jtwHelper = new JwtHelperService();

	/**
	 * The profile picture url behaviour subject.
	 */
	public readonly profilePictureUrl = new BehaviorSubject<string>(DEFAULT_PICTURE);

	/**
	 * The profile picture url observable.
	 */
	public readonly profilePictureUrlObservable = this.profilePictureUrl.asObservable();

	/**
	 * The known as behaviour subject.
	 */
	public readonly knownAs = new BehaviorSubject<string>(DEFAULT_KNOWN_AS);

	/**
	 * The known as observable.
	 */
	public readonly knownAsObservable = this.knownAs.asObservable();

	/**
	 * The logged in user.
	 */
	public user: User = null;
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
	public constructor (private readonly http: HttpClient)
	{
		const storedUser = localStorage.getItem('user');
		const storedToken = localStorage.getItem('token');

		if (storedUser && storedToken)
		{
			this.user = JSON.parse(storedUser);
			this.encodedToken = storedToken;
			this.decodedToken = this.jtwHelper.decodeToken(this.encodedToken);

			this.knownAs.next(this.user.knownAs);
			this.profilePictureUrl.next(this.user.profilePictureUrl);
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
	public logInWithUserName (model: LoginWithUserNameRequest): Observable<LoginToken>
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
	public logInWithPhoneNumber (model: LoginWithPhoneNumberRequest): Observable<LoginToken>
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
	public logInWithEmailAddress (model: LoginWithEmailAddressRequest): Observable<LoginToken>
	{
		return this.logIn(this.baseURL + 'login/email-address', model);
	}

	/**
	 * Logs in a user using the given url and model.
	 *
	 * @param url The url.
	 * @param model The model.
	 */
	private logIn (url: string, model: any): Observable<LoginToken>
	{
		const observable = this.http.post<LoginToken>(url, model).pipe(map
		(
			(body: LoginToken) =>
			{
				if (body.user && body.token)
				{
					if (body.user.profilePictureUrl === '' || body.user.profilePictureUrl === null)
					{
						body.user.profilePictureUrl = DEFAULT_PICTURE;
					}

					this.user = body.user;
					this.encodedToken = body.token;
					this.decodedToken = this.jtwHelper.decodeToken(this.encodedToken);

					this.knownAs.next(this.user.knownAs);
					this.profilePictureUrl.next(this.user.profilePictureUrl);

					localStorage.setItem('user', JSON.stringify(this.user));
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
		this.user = null;
		this.encodedToken = null;
		this.decodedToken = null;

		this.knownAs.next(null);
		this.profilePictureUrl.next(null);

		localStorage.removeItem('user');
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

	/**
	 * Sets the profile picture url.
	 *
	 * @param profilePictureUrl The profile picture url.
	 */
	public setProfilePictureUrl (profilePictureUrl: string)
	{
		this.user.profilePictureUrl = profilePictureUrl;
		this.profilePictureUrl.next(profilePictureUrl);

		localStorage.setItem('user', JSON.stringify(this.user));
	}

	/**
	 * Sets the known as.
	 *
	 * @param knownAs The known as.
	 */
	public setKnownAs (knownAs: string)
	{
		this.user.knownAs = knownAs;
		this.knownAs.next(knownAs);

		localStorage.setItem('user', JSON.stringify(this.user));
	}
}