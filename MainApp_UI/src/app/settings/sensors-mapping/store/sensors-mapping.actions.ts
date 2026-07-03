export class LoadAllSensorsMapping {
	static readonly type = '[Sensor Map] Load All Sensor Mapping';
}

export class AddNewSensor {
	static readonly type = '[Sensor Map] Add new sensor';
	constructor(
		public sensorId: number,
		public sensorType: number,
		public sensorName: string,
	) {}
}

export class RenameSensor {
	static readonly type = '[Sensor Map] Rename sensor';
	constructor(
		public mappedSensorId: string,
		public newSensorName: string,
	) {}
}
