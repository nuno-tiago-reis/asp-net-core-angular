﻿using Kindly.API.Contracts.Roles;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UserWithRolesDto : UserDto
	{
		/// <summary>
		/// Gets or sets the roles
		/// </summary>
		[JsonProperty(Order = 13)]
		public ICollection<RoleDto> Roles { get; set; }
	}
}