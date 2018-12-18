import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({

	providedIn: 'root'
})

export class ServiceInterceptor implements HttpInterceptor
{
	public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
	{
		return next.handle(request).pipe(

			tap((event: HttpEvent<any>) =>
			{
				if (event instanceof HttpResponse)
				{
					console.log(event.status);
					console.log(event.body);
					console.log(`${event.url} successfull`);
				}

				return event;
			}),

			catchError(error =>
			{
				if (error instanceof HttpErrorResponse)
				{
					// Application errors are custom errors
					const applicationError = error.headers.get('Application-Error');

					if (applicationError)
					{
						console.error(`An application error occurred: ${applicationError}`);
						return throwError(applicationError);
					}

					// Model errors are ASP.NET model validation errors
					const serverError = error.error;

					if (serverError && typeof serverError === 'object')
					{
						let modelErrors = '\n';

						for (const key in serverError)
						{
							if (!serverError[key])
								continue;

							modelErrors += serverError[key] + '\n';
						}

						if (modelErrors !== '\n')
						{
							console.error(`A model error occurred: ${modelErrors}`);
							return throwError(modelErrors);
						}
						else
						{
							console.error(`A server error occurred: ${serverError}`);
							return throwError(serverError);
						}

					}

					// A client-side or network error occurred. Handle it accordingly.
					console.error(`An internal server error occured: ${error.message}`);
					return throwError('Internal server error');
				}
				else
				{
					// The backend returned an unsuccessful response code.
					// The response body may contain clues as to what went wrong,
					console.error(`A backend error occurred: ${error.message}`);
					console.error(error.error);
				}

				// return an observable with a user-facing error message
				return throwError(`Something bad happened; please try again later.`);
			})
		);
	}
}

export const ServiceInterceptorProvider =
{
	provide: HTTP_INTERCEPTORS,
	useClass: ServiceInterceptor,
	multi: true
};