export class UserModel {
    id: string;
    userName: string;

	constructor(partial?: Partial<UserModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
