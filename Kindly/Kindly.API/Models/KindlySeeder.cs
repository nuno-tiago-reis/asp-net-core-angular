using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;

namespace Kindly.API.Models
{
	public sealed class KindlySeeder
	{
		/// <summary>
		/// The user password.
		/// </summary>
		private const string UserPassword = "kindly#2018";

		/// <summary>
		/// The context.
		/// </summary>
		private readonly KindlyContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlySeeder"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public KindlySeeder(KindlyContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Seeds the users.
		/// </summary>
		public void SeedUsers()
		{
			if (this.context.Users.Any())
				return;

			// Read the seed file
			string data = File.ReadAllText("SeedData.Values.json");

			var users = JsonConvert.DeserializeObject<List<User>>(data);

			foreach (var user in users)
			{
				// Confirm the contacts
				user.PhoneNumberConfirmed = true;
				user.EmailAddressConfirmed = true;

				// Update the passwords
				UserRepository.CreatePasswordHashAndSalt(user, UserPassword);

				// Save the pictures
				this.context.AddRange(user.Pictures);
			}

			// Save the users
			this.context.AddRange(users);

			// Save the changes
			this.context.SaveChanges();
		}
	}
}