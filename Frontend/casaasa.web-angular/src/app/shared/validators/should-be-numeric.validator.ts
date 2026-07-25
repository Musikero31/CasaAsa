import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function shouldBeNumeric() : ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const value = control.value?.trim();
        const isNumeric = /^\d+$/.test(String(value));
        if (value !== null && value !== undefined && !isNumeric) {
            return { notNumeric: true };
        }
        return null;
    }
}