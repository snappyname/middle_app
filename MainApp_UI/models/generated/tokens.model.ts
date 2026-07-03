export class TokensModel {
    jwtToken: string;
    refreshToken: string;

	constructor(partial?: Partial<TokensModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
