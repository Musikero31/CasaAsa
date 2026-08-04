import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'casa-forgot-password',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.scss',
})
export class ForgotPassword {
  private _formBuilder = inject(FormBuilder);
  private _authApi = inject(AuthApiService);
  private _notificationSvc = inject(NotificationService);
  private _router = inject(Router); 

  emailForm = this._formBuilder.group({
    email: ['', [Validators.required, Validators.email]]
  });

  forgotPassword() {
    if (this.emailForm.invalid) {
      this.emailForm.markAllAsTouched();
      this._notificationSvc.showMessage("Please enter a valid email address.");
      return;
    }

    this._authApi.forgotPassword(this.emailForm.value.email!)
      .subscribe({
        next: (response) => {
          if (!response.success) {
            this._notificationSvc.showMessage("Failed to send password reset link.");
            console.error("Failed to send password reset link: " + response.errors?.join(', '));
            return;
          }

          this._notificationSvc.showMessage(response.message || "Password reset link sent successfully. Please check your email.");
          this._router.navigate(['/login']);
        },
        error: (err) => {
          this._notificationSvc.showMessage('Failed to send password reset link.');
          console.error('Failed to send password reset link', err);
        }
      });
  }
}
