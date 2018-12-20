// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions, NgxGalleryAnimation } from 'ngx-gallery';

// services
import { UsersService } from '../../-services/users/users.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';

// models
import { User } from '../../-models/user';

@Component
({
	selector: 'app-member-detail',
	templateUrl: './member-detail.component.html',
	styleUrls: [ './member-detail.component.css' ]
})

export class MemberDetailComponent implements OnInit
{
	/**
	 * The user.
	 */
	public user: User;

	/**
	 * The gallery images.
	 */
	public galleryImages: NgxGalleryImage[];

	/**
	 * The gallery options.
	 */
	public galleryOptions: NgxGalleryOptions[];

	/**
	 * Creates an instance of the member detail component.
	 *
	 * @param route The activated route.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor (private route: ActivatedRoute, private usersApi: UsersService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.route.data.subscribe(data => { this.user = data['user']; });

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
	}
}