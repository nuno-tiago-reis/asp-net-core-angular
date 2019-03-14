// components
import { Component, OnInit, Input } from '@angular/core';
import { tap } from 'rxjs/operators';

// services
import { AuthService } from '../../../-services/auth/auth.service';
import { MessagesService } from '../../../-services/messages/messages.service';
import { AlertifyService } from '../../../-services/alertify/alertify.service';

// models
import { Message } from '../../../-models/message';
import { CreateRequest, UpdateRequest } from '../../../-services/messages/messages.models';

@Component
({
	selector: 'app-member-messages',
	templateUrl: './member-messages.component.html',
	styleUrls: ['./member-messages.component.css']
})

export class MemberMessagesComponent implements OnInit
{
	/**
	 * The recipient id.
	 */
	@Input()
	public recipientID: string;

	/**
	 * The array of messages.
	 */
	public messages: Message[];

	/**
	 * The new message.
	 */
	public newMessage: CreateRequest;

	/**
	 * Creates an instance of the member messages component.
	 *
	 * @param authApi The auth service.
	 * @param messagesApi The messages service.
	 * @param alertify The alertify service.
	 * @param timeAgo The time ago pipe.
	 */
	public constructor
	(
		private readonly authApi: AuthService,
		private readonly messagesApi: MessagesService,
		private readonly alertify: AlertifyService
	)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit(): void
	{
		this.newMessage =
		{
			content: '',
			recipientID: this.recipientID
		};

		this.getMessages();
	}

	/**
	 * Gets the messages.
	 */
	public getMessages(): void
	{
		const user = this.authApi.user;
		const date = new Date();
		const updateRequest: UpdateRequest =
		{
			isRead: true,
			readAt: date
		};

		this.messagesApi.getThread(user.id, this.recipientID).pipe
		(
			tap((messages: Message[]) =>
			{
				for (let i = 0; i < messages.length; i++)
				{
					const message = messages[i];

					if (message.isRead === false && message.recipientID === user.id)
					{
						this.messagesApi.update(message.id, user.id, updateRequest).subscribe();
					}
				}
			})
		)
		.subscribe
		(
			(messages: Message[]) =>
			{
				this.messages = messages;
			},
			(error: any) =>
			{
				this.alertify.error('An error occured while retrieving the profile messages.');
			}
		);
	}

	/**
	 * Sends a message.
	 */
	public sendMessage(): void
	{
		this.newMessage.recipientID = this.recipientID;

		this.messagesApi.create(this.authApi.user.id, this.newMessage).subscribe
		(
			(message: any) =>
			{
				this.messages.push(message);
				this.newMessage.content = '';
			},
			error =>
			{
				this.alertify.error('An error occured while sending the message.');
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

	/**
	 * Returns the read message tooltip html.
	 *
	 * @param createdAt The created at string.
	 * @param readAt The read at string.
	 */
	public getReadTooltipHtml(createdAt: string, readAt: string): string
	{
		const html =
			`${createdAt} <span class="text-success read-message">(read ${readAt})</span>`;

		return html;
	}

	/**
	 * Returns the unread message tooltip html.
	 *
	 * @param createdAt The created at string.
	 */
	public getUnreadTooltipHtml(createdAt: string): string
	{
		const html =
			`${createdAt} <span class="text-danger unread-message"> (unread)</span>`;

		return html;
	}
}