using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Users;
using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	public sealed class UsersController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IUserRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="UsersController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		public UsersController(IMapper mapper, IUserRepository repository)
		{
			this.Mapper = mapper;
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates the specified user.
		/// </summary>
		/// 
		/// <param name="createUserInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(CreateUserDto createUserInfo)
		{
			var user = await this.Repository.Create(Mapper.Map<User>(createUserInfo));

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{user.ID}"), Mapper.Map<UserDto>(user));
		}

		/// <summary>
		/// Updates the specified user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="updateUserInfo">The update information.</param>
		[HttpPut("{userID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, UpdateUserDto updateUserInfo)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			var user = Mapper.Map<User>(updateUserInfo);
			user.ID = userID;

			await this.Repository.Update(user);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpDelete("{userID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			await this.Repository.Delete(userID);

			return this.Ok();
		}

		/// <summary>
		/// Gets a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpGet("{userID:Guid}")]
		public async Task<IActionResult> Get(Guid userID)
		{
			var user = await this.Repository.Get(userID);
			var userDto = this.Mapper.Map<UserDetailedDto>(user);

			return this.Ok(userDto);
		}

		/// <summary>
		/// Gets the users.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] UserParameters parameters)
		{
			parameters.UserID = this.GetInvocationUserID();

			var users = await this.Repository.GetAll(parameters);
			var userDtos = users.Select(user => this.Mapper.Map<UserDto>(user));

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				users.PageNumber,
				users.PageSize,
				users.TotalPages,
				users.TotalCount
			));

			return this.Ok(userDtos);
		}
		#endregion
	}
}