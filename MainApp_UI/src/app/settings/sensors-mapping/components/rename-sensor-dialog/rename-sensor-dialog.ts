import { Component, inject } from '@angular/core';
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogClose,
	MatDialogContent,
	MatDialogTitle,
} from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { Store } from '@ngxs/store';
import { RenameSensor } from '../../store/sensors-mapping.actions';
import { FormsModule } from '@angular/forms';

@Component({
	selector: 'app-rename-sensor-dialog',
	imports: [
		MatFormField,
		MatLabel,
		MatInput,
		MatDialogContent,
		MatDialogTitle,
		MatDialogActions,
		MatButton,
		MatDialogClose,
		FormsModule,
	],
	templateUrl: './rename-sensor-dialog.html',
	styleUrl: './rename-sensor-dialog.scss',
})
export class RenameSensorDialog {
	data = inject(MAT_DIALOG_DATA);
	store = inject(Store);

	protected saveNewName() {
		this.store.dispatch(new RenameSensor(this.data.mappedSensorId, this.data.originalName ?? ''));
	}
}
