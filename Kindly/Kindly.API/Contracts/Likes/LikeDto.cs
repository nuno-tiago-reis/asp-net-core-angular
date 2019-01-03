using Kindly.API.Contracts.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class LikeDto
	{
		/// <summary>
		/// Gets or sets the like identifier.
		/// </summary>
		[Required]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the target identifier.
		/// </summary>
		[Required]
		public Guid? TargetID { get; set; }

		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		[Required]
		public UserDto Target { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// </summary>
		[Required]
		public Guid? SourceID { get; set; }

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		[Required]
		public UserDto Source { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Cleans the source.
		/// </summary>
		public void CleanSource()
		{
			this.SourceID = null;
			this.Source = null;
		}

		/// <summary>
		/// Cleans the target.
		/// </summary>
		public void CleanTarget()
		{
			this.TargetID = null;
			this.Target = null;
		}
	}
}