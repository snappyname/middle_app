export class RegisterModel {
    email: string;
    password: string;

	constructor(partial?: Partial<RegisterModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
