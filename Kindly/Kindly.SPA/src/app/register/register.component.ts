import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService, RegisterRequest } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

@Component({

	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: [ './register.component.css' ]
})

export class RegisterComponent implements OnInit
{
	@Output() submitRegistry = new EventEmitter;
	@Output() cancelRegistry = new EventEmitter;

	/**
	 * The registration model.
	 */
	model: RegisterRequest = { userName: '', phoneNumber: '', emailAddress: '', password: ''  };

	/**
	 * Creates an instance of the register component.
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
		// Nothing to do here.
	}

	public submit (): void
	{
		this.auth.register(this.model).subscribe(

			(next: any) =>
			{
				this.alertify.success('Registered successfully.');
			},
			(error: any) =>
			{
				this.alertify.error(error);
			}
		);

		this.submitRegistry.emit(this.model);
	}

	public cancel (): void
	{
		this.cancelRegistry.emit(this.model);
	}
}