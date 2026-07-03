import { AfterViewInit, Component, DestroyRef, inject, Input } from '@angular/core';
import { Store } from '@ngxs/store';
import { DashboardState } from '../../store/dashboard.state';
import {
	CategoryScale,
	Chart,
	Legend,
	LinearScale,
	LineController,
	LineElement,
	PointElement,
	Title,
	Tooltip,
} from 'chart.js';
import { SensorModel } from '../../../../../models/generated/sensor.model';
import { Subscription } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
	selector: 'app-dashboard-chart',
	imports: [],
	templateUrl: './dashboard-chart.html',
	styleUrl: './dashboard-chart.scss',
})
export class DashboardChart implements AfterViewInit {
	@Input()
	public sensor: SensorModel;
	public store = inject(Store);
	chart: any;
	private dataSubscription: Subscription;

	constructor(private destroyRef: DestroyRef) {}

	private _sensorData!: { value: number; timestamp: number }[];

	get sensorData(): { value: number; timestamp: number }[] {
		return this._sensorData;
	}

	@Input()
	set sensorData(value: { value: number; timestamp: number }[]) {
		this._sensorData = value;
		this.dataSubscription.unsubscribe();
		this.chart.data.labels = this.formatUnixDates(this._sensorData.map((x) => x.timestamp.toString()));
		this.chart.data.datasets[0].data = this._sensorData.map((x) => x.value);
		this.chart.update();
	}

	ngAfterViewInit() {
		Chart.register(LineElement, LineController, PointElement, LinearScale, CategoryScale, Title, Tooltip, Legend);
		this.chart = new Chart(`lineChart${this.sensor.mappedSensorId}`, {
			type: 'line',
			data: {
				labels: [],
				datasets: [
					{
						label: this.sensor.sensorName,
						data: [],
						borderColor: 'blue',
						tension: 0.3,
						fill: false,
					},
				],
			},
			options: {
				responsive: true,
				plugins: {
					legend: {
						display: true,
					},
				},
				scales: {
					y: {
						beginAtZero: true,
					},
				},
			},
		});
		this.dataSubscription = this.store
			.select(DashboardState.sensorValues)
			.pipe(takeUntilDestroyed(this.destroyRef))
			.subscribe((values) => {
				const sensorValues = values.filter((x) => x.sensorMapId == this.sensor.mappedSensorId);
				this.chart.data.labels = this.formatUnixDates(
					sensorValues.map((x) => x.timestamp.toString()).slice(-5),
				);
				this.chart.data.datasets[0].data = sensorValues.map((x) => x.value).slice(-5);
				this.chart.update();
			});
	}

	formatUnixDates(timestamps: string[]): string[] {
		return timestamps.map((timestamp) => {
			const date = new Date(Number(timestamp) * 1000);
			return date.toLocaleString('ru-RU', {
				day: '2-digit',
				month: '2-digit',
				hour: '2-digit',
				minute: '2-digit',
				second: '2-digit',
			});
		});
	}
}
