using Kindly.API.Contracts.Likes;
using Kindly.API.Contracts.Pictures;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	/// <summary>
	/// The detailed data transfer object for the user entity.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UserDetailedDto : UserDto
	{
		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 13)]
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 14)]
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 15)]
		public string LookingFor { get; set; }

		/// <summary>
		/// Gets or sets the pictures.
		/// </summary>
		[Required]
		[JsonProperty(Order = 16)]
		public ICollection<PictureDto> Pictures { get; set; }

		/// <summary>
		/// Gets or sets the like senders.
		/// </summary>
		[Required]
		[JsonProperty(Order = 17)]
		public ICollection<LikeDto> LikeSenders { get; set; }

		/// <summary>
		/// Gets or sets the like recipients.
		/// </summary>
		[Required]
		[JsonProperty(Order = 18)]
		public ICollection<LikeDto> LikeRecipients { get; set; }
	}
}