import { SensorValueNotificationModel } from '../../../../models/generated/sensor-value-notification.model';

export class AddNewSensorValue {
	static readonly type = '[Dashboard] Add new sensor value';
	constructor(public sensorValue: SensorValueNotificationModel[]) {}
}

export class LoadUserSensorsMapping {
	static readonly type = '[Dashboard] Load user sensors mapping';
}

export class LoadStatisticsSensorValues {
	static readonly type = '[Dashboard] Load statistics sensors mapping';
	constructor(
		public mappedSensorId: string,
		public startTime: number,
		public endTime: number,
		public count: number,
	) {}
}
