import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { DashboardStateModel } from './dashboard.model';
import { DashboardApiService } from '../dashboard.api.service';
import { AddNewSensorValue, LoadStatisticsSensorValues, LoadUserSensorsMapping } from './dashboard.actions';
import { tap } from 'rxjs';

@State<DashboardStateModel>({
	name: 'dashboard',
	defaults: {
		sensorsMap: null,
		sensorValues: [],
	},
})
@Injectable()
export class DashboardState {
	constructor(private apiService: DashboardApiService) {}

	@Selector()
	static sensorMap(state: DashboardStateModel) {
		return state.sensorsMap;
	}

	@Selector()
	static sensorValues(state: DashboardStateModel) {
		return state.sensorValues;
	}

	@Action(AddNewSensorValue)
	addNewSensorValue(ctx: StateContext<DashboardStateModel>, action: AddNewSensorValue) {
		ctx.patchState({
			sensorValues: [...ctx.getState().sensorValues, ...action.sensorValue],
		});
	}

	@Action(LoadUserSensorsMapping)
	loadUserSensorsMapping(ctx: StateContext<DashboardStateModel>, action: LoadUserSensorsMapping) {
		this.apiService
			.getUserSensors()
			.pipe(
				tap((x) =>
					ctx.patchState({
						sensorsMap: x.flat(),
					}),
				),
			)
			.subscribe();
	}

	@Action(LoadStatisticsSensorValues)
	loadStatisticsSensorValues(ctx: StateContext<DashboardStateModel>, action: LoadStatisticsSensorValues) {
		this.apiService
			.getSensorValues(action.mappedSensorId, action.startTime, action.endTime, action.count)
			.pipe(
				tap((x) => {
					console.error(x);
				}),
			)
			.subscribe();
	}
}
