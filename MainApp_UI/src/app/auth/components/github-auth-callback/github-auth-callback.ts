import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { Navigate } from '@ngxs/router-plugin';
import { LoginWithGithub } from '../../store/auth.actions';

@Component({
	selector: 'app-github-auth-callback',
	imports: [],
	templateUrl: './github-auth-callback.html',
	styleUrl: './github-auth-callback.css',
})
export class GithubAuthCallback implements OnInit {
	constructor(public store: Store) {}

	ngOnInit(): void {
		const code = new URLSearchParams(window.location.search).get('code');
		if (!code) {
			this.store.dispatch(new Navigate(['/']));
			return;
		}
		this.store.dispatch(new LoginWithGithub(code)).subscribe(() => this.store.dispatch(new Navigate(['/'])));
	}
}
