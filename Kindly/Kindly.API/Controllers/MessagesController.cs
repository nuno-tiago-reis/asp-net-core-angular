using AutoMapper;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Messages;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/users/{userID:Guid}/[controller]")]
	public sealed class MessagesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IMessageRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MessagesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		public MessagesController(IMapper mapper, IMessageRepository repository)
		{
			this.Mapper = mapper;
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates a message for a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="createMessageInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(Guid userID, CreateMessageDto createMessageInfo)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			var message = Mapper.Map<Message>(createMessageInfo);
			message.SenderID = userID;

			await this.Repository.Create(message);

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{message.ID}"), Mapper.Map<MessageDto>(message));
		}

		/// <summary>
		/// Updates a users message.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="messageID">The message identifier.</param>
		/// <param name="updateMessageInfo">The update information.</param>
		[HttpPut("{messageID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, Guid messageID, UpdateMessageDto updateMessageInfo)
		{
			if (userID != this.GetInvocationUserID())
					return this.Unauthorized();

			if (await this.Repository.MessageBelongsToUser(userID, messageID) == false)
				return this.NotFound();

			var message = Mapper.Map<Message>(updateMessageInfo);
			message.ID = messageID;

			await this.Repository.Update(message);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a users message.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="messageID">The message identifier.</param>
		[HttpDelete("{messageID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID, Guid messageID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.MessageBelongsToUser(userID, messageID) == false)
				return this.NotFound();

			var message = await this.Repository.Get(messageID);

			if (userID == message.SenderID)
				message.SenderDeleted = true;

			if (userID == message.RecipientID)
				message.RecipientDeleted = true;

			if (message.SenderDeleted != null && message.SenderDeleted.Value &&
				message.RecipientDeleted != null && message.RecipientDeleted.Value)
			{
				await this.Repository.Update(message);

				await this.Repository.Delete(messageID);
			}
			else
			{
				await this.Repository.Update(message);
			}

			return this.Ok();
		}

		/// <summary>
		/// Gets a users message.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="messageID">The message identifier.</param>
		[HttpGet("{messageID:Guid}")]
		public async Task<IActionResult> Get(Guid userID, Guid messageID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.MessageBelongsToUser(userID, messageID) == false)
				return this.NotFound();

			var message = await this.Repository.Get(messageID);
			var messageDto = this.Mapper.Map<MessageDto>(message);

			return this.Ok(messageDto);
		}

		/// <summary>
		/// Gets a users messages.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="parameters">The parameters.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID, [FromQuery] MessageParameters parameters)
		{
			var messages = await this.Repository.GetByUser(userID, parameters);
			var messageDtos = messages.Select(l => this.Mapper.Map<MessageDto>(l)).ToList();

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				messages.PageNumber,
				messages.PageSize,
				messages.TotalPages,
				messages.TotalCount
			));

			return this.Ok(messageDtos);
		}

		/// <summary>
		/// Gets a message thread between two users.
		/// </summary>
		/// 
		/// <param name="userID">The first user identifier.</param>
		/// <param name="secondUserID">The second user identifier.</param>
		[HttpGet("thread/{secondUserID:Guid}")]
		public async Task<IActionResult> GetThread(Guid userID, Guid secondUserID)
		{
			var messages = await this.Repository.GetThreadByUsers(userID, secondUserID);
			var messageDtos = messages.Select(l => this.Mapper.Map<MessageDto>(l)).ToList();

			return this.Ok(messageDtos);
		}
		#endregion
	}
}