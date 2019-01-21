namespace Kindly.API.Controllers
{
	/// <summary>
	/// Implements the resource owner handler.
	/// </summary>
	/// 
	/// <seealso cref="GenericHandler{ElevatedUserRequirement}" />
	public abstract class ResourceOwnerHandler<TResource> : GenericHandler<ResourceOwnerRequirement, TResource>
	{
		// Nothing to do here.
	}
}