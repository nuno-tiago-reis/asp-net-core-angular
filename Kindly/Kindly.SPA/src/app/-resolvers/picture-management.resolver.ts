// components
import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// services
import { PicturesService } from '../-services/pictures/pictures.service';
import { AlertifyService } from '../-services/alertify/alertify.service';
import { PictureParameters, PictureMode } from '../-services/pictures/pictures.models';

// models
import { Picture } from '../-models/picture';
import { PaginatedResult } from '../-models/paginated-result';

@Injectable()
export class PictureManagementResolver implements Resolve<PaginatedResult<Picture>>
{
	/**
	 * The page number.
	 */
	public readonly pageNumber = 1;

	/**
	 * The page size.
	 */
	public readonly pageSize = 18;

	/**
	 * Creates an instance of the member list resolver.
	 *
	 * @param router The router.
	 * @param picturesApi The pictures service.
	 * @param alertify The alertify service.
	 */
	public constructor(private router: Router, private picturesApi: PicturesService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * Resolves a route.
	 *
	 * @param route the route.
	 */
	public resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Picture>>
	{
		const filterParameters: PictureParameters =
		{
			container: PictureMode.Unapproved
		};

		return this.picturesApi.getAllForAdministration(this.pageNumber, this.pageSize, filterParameters).pipe
		(
			catchError
			((error) =>
			{
				this.alertify.error('Problem retrieving the pictures list.');
				this.router.navigate(['/home']);

				return of(null);
			})
		);
	}
}