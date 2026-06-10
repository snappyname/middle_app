import { Component, signal } from '@angular/core';
import { Store } from '@ngxs/store';
import { RegisterAction } from '../../store/auth.actions';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../../../app-routes.const';
import { authRoutes } from '../../auth-routes.const';

@Component({
	selector: 'app-register',
	imports: [],
	templateUrl: './register.html',
	styleUrl: './register.css',
})
export class Register {
	email = signal('');

	password = signal('');

	constructor(public store: Store) {}

	register() {
		this.store.dispatch(new RegisterAction(this.email(), this.password()));
	}

	navigateToLogin() {
		this.store.dispatch(new Navigate([`/${appRoutes.auth}/${authRoutes.login}`]));
	}
}
