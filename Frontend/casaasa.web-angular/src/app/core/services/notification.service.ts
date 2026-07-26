import { Injectable, signal } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    private readonly _message = signal<string | null>(null);
    
    readonly message = this._message.asReadonly();

    showMessage(message: string) {
        this._message.set(message);
    }

    clearMessage() {
        this._message.set(null);
    }
}