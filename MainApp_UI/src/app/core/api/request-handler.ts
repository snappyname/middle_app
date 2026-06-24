import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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

	//protected httpGet<T>(url: string): Observable<T> {
	//	return this.http.get<T>(this.baseUrl + url, {});
	//}

	protected httpGet<T>(
		url: string,
		params?: Record<string, any>,
		headers?: HttpHeaders | Record<string, string>,
	): Observable<T> {
		const httpParams = this.buildParams(params);
		const httpHeaders = this.mergeHeaders(headers);

		return this.http.get<T>(`${this.baseUrl}${url}`, {
			params: httpParams,
			headers: httpHeaders,
		});
	}

	protected httpPost<T>(
		url: string,
		body?: any,
		params?: Record<string, any>,
		headers?: HttpHeaders | Record<string, string>,
	): Observable<T> {
		const httpParams = this.buildParams(params);
		const httpHeaders = this.mergeHeaders(headers);

		return this.http.post<T>(`${this.baseUrl}${url}`, body, {
			params: httpParams,
			headers: httpHeaders,
		});
	}

	protected getRefreshToken(): string {
		return localStorage.getItem('refresh_token') || '';
	}

	private buildParams(params?: Record<string, any>): HttpParams {
		let httpParams = new HttpParams();

		if (params) {
			Object.keys(params).forEach((key) => {
				const value = params[key];

				if (value !== null && value !== undefined && value !== '') {
					if (value instanceof Date) {
						httpParams = httpParams.append(key, value.toISOString());
					} else if (Array.isArray(value)) {
						value.forEach((item) => {
							httpParams = httpParams.append(key, item.toString());
						});
					} else {
						httpParams = httpParams.append(key, value.toString());
					}
				}
			});
		}

		return httpParams;
	}

	private getHeaders(): HttpHeaders {
		return new HttpHeaders({
			'Content-Type': 'application/json',
		});
	}

	private mergeHeaders(customHeaders?: HttpHeaders | Record<string, string>): HttpHeaders {
		let headers = this.getHeaders();

		if (!customHeaders) {
			return headers;
		}

		if (customHeaders instanceof HttpHeaders) {
			customHeaders.keys().forEach((key) => {
				const value = customHeaders.get(key);
				if (value !== null) {
					headers = headers.set(key, value);
				}
			});

			return headers;
		}

		Object.entries(customHeaders).forEach(([key, value]) => {
			headers = headers.set(key, value);
		});

		return headers;
	}
}
