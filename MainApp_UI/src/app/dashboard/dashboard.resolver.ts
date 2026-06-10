import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';
import { DashboardApiService } from './dashboard.api.service';
import { Store } from '@ngxs/store';
import { tap } from 'rxjs';
import { SetUserDetails } from './store/dashboard.actions';
import { UserModel } from '../../../models/generated/user.model';

export const userResolver: ResolveFn<UserModel> = () => {
	const dashboardApi = inject(DashboardApiService);
	const store = inject(Store);
	return dashboardApi.getMe().pipe(tap((user) => store.dispatch(new SetUserDetails(user))));
};
