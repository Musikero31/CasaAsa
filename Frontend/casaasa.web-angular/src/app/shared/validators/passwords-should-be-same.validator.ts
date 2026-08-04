import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordsShouldBeSame() : ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
        const password = group.get('password');
        const confirmPassword = group.get('confirmPassword');

        if (password && confirmPassword && password.value !== confirmPassword.value) {
            return { passwordsDoNotMatch: true };
        }

        return null;
    }
}