// components
import { Injectable } from '@angular/core';

declare let alertify: any;

@Injectable
({
	providedIn: 'root'
})

export class AlertifyService
{
	/**
	 * Creates an instance of the alertify service.
	 */
	public constructor ()
	{
		// Nothing to do here.
	}

	/**
	 * Creates a confirmation pop-up using alertify.
	 *
	 * @param message The pop-up message.
	 * @param onConfirm The confirmation callback.
	 * @param onCancel The cancelation callback.
	 */
	public confirm (message: string, onConfirm: () => any, onCancel: () => any): void
	{
		alertify.confirm
		(
			// Message
			message,

			// Confirm callback
			function(event: any)
			{
				if (event)
					onConfirm();
			},

			// Cancel callback
			function(event: any)
			{
				if (event)
					onCancel();
			}
		);
	}

	/**
	 * Creates a generic pop-up using alertify.
	 *
	 * @param message The pop-up message.
	 */
	public message (message: string): void
	{
		alertify.message(message);
	}

	/**
	 * Creates a success pop-up using alertify.
	 *
	 * @param message The pop-up message.
	 */
	public success (message: string): void
	{
		alertify.success(message);
	}

	/**
	 * Creates a warning pop-up using alertify.
	 *
	 * @param message The pop-up message.
	 */
	public warning (message: string): void
	{
		alertify.warning(message);
	}

	/**
	 * Creates an error pop-up using alertify.
	 *
	 * @param message The pop-up message.
	 */
	public error (message: string): void
	{
		alertify.error(message);
	}
}