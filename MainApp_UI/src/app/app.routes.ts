import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { appRoutes } from './app-routes.const';
import { userResolver } from './dashboard/dashboard.resolver';
import { DashboardApiService } from './dashboard/dashboard.api.service';

export const routes: Routes = [
	{
		path: appRoutes.auth,
		loadChildren: () => import('../app/auth/auth.module').then((x) => x.AuthModule),
	},
	{
		path: appRoutes.user,
		canActivate: [AuthGuard],
		resolve: {
			userResolver,
		},
		providers: [DashboardApiService],
		loadChildren: () => import('../app/dashboard/dashboard.module').then((x) => x.DashboardModule),
	},
	{
		path: '**',
		redirectTo: appRoutes.user,
	},
];
