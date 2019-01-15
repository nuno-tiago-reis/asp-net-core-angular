// components
import { Injectable } from '@angular/core';

declare let alertify: any;

@Injectable
(({
	providedIn: 'root'
}) as any)

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
	 * @param title The pop-up title.
	 * @param message The pop-up message.
	 * @param onConfirm The confirmation callback.
	 * @param onCancel The cancellation callback.
	 */
	public confirm (title: string, message: string, onConfirm: () => any, onCancel: () => any): void
	{
		alertify.confirm
		(
			// Title
			title,
			// Message
			message,

			// Confirm callback
			(event: any) =>
			{
				if (event)
					onConfirm();
			},

			// Cancel callback
			(event: any) =>
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