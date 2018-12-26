// components
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

// services
import { AuthService } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { RegisterRequest } from '../-services/auth/auth.models';
import { NullAstVisitor } from '@angular/compiler';

@Component
({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: [ './register.component.css' ]
})

export class RegisterComponent implements OnInit
{
	@Output()
	public submitRegistry = new EventEmitter;

	@Output()
	public cancelRegistry = new EventEmitter;

	/**
	 * The registration model.
	 */
	public model: RegisterRequest =
	{
		userName: '',
		phoneNumber: '',
		emailAddress: '',
		knownAs: '',
		gender: '',
		birthDate: null,
		city: '',
		country: '',
		introduction: '',
		lookingFor: '',
		interests: '',
		password: '',
	};

	/**
	 * Creates an instance of the register component.
	 *
	 * @param authApi The auth service.
	 */
	public constructor (private authApi: AuthService, private alertify: AlertifyService)
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
	 * Invoked when the forms submit button is clicked.
	 */
	public submit (): void
	{
		this.authApi.register(this.model).subscribe(

			(next: any) =>
			{
				this.alertify.success('Registered successfully.');

				this.submitRegistry.emit(this.model);
			},
			(error: any) =>
			{
				this.alertify.error(error);
			}
		);
	}

	/**
	 * Invoked when the forms cancel button is clicked.
	 */
	public cancel (): void
	{
		this.cancelRegistry.emit(this.model);
	}
}