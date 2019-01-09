// components
import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { ProfileEditorComponent } from '../page/profile/profile-editor/profile-editor.component';

@Injectable
({
	providedIn: 'root'
})

export class PreventUnsavedChangesGuard implements CanDeactivate<ProfileEditorComponent>
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
	public canDeactivate (component: ProfileEditorComponent): Observable<boolean> | Promise<boolean> | boolean
	{
		if (component.contactsForm.dirty || component.profileForm.dirty)
		{
			return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
		}

		return true;
	}
}