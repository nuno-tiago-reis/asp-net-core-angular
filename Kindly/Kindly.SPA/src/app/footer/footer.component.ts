// components
import { Component, OnInit } from '@angular/core';

@Component
({
	selector: 'app-footer',
	templateUrl: './footer.component.html',
	styleUrls: [ './footer.component.css' ]
})

export class FooterComponent implements OnInit
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
	 * Creates an instance of the footer component.
	 */
	public constructor ()
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit ()
	{
		// Nothing to do here.
	}
}