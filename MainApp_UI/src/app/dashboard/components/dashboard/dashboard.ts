import { Component, inject } from '@angular/core';
import { DashboardHeader } from '../dashboard-header/dashboard-header';
import { Store } from '@ngxs/store';
import { DashboardState } from '../../store/dashboard.state';
import { DashboardChart } from '../dashboard-chart/dashboard-chart';
import { MatButton } from '@angular/material/button';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../../../app-routes.const';

@Component({
	selector: 'app-dashboard',
	imports: [DashboardHeader, DashboardChart, MatButton],
	templateUrl: './dashboard.html',
	styleUrl: './dashboard.scss',
})
export class Dashboard {
	private store = inject(Store);
	sensorsMap = this.store.selectSignal(DashboardState.sensorMap);

	protected navigateToSensor(mappedSensorId: string) {
		this.store.dispatch(new Navigate([appRoutes.dashboard, mappedSensorId]));
	}
}
