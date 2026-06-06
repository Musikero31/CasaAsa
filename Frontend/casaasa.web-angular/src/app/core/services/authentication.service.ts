import { computed, Injectable, signal } from "@angular/core";
import { CurrentUserModel } from "../../shared/models/authentication/currentUser.model";

/// Manages the authentication state and user roles
@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    readonly currentUser = signal<CurrentUserModel | null>(null);
    
    readonly isLoggedIn = computed(() =>
        this.currentUser() !== null
    );

    readonly roles = computed(() =>
        this.currentUser()?.roles ?? []
    );

    setCurrentUser(user: CurrentUserModel | null) {
        this.currentUser.set(user);
    }

    clearCurrentUser() {
        this.currentUser.set(null);
    }
}