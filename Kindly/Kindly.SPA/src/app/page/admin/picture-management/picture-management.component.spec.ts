import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PictureManagementComponent } from './picture-management.component';

describe('PictureManagementComponent', () =>
{
	let component: PictureManagementComponent;
	let fixture: ComponentFixture<PictureManagementComponent>;

	beforeEach(async(() =>
	{
		TestBed.configureTestingModule
		({
			declarations: [PictureManagementComponent]
		})
		.compileComponents();
	}));

	beforeEach(() =>
	{
		fixture = TestBed.createComponent(PictureManagementComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () =>
	{
		expect(component).toBeTruthy();
	});
});