import { Component, signal } from '@angular/core';
import { Store } from '@ngxs/store';
import { LoginAction } from '../../store/auth.actions';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../../../app-routes.const';
import { authRoutes } from '../../auth-routes.const';
import { take } from 'rxjs';
import { AppConsts } from '../../../app.const';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
	selector: 'app-login',
	imports: [MatFormField, MatLabel, MatInput, MatButton, MatIcon],
	templateUrl: './login.html',
	styleUrl: './login.scss',
})
export class Login {
	email = signal('');

	password = signal('');

	constructor(public store: Store) {}

	login() {
		this.store
			.dispatch(new LoginAction(this.email(), this.password()))
			.pipe(take(1))
			.subscribe(() => this.store.dispatch(new Navigate([`/${appRoutes.user}`])));
	}

	protected navigateToRegister() {
		this.store.dispatch(new Navigate([`/${appRoutes.auth}/${authRoutes.register}`]));
	}

	protected loginByGoogle() {
		const params = new URLSearchParams(AppConsts.googleAuthSettings);
		window.location.href = AppConsts.googleAuthWindow + params.toString();
	}

	protected loginByGithub() {
		const params = new URLSearchParams(AppConsts.githubAuthSettings);
		window.location.href = AppConsts.githubAuthWindow + params.toString();
	}
}
