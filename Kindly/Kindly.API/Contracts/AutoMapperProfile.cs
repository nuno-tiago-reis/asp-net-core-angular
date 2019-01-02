using System.Linq;

using AutoMapper;

using Kindly.API.Contracts.Auth;
using Kindly.API.Contracts.Likes;
using Kindly.API.Contracts.Pictures;
using Kindly.API.Contracts.Users;
using Kindly.API.Models.Domain;
using Kindly.API.Utility;

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
			// Users
			this.CreateMap<UserDto, User>();
			this.CreateMap<User, UserDto>()
				.ForMember
				(
					userDto => userDto.Age,
					option => option.MapFrom(user => user.BirthDate.CalculateAge())
				)
				.ForMember
				(
					userDto => userDto.ProfilePictureUrl,
					option => option.MapFrom
					(
						source => source.Pictures.FirstOrDefault(picture => picture.IsProfilePicture).Url
					)
				);

			this.CreateMap<UserDetailedDto, User>();
			this.CreateMap<User, UserDetailedDto>()
				.ForMember
				(
					userDto => userDto.Age,
					option => option.MapFrom(user => user.BirthDate.CalculateAge())
				)
				.ForMember
				(
					userDto => userDto.ProfilePictureUrl,
					option => option.MapFrom
					(
						source => source.Pictures.FirstOrDefault(picture => picture.IsProfilePicture).Url
					)
				);

			this.CreateMap<RegisterDto, User>();
			this.CreateMap<CreateUserDto, User>();
			this.CreateMap<UpdateUserDto, User>();

			// Pictures
			this.CreateMap<PictureDto, Picture>();
			this.CreateMap<Picture, PictureDto>();

			this.CreateMap<CreatePictureDto, Picture>();
			this.CreateMap<UpdatePictureDto, Picture>();

			// Likes
			this.CreateMap<LikeDto, Like>();
			this.CreateMap<Like, LikeDto>();

			this.CreateMap<CreateLikeDto, Like>();
			this.CreateMap<UpdateLikeDto, Like>();
		}
	}
}