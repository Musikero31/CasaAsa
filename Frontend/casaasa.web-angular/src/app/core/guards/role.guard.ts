import { inject } from "@angular/core"
import { AuthenticationService } from "../services/authentication.service"
import { CanActivateFn, Router } from "@angular/router"

export const roleGuard: CanActivateFn = (route) => {
    const _authService = inject(AuthenticationService)
    const _router = inject(Router)

    const currentUser = _authService.currentUser();

    if (!currentUser) {
        return _router.createUrlTree(["/login"])
    }

    const requiredRoles = route.data['roles'] as string[] | undefined;

    if(!requiredRoles?.length) {
        return true;
    }
    
    const hasAccess = currentUser.roles.some(role => requiredRoles.includes(role));
    let userRole = currentUser.roles[0];

    if (!hasAccess) {
        return _router.createUrlTree([`/${userRole}/dashboard`])
    }

    return true;
}