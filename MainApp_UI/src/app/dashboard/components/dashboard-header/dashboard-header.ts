import { Component } from '@angular/core';
import { MatFabButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { Store } from '@ngxs/store';
import { AuthState } from '../../../auth/store/auth.state';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../../../app-routes.const';

@Component({
	selector: 'app-dashboard-header',
	imports: [MatFabButton, MatIcon],
	templateUrl: './dashboard-header.html',
	styleUrl: './dashboard-header.css',
})
export class DashboardHeader {
	userEmail: string;
	isUserAdmin: boolean;

	constructor(private store: Store) {
		this.userEmail = this.store.selectSnapshot(AuthState.userEmail);
		this.isUserAdmin = this.store.selectSnapshot(AuthState.isAdmin);
	}

	protected goToSettings() {
		this.store.dispatch(new Navigate([appRoutes.settings]));
	}
}
