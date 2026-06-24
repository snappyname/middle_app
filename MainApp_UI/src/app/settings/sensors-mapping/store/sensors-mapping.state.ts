import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { SensorsMappingModel } from './sensors-mapping.model';
import { SettingsApiService } from '../../settings.api.service';
import { AddNewSensor, LoadAllSensorsMapping, RenameSensor } from './sensors-mapping.actions';
import { tap } from 'rxjs';
import { SensorModel } from '../../../../../models/generated/sensor.model';

@State<SensorsMappingModel>({
	name: 'sensorsList',
	defaults: {
		sensors: [],
	},
})
@Injectable()
export class SensorsMappingState {
	constructor(private apiService: SettingsApiService) {}

	@Selector()
	static allSensors(state: SensorsMappingModel) {
		return state.sensors;
	}

	@Action(LoadAllSensorsMapping)
	loadAllSensorsMapping(ctx: StateContext<SensorsMappingModel>) {
		return this.apiService.loadAllSensorMap().pipe(
			tap((x) => {
				ctx.patchState({
					sensors: x,
				});
			}),
		);
	}

	@Action(AddNewSensor)
	addNewSensor(ctx: StateContext<SensorsMappingModel>, action: AddNewSensor) {
		return this.apiService.addNewSensor(
			new SensorModel({
				sensorId: action.sensorId,
				sensorType: action.sensorType,
				sensorName: action.sensorName,
			}),
		);
	}

	@Action(RenameSensor)
	renameSensor(ctx: StateContext<SensorsMappingModel>, action: RenameSensor) {
		return this.apiService.renameSensor(action.mappedSensorId, action.newSensorName).pipe(
			tap(() => {
				const state = ctx.getState();
				ctx.patchState({
					sensors: state.sensors.map((sensor) =>
						sensor.mappedSensorId === action.mappedSensorId
							? { ...sensor, sensorName: action.newSensorName }
							: sensor,
					),
				});
			}),
		);
	}
}
