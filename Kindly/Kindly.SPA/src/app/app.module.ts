import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';

@NgModule
({
	imports:
	[
		BrowserModule,
		HttpClientModule
	],
	providers:
	[
		// TODO
	],
	bootstrap:
	[
		AppComponent
	],
	declarations:
	[
		AppComponent,
		ValueComponent
	]
})

export class AppModule {}