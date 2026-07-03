import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthState } from './store/auth.state';
import { Store } from '@ngxs/store';
import { Navigate } from '@ngxs/router-plugin';
import { appRoutes } from '../app-routes.const';
import { authRoutes } from './auth-routes.const';
import { LoadUserFromJWT } from './store/auth.actions';
import { map, Observable, of, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
	constructor(private store: Store) {}

	canActivate(): Observable<boolean> {
		const isAuth = this.store.selectSnapshot(AuthState.isAuthenticated);

		if (isAuth) {
			return of(true);
		}

		return this.store.dispatch(new LoadUserFromJWT()).pipe(
			map(() => this.store.selectSnapshot(AuthState.isAuthenticated)),
			tap((isAuthenticated) => {
				if (!isAuthenticated) {
					this.store.dispatch(new Navigate([`/${appRoutes.auth}/${authRoutes.login}`]));
				}
			}),
		);
	}
}
