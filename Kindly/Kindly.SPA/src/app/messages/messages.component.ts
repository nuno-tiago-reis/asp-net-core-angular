// components
import { Component, OnInit } from '@angular/core';

@Component
({
	selector: 'app-messages',
	templateUrl: './messages.component.html',
	styleUrls: [ './messages.component.css' ]
})

export class MessagesComponent implements OnInit
{
	/**
	 * Creates an instance of the messages component.
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