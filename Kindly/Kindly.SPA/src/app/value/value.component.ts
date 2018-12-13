import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component
({
	selector: 'app-value',
	templateUrl: './value.component.html',
	styleUrls: [ './value.component.css' ]
})

export class ValueComponent implements OnInit
{
	values: any;

	constructor (private readonly http: HttpClient)
	{
		// TODO
	}

	ngOnInit ()
	{
		this.getValues();
	}

	getValues()
	{
		this.http.get( 'https://kindly.com:44351/api/values').subscribe
		(
			response =>
			{
				this.values = response;
			},
			error =>
			{
				console.log(error);
			}
		);
	}
}