import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { AuthState } from '../auth/store/auth.state';
import { SettingsApiService } from './settings.api.service';
import { SettingsRoutingModule } from './settings-routing.module';
import { UsersListState } from './users-list/store/users-list.state';
import { SensorsMappingState } from './sensors-mapping/store/sensors-mapping.state';

@NgModule({
	declarations: [],
	imports: [
		CommonModule,
		SettingsRoutingModule,
		NgxsModule.forFeature([AuthState, UsersListState, SensorsMappingState]),
	],
	providers: [SettingsApiService],
})
export class SettingsModule {}
