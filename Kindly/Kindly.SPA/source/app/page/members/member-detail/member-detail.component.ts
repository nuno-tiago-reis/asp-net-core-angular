// components
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { TabsetComponent } from 'ngx-bootstrap';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from 'ngx-gallery';

// services
import { AuthService } from '../../../-services/auth/auth.service';
import { LikesService } from '../../../-services/likes/likes.service';
import { AlertifyService } from '../../../-services/alertify/alertify.service';

// models
import { User } from '../../../-models/user';
import { Like } from '../../../-models/like';

@Component
({
	selector: 'app-member-detail',
	templateUrl: './member-detail.component.html',
	styleUrls: [ './member-detail.component.css' ]
})

export class MemberDetailComponent implements OnInit
{
	/**
	 * The date pipe.
	 */
	public datePipe = new DatePipe('en-US');

	/**
	 * The user.
	 */
	public user: User;

	/**
	 * The like.
	 */
	public like: Like;

	/**
	 * The gallery images.
	 */
	public galleryImages: NgxGalleryImage[];

	/**
	 * The gallery options.
	 */
	public galleryOptions: NgxGalleryOptions[];

	/**
	 * The member tabs component.
	 */
	@ViewChild('memberTabs')
	public memberTabs: TabsetComponent;

	/**
	 * Creates an instance of the member detail component.
	 *
	 * @param activatedRoute The activated route.
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param likesApi The likes service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private readonly activatedRoute: ActivatedRoute,
		private readonly router: Router,
		private readonly authApi: AuthService,
		private readonly likesApi: LikesService,
		private readonly alertify: AlertifyService
	)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.activatedRoute.data.subscribe
		(
			data =>
			{
				this.user = data['user'];
			}
		);

		this.galleryImages = [];

		for (let i = 0; i < this.user.pictures.length; i++)
		{
			const picture = this.user.pictures[i];

			this.galleryImages.push
			({
				small: picture.url,
				medium: picture.url,
				big: picture.url,
				description: picture.description
			});
		}

		this.galleryOptions =
		[
			{
				width: '500px',
				height: '500px',
				imagePercent: 100,
				thumbnailsColumns: 4,
				imageAnimation: NgxGalleryAnimation.Slide,
				preview: false
			}
		];

		Promise.resolve(null).then(() =>
		{
			this.activatedRoute.queryParams.subscribe
			(
				parameters =>
				{
					const tab = parameters['tab'];

					switch (tab)
					{
						case 'about':
							this.selectTab(0);
							break;
						case 'pictures':
							this.selectTab(1);
							break;
						case 'messages':
							this.selectTab(2);
							break;
					}
				}
			);
		});
	}

	/**
	 * Invoked when a tab is selected
	 *
	 * @param tabID The tab selected.
	 */
	public onSelectTab(tab: string)
	{
		this.router.navigate
		(
			[],
			{
				relativeTo: this.activatedRoute,
				queryParams: { tab: tab },
				queryParamsHandling: 'merge'
			}
		);
	}

	/**
	 * Selects a tab using the given id.
	 *
	 * @param tabID The tab to select.
	 */
	public selectTab(tabID: number)
	{
		this.memberTabs.tabs[tabID].active = true;
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
				this.alertify.error(`An error occured while liking ${this.user.knownAs}.`);
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
				this.alertify.error(`An error occured while unliking ${this.user.knownAs}.`);
			}
		);
	}
}