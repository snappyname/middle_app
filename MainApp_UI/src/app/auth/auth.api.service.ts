import { RequestHandler } from '../core/api/request-handler';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { AppConsts } from '../app.const';
import { LoginModel } from '../../../models/generated/login.model';
import { TokensModel } from '../../../models/generated/tokens.model';
import { RefreshTokenModel } from '../../../models/generated/refresh-token.model';
import { OAuthTokenModel } from '../../../models/generated/o-auth-token.model';

@Injectable({ providedIn: 'root' })
export class AuthApiService extends RequestHandler {
	public setTokens(jwt: string, refreshToken: string) {
		super.setToken(jwt, refreshToken);
	}

	public override logout() {
		super.logout();
	}

	public getToken(): string {
		return super.getJWT();
	}

	public getTokes(email: string, password: string): Observable<TokensModel> {
		return this.httpPost<TokensModel, LoginModel>(
			'/auth/login',
			new LoginModel({ email: email, password: password }),
		);
	}

	public register(email: string, password: string): Observable<TokensModel> {
		return this.httpPost<TokensModel, LoginModel>(
			'/auth/register',
			new LoginModel({ email: email, password: password }),
		);
	}

	public refreshToken(): Observable<TokensModel> {
		return this.httpPost<TokensModel, RefreshTokenModel>(
			'/auth/refreshToken',
			new RefreshTokenModel({ refreshToken: this.getRefreshToken() }),
		);
	}

	public loginWithGoogle(token: string): Observable<TokensModel> {
		return this.httpPost<TokensModel, OAuthTokenModel>(
			AppConsts.googleAuthRoute,
			new OAuthTokenModel({ idToken: token }),
		);
	}

	public loginWitGithub(token: string): Observable<TokensModel> {
		return this.httpPost<TokensModel, OAuthTokenModel>(
			AppConsts.githubAuthRoute,
			new OAuthTokenModel({ idToken: token }),
		);
	}
}
