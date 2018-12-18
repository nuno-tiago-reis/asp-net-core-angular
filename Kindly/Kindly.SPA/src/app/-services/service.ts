import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

export class Service
{
	/**
	 * The http client.
	 */
	protected http: HttpClient;

	/**
	 * The base URL.
	 */
	protected baseURL: string;

	/**
	 * Handles HttpErrorResponse errors by logging them to the console.
	 *
	 * @param error The HttpErrorResponse error.
	 */
	protected handleResponse<T> (response: HttpResponse<T>)
	{
		console.log(response.body);
		console.log(response.status);
		console.log(`${response.url} successfull`);
	}

	/**
	 * Handles HttpErrorResponse errors by logging them to the console.
	 *
	 * @param error The HttpErrorResponse error.
	 */
	protected handleErrorResponse (errorResponse: HttpErrorResponse)
	{
		if (errorResponse.error instanceof ErrorEvent)
		{
			// A client-side or network error occurred. Handle it accordingly.
			console.error(`An error occurred: ${errorResponse.error.message}`);
		}
		else
		{
			// The backend returned an unsuccessful response code.
			// The response body may contain clues as to what went wrong,
			console.error(`Backend returned code ${errorResponse.status} body was: ${errorResponse.error}`);
		}

		// return an observable with a user-facing error message
		return throwError(`Something bad happened; please try again later.`);
	}
}