using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Security.Claims;

namespace Kindly.API.Controllers
{
	public abstract class KindlyController : ControllerBase
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		protected IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the authorization service.
		/// </summary>
		protected IAuthorizationService AuthorizationService { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="authorizationService">The authorization service.</param>
		protected KindlyController(IMapper mapper, IAuthorizationService authorizationService)
		{
			this.Mapper = mapper;
			this.AuthorizationService = authorizationService;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		protected KindlyController(IMapper mapper)
		{
			this.Mapper = mapper;
			this.AuthorizationService = null;
		}
		#endregion

		#region [Utility Methods]
		/// <summary>
		/// Gets the invocation user identifier.
		/// </summary>
		/// <returns></returns>
		protected Guid GetInvocationUserID()
		{
			return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
		}
		#endregion
	}
}