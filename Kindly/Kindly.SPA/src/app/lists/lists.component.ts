// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { AuthService } from '../-services/auth/auth.service';
import { LikesService } from '../-services/likes/likes.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { User } from '../-models/user';
import { Like } from '../-models/like';
import { Pagination } from '../-models/pagination';
import { PaginatedResult } from '../-models/paginated-result';
import { LikeMode, LikeParameters } from '../-services/likes/likes.models';

@Component
({
	selector: 'app-lists',
	templateUrl: './lists.component.html',
	styleUrls: [ './lists.component.css' ]
})

export class ListsComponent implements OnInit
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
	 * The like mode for the sources (uses that like me).
	 */
	public readonly LikeModeSources: LikeMode = LikeMode.Sources;

	/**
	 * The like mode for the sources (users that i like).
	 */
	public readonly LikeModeTargets: LikeMode = LikeMode.Targets;

	/**
	 * Creates an instance of the member lists component.
	 *
	 * @param route The activated route.
	 * @param authApi The auth service.
	 * @param likesApi The likes service.
	 * @param alertify The alertify service.
	 */
	public constructor (private route: ActivatedRoute, private authApi: AuthService, private likesApi: LikesService, private alertify: AlertifyService)
	{
		this.filterParameters =
		{
			mode: LikeMode.Sources,
			includeRequestUser: false
		};
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.route.data.subscribe(data =>
		{
			this.users = [];

			for (const like of data['likes'].results)
			{
				if (this.filterParameters.mode === LikeMode.Sources)
					this.users.push(like.source);
				else
					this.users.push(like.target);
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
				if (this.filterParameters.mode === LikeMode.Sources)
					this.users = body.results.map(like => like.source);
				else
					this.users = body.results.map(like => like.target);

				this.pagination = body.pagination;
			},
			(error: any) =>
			{
				this.alertify.error('Problem retrieving members data.');
			}
		);
	}
}