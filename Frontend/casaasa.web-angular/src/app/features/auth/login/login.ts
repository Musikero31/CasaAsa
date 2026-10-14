import { ChangeDetectorRef, Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { Router, RouterLink } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthErrorCodes } from '../../../shared/enums/auth-error-codes.enum';

@Component({
  selector: 'casa-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  private _router = inject(Router);
  private _authApi = inject(AuthApiService);
  private _formBuilder = inject(FormBuilder);
  private _destroyRef = inject(DestroyRef);
  private _notificationSvc = inject(NotificationService);
  private _authSvc = inject(AuthenticationService);
  private _cdRef = inject(ChangeDetectorRef);

  message: string | null = null;

  showResendConfirmation: boolean = false;
  
  loginForm = this._formBuilder.group({
    username: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor() { }
  
  ngOnInit(): void {
    this.message = this._notificationSvc.message();
    this._notificationSvc.clearMessage();

    this.loginForm.controls.username.valueChanges
      .pipe(
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe(() => {
        this.showResendConfirmation = false;
      });
  }
  
  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
        // TODO: Display validation errors in a more user-friendly way
        // TODO: Highlight invalid fields
        alert("Errors: " + JSON.stringify(this.loginForm.errors));
      return;
    }

    this._authApi.login(this.loginForm.value.username!, this.loginForm.value.password!)
      .pipe(        
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe({
        next: (loggedUser) => {
          if (!loggedUser.success) {
            if (loggedUser.errorCode === AuthErrorCodes.EmailNotConfirmed) {
              this.showResendConfirmation = true;
              this._cdRef.detectChanges(); // Trigger change detection to update the view              
              this._notificationSvc.showMessage('Your email is not confirmed. Please check your email for the confirmation link.');
            }
            
            return;
          }          

          this._authSvc.setCurrentUser(loggedUser);

          switch(true)
          {
            case this._authSvc.roles().includes('Admin'):
              this._router.navigate(['/admin/dashboard']);
              break;
            case this._authSvc.roles().includes('Customer'):
              this._router.navigate(['/customer/dashboard']);
              break;
            default:
              console.error('Unknown user role:', this._authSvc.roles());
              this._notificationSvc.showMessage('An error occurred while trying to login.');
              break;
          }
        },
        error: (err) => {
          console.error('Login request error', err);
          const msg = err?.error?.errors?.[0] ?? 'An error occurred while trying to login.';
          this._notificationSvc.showMessage(msg);
        }
      });
  }

  resendConfirmationEmail() {
    let username = this.loginForm.value.username;
    if (!username) {
      this._notificationSvc.showMessage('Please enter your email address to resend the confirmation email.');
      return;
    }

    this._authApi.resendConfirmationEmail(username)
      .pipe(
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe({
        next: (response) => {
          if (!response.success) {
            console.error('Resend confirmation email failed:', response.errors);
            this._notificationSvc.showMessage(response.message);
            return;
          }
          else {
            this.showResendConfirmation = false;
            this._notificationSvc.showMessage('Confirmation email has been resent successfully.');
          }
        },
        error: (err) => {
          console.error('Resend confirmation email error', err);
          this._notificationSvc.showMessage('An error occurred while trying to resend the confirmation email.');
        }
      });
  }
}
