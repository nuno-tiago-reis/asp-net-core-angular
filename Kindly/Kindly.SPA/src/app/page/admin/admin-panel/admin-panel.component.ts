// components
import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap';

@Component
({
	selector: 'app-admin-panel',
	templateUrl: './admin-panel.component.html',
	styleUrls: ['./admin-panel.component.css']
})

export class AdminPanelComponent implements OnInit
{
	/**
	 * The admin tabs component.
	 */
	@ViewChild('adminTabs')
	public adminTabs: TabsetComponent;

	/**
	 * Creates an instance of the home component.
	 *
	 * @param activatedRoute The activated route.
	 * @param router The router.
	 */
	public constructor
	(
		private readonly activatedRoute: ActivatedRoute,
		private readonly router: Router
	)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		Promise.resolve(null).then(() =>
		{
			this.activatedRoute.queryParams.subscribe
			(
				parameters =>
				{
					const tab = parameters['tab'];

					switch (tab)
					{
						case 'members':
							this.selectTab(0);
							break;
						case 'pictures':
							this.selectTab(1);
							break;
					}
				}
			);
		});
	}

	/**
	 * Invoked when a tab is selected
	 *
	 * @param tabID The tab selected.
	 */
	public onSelectTab(tab: string)
	{
		this.router.navigate
		(
			[],
			{
				relativeTo: this.activatedRoute,
				queryParams: { tab: tab },
				queryParamsHandling: 'merge'
			}
		);
	}

	/**
	 * Selects a tab using the given id.
	 *
	 * @param tabID The tab to select.
	 */
	public selectTab(tabID: number)
	{
		this.adminTabs.tabs[tabID].active = true;
	}
}