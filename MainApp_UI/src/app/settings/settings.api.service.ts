import { RequestHandler } from '../core/api/request-handler';
import { Observable } from 'rxjs';
import { UserModel } from '../../../models/generated/user.model';
import { SensorModel } from '../../../models/generated/sensor.model';

export class SettingsApiService extends RequestHandler {
	public getUsers(): Observable<UserModel[]> {
		return this.httpGet<UserModel[]>('/admin/allUsers');
	}

	public loadAllSensorMap(): Observable<SensorModel[]> {
		return this.httpGet<SensorModel[]>('/sensors/allSensors');
	}

	public addNewSensor(sensor: SensorModel): Observable<void> {
		return this.httpPost('/admin/addNewSensor', sensor);
	}

	public renameSensor(mappedSensorId: string, sensorName: string): Observable<void> {
		return this.httpPost(
			'/sensors/renameSensor',
			{},
			{
				mappedSensorId: mappedSensorId,
				sensorName: sensorName,
			},
		);
	}

	public updateUser(userId: string, userName: string, isAdmin: boolean) {
		return this.httpPost('/admin/updateUser', {}, { userId: userId, userName: userName, isAdmin: isAdmin });
	}

	public updateUserSensors(sensors: SensorModel[]) {
		return this.httpPost('/admin/assignSensors', sensors);
	}
}
