import { Component, inject, Signal } from '@angular/core';
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
import { Store } from '@ngxs/store';
import { LoadAllUsers, UpdateUserSensors } from '../../store/users-list.actions';
import { UsersListState } from '../../store/users-list.state';
import { UserModel } from '../../../../../../models/generated/user.model';
import { MatIcon } from '@angular/material/icon';
import { MatButton, MatMiniFabButton } from '@angular/material/button';
import { MatDrawer, MatDrawerContainer } from '@angular/material/sidenav';
import { SettingsSidenav } from '../../../components/settings-sidenav/settings-sidenav';
import { MatDialog } from '@angular/material/dialog';
import { EditUserDialog } from '../edit-user-dialog/edit-user-dialog';
import { AuthState } from '../../../../auth/store/auth.state';
import { LoadAllSensorsMapping } from '../../../sensors-mapping/store/sensors-mapping.actions';
import { SensorsMappingState } from '../../../sensors-mapping/store/sensors-mapping.state';
import { SensorModel } from '../../../../../../models/generated/sensor.model';
import { MatFormField, MatLabel } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
	selector: 'app-users-list',
	imports: [
		MatTable,
		MatColumnDef,
		MatHeaderCell,
		MatCell,
		MatCellDef,
		MatHeaderCellDef,
		MatHeaderRowDef,
		MatRowDef,
		MatRow,
		MatHeaderRow,
		MatIcon,
		MatMiniFabButton,
		MatDrawerContainer,
		MatDrawer,
		SettingsSidenav,
		MatFormField,
		MatLabel,
		MatSelect,
		MatOption,
		MatButton,
		ReactiveFormsModule,
	],
	templateUrl: './users-list.html',
	styleUrl: './users-list.scss',
})
export class UsersList {
	displayedColumns: string[] = ['username', 'email', 'isAdmin', 'edit', 'assignSensor', 'save'];
	dataSource: Signal<UserModel[]>;
	dialog = inject(MatDialog);
	sensorsList: Signal<SensorModel[]>;
	sensorControls = new Map<string, FormControl>();

	constructor(private store: Store) {
		this.store.dispatch([new LoadAllUsers(), new LoadAllSensorsMapping()]);
		this.dataSource = this.store.selectSignal(UsersListState.allUsers);
		this.sensorsList = this.store.selectSignal(SensorsMappingState.allSensors);
	}

	editUser(userModel: UserModel) {
		this.dialog.open(EditUserDialog, {
			data: {
				userId: userModel.id,
				originalName: userModel.username,
				isAdmin: userModel.isAdmin,
				isCurrentUser: this.store.selectSnapshot(AuthState.userId) == userModel.id,
			},
		});
	}

	getControl(element: UserModel): FormControl {
		if (!this.sensorControls.has(element.id)) {
			this.sensorControls.set(element.id, new FormControl(element.assignedSensors ?? []));
		}
		return this.sensorControls.get(element.id)!;
	}

	saveUser(element: UserModel) {
		const selected = this.getControl(element).value;
		const sensors = this.sensorsList().filter((x) => selected.includes(x.mappedSensorId));
		this.store.dispatch(new UpdateUserSensors(sensors));
	}
}
