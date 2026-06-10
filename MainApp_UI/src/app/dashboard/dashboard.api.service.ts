import { RequestHandler } from '../core/api/request-handler';
import { UserModel } from '../../../models/generated/user.model';

export class DashboardApiService extends RequestHandler {
	public getMe() {
		return this.httpGet<UserModel>('/users/me');
	}
}
