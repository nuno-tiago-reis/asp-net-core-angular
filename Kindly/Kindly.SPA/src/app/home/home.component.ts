import { Component, OnInit } from '@angular/core';

@Component({

	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: [ './home.component.css' ]
})

export class HomeComponent implements OnInit
{
	public registerVisible = false;

	/**
	 * Creates an instance of the home component.
	 */
	public constructor ()
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

	public showRegister (): void
	{
		this.registerVisible = true;
	}

	public hideRegister (): void
	{
		this.registerVisible = false;
	}

	public onSubmitRegistry (): void
	{
		this.hideRegister();
	}

	public onCancelRegistry (): void
	{
		this.hideRegister();
	}
}