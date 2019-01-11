using AutoMapper;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Roles;
using Kindly.API.Models.Repositories.Roles;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Roles
{
	[Authorize(Roles = nameof(KindlyRoles.Administrator))]
	[ApiController]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/[controller]")]
	public sealed class RolesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IRoleRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="RolesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		public RolesController(IMapper mapper, IRoleRepository repository) : base(mapper)
		{
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates a role.
		/// </summary>
		/// 
		/// <param name="createRoleInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(CreateRoleDto createRoleInfo)
		{
			var role = await this.Repository.Create(this.Mapper.Map<Role>(createRoleInfo));

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{role.ID}"), this.Mapper.Map<RoleDto>(role));
		}

		/// <summary>
		/// Updates a role.
		/// </summary>
		/// 
		/// <param name="roleID">The role identifier.</param>
		/// <param name="updateRoleInfo">The update information.</param>
		[HttpPut("{roleID:Guid}")]
		public async Task<IActionResult> Update(Guid roleID, UpdateRoleDto updateRoleInfo)
		{
			var role = this.Mapper.Map<Role>(updateRoleInfo);
			role.ID = roleID;

			await this.Repository.Update(role);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a role.
		/// </summary>
		/// 
		/// <param name="roleID">The role identifier.</param>
		[HttpDelete("{roleID:Guid}")]
		public async Task<IActionResult> Delete(Guid roleID)
		{
			await this.Repository.Delete(roleID);

			return this.Ok();
		}

		/// <summary>
		/// Gets a role.
		/// </summary>
		/// 
		/// <param name="roleID">The role identifier.</param>
		[HttpGet("{roleID:Guid}")]
		public async Task<IActionResult> Get(Guid roleID)
		{
			var role = await this.Repository.Get(roleID);
			var roleDTo = this.Mapper.Map<RoleDto>(role);

			return this.Ok(roleDTo);
		}

		/// <summary>
		/// Gets the roles.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] RoleParameters parameters)
		{
			var roles = await this.Repository.GetAll(parameters);
			var roleDtos = roles.Select(l => this.Mapper.Map<RoleDto>(l)).ToList();

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				roles.PageNumber,
				roles.PageSize,
				roles.TotalPages,
				roles.TotalCount
			));

			return this.Ok(roleDtos);
		}
		#endregion
	}
}