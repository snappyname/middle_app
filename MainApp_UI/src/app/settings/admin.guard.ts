import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Store } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { AuthState } from '../auth/store/auth.state';

@Injectable({ providedIn: 'root' })
export class AdminGuard implements CanActivate {
	constructor(private store: Store) {}

	canActivate(): Observable<boolean> {
		const isAdmin = this.store.selectSnapshot(AuthState.isAdmin);

		if (isAdmin) {
			return of(true);
		}
		return of(false);
	}
}
