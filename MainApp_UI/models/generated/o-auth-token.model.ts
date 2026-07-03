export class OAuthTokenModel {
    idToken: string;

	constructor(partial?: Partial<OAuthTokenModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
