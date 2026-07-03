import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, filter, switchMap, take, tap } from 'rxjs/operators';
import { AuthApiService } from '../../auth/auth.api.service';
import { jwtDecode } from 'jwt-decode';
import { JwtModel } from './JwtModel';
import { AppConsts } from '../../app.const';
import { Store } from '@ngxs/store';
import { Logout } from '../../auth/store/auth.actions';

@Injectable()
export class AuthRefreshInterceptor implements HttpInterceptor {
	private isRefreshing = false;

	private refreshSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

	constructor(
		private authApi: AuthApiService,
		private store: Store,
	) {}
	/* eslint-disable @typescript-eslint/no-explicit-any */
	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (AppConsts.anonymousRequests.some((url) => req.url.includes(url))) {
			return next.handle(req);
		}

		const token = this.authApi.getToken();
		if (token && !this.isTokenExpired(token)) {
			req = this.attachToken(req, token);
			return next.handle(req);
		}
		if (!this.isRefreshing) {
			this.isRefreshing = true;
			this.refreshSubject.next(null);

			return this.authApi.refreshToken().pipe(
				tap((tokens) => {
					this.isRefreshing = false;
					this.authApi.setTokens(tokens.jwtToken, tokens.refreshToken);
					this.refreshSubject.next(tokens.jwtToken);
				}),
				switchMap((tokens) => {
					req = this.attachToken(req, tokens.jwtToken);
					return next.handle(req);
				}),
				catchError((err) => {
					this.isRefreshing = false;
					this.store.dispatch(new Logout());
					return throwError(() => err);
				}),
			);
		} else {
			return this.refreshSubject.pipe(
				filter((t) => t !== null),
				take(1),
				switchMap((t) => next.handle(this.attachToken(req, t!))),
			);
		}
	}

	private attachToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
		return req.clone({
			setHeaders: {
				Authorization: `Bearer ${token}`,
			},
		});
	}

	private isTokenExpired(token: string): boolean {
		try {
			const payload = jwtDecode<JwtModel>(token);
			return payload.exp * 1000 <= Date.now();
		} catch {
			return true;
		}
	}
}
