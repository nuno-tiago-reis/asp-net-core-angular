using System;
using System.Threading.Tasks;

using Kindly.API.Contracts.Users;
using Kindly.API.Models.Domain;

namespace Kindly.API.Models.Repositories
{
	public interface IUserRepository : IEntityRepository<User, UserParameters>
	{
		/// <summary>
		/// Logs in the user using its user ID.
		/// </summary>
		/// 
		/// <param name="userID">The user ID.</param>
		/// <param name="password">The password.</param>
		Task<User> LoginWithID(Guid userID, string password);

		/// <summary>
		/// Logs in the user using its user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		/// <param name="password">The password.</param>
		Task<User> LoginWithUserName(string userName, string password);

		/// <summary>
		/// Logs in the user using its phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		/// <param name="password">The password.</param>
		Task<User> LoginWithPhoneNumber(string phoneNumber, string password);

		/// <summary>
		/// Logs in the user using its email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		/// <param name="password">The password.</param>
		Task<User> LoginWithEmailAddress(string emailAddress, string password);

		/// <summary>
		/// Adds the password.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		Task AddPassword(User user, string password);

		/// <summary>
		/// Updates the password.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="oldPassword">The old password.</param>
		/// <param name="newPassword">The new password.</param>
		Task ChangePassword(User user, string oldPassword, string newPassword);

		/// <summary>
		/// Checks if a user exists with the given user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		Task<bool> UserNameExists(string userName);

		/// <summary>
		/// Checks if a user exists with the given phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		Task<bool> PhoneNumberExists(string phoneNumber);

		/// <summary>
		/// Checks if a user exists with the given email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		Task<bool> EmailAddressExists(string emailAddress);

		/// <summary>
		/// Gets a user by user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		Task<User> GetByUserName(string userName);

		/// <summary>
		/// Gets a user by phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		Task<User> GetByPhoneNumber(string phoneNumber);

		/// <summary>
		/// Gets a user by email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		Task<User> GetByEmailAddress(string emailAddress);
	}
}