// components
import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/edit/member-edit.component';

@Injectable
({
	providedIn: 'root'
})

export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent>
{
	/**
	 * Creates an instance of the auth guard.
	 */
	public constructor()
	{
		// Nothing to do here.
	}

	/**
	 * Checks if a route can be deactivated.
	 */
	public canDeactivate (component: MemberEditComponent): Observable<boolean> | Promise<boolean> | boolean
	{
		if (component.editForm.dirty)
		{
			return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
		}

		return true;
	}
}