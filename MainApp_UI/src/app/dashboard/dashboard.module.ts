import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { AuthState } from '../auth/store/auth.state';
import { DashboardApiService } from './dashboard.api.service';
import { DashboardState } from './store/dashboard.state';
import { DashboardRoutingModule } from './dashboard-routing.module';

@NgModule({
	declarations: [],
	imports: [CommonModule, DashboardRoutingModule, NgxsModule.forFeature([AuthState, DashboardState])],
	providers: [DashboardApiService],
})
export class DashboardModule {}
