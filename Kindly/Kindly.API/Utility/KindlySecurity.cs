namespace Kindly.API.Utility
{
	/// <summary>
	/// The available user roles.
	/// </summary>
	public enum KindlyRoles
	{
		/// <summary>
		/// The administrator role.
		/// </summary>
		Administrator,

		/// <summary>
		/// The moderator role.
		/// </summary>
		Moderator,

		/// <summary>
		/// The member role.
		/// </summary>
		Member
	}

	/// <summary>
	/// The available security policies.
	/// </summary>
	public enum KindlyPolicies
	{
		/// <summary>
		/// Allows if the user has an elevated role.
		/// </summary>
		AllowIfElevatedUser,

		/// <summary>
		/// Allows if the user is the resource owner.
		/// </summary>
		AllowIfOwner
	}
}