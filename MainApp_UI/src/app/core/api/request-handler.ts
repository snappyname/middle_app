import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConst } from '../../app.const';

export abstract class RequestHandler {
	protected readonly http = inject(HttpClient);

	protected baseUrl: string = AppConst.baseUrl;

	protected setToken(jwt: string, refreshToken: string) {
		localStorage.setItem('access_token', jwt);
		localStorage.setItem('refresh_token', refreshToken);
	}

	protected logout() {
		localStorage.removeItem('access_token');
		localStorage.removeItem('refresh_token');
	}

	protected getJWT(): string {
		return localStorage.getItem('access_token') ?? '';
	}

	protected getRefreshToken(): string {
		return localStorage.getItem('refresh_token') || '';
	}

	protected httpGet<T>(url: string): Observable<T> {
		return this.http.get<T>(this.baseUrl + url, {});
	}

	protected httpPost<T, P = unknown>(url: string, payload: P): Observable<T> {
		return this.http.post<T>(this.baseUrl + url, payload);
	}
}
