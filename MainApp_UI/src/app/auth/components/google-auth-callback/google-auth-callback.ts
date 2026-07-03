import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { LoginWithGoogle } from '../../store/auth.actions';
import { Navigate } from '@ngxs/router-plugin';

@Component({
	selector: 'app-google-auth-callback',
	imports: [],
	templateUrl: './google-auth-callback.html',
	styleUrl: './google-auth-callback.css',
})
export class GoogleAuthCallback implements OnInit {
	constructor(public store: Store) {}

	ngOnInit(): void {
		const code = new URLSearchParams(window.location.search).get('code');
		if (!code) {
			this.store.dispatch(new Navigate(['/']));
			return;
		}
		this.store.dispatch(new LoginWithGoogle(code)).subscribe(() => this.store.dispatch(new Navigate(['/'])));
	}
}
