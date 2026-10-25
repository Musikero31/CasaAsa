import { CanMatchFn, Router } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";
import { inject } from "@angular/core";

export const customerGuard: CanMatchFn = () => {
    const _authService = inject(AuthenticationService)
    const _router = inject(Router)

    if (!_authService.roles().includes('Customer')) {
        return _router.createUrlTree(["/admin/dashboard"]);
    }

    if(!_authService.isLoggedIn()) {
        return _router.createUrlTree(["/login"])
    }

    return true;
};