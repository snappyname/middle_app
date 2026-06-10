export class RefreshTokenModel {
    refreshToken: string;

	constructor(partial?: Partial<RefreshTokenModel>) {
		if (partial) {
			Object.assign(this, partial);
		}
	}
}
