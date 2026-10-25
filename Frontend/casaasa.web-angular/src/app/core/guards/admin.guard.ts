import { inject } from "@angular/core";
import { CanMatchFn, Router } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";

export const adminGuard: CanMatchFn = () => {
    const _authService = inject(AuthenticationService)
    const _router = inject(Router)

    if(!_authService.isLoggedIn()) {
        return _router.createUrlTree(["/login"])
    }

    if (!_authService.roles().includes('Admin')) {
        return _router.createUrlTree(["/customer/dashboard"])
    }

    return true;
};