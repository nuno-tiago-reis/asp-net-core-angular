// components
import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';

// services
import { AuthService } from '../-services/auth/auth.service';

@Directive
({
	selector: '[appHasRole]'
})

export class HasRoleDirective implements OnInit
{
	/**
	 * The required roles.
	 */
	@Input('appHasRole')
	public requiredRoles: string[];

	/**
	 * Whether the directive is visible.
	 */
	public isVisible: boolean;

	/**
	 * Creates an instance of the has role directive.
	 *
	 * @param viewContainerRef The view container reference.
	 * @param templateRef The template reference.
	 * @param authApi The auth service.
	 */
	public constructor
	(
		private readonly viewContainerRef: ViewContainerRef,
		private readonly templateRef: TemplateRef<any>,
		private readonly authApi: AuthService
	)
	{
		// nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit(): void
	{
		if (this.authApi.hasRoles())
		{
			this.isVisible = false;
			this.viewContainerRef.clear();
		}

		if (this.authApi.isInRoles(this.requiredRoles))
		{
			if (this.isVisible === false)
			{
				this.isVisible = true;
				this.viewContainerRef.createEmbeddedView(this.templateRef);
			}
		}
		else
		{
			this.isVisible = false;
			this.viewContainerRef.clear();
		}
	}
}