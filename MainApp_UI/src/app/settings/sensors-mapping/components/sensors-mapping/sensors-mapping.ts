import { Component, inject } from '@angular/core';
import { MatDrawer, MatDrawerContainer } from '@angular/material/sidenav';
import { MatIcon } from '@angular/material/icon';
import { MatButton, MatMiniFabButton } from '@angular/material/button';
import { SettingsSidenav } from '../../../components/settings-sidenav/settings-sidenav';
import { MatError, MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { Store } from '@ngxs/store';
import { AddNewSensor, LoadAllSensorsMapping } from '../../store/sensors-mapping.actions';
import { SensorsMappingState } from '../../store/sensors-mapping.state';
import {
	MatCell,
	MatCellDef,
	MatColumnDef,
	MatHeaderCell,
	MatHeaderCellDef,
	MatHeaderRow,
	MatHeaderRowDef,
	MatRow,
	MatRowDef,
	MatTable,
} from '@angular/material/table';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { SENSORS_MAP } from '../../../../sensors/sensor-type-values';
import { KeyValuePipe } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { RenameSensorDialog } from '../rename-sensor-dialog/rename-sensor-dialog';
import { SensorModel } from '../../../../../../models/generated/sensor.model';

@Component({
	selector: 'app-sensors-mapping',
	imports: [
		MatDrawer,
		MatDrawerContainer,
		MatIcon,
		MatMiniFabButton,
		SettingsSidenav,
		MatFormField,
		MatLabel,
		MatInput,
		MatOption,
		MatSelect,
		MatTable,
		MatColumnDef,
		MatHeaderCellDef,
		MatCellDef,
		MatHeaderCell,
		MatCell,
		MatHeaderRowDef,
		MatRowDef,
		MatHeaderRow,
		MatRow,
		MatButton,
		ReactiveFormsModule,
		MatError,
		KeyValuePipe,
	],
	templateUrl: './sensors-mapping.html',
	styleUrl: './sensors-mapping.scss',
})
export class SensorsMapping {
	displayedColumns: string[] = ['sensorTypeName', 'sensorId', 'sensorName'];
	public sensorsMapping = SENSORS_MAP;
	dialog = inject(MatDialog);
	private store = inject(Store);
	sensorsMap = this.store.selectSignal(SensorsMappingState.allSensors);
	private fb = inject(FormBuilder);
	form = this.fb.group({
		sensorId: this.fb.control<number | null>(0, {
			validators: [Validators.required, Validators.min(0)],
		}),
		sensorType: this.fb.control<number | null>(0, {
			validators: [Validators.required],
		}),
		sensorName: ['', Validators.required],
	});

	constructor() {
		this.store.dispatch(new LoadAllSensorsMapping());
	}

	submit(): void {
		if (this.form.invalid) {
			this.form.markAllAsTouched();
			return;
		}
		this.store.dispatch(
			new AddNewSensor(
				this.form.value.sensorId ?? 0,
				this.form.value.sensorType ?? 0,
				this.form.value.sensorName ?? '',
			),
		);
	}

	protected openRenameDialog(sensor: SensorModel) {
		this.dialog.open(RenameSensorDialog, {
			data: {
				mappedSensorId: sensor.mappedSensorId,
				originalName: sensor.sensorName,
			},
		});
	}
}
