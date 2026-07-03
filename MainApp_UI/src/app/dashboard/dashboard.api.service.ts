import { RequestHandler } from '../core/api/request-handler';
import { SensorModel } from '../../../models/generated/sensor.model';
import { inject } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';

export class DashboardApiService extends RequestHandler {
	private apollo = inject(Apollo);

	public getUserSensors() {
		return this.httpGet<SensorModel[]>('/users/my-sensors');
	}

	public getSensorValues(sensorId: string, startTime: number, endTime: number, count: number) {
		return this.apollo.query<{
			sensorValues: {
				timestamp: number;
				value: number;
			}[];
		}>({
			query: gql`
				query GetSensorValues($sensorId: UUID!, $startTime: Long!, $endTime: Long!, $count: Int!) {
					sensorValues(sensorId: $sensorId, startTime: $startTime, endTime: $endTime, count: $count) {
						timestamp
						value
					}
				}
			`,
			variables: {
				sensorId,
				startTime,
				endTime,
				count,
			},
		});
	}
}
