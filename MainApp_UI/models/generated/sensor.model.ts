export class SensorModel {
	mappedSensorId: string;
	sensorType: number;
	sensorId: number;
	sensorName: string;

	constructor(partial?: Partial<SensorModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
