import { SensorModel } from '../../../../../models/generated/sensor.model';

export class LoadAllUsers {
	static readonly type = '[Users List] Load All Users';
}

export class UpdateUser {
	static readonly type = '[Users List] Update User';
	constructor(
		public userId: string,
		public userName: string,
		public isAdmin: boolean,
	) {}
}

export class UpdateUserSensors {
	static readonly type = '[Users List] Update User Sensors';
	constructor(public sensors: SensorModel[]) {}
}
