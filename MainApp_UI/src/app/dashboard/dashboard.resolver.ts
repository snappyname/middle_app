import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';
import { Store } from '@ngxs/store';
import { LoadUserSensorsMapping } from './store/dashboard.actions';
import { DashboardState } from './store/dashboard.state';
import { filter, take } from 'rxjs/operators';
import { SensorModel } from '../../../models/generated/sensor.model';

export const DashboardResolver: ResolveFn<SensorModel[]> = () => {
	const store = inject(Store);

	if (store.selectSnapshot(DashboardState.sensorMap) === null) {
		store.dispatch(new LoadUserSensorsMapping());
	}

	return store.select(DashboardState.sensorMap).pipe(
		filter((sensorMap) => sensorMap !== null),
		take(1),
	);
};
