import { Component, inject } from '@angular/core';
import { Store } from '@ngxs/store';
import { SensorModel } from '../../../../../models/generated/sensor.model';
import { DashboardState } from '../../store/dashboard.state';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { DashboardHeader } from '../dashboard-header/dashboard-header';
import { DashboardChart } from '../dashboard-chart/dashboard-chart';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DashboardApiService } from '../../dashboard.api.service';

@Component({
	selector: 'app-dashboard-sensor',
	imports: [
		DashboardHeader,
		DashboardChart,
		MatFormFieldModule,
		MatDatepickerModule,
		MatInput,
		FormsModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatIconModule,
		FormsModule,
		MatFormFieldModule,
	],
	providers: [provideNativeDateAdapter()],
	templateUrl: './dashboard-sensor.html',
	styleUrl: './dashboard-sensor.scss',
})
export class DashboardSensor {
	public sensor: SensorModel;
	startDate: Date | null = null;
	endDate: Date | null = null;
	public sensorValues: {
		timestamp: number;
		value: number;
	}[];
	protected sensorPointsCount = 20;
	private store = inject(Store);

	constructor(
		private route: ActivatedRoute,
		private apiService: DashboardApiService,
	) {
		const id = this.route.snapshot.paramMap.get('id');
		const currentSensor = this.store.selectSnapshot(DashboardState.sensorMap)?.find((x) => x.mappedSensorId == id);
		if (currentSensor) {
			this.sensor = currentSensor;
		}
	}

	getUnixDates(date: Date) {
		return date ? Math.floor(date.getTime() / 1000) : null;
	}

	loadValues() {
		if (!this.startDate || !this.endDate) {
			return;
		}
		const startDate = this.getUnixDates(this.startDate);
		const endDate = this.getUnixDates(this.endDate);
		if (!startDate || !endDate) {
			return;
		}
		this.apiService
			.getSensorValues(this.sensor.mappedSensorId, startDate, endDate, 20)
			.subscribe((x) => (this.sensorValues = x.data?.sensorValues ?? []));
	}
}
