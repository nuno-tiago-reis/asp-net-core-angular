using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Kindly.API.Utility;

using Microsoft.EntityFrameworkCore;

namespace Kindly.API.Models.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public KindlyContext Context { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public UserRepository(KindlyContext context)
		{
			this.Context = context;
		}
		#endregion

		#region [Interface Methods]
		/// <inheritdoc />
		public async Task<User> CreateUser(User user)
		{
			if (string.IsNullOrWhiteSpace(user.UserName))
				throw new KindlyException("User name is invalid.");
			if (await this.UserNameExists(user.UserName))
				throw new KindlyException("User name is taken.");

			if (string.IsNullOrWhiteSpace(user.PhoneNumber))
				throw new KindlyException("Phone number is invalid.");
			if (await this.PhoneNumberExists(user.PhoneNumber))
				throw new KindlyException("Phone number is taken.");

			if (string.IsNullOrWhiteSpace(user.EmailAddress))
				throw new KindlyException("Email address is invalid.");
			if (await this.EmailAddressExists(user.EmailAddress))
				throw new KindlyException("Email address is taken.");

			this.Context.Add(user);
			await this.Context.SaveChangesAsync();

			return user;
		}

		/// <inheritdoc />
		public async Task<User> UpdateUser(User user)
		{
			var databaseUser = await this.GetUserByID(user.ID);
			if (databaseUser == null)
				throw new KindlyException("User does not exist.");

			if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && databaseUser.PhoneNumber != user.PhoneNumber && await this.PhoneNumberExists(user.PhoneNumber))
				throw new KindlyException("Phone number already exists.");

			if (!string.IsNullOrWhiteSpace(user.EmailAddress) && databaseUser.EmailAddress != user.EmailAddress && await this.EmailAddressExists(user.EmailAddress))
				throw new KindlyException("Email address already exists.");

			databaseUser.PhoneNumber = user.PhoneNumber ?? databaseUser.PhoneNumber;
			databaseUser.EmailAddress = user.EmailAddress ?? databaseUser.EmailAddress;

			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> DeleteUser(Guid userID)
		{
			var databaseUser = await this.Context.Users.FindAsync(userID);
			if (databaseUser == null)
				throw new KindlyException("User does not exist.");

			this.Context.Users.Remove(databaseUser);
			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> LoginWithID(Guid userID, string password)
		{
			var databaseUser = await this.GetUserByID(userID);

			return await this.Login(databaseUser, password);
		}

		/// <inheritdoc />
		public async Task<User> LoginWithUserName(string userName, string password)
		{
			var databaseUser = await this.GetUserByUserName(userName);

			return await this.Login(databaseUser, password);
		}

		/// <inheritdoc />
		public async Task<User> LoginWithPhoneNumber(string phoneNumber, string password)
		{
			var databaseUser = await this.GetUserByPhoneNumber(phoneNumber);

			return await this.Login(databaseUser, password);
		}

		/// <inheritdoc />
		public async Task<User> LoginWithEmailAddress(string emailAddress, string password)
		{
			var databaseUser = await this.GetUserByEmailAddress(emailAddress);

			return await this.Login(databaseUser, password);
		}

		/// <inheritdoc />
		public async Task<User> AddPassword(User user, string password)
		{
			var databaseUser = await this.GetUserByID(user.ID);
			if (databaseUser == null)
				throw new KindlyException("User does not exist.");

			if(databaseUser.PasswordHash != null)
				throw new KindlyException("User already has a password.");

			CreatePasswordHashAndSalt(databaseUser, password);

			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<User> ChangePassword(User user, string oldPassword, string newPassword)
		{
			var databaseUser = await this.GetUserByID(user.ID);
			if (databaseUser == null)
				throw new KindlyException("User does not exist.");

			if (CheckIfPasswordMatches(databaseUser, oldPassword) == false)
				throw new KindlyException("The password is incorrect.");

			CreatePasswordHashAndSalt(databaseUser, newPassword);

			await this.Context.SaveChangesAsync();

			return databaseUser;
		}

		/// <inheritdoc />
		public async Task<bool> UserIdExists(Guid userID)
		{
			return await this.Context.Users.AnyAsync(user => user.ID == userID);
		}

		/// <inheritdoc />
		public async Task<bool> UserNameExists(string userName)
		{
			return await this.Context.Users.AnyAsync(user => user.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<bool> PhoneNumberExists(string phoneNumber)
		{
			return await this.Context.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<bool> EmailAddressExists(string emailAddress)
		{
			return await this.Context.Users.AnyAsync(user => user.EmailAddress == emailAddress);
		}

		/// <inheritdoc />
		public async Task<User> GetUserByID(Guid userID)
		{
			return await this.Context.Users.FindAsync(userID);
		}

		/// <inheritdoc />
		public async Task<User> GetUserByUserName(string userName)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.UserName == userName);
		}

		/// <inheritdoc />
		public async Task<User> GetUserByPhoneNumber(string phoneNumber)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
		}

		/// <inheritdoc />
		public async Task<User> GetUserByEmailAddress(string emailAddress)
		{
			return await this.Context.Users.SingleOrDefaultAsync(user => user.EmailAddress == emailAddress);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<User>> GetUsers()
		{
			return await this.Context.Users.ToListAsync();
		}
		#endregion

		#region [Utility Methods]
		/// <summary>
		/// Logs in the specified user.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		///
		/// <exception cref="Exception">
		/// User does not exist.
		/// or
		/// The password is incorrect.
		/// </exception>
		private async Task<User> Login(User user, string password)
		{
			var databaseUser = user != null ? await this.GetUserByID(user.ID) : null;
			if (databaseUser == null)
				throw new KindlyException("The user or password are incorrect.");

			if (CheckIfPasswordMatches(databaseUser, password) == false)
				throw new KindlyException("The user or password are incorrect.");

			return databaseUser;
		}

		/// <summary>
		/// Creates the password hash and salt.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		private static void CreatePasswordHashAndSalt(User user, string password)
		{
			using (var hmac = new HMACSHA512())
			{
				hmac.Initialize();
				hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				user.PasswordHash = hmac.Hash;
				user.PasswordSalt = hmac.Key;
			}
		}

		/// <summary>
		/// Update the password hash and salt.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		private static bool CheckIfPasswordMatches(User user, string password)
		{
			using (var hmac = new HMACSHA512(user.PasswordSalt))
			{
				hmac.Initialize();
				hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				return hmac.Hash.SequenceEqual(user.PasswordHash);
			}
		}
		#endregion
	}
}