using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Kindly.API.Models.Repositories.Roles;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility.Settings;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Kindly.API.Models
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public sealed class KindlySeeder
	{
		#region [Constants]
		/// <summary>
		/// The administrator role name.
		/// </summary>
		private const string AdministratorRole = "Administrator";

		/// <summary>
		/// The moderator role name.
		/// </summary>
		private const string ModeratorRole = "Moderator";

		/// <summary>
		/// The member role name.
		/// </summary>
		private const string MemberRole = "Member";

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
		private readonly UserManager<User> UserManager;

		/// <summary>
		/// The role manager.
		/// </summary>
		private readonly RoleManager<Role> RoleManager;

		/// <summary>S
		/// The cloudinary proxy.
		/// </summary>
		private readonly Cloudinary Cloudinary;

		/// <summary>
		/// The administrator user.
		/// </summary>
		private readonly User Administrator = new User
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
				this.UserManager = userManager;
				this.RoleManager = roleManager;
				this.Cloudinary = new Cloudinary(new Account
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
			if (!this.RoleManager.Roles.Any())
			{
				var roles = new[]
				{
					new Role { Name = AdministratorRole },
					new Role { Name = ModeratorRole },
					new Role { Name = MemberRole }
				};

				// Create the roles
				foreach (var role in roles)
				{
					this.RoleManager.CreateAsync(role).Wait();
				}
			}

			if (!this.UserManager.Users.Any(u => u.UserName == Administrator.UserName))
			{
				// Create the administrator
				this.UserManager.CreateAsync(Administrator, DefaultUserPassword).Wait();
				// Create the administrators roles
				this.UserManager.AddToRoleAsync(Administrator, MemberRole).Wait();
				this.UserManager.AddToRoleAsync(Administrator, ModeratorRole).Wait();
				this.UserManager.AddToRoleAsync(Administrator, AdministratorRole).Wait();
			}

			if ( this.UserManager.Users.Any(u => u.UserName == Administrator.UserName) && this.UserManager.Users.Count() == 1)
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
						var uploadParameters = new ImageUploadParams
						{
							File = new FileDescription(picture.Url),
							Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
						};

						var uploadResult = this.Cloudinary.Upload(uploadParameters);
						if (uploadResult.Error != null)
							throw new ArgumentException("There was an error uploading the picture: " + uploadResult.Error.Message);

						picture.Url = uploadResult.Uri.ToString();
						picture.PublicID = uploadResult.PublicId;

						Console.WriteLine($"Uploaded picture {picture.PublicID} for {user.UserName}");
					}

					// Create the user
					this.UserManager.CreateAsync(user, DefaultUserPassword).Wait();
					// Create the users roles
					this.UserManager.AddToRoleAsync(user, MemberRole).Wait();
				}
			}
		}
		#endregion
	}
}