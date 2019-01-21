// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { AlertifyService } from '../../../-services/alertify/alertify.service';
import { PicturesService } from '../../../-services/pictures/pictures.service';
import { PictureParameters, PictureMode } from '../../../-services/pictures/pictures.models';

// models
import { Picture } from '../../../-models/picture';
import { Pagination } from '../../../-models/pagination';
import { PaginatedResult } from '../../../-models/paginated-result';

@Component
({
	selector: 'app-picture-management',
	templateUrl: './picture-management.component.html',
	styleUrls: ['./picture-management.component.css']
})

export class PictureManagementComponent implements OnInit
{
	/**
	 * The pictures.
	 */
	public pictures: Picture[];

	/**
	 * The pagination options.
	 */
	public pagination: Pagination;

	/**
	 * Creates an instance of the picture management component.
	 *
	 * @param activatedRoute The activated route.
	 * @param picturesApi The pictures service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private readonly activatedRoute: ActivatedRoute,
		private readonly picturesApi: PicturesService,
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
		this.activatedRoute.data.subscribe(data =>
		{
			this.pictures = data['pictures'].results;
			this.pagination = data['pictures'].pagination;
		});
	}

	/**
	 * Invoked when the page is changed.
	 */
	public changePage(event: any): void
	{
		this.pagination.pageNumber = event.page;

		this.getPictures();
	}

	/**
	 * Gets the pictures.
	 */
	public getPictures(): void
	{
		const filterParameters: PictureParameters =
		{
			container: PictureMode.Unapproved
		};

		this.picturesApi.getAllForAdministration(this.pagination.pageNumber, this.pagination.pageSize, filterParameters).subscribe
		(
			(body: PaginatedResult<Picture>) =>
			{
				this.pictures = body.results;
				this.pagination = body.pagination;
			},
			(error: any) =>
			{
				this.alertify.error('An error occured while retrieving the pictures.');
			}
		);
	}

	/**
	 * Approves a picture.
	 *
	 * @param pictureID The picture ID.
	 */
	public approvePicture(pictureID: string): void
	{
		this.picturesApi.approve(pictureID).subscribe(() =>
		{
			this.pictures.splice(this.pictures.findIndex(p => p.id === pictureID), 1);

			this.alertify.success('You have approved the picture.');
		},
		(error: any) =>
		{
			this.alertify.error('An error occured while approving the picture.');
		});
	}

	/**
	 * Rejects a picture.
	 *
	 * @param pictureID The picture ID.
	 */
	public rejectPicture(pictureID: string): void
	{
		this.picturesApi.reject(pictureID).subscribe(() =>
		{
			this.pictures.splice(this.pictures.findIndex(p => p.id === pictureID), 1);

			this.alertify.success('You have rejected the picture.');
		},
		(error: any) =>
		{
			this.alertify.error('An error occured while rejecting the picture.');
		});
	}
}