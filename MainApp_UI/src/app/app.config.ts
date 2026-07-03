import {
	ApplicationConfig,
	inject,
	provideBrowserGlobalErrorListeners,
	provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { withNgxsReduxDevtoolsPlugin } from '@ngxs/devtools-plugin';
import { withNgxsFormPlugin } from '@ngxs/form-plugin';
import { withNgxsLoggerPlugin } from '@ngxs/logger-plugin';
import { withNgxsRouterPlugin } from '@ngxs/router-plugin';
import { withNgxsWebSocketPlugin } from '@ngxs/websocket-plugin';
import { provideStore as provideStore_alias } from '@ngxs/store';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthRefreshInterceptor } from './core/api/refresh-token-interceptor';
import { environment } from '../environment/environment';
import { HttpLink } from 'apollo-angular/http';
import { provideApollo } from 'apollo-angular';
import { InMemoryCache } from '@apollo/client/cache';

export const appConfig: ApplicationConfig = {
	providers: [
		provideApollo(() => {
			const httpLink = inject(HttpLink);

			return {
				link: httpLink.create({
					uri: environment.graphqlUrl,
				}),
				cache: new InMemoryCache(),
			};
		}),

		provideBrowserGlobalErrorListeners(),
		provideZoneChangeDetection({ eventCoalescing: true }),
		provideRouter(routes),
		provideStore_alias(
			[],
			...(environment.production ? [] : [withNgxsReduxDevtoolsPlugin(), withNgxsLoggerPlugin()]),
			withNgxsFormPlugin(),
			withNgxsRouterPlugin(),
			withNgxsWebSocketPlugin(),
		),
		provideHttpClient(withInterceptorsFromDi()),
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthRefreshInterceptor,
			multi: true,
		},
	],
};
