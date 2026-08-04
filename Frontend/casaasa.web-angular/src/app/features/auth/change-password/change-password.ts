import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { PasswordComponent, PasswordInfo } from '../../../shared/components/password-component/password-component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { NotificationService } from '../../../core/services/notification.service';
import { ChangePasswordModel } from '../../../shared/models/authentication/changePassword.model';

// This component is used to change the password of the user.
// It is used in the change-password route.
// It is a simple form that takes the new password and confirm password. 
// It also has a submit button that calls the changePassword() method. 
// The changePassword() method is not implemented yet. 
// It will be implemented in the future.
@Component({
  selector: 'casa-change-password',
  imports: [PasswordComponent, ReactiveFormsModule, RouterLink],
  templateUrl: './change-password.html',
  styleUrl: './change-password.scss',
})
export class ChangePassword implements OnInit {
  private _route = inject(ActivatedRoute);
  private _authAPI = inject(AuthApiService);
  private _router = inject(Router); 
  private _notificationSvc = inject(NotificationService);
  private _formBuilder = inject(FormBuilder);

  message: string | null = null;

  token: string | null = null;
  userId: string | null = null;

  changePasswordForm = this._formBuilder.group({
    passwordInfo: this._formBuilder.control<PasswordInfo | null>(null)
  });

  ngOnInit(): void {
    this._route.queryParamMap.subscribe(params => {
      const token = params.get('token') ?? ""
      const userId = params.get('userId') ?? ""
      
      this.token = token;
      this.userId = userId;
      
      if (!token || !userId) {
        // Handle missing token or userId, e.g., redirect or show errors
        this._notificationSvc.showMessage('Missing token or userId');
        this._router.navigate(['/login']); // Redirect to home or appropriate page
        return;
      }
    });
  }

  changePassword() {
    if (this.changePasswordForm.invalid) {
      this.changePasswordForm.markAllAsTouched();
      return;
    }

    let passwordInfo = this.changePasswordForm.get('passwordInfo')?.value ?? null;

    let changePasswordData: ChangePasswordModel = {
      token: this.token ?? "",
      userId: this.userId ?? "",
      newPassword: passwordInfo?.password ?? ""
    };

    this._authAPI.changePassword(changePasswordData).subscribe({
      next: (changePassword) => {
        if (changePassword.success) {
          this._notificationSvc.showMessage(changePassword.message);
          this._router.navigate(['/login']);
        } 
        else {
          //this._notificationSvc.showMessage('Failed to change password');
          alert('Failed to change password');
          console.error('Failed to change password', changePassword.errors);
        }
      },
      error: (err) => {
        //this._notificationSvc.showMessage('Failed to change password');
        alert('Failed to change password');
        console.error('Failed to change password', err);
      }
    });

    this.message = this._notificationSvc.message();
    this._notificationSvc.clearMessage();
  }
}
