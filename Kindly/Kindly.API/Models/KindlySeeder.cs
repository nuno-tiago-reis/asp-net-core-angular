using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility.Settings;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kindly.API.Models
{
	public sealed class KindlySeeder
	{
		#region [Constants]
		/// <summary>
		/// The user password.
		/// </summary>
		private const string UserPassword = "kindly#2018";
		#endregion

		#region [Properties]
		/// <summary>
		/// The context.
		/// </summary>
		private readonly KindlyContext context;

		/// <summary>
		/// The cloudinary proxy.
		/// </summary>
		private readonly Cloudinary cloudinary;
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="KindlySeeder"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		/// <param name="cloudinarySettings">The cloudinary settings.</param>
		public KindlySeeder(KindlyContext context, IOptions<CloudinarySettings> cloudinarySettings)
		{
			this.context = context;
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
			if (this.context.Users.Any())
				return;

			// Read the seed file
			string data = File.ReadAllText("SeedData.Values.json");

			// Deserialize the users
			var users = JsonConvert.DeserializeObject<List<User>>(data);

			foreach (var user in users)
			{
				// Create the password
				UserRepository.CreatePasswordHashAndSalt(user, UserPassword);

				// Upload the pictures
				foreach (var picture in user.Pictures)
				{
					var uploadParameters = new ImageUploadParams
					{
						File = new FileDescription(picture.Url),
						Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
					};

					var uploadResult = this.cloudinary.Upload(uploadParameters);
					if (uploadResult.Error != null)
						throw new ArgumentException("There was an error uploading the picture: " + uploadResult.Error.Message);

					picture.Url = uploadResult.Uri.ToString();
					picture.PublicID = uploadResult.PublicId;

					Console.WriteLine($"Uploaded picture {picture.PublicID} for {user.UserName}");
				}

				// Save the pictures
				this.context.AddRange(user.Pictures);

				Console.WriteLine($"Uploaded pictures for {user.UserName}");
			}

			// Save the users
			this.context.AddRange(users);

			// Save the changes
			this.context.SaveChanges();
		}
		#endregion
	}
}