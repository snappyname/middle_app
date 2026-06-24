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
import { LoadAllUsers } from '../../store/users-list.actions';
import { UsersListState } from '../../store/users-list.state';
import { UserModel } from '../../../../../../models/generated/user.model';
import { MatIcon } from '@angular/material/icon';
import { MatMiniFabButton } from '@angular/material/button';
import { MatDrawer, MatDrawerContainer } from '@angular/material/sidenav';
import { SettingsSidenav } from '../../../components/settings-sidenav/settings-sidenav';
import { MatDialog } from '@angular/material/dialog';
import { EditUserDialog } from '../edit-user-dialog/edit-user-dialog';
import { AuthState } from '../../../../auth/store/auth.state';

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
	],
	templateUrl: './users-list.html',
	styleUrl: './users-list.scss',
})
export class UsersList {
	displayedColumns: string[] = ['username', 'email', 'isAdmin', 'edit'];
	dataSource: Signal<UserModel[]>;
	dialog = inject(MatDialog);
	constructor(private store: Store) {
		this.store.dispatch(new LoadAllUsers());
		this.dataSource = this.store.selectSignal(UsersListState.allUsers);
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
}
