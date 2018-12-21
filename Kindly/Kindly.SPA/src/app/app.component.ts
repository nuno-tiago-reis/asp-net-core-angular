import { Component } from '@angular/core';

@Component
({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: [ './app.component.css' ]
})

export class AppComponent
{
	/**
	 * The title.
	 */
	public title = 'Kindly';

	/**
	 * The current date.
	 */
	public date = new Date();

	/**
	 * Creates an instance of the app component.
	 */
	public constructor ()
	{
		// Nothing to do here.
	}
}