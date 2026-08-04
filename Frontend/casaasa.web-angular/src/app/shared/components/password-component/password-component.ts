import { Component, DestroyRef, forwardRef, inject } from '@angular/core';
import { ControlValueAccessor, FormBuilder, NG_VALUE_ACCESSOR, 
  NG_VALIDATORS, ReactiveFormsModule, ValidationErrors, Validator, Validators } from '@angular/forms';
import { passwordsShouldBeSame } from '../../validators/passwords-should-be-same.validator';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export interface PasswordInfo {
  password: string;
  confirmPassword: string;
}

@Component({
  selector: 'password-component',
  imports: [ReactiveFormsModule],
  templateUrl: './password-component.html',
  styleUrl: './password-component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => PasswordComponent),
      multi: true
    }
  ]
})
export class PasswordComponent implements ControlValueAccessor, Validator {

  private _passwordFormBuilder = inject(FormBuilder);
  private _destroyRef = inject(DestroyRef);
  private onPasswordChange = (_: PasswordInfo | null) => {};  
  private onPasswordValidatorChange = (_: () => void) => {};
  onPasswordTouched = () => {};

  readonly passwordForm = this._passwordFormBuilder.group({
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
  }, {
    validators: [passwordsShouldBeSame()]
  });

  constructor() {
    this.passwordForm.valueChanges
      .pipe(takeUntilDestroyed(this._destroyRef))
      .subscribe(value => {
        if (this.passwordForm.valid) {
          this.onPasswordChange(value as PasswordInfo);
        } 
        else {
          this.onPasswordChange(null);
        }
      });
  }

  validate(): ValidationErrors | null {
    if (this.passwordForm.valid) {
      return null;
    }

    return this.passwordForm.errors || { invalid: true };
  }

  registerOnValidatorChange?(fn: () => void): void {
    this.onPasswordValidatorChange = fn;
  }

  writeValue(value: PasswordInfo | null): void {
    if (!value) {
      this.passwordForm.reset();
      return;
    }

    this.passwordForm.patchValue(value, { emitEvent: false });
  }

  registerOnChange(fn: (value: PasswordInfo | null) => void): void {
    this.onPasswordChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onPasswordTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.passwordForm.disable();
    }
    else {
      this.passwordForm.enable();
    }
  }  
}
