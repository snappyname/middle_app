export interface BroadcastMessageModel<T = any> {
	type: string;
	payload: T;
}
