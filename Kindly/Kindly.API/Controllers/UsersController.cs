using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

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
	[ServiceFilter(typeof(LogUserActivityFilter))]
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
		/// <param name="id">The user identifier.</param>
		/// <param name="updateUserInfo">The update information.</param>
		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> Update(Guid id, UpdateUserDto updateUserInfo)
		{
			if (id != this.GetInvocationUserID())
				return this.Unauthorized();

			var user = Mapper.Map<User>(updateUserInfo);
			user.ID = id;

			await this.Repository.Update(user);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a user.
		/// </summary>
		/// 
		/// <param name="id">The user identifier.</param>
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id != this.GetInvocationUserID())
				return this.Unauthorized();

			await this.Repository.Delete(id);

			return this.Ok();
		}

		/// <summary>
		/// Gets a user.
		/// </summary>
		/// 
		/// <param name="id">The user identifier.</param>
		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var user = await this.Repository.Get(id);
			var userDto = this.Mapper.Map<UserDetailedDto>(user);

			return this.Ok(userDto);
		}

		/// <summary>
		/// Gets the users.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await this.Repository.GetAll();
			var userDtos = users.Select(user => this.Mapper.Map<UserDto>(user));

			return this.Ok(userDtos);
		}
		#endregion
	}
}