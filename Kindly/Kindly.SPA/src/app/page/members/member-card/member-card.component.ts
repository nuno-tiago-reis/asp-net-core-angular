// components
import { Component, Input, OnInit } from '@angular/core';

// models
import { User } from '../../../-models/user';
import { Like } from '../../../-models/like';
import { AuthService } from '../../../-services/auth/auth.service';
import { LikesService } from '../../../-services/likes/likes.service';
import { AlertifyService } from '../../../-services/alertify/alertify.service';

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
	 * The like.
	 */
	public like: Like;

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
	 * Checks whether the logged in user has liked this user.
	 */
	public hasLike(): boolean
	{
		for (const like of this.authApi.user.likeRecipients)
		{
			if (like.recipientID === this.user.id)
			{
				this.like = like;
				return true;
			}
		}

		return false;
	}

	/**
	 * Sends a like to the user.
	 */
	public sendLike(): void
	{
		this.likesApi.create(this.authApi.user.id, { recipientID: this.user.id }).subscribe
		(
			(like) =>
			{
				this.like = like;
				this.authApi.user.likeRecipients.push(like);
				this.authApi.refreshUserInStorage();

				this.alertify.success(`You have liked ${this.user.knownAs}.`);
			},
			(error) =>
			{
				this.alertify.error(error);
			}
		);
	}

	/**
	 * Removes a like from the user.
	 */
	public removeLike(): void
	{
		this.likesApi.delete(this.like.id, this.authApi.user.id).subscribe
		(
			() =>
			{
				this.authApi.user.likeRecipients.splice(this.authApi.user.likeRecipients.findIndex(l => l.id === this.like.id), 1);
				this.authApi.refreshUserInStorage();
				this.like = null;

				this.alertify.success(`You have unliked ${this.user.knownAs}.`);
			},
			(error) =>
			{
				this.alertify.error(error);
			}
		);
	}
}