import { Injectable, NgZone } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BroadcastMessageTypes } from './message-types';
import { AppConst } from '../../app.const';
import { AuthApiService } from '../../auth/auth.api.service';
import { BroadcastMessageModel } from './broadcast-message.model';
import { Store } from '@ngxs/store';
import { AddNewSensorValue } from '../../dashboard/store/dashboard.actions';

@Injectable({ providedIn: 'root' })
export class SignalRService {
	private hubConnection!: signalR.HubConnection;

	private readonly broadcastMethod: string = 'broadcast';

	constructor(
		private zone: NgZone,
		private apiService: AuthApiService,
		private store: Store,
	) {}

	public start(): void {
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(AppConst.sinalRUrl, {
				accessTokenFactory: () => this.apiService.getToken(),
			})
			.withAutomaticReconnect()
			.build();

		this.registerHandlers();

		this.hubConnection
			.start()
			.then(() => console.warn('[SignalR] connected'))
			.catch((err) => console.error('[SignalR] error', err));
	}

	public stop(): void {
		this.hubConnection?.stop();
	}

	private registerHandlers(): void {
		this.hubConnection.on(this.broadcastMethod, (message: BroadcastMessageModel) => {
			this.zone.run(() => {
				this.handleMessage(message);
			});
		});
	}

	private handleMessage(message: BroadcastMessageModel): void {
		switch (message.type) {
			case BroadcastMessageTypes.SensorUpdated:
				this.store.dispatch(new AddNewSensorValue(message.payload));
				break;
			default:
				console.error(message.payload);
				break;
		}
	}
}
