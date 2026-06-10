import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthState } from './store/auth.state';

@NgModule({
	declarations: [],
	imports: [CommonModule, AuthRoutingModule, NgxsModule.forFeature([AuthState])],
	providers: [],
})
export class AuthModule {}
