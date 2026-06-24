import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { DashboardStateModel } from './dashboard.model';
import { DashboardApiService } from '../dashboard.api.service';
import { SetUserDetails } from './dashboard.actions';

@State<DashboardStateModel>({
	name: 'dashboard',
	defaults: {
		userId: '',
		userEmail: '',
		twoFactorEnabled: false,
	},
})
@Injectable()
export class DashboardState {
	constructor(private apiService: DashboardApiService) {}

	@Selector()
	static userId(state: DashboardStateModel) {
		return state.userId;
	}

	@Selector()
	static userEmail(state: DashboardStateModel) {
		return state.userEmail;
	}

	@Selector()
	static twoFactorEnabled(state: DashboardStateModel) {
		return state.twoFactorEnabled;
	}

	@Action(SetUserDetails)
	setUser(ctx: StateContext<DashboardStateModel>, action: SetUserDetails) {
		ctx.patchState({ userId: action.user.id });
	}
}
