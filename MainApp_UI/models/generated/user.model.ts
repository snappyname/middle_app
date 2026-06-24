export class UserModel {
	id: string;
	username: string;
	email: string;
	isAdmin: boolean;

	constructor(partial?: Partial<UserModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
