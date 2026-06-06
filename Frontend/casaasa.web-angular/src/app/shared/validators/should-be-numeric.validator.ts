import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function shouldBeNumeric() : ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        let value = control.value;
        if (value && isNaN(value)) {
            return { notNumeric: true };
        }
        return null;
    }
}