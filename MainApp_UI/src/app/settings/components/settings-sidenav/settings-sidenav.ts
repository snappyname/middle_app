import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { Store } from '@ngxs/store';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes, settingsRoutes } from '../../../app-routes.const';

@Component({
	selector: 'app-settings-sidenav',
	imports: [MatButton],
	templateUrl: './settings-sidenav.html',
	styleUrl: './settings-sidenav.scss',
})
export class SettingsSidenav {
	public currentPath = '';
	public settingRoutes = settingsRoutes;

	constructor(private store: Store) {
		const routerState: string = this.store.selectSnapshot((state) => state.router).state.url;
		this.currentPath = routerState.split('/').pop() ?? '';
	}

	protected goToSensorsMapping() {
		this.store.dispatch(new Navigate([appRoutes.settings, settingsRoutes.sensorsMapping]));
	}

	protected goToUsers() {
		this.store.dispatch(new Navigate([appRoutes.settings, settingsRoutes.users]));
	}
}
