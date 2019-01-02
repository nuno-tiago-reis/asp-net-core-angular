import { TestBed, inject } from '@angular/core/testing';
import { LikesService } from './likes.service';

describe('Service: Likes', () =>
{
	beforeEach(() =>
	{
		TestBed.configureTestingModule
		({
			providers: [ LikesService ]
		});
	});

	it('should ...', inject([ LikesService ], (service: LikesService) =>
	{
		expect(service).toBeTruthy();
	}));
});