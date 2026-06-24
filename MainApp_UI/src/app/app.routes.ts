import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { appRoutes } from './app-routes.const';
import { userResolver } from './dashboard/dashboard.resolver';
import { DashboardApiService } from './dashboard/dashboard.api.service';
import { SettingsApiService } from './settings/settings.api.service';
import { AdminGuard } from './settings/admin.guard';

export const routes: Routes = [
	{
		path: appRoutes.auth,
		loadChildren: () => import('../app/auth/auth.module').then((x) => x.AuthModule),
	},
	{
		path: appRoutes.dashboard,
		canActivate: [AuthGuard],
		resolve: {
			userResolver,
		},
		providers: [DashboardApiService],
		loadChildren: () => import('../app/dashboard/dashboard.module').then((x) => x.DashboardModule),
	},
	{
		path: appRoutes.settings,
		canActivate: [AuthGuard, AdminGuard],
		resolve: {
			//userResolver,
		},
		providers: [SettingsApiService],
		loadChildren: () => import('../app/settings/settings.module').then((x) => x.SettingsModule),
	},
	{
		path: '**',
		redirectTo: appRoutes.dashboard,
	},
];
