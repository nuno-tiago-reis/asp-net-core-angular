using Kindly.API.Contracts.Likes;
using Kindly.API.Contracts.Pictures;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UserDetailedDto : UserDto
	{
		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		[DataType(DataType.Text)]
		[JsonProperty(Order = 13)]
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		[DataType(DataType.Text)]
		[JsonProperty(Order = 14)]
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		[DataType(DataType.Text)]
		[JsonProperty(Order = 15)]
		public string LookingFor { get; set; }

		/// <summary>
		/// Gets or sets the pictures.
		/// </summary>
		[JsonProperty(Order = 16)]
		public ICollection<PictureDto> Pictures { get; set; }

		/// <summary>
		/// Gets or sets the like sources.
		/// </summary>
		[JsonProperty(Order = 17)]
		public ICollection<LikeDto> LikeSources { get; set; }

		/// <summary>
		/// Gets or sets the like targets.
		/// </summary>
		[JsonProperty(Order = 18)]
		public ICollection<LikeDto> LikeTargets { get; set; }

		/// <summary>
		/// Cleans the like sources and targets.
		/// </summary>
		public void CleanLikeSourcesAndTargets()
		{
			foreach (var likeDto in this.LikeTargets)
			{
				likeDto.Target = null;
				likeDto.Source = null;
			}

			foreach (var likeDto in this.LikeSources)
			{
				likeDto.Target = null;
				likeDto.Source = null;
			}
		}
	}
}