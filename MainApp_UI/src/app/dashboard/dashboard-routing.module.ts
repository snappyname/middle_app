import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Dashboard } from './components/dashboard/dashboard';
import { DashboardSensor } from './components/dashboard-sensor/dashboard-sensor';

const routes: Routes = [
	{
		path: ``,
		component: Dashboard,
	},
	{
		path: ':id',
		component: DashboardSensor,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DashboardRoutingModule {}
