export class LoginAction {
	static readonly type = '[Auth] Login';
	constructor(
		public email: string,
		public password: string,
	) {}
}

export class RegisterAction {
	static readonly type = '[Auth] Register';
	constructor(
		public email: string,
		public password: string,
	) {}
}

export class LoadUserFromJWT {
	static readonly type = '[Auth] LoadUserFromJWT';
}

export class Logout {
	static readonly type = '[Auth] Logout';
}

export class LoginWithGoogle {
	static readonly type = '[Auth] Login With Google';
	constructor(public idToken: string) {}
}
export class LoginWithGithub {
	static readonly type = '[Auth] Login With Github';
	constructor(public idToken: string) {}
}
