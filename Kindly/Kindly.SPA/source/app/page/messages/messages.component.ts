// components
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// services
import { AuthService } from '../../-services/auth/auth.service';
import { MessagesService } from '../../-services/messages/messages.service';
import { AlertifyService } from '../../-services/alertify/alertify.service';
import { ContainerMode, MessageParameters } from '../../-services/messages/messages.models';

// models
import { Message } from '../../-models/message';
import { Pagination } from '../../-models/pagination';
import { PaginatedResult } from '../../-models/paginated-result';

@Component
({
	selector: 'app-messages',
	templateUrl: './messages.component.html',
	styleUrls: [ './messages.component.css' ]
})

export class MessagesComponent implements OnInit
{
	/**
	 * The array of messages.
	 */
	public messages: Message[];

	/**
	 * The pagination options.
	 */
	public pagination: Pagination;

	/**
	 * The filter parameters.
	 */
	public filterParameters: MessageParameters;

	/**
	 * The container mode for inbound messages.
	 */
	public readonly containerModeInbox: ContainerMode = ContainerMode.Inbox;

	/**
	 * The container mode for unread messages.
	 */
	public readonly containerModeUnread: ContainerMode = ContainerMode.Unread;

	/**
	 * The container mode for outbound messages.
	 */
	public readonly containerModeOutbox: ContainerMode = ContainerMode.Outbox;

	/**
	 * Creates an instance of the messages component.
	 *
	 * @param route The activated route.
	 * @param authApi The auth service.
	 * @param messagesApi The messages service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private readonly route: ActivatedRoute,
		private readonly authApi: AuthService,
		private readonly messagesApi: MessagesService,
		private readonly alertify: AlertifyService
	)
	{
		this.filterParameters =
		{
			container: ContainerMode.Unread
		};
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		this.route.data.subscribe(data =>
		{
			this.messages = data['messages'].results;
			this.pagination = data['messages'].pagination;
		});
	}

	/**
	 * Invoked when the page is changed.
	 */
	public changePage(event: any): void
	{
		this.pagination.pageNumber = event.page;

		this.getMessages();
	}

	/**
	 * Gets the messages.
	 */
	public getMessages(): void
	{
		this.messagesApi.getAll(this.authApi.user.id, this.pagination.pageNumber, this.pagination.pageSize, this.filterParameters).subscribe
		(
			(body: PaginatedResult<Message>) =>
			{
				this.messages = body.results;
				this.pagination = body.pagination;
			},
			(error: any) =>
			{
				this.alertify.error('An error occured while retrieving the messages.');
			}
		);
	}

	/**
	 * Deletes a message.
	 */
	public deleteMessage(message: Message): void
	{
		this.alertify.confirm('Delete Message', 'Are you sure you want to delete this message?',
		() =>
		{
			this.messagesApi.delete(message.id, this.authApi.user.id).subscribe
			(
				(next: void) =>
				{
					this.messages.splice(this.messages.findIndex(p => p.id === message.id), 1);
					this.alertify.success('You have deleted the message.');
				},
				(error: any) =>
				{
					this.alertify.error('An error occured while deleting the message.');
				}
			);
		},
		() =>
		{
			// Nothing to do here.
		});
	}
}