import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersList } from './users-list/components/users-list/users-list';
import { settingsRoutes } from '../app-routes.const';
import { SensorsMapping } from './sensors-mapping/components/sensors-mapping/sensors-mapping';

const routes: Routes = [
	{
		path: settingsRoutes.users,
		component: UsersList,
	},
	{
		path: settingsRoutes.sensorsMapping,
		component: SensorsMapping,
	},
	{
		path: '**',
		redirectTo: settingsRoutes.users,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SettingsRoutingModule {}
