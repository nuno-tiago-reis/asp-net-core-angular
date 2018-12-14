using AutoMapper;

using Kindly.API.Contracts.Users;
using Kindly.API.Models;

namespace Kindly.API.Contracts
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
	/// </summary>
	public sealed class AutoMapperProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
		/// </summary>
		public AutoMapperProfile()
		{
			this.CreateMap<User, UserDto>();
			this.CreateMap<UserDto, User>();
		}
	}
}
