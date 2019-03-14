import { HasRoleDirective } from './has-role.directive';

describe('Directive: HasRole', () =>
{
	it('should create an instance', () =>
	{
		const directive = new HasRoleDirective(null, null, null);
		expect(directive).toBeTruthy();
	});
});