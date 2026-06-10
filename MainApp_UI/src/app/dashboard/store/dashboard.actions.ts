import { UserModel } from '../../../../models/generated/user.model';

export class LoadUserDetails {
	static readonly type = '[Dashboard] LoadUserDetails';
}

export class SetUserDetails {
	static readonly type = '[Dashboard] SetUserDetails';
	constructor(public user: UserModel) {}
}
