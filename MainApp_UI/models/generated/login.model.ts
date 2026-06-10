export class LoginModel {
    email: string;
    password: string;

	constructor(partial?: Partial<LoginModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
