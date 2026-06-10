import { Component, inject } from '@angular/core';
import { Store } from '@ngxs/store';
import { DashboardState } from '../../store/dashboard.state';
import { LoadUserDetails } from '../../store/dashboard.actions';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../../../app-routes.const';
import { authRoutes } from '../../../auth/auth-routes.const';
import { Logout } from '../../../auth/store/auth.actions';

@Component({
	selector: 'app-dashboard',
	imports: [],
	templateUrl: './dashboard.html',
	styleUrl: './dashboard.css',
})
export class Dashboard {
	private store = inject(Store);

	public userId = this.store.selectSignal(DashboardState.userId);

	public userEmail = this.store.selectSignal(DashboardState.userEmail);

	public is2FAEnabled = this.store.selectSignal(DashboardState.twoFactorEnabled);

	public loadData() {
		this.store.dispatch(new LoadUserDetails());
	}

	public logout() {
		this.store.dispatch([new Logout(), new Navigate([`${appRoutes.auth}/${authRoutes.login}`])]);
	}
}
