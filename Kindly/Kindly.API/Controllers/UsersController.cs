using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using Kindly.API.Contracts.Users;
using Kindly.API.Models;
using Kindly.API.Models.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public sealed class UsersController : ControllerBase
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
			this.Repository = repository;
			this.Mapper = mapper;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Gets the users.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var users = await this.Repository.GetUsers();
				var userDtos = users.Select(user => this.Mapper.Map<UserDto>(user));

				return this.Ok(userDtos);
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Gets a user.
		/// </summary>
		/// 
		/// <param name="id">The user identifier.</param>
		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetUser(Guid id)
		{
			try
			{
				var user = await this.Repository.GetUserByID(id);
				var userDto = this.Mapper.Map<UserDto>(user);

				return this.Ok(userDto);
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Creates the specified user.
		/// </summary>
		/// 
		/// <param name="createInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(CreateDto createInfo)
		{
			try
			{
				var user = new User
				{
					UserName = createInfo.UserName,
					PhoneNumber = createInfo.PhoneNumber,
					EmailAddress = createInfo.EmailAddress
				};

				await this.Repository.CreateUser(user);

				return this.Created(new Uri($"{Request.GetDisplayUrl()}/{user.ID}"), Mapper.Map<UserDto>(user));
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Updates the specified user.
		/// </summary>
		/// 
		/// <param name="id">The user identifier.</param>
		/// <param name="updateInfo">The update information.</param>
		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> Update(Guid id, UpdateDto updateInfo)
		{
			try
			{
				var user = new User
				{
					ID = id,
					PhoneNumber = updateInfo.PhoneNumber,
					EmailAddress = updateInfo.EmailAddress
				};

				await this.Repository.UpdateUser(user);

				return this.Ok();
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Deletes a user.
		/// </summary>
		/// 
		/// <param name="id">The user identifier.</param>
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			try
			{
				await this.Repository.DeleteUser(id);

				return this.Ok();
			}
			catch (Exception exception)
			{
				return this.BadRequest(exception.Message);
			}
		}
		#endregion
	}
}