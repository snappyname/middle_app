import { SensorModel } from '../../../../models/generated/sensor.model';
import { SensorValueNotificationModel } from '../../../../models/generated/sensor-value-notification.model';

export interface DashboardStateModel {
	sensorsMap: SensorModel[] | null;
	sensorValues: SensorValueNotificationModel[];
}
