import { inject } from "@angular/core";
import { AuthApiService } from "../../shared/services/features/auth-api.service";
import { AuthenticationService } from "../services/authentication.service";
import { catchError, firstValueFrom, of, tap } from "rxjs";

export function initializeAuthState() {
  // Initialization logic for authentication
  const _authAPI = inject(AuthApiService);
  const _authService = inject(AuthenticationService);

  return firstValueFrom(_authAPI.loadCurrentUser()
    .pipe(
        tap(user => {
            _authService.setCurrentUser(user);
        }),
        catchError(() => {
            // Anonymous users are allowed
                _authService.clearCurrentUser();

                return of(null);
        })
    ));
}