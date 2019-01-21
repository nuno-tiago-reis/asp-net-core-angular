using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Kindly.API.Models.Repositories.Roles;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility.Settings;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kindly.API.Models
{
	/// <summary>
	/// Implements the kindly database seeder.
	/// </summary>
	public sealed class KindlySeeder
	{
		#region [Constants]
		/// <summary>
		/// The default user password.
		/// </summary>
		private const string DefaultUserPassword = "kindly#2018";

		/// <summary>
		/// The seed data values json file name.
		/// </summary>
		private const string SeedDataValuesJson = "SeedData.Values.json";
		#endregion

		#region [Properties]
		/// <summary>
		/// The user manager.
		/// </summary>
		private readonly UserManager<User> userManager;

		/// <summary>
		/// The role manager.
		/// </summary>
		private readonly RoleManager<Role> roleManager;

		/// <summary>S
		/// The cloudinary proxy.
		/// </summary>
		private readonly Cloudinary cloudinary;

		/// <summary>
		/// The administrator user.
		/// </summary>
		private readonly User administrator = new User
		{
			KnownAs = "Administrator",
			UserName = "administrator",
			Email = "nuno-tiago-reis@outlook.pt",
			PhoneNumber = "+9324323293",
			Gender = Gender.Undefined,
			BirthDate = DateTime.Parse("1991-12-08"),
			Introduction = "I administrate.",
			LookingFor = "Administradors.",
			Interests = "Administrating.",
			City = "Lisbon",
			Country = "Portugal"
		};
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="KindlySeeder"/> class.
		/// </summary>
		/// 
		/// <param name="userManager">The user manager.</param>
		/// <param name="roleManager">The role manager.</param>
		/// <param name="cloudinarySettings">The cloudinary settings.</param>
		public KindlySeeder
			(
				UserManager<User> userManager,
				RoleManager<Role> roleManager,
				IOptions<CloudinarySettings> cloudinarySettings
			)
			{
				this.userManager = userManager;
				this.roleManager = roleManager;
				this.cloudinary = new Cloudinary(new Account
				(
					cloudinarySettings.Value.Cloud,
					cloudinarySettings.Value.ApiKey,
					cloudinarySettings.Value.ApiSecret
				));
			}
			#endregion

		#region [Seed Methods]
		/// <summary>
		/// Seeds the users.
		/// </summary>
		public void SeedUsers()
		{
			if (!this.roleManager.Roles.Any())
			{
				var roles = Enum.GetNames(typeof(KindlyRoles));

				// Create the roles
				foreach (string role in roles)
				{
					this.roleManager.CreateAsync(new Role { Name = role }).Wait();
				}
			}

			if (!this.userManager.Users.Any(u => u.UserName == this.administrator.UserName))
			{
				// Create the administrator
				this.userManager.CreateAsync(this.administrator, DefaultUserPassword).Wait();
				// Create the administrators roles
				this.userManager.AddToRoleAsync(this.administrator, nameof(KindlyRoles.Member)).Wait();
				this.userManager.AddToRoleAsync(this.administrator, nameof(KindlyRoles.Moderator)).Wait();
				this.userManager.AddToRoleAsync(this.administrator, nameof(KindlyRoles.Administrator)).Wait();
			}

			if ( this.userManager.Users.Any(u => u.UserName == this.administrator.UserName) && this.userManager.Users.Count() == 1)
			{
				var users = JsonConvert.DeserializeObject<List<User>>
				(
					File.ReadAllText(SeedDataValuesJson)
				);

				// Create the users
				foreach (var user in users)
				{
					// Create the pictures
					foreach (var picture in user.Pictures)
					{
						try
						{
							var uploadParameters = new ImageUploadParams
							{
								File = new FileDescription(picture.Url),
								Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
							};

							var uploadResult = this.cloudinary.Upload(uploadParameters);
							if (uploadResult.Error != null)
								throw new ArgumentException("There was an error uploading the picture: " + uploadResult.Error.Message);

							picture.Url = uploadResult.SecureUri.ToString();
							picture.PublicID = uploadResult.PublicId;
							picture.IsApproved = true;

							Console.WriteLine($"Uploaded picture {picture.PublicID} for {user.UserName}");
						}
						catch(Exception exception)
						{
							Console.WriteLine(exception.Message);
							Console.WriteLine(exception.StackTrace);
						}
					}

					// Create the user
					this.userManager.CreateAsync(user, DefaultUserPassword).Wait();
					// Create the users roles
					this.userManager.AddToRoleAsync(user, nameof(KindlyRoles.Member)).Wait();
				}
			}
		}
		#endregion
	}
}