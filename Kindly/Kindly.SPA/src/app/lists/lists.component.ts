// components
import { Component, OnInit } from '@angular/core';

@Component
({
	selector: 'app-lists',
	templateUrl: './lists.component.html',
	styleUrls: [ './lists.component.css' ]
})

export class ListsComponent implements OnInit
{
	/**
	 * Creates an instance of the lists component.
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
}