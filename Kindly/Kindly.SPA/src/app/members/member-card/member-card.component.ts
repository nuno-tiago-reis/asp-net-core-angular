// components
import { Component, Input, OnInit } from '@angular/core';

// models
import { User } from '../../-models/user';
import { AuthService } from '../../-services/auth/auth.service';
import { LikesService } from '../../-services/likes/likes.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

@Component
({
	selector: 'app-member-card',
	templateUrl: './member-card.component.html',
	styleUrls: [ './member-card.component.css' ]
})

export class MemberCardComponent implements OnInit
{
	/**
	 * The user.
	 */
	@Input()
	public user: User;

	/**
	 * Creates an instance of the member card component.
	 *
	 * @param authApi The auth service.
	 * @param likesApi The likes service.
	 * @param alertify The alertify service.
	 */
	public constructor (private authApi: AuthService, private likesApi: LikesService, private alertify: AlertifyService)
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
	 * Sends a like to the user.
	 */
	public sendLike()
	{
		this.likesApi.create(this.authApi.user.id, { targetID: this.user.id }).subscribe
		(
			(data) =>
			{
				this.alertify.success(`You have liked ${this.user.knownAs}.`);
			},
			(error) =>
			{
				this.alertify.error(error);
			}
		);
	}
}