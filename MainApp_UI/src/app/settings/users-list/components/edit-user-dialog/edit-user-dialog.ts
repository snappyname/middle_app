import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogClose,
	MatDialogContent,
	MatDialogTitle,
} from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { Store } from '@ngxs/store';
import { MatCheckbox } from '@angular/material/checkbox';
import { UpdateUser } from '../../store/users-list.actions';

@Component({
	selector: 'app-edit-user-dialog',
	imports: [
		MatButton,
		MatDialogActions,
		MatDialogClose,
		MatDialogContent,
		MatDialogTitle,
		ReactiveFormsModule,
		MatFormField,
		MatLabel,
		FormsModule,
		MatInput,
		MatCheckbox,
	],
	templateUrl: './edit-user-dialog.html',
	styleUrl: './edit-user-dialog.scss',
})
export class EditUserDialog {
	data = inject(MAT_DIALOG_DATA);
	store = inject(Store);

	protected saveUser() {
		this.store.dispatch(new UpdateUser(this.data.userId, this.data.originalName, this.data.isAdmin));
	}
}
