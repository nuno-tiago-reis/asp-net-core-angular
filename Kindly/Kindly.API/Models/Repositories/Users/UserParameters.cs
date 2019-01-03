﻿using System;

namespace Kindly.API.Models.Repositories.Users
{
	public sealed class UserParameters : PaginationParameters
	{
		#region [Constants]
		/// <summary>
		/// The maximum age.
		/// </summary>
		private const int MaximumAllowedAge = 100;

		/// <summary>
		/// The minimum age.
		/// </summary>
		private const int MinimumAllowedAge = 18;
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		public Guid? UserID { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		public Gender? Gender { get; set; }

		/// <summary>
		/// The minimum age.
		/// </summary>
		private int? minimumAge;

		/// <summary>
		/// Gets or sets the minimum age.
		/// </summary>
		public int? MinimumAge
		{
			get { return this.minimumAge; }
			set { this.minimumAge = value.HasValue ? Math.Clamp(value.Value, MinimumAllowedAge, MaximumAllowedAge) : default(int?); }

		}

		/// <summary>
		/// The maximum age.
		/// </summary>
		private int? maximumAge;

		/// <summary>
		/// Gets or sets the maximum age.
		/// </summary>
		public int? MaximumAge
		{
			get { return this.maximumAge; }
			set { this.maximumAge = value.HasValue ? Math.Clamp(value.Value, MinimumAllowedAge, MaximumAllowedAge) : default(int?); }

		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="UserParameters" /> class.
		/// </summary>
		public UserParameters()
		{
			this.MinimumAge = MinimumAllowedAge;
			this.MaximumAge = MaximumAllowedAge;
		}
	}
}