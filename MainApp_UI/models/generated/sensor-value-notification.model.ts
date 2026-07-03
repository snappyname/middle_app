export class SensorValueNotificationModel {
	sensorMapId: string;
	sensorId: number;
	sensorType: number;
	sensorName: string;
	value: string;
	timestamp: number;

	constructor(partial?: Partial<SensorValueNotificationModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
