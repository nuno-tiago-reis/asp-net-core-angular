// components
import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

// services
import { AuthService } from '../../../-services/auth/auth.service';
import { AlertifyService } from '../../../-services/alertify/alertify.service';

// models
import { User } from '../../../-models/user';
import { RegisterRequest } from '../../../-services/auth/auth.models';

@Component
({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: [ './register.component.css' ]
})

export class RegisterComponent implements OnInit
{
	/**
	 * The register request user.
	 */
	public registerRequest: RegisterRequest =
	{
		userName: '',
		phoneNumber: '',
		email: '',
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
	 * The register form.
	 */
	@ViewChild('registerForm')
	public registerForm: FormGroup;

	/**
	 * The cancel registry event emitter.
	 */
	@Output()
	public cancelRegistry = new EventEmitter;

	/**
	 * The submit registry event emitter.
	 */
	@Output()
	public submitRegistry = new EventEmitter;

	/**
	 * Creates an instance of the register component.
	 *
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param alertify The auth service.
	 * @param formBuilder The form builder.
	 */
	public constructor
	(
		private readonly router: Router,
		private readonly authApi: AuthService,
		private readonly alertify: AlertifyService,
		private readonly formBuilder: FormBuilder
	)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.registerForm = this.formBuilder.group
		(
			{
				knownAs:
					[ '', 		[ Validators.required, Validators.maxLength(25) ] ],
				userName:
					[ '', 		[ Validators.required, Validators.maxLength(25), Validators.minLength(10) ] ],
				phoneNumber:
					[ '', 		[ Validators.required, Validators.maxLength(15), Validators.pattern ] ],
				email:
					[ '', 		[ Validators.required, Validators.maxLength(254), Validators.email ] ],
				gender:
					[ 'male',	[] ],
				birthDate:
					[ null, 	[ Validators.required, this.birthDateValidator ] ],
				city:
					[ '', 		[ Validators.required, Validators.maxLength(50) ] ],
				country:
					[ '', 		[ Validators.required, Validators.maxLength(50) ] ],
				password:
					[ '', 		[ Validators.required, Validators.minLength(10) ] ],
				confirmPassword:
					[ '', 		[ Validators.required, Validators.minLength(10) ] ]
			},
			{
				validator: this.passwordValidator
			}
		);
	}

	/**
	 * Invoked when the forms submit button is clicked.
	 */
	public submit (): void
	{
		if (this.registerForm.valid)
		{
			this.registerRequest = Object.assign({}, this.registerForm.value);

			this.authApi.register(this.registerRequest).subscribe
			(
				(user: User) =>
				{
					this.alertify.success('You have registered successfully.');
					this.submitRegistry.emit(this.registerRequest);

					return user;
				},
				(error: any) =>
				{
					this.alertify.error('An error occured while registrating.');
				},
				() =>
				{
					const loginRequest =
					{
						userName: this.registerRequest.userName,
						password: this.registerRequest.password
					};

					this.authApi.logInWithUserName(loginRequest).subscribe
					(
						() =>
						{
							this.router.navigate(['/members']);
						}
					);
				}
			);
		}
	}

	/**
	 * Invoked when the forms cancel button is clicked.
	 */
	public cancel (): void
	{
		this.cancelRegistry.emit(this.registerRequest);
	}

	/**
	 * Validates the password fields.
	 *
	 * @param formGroup The form group.
	 */
	public passwordValidator(formGroup: FormGroup): any
	{
		return formGroup.get('password').value === formGroup.get('confirmPassword').value ? null : { 'mismatch' : true };
	}

	/**
	 * Validates the birth date field.
	 *
	 * @param birthDate The birthDate form control.
	 */
	public birthDateValidator(birthDate: FormControl): any
	{
		const difference = Date.now() - Date.parse(birthDate.value);
		const differenceDate = new Date(difference);

		return Math.abs(differenceDate.getUTCFullYear() - 1970) >= 18 ? null : { 'age-requirement' : true };
	}
}