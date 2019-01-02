using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Kindly.API.Models.Domain
{
	public class User
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
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the password hash.
		/// </summary>
		public byte[] PasswordHash { get; set; }

		/// <summary>
		/// Gets or sets the password salt.
		/// </summary>
		public byte[] PasswordSalt { get; set; }

		/// <summary>
		/// Gets or sets the known as name.
		/// </summary>
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the birth date.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		public string LookingFor { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the pictures.
		/// </summary>
		public ICollection<Picture> Pictures { get; set; }

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
		#endregion
	}

	public enum Gender
	{
		[UsedImplicitly] Undefined = 0,
		[UsedImplicitly] Female = 1,
		[UsedImplicitly] Male = 2
	}
}