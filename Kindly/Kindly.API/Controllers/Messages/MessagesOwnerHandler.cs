using Kindly.API.Models.Repositories.Messages;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Messages
{
	/// <summary>
	/// Implements the owner handler for the messages controller.
	/// </summary>
	/// 
	/// <seealso cref="MessagesController"/>.
	/// <seealso cref="ResourceOwnerHandler{Message}" />
	public sealed class MessagesOwnerHandler : ResourceOwnerHandler<Message>
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IMessageRepository Repository { get; set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes a new instance of the <see cref="MessagesOwnerHandler"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		public MessagesOwnerHandler(IMessageRepository repository)
		{
			this.Repository = repository;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			ResourceOwnerRequirement requirement,
			Message message
		)
		{
			var userID = this.GetInvocationUserID(context);

			if
			(
				// The invoking user is the same as the api parameter
				userID == message.SenderID || userID == message.RecipientID &&
				// The message belongs to the invoking user (which is the same as the api parameter)
				message.ID == default(Guid) && message.ID == default(Guid) || this.Repository.MessageBelongsToUser(userID, message.ID).Result)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
		#endregion
	}
}