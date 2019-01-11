using Kindly.API.Models.Repositories.Messages;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Messages
{
	public sealed class MessagesAuthorizationHandler : KindlyAuthorizationHandler<AllowIfOwnerRequirement, Message>
	{
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		public IMessageRepository Repository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MessagesAuthorizationHandler"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		public MessagesAuthorizationHandler(IMessageRepository repository)
		{
			this.Repository = repository;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			AllowIfOwnerRequirement requirement,
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

			return Task.FromResult(0);
		}
	}
}