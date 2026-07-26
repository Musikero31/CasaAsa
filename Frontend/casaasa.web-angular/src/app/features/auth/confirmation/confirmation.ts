import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { NotificationService } from '../../../core/services/notification.service';
import { ConfirmUserModel } from '../../../shared/models/authentication/confirmUser.model';

@Component({
  selector: 'app-confirmation',
  imports: [],
  templateUrl: './confirmation.html',
  styleUrl: './confirmation.scss',
})
export class Confirmation implements OnInit{  
  private _route = inject(ActivatedRoute);
  private _authAPI = inject(AuthApiService);
  private _router = inject(Router);
  private _notificationSvc = inject(NotificationService);

  userId: string | null = null;
  token: string | null = null;
  message: string | null = null;

  ngOnInit(): void {
    this._route.queryParamMap.subscribe(params => {
      this.userId = params.get('userId') ?? "";
      this.token = params.get('token') ?? "";

      if (!this.userId || !this.token) {
        this._notificationSvc.showMessage('Invalid confirmation link.');
        this._router.navigate(['/login']);
        return;
      }

      let confirmUser: ConfirmUserModel = {
        userId: this.userId,
        token: this.token
      };

      this._authAPI.confirmEmail(confirmUser).subscribe({
        next: (response) => {
          this._notificationSvc.showMessage(response.message);
          this._router.navigate(['/login']);
        },
        error: (error) => {
          let message = 'Error confirming email.';

          if (typeof error.error === 'string') {
            message = error.error;
          } 
          else if (error.error?.message) {
            message = error.error.message;
          } 
          else if (error.message) {
            message = error.message;
          }

          console.error('Error confirming email:', error);
          this._notificationSvc.showMessage(message);
          this._router.navigate(['/login']);
        }
      });
    });
  }
}
