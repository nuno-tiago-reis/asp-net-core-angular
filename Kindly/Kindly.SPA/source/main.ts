// components
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

// modules
import { AppModule } from './app/app.module';

// environment
import { environment } from './environments/environment';

if (environment.production)
{
	enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule).catch(error => console.error(error));