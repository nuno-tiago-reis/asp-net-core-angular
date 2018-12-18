import { Component, OnInit } from '@angular/core';

@Component
({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: [ './home.component.css' ]
})

export class HomeComponent implements OnInit
{
	/**
	 * Whether the register page is visible.
	 */
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

	/**
	 * Shows the register page.
	 */
	public showRegister (): void
	{
		this.registerVisible = true;
	}

	/**
	 * Hides the register page.
	 */
	public hideRegister (): void
	{
		this.registerVisible = false;
	}

	/**
	 * Invoked when the register form is submitted.
	 */
	public onSubmitRegistry (): void
	{
		this.hideRegister();
	}

	/**
	 * Invoked when the register form is cancelled.
	 */
	public onCancelRegistry (): void
	{
		this.hideRegister();
	}
}