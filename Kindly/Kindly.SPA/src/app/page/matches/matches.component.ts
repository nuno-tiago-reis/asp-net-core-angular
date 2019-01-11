// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { AuthService } from '../../-services/auth/auth.service';
import { LikesService } from '../../-services/likes/likes.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';
import { Like } from '../../-models/like';
import { Pagination } from '../../-models/pagination';
import { PaginatedResult } from '../../-models/paginated-result';
import { LikeMode, LikeParameters } from '../../-services/likes/likes.models';

@Component
({
	selector: 'app-matches',
	templateUrl: './matches.component.html',
	styleUrls: [ './matches.component.css' ]
})

export class MatchesComponent implements OnInit
{
	/**
	 * The array of users.
	 */
	public users: User[];

	/**
	 * The pagination options.
	 */
	public pagination: Pagination;

	/**
	 * The filter parameters.
	 */
	public filterParameters: LikeParameters;

	/**
	 * The like mode for the senders (uses that like me).
	 */
	public readonly LikeModeSenders: LikeMode = LikeMode.Senders;

	/**
	 * The like mode for the recipients (users that i like).
	 */
	public readonly LikeModeRecipients: LikeMode = LikeMode.Recipients;

	/**
	 * Creates an instance of the matches component.
	 *
	 * @param activatedRoute The activated route.
	 * @param authApi The auth service.
	 * @param likesApi The likes service.
	 * @param alertify The alertify service.
	 */
	public constructor (private activatedRoute: ActivatedRoute, private authApi: AuthService, private likesApi: LikesService, private alertify: AlertifyService)
	{
		this.filterParameters =
		{
			mode: LikeMode.Senders,
			includeRequestUser: false
		};
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.activatedRoute.data.subscribe(data =>
		{
			this.users = [];

			for (const like of data['likes'].results as Like[])
			{
				if (this.filterParameters.mode === LikeMode.Senders)
					this.users.push(like.sender);
				else
					this.users.push(like.recipient);
			}

			this.pagination = data['likes'].pagination;
		});
	}

	/**
	 * Invoked when the page is changed.
	 */
	public changePage(event: any): void
	{
		this.pagination.pageNumber = event.page;

		this.getUsers();
	}

	/**
	 * Gets the users.
	 */
	public getUsers(): void
	{
		this.likesApi.getAll(this.authApi.user.id, this.pagination.pageNumber, this.pagination.pageSize, this.filterParameters).subscribe
		(
			(body: PaginatedResult<Like>) =>
			{
				if (this.filterParameters.mode === LikeMode.Senders)
					this.users = body.results.map(like => like.sender);
				else
					this.users = body.results.map(like => like.recipient);

				this.pagination = body.pagination;
			},
			(error: any) =>
			{
				this.alertify.error('Problem retrieving lists data.');
			}
		);
	}
}