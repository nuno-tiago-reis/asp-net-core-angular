using System;
using System.Security.Claims;
using Kindly.API.Models.Repositories.Pictures;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

namespace Kindly.API.Utility
{
	public enum KindlyRoles
	{
		Administrator,
		Moderator,
		Member
	}

	public enum KindlyPolicies
	{
		AllowIfOwner
	}
}