using JetBrains.Annotations;

using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Models.Repositories.Users.Identity;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;

namespace Kindly.API.Models.Repositories.Users
{
	public class User : IdentityUser<Guid>
	{
		#region [Constants]
		/// <summary>
		/// The user does not exist message.
		/// </summary>
		public const string DoesNotExist = "The user does not exist.";

		/// <summary>
		/// The password is incorrect message.
		/// </summary>
		public const string PasswordIsIncorrect = "The password is incorrect.";

		/// <summary>
		/// The user already has a password. 
		/// </summary>
		public const string PasswordAlreadyExists = "The user already has a password.";

		/// <summary>
		/// The user or password is incorrect message.
		/// </summary>
		public const string UserOrPasswordAreIncorrect = "The user or the password is incorrect.";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID
		{
			get { return this.Id; }
			set { this.Id = value; }
		}

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the birth date.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the known as name.
		/// </summary>
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		public string LookingFor { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the last active at date.
		/// </summary>
		public DateTime LastActiveAt { get; set; }

		/// <summary>
		/// Gets or sets the like targets.
		/// </summary>
		public ICollection<Like> LikeTargets { get; set; }

		/// <summary>
		/// Gets or sets the likes sources.
		/// </summary>
		public ICollection<Like> LikeSources { get; set; }

		/// <summary>
		/// Gets or sets the messages sent.
		/// </summary>
		public ICollection<Message> MessagesSent { get; set; }

		/// <summary>
		/// Gets or sets the messages received.
		/// </summary>
		public ICollection<Message> MessagesReceived { get; set; }

		/// <summary>
		/// Gets or sets the pictures.
		/// </summary>
		public ICollection<Picture> Pictures { get; set; }

		/// <summary>
		/// Gets or sets the roles.
		/// </summary>
		public ICollection<UserRole> UserRoles { get; set; }

		/// <summary>
		/// Gets or sets the claims.
		/// </summary>
		public ICollection<UserClaim> UserClaims { get; set; }

		/// <summary>
		/// Gets or sets the logins.
		/// </summary>
		public ICollection<UserLogin> UserLogins { get; set; }

		/// <summary>
		/// Gets or sets the tokens.
		/// </summary>
		public ICollection<UserToken> UserTokens { get; set; }
		#endregion
	}

	public enum Gender
	{
		[UsedImplicitly] Undefined = 0,
		[UsedImplicitly] Female = 1,
		[UsedImplicitly] Male = 2
	}
}