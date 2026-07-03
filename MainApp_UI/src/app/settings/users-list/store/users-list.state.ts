import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Injectable } from '@angular/core';
import { UsersListModel } from './users-list.model';
import { SettingsApiService } from '../../settings.api.service';
import { LoadAllUsers, UpdateUser, UpdateUserSensors } from './users-list.actions';
import { tap } from 'rxjs';

@State<UsersListModel>({
	name: 'usersList',
	defaults: {
		users: [],
	},
})
@Injectable()
export class UsersListState {
	constructor(private apiService: SettingsApiService) {}

	@Selector()
	static allUsers(state: UsersListModel) {
		return state.users;
	}

	@Action(LoadAllUsers)
	setUser(ctx: StateContext<UsersListModel>) {
		return this.apiService.getUsers().pipe(
			tap((users) => {
				ctx.patchState({ users: users });
			}),
		);
	}

	@Action(UpdateUser)
	updateUser(ctx: StateContext<UsersListModel>, action: UpdateUser) {
		return this.apiService.updateUser(action.userId, action.userName, action.isAdmin).pipe(
			tap(() => {
				const state = ctx.getState();
				ctx.patchState({
					users: state.users.map((user) =>
						user.id === action.userId
							? { ...user, username: action.userName, isAdmin: action.isAdmin }
							: user,
					),
				});
			}),
		);
	}

	@Action(UpdateUserSensors)
	updateUserSensors(ctx: StateContext<UsersListModel>, action: UpdateUserSensors) {
		return this.apiService.updateUserSensors(action.sensors).pipe().subscribe();
	}
}
