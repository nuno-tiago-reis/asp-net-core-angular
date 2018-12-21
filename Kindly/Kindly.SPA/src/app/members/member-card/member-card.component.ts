// components
import { Component, Input, OnInit } from '@angular/core';

// models
import { User } from '../../-models/user';

@Component
({
	selector: 'app-member-card',
	templateUrl: './member-card.component.html',
	styleUrls: [ './member-card.component.css' ]
})

export class MemberCardComponent implements OnInit
{
	@Input()
	public user: User;

	/**
	 * Creates an instance of the member card component.
	 *
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
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