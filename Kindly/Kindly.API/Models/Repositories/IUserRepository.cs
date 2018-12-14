using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kindly.API.Models.Repositories
{
	public interface IUserRepository
	{
		/// <summary>
		/// Creates the specified user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		Task<User> CreateUser(User user);

		/// <summary>
		/// Updates the specified user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		Task<User> UpdateUser(User user);

		/// <summary>
		/// Deletes the specified user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<User> DeleteUser(Guid userID);

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
		Task<User> AddPassword(User user, string password);

		/// <summary>
		/// Updates the password.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="oldPassword">The old password.</param>
		/// <param name="newPassword">The new password.</param>
		Task<User> ChangePassword(User user, string oldPassword, string newPassword);

		/// <summary>
		/// Checks if a user exists with the given user userID.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<bool> UserIdExists(Guid userID);

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
		/// Gets a user by userID.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<User> GetUserByID(Guid userID);

		/// <summary>
		/// Gets a user by user name.
		/// </summary>
		/// 
		/// <param name="userName">The user name.</param>
		Task<User> GetUserByUserName(string userName);

		/// <summary>
		/// Gets a user by phone number.
		/// </summary>
		/// 
		/// <param name="phoneNumber">The phone number.</param>
		Task<User> GetUserByPhoneNumber(string phoneNumber);

		/// <summary>
		/// Gets a user by email address.
		/// </summary>
		/// 
		/// <param name="emailAddress">The email address.</param>
		Task<User> GetUserByEmailAddress(string emailAddress);

		/// <summary>
		/// Gets the users.
		/// </summary>
		Task<IEnumerable<User>> GetUsers();
	}
}