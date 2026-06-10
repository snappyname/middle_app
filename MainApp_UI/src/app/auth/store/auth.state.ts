import { State, Action, Selector, StateContext, Store } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { LoadUserFromJWT, LoginAction, LoginWithGithub, LoginWithGoogle, Logout, RegisterAction } from './auth.actions';
import { AuthStateModel } from './auth.model';
import { AuthApiService } from '../auth.api.service';
import { tap } from 'rxjs';
import { TokensModel } from '../../../../models/generated/tokens.model';
import { jwtDecode } from 'jwt-decode';
import { JwtModel } from '../../core/api/JwtModel';

@State<AuthStateModel>({
	name: 'auth',
	defaults: {
		userId: '',
		userEmail: '',
		isAuthenticated: false,
	},
})
@Injectable()
export class AuthState {
	constructor(
		private apiService: AuthApiService,
		private store: Store,
	) {}

	@Selector()
	static isAuthenticated(state: AuthStateModel) {
		return state.isAuthenticated;
	}

	@Action(LoginAction)
	login(ctx: StateContext<AuthStateModel>, action: LoginAction) {
		return this.apiService.getTokes(action.email, action.password).pipe(
			tap((x) => {
				this.apiService.setTokens(x.jwtToken, x.refreshToken);
				this.store.dispatch(LoadUserFromJWT);
			}),
		);
	}

	@Action(RegisterAction)
	register(ctx: StateContext<AuthStateModel>, action: RegisterAction) {
		return this.apiService.register(action.email, action.password).pipe(
			tap((x: TokensModel) => {
				this.apiService.setTokens(x.jwtToken, x.refreshToken);
				this.store.dispatch(LoadUserFromJWT);
			}),
		);
	}

	@Action(LoadUserFromJWT)
	loadUserFromJWT(ctx: StateContext<AuthStateModel>) {
		if (this.apiService.getToken()) {
			const payload = jwtDecode<JwtModel>(this.apiService.getToken());
			ctx.patchState({
				userEmail: payload.userEmail,
				userId: payload.userId,
				isAuthenticated: true,
			});
		}
	}

	@Action(Logout)
	logout(ctx: StateContext<AuthStateModel>) {
		ctx.patchState({
			userEmail: '',
			userId: '',
			isAuthenticated: false,
		});
		this.apiService.logout();
	}

	@Action(LoginWithGoogle)
	loginWithGoogle(ctx: StateContext<AuthStateModel>, action: LoginWithGoogle) {
		return this.apiService.loginWithGoogle(action.idToken).pipe(
			tap((x) => {
				this.apiService.setTokens(x.jwtToken, x.refreshToken);
				this.store.dispatch(LoadUserFromJWT);
			}),
		);
	}

	@Action(LoginWithGithub)
	loginWithGithub(ctx: StateContext<AuthStateModel>, action: LoginWithGoogle) {
		return this.apiService.loginWitGithub(action.idToken).pipe(
			tap((x) => {
				this.apiService.setTokens(x.jwtToken, x.refreshToken);
				this.store.dispatch(LoadUserFromJWT);
			}),
		);
	}
}
