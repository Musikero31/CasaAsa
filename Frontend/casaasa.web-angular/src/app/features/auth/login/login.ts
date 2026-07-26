import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { Router, RouterLink } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { NotificationService } from '../../../core/services/notification.service';

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

  message: string | null = null;
  
  loginForm = this._formBuilder.group({
    username: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor() { }
  
  ngOnInit(): void {
    this.message = this._notificationSvc.message();
    this._notificationSvc.clearMessage();
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
        switchMap(response => {
          if (!response.succeeded) {
            throw new Error(response.errors?.join(', '));
          }

          // Cookie has been set by the browser.
          // Now ask the server who the current user is.
          return this._authApi.loadCurrentUser();
        }),
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe({
        next: (loggedUser) => {
          if (loggedUser.succeeded) {
            this._authSvc.setCurrentUser(loggedUser);
            
            switch (true) {
              case loggedUser.roles.includes("Admin"):
                this._router.navigate(['/admin']);
                break;
              case loggedUser.roles.includes("Customer"):
                this._router.navigate(['/customer']);
                break;
              default:
                console.error('Login failed', loggedUser.errors);
                alert('Login failed: ' + loggedUser.errors?.join(', '));
            }
          }
          else {
            console.error('Login failed', loggedUser.errors);
            alert('Login failed: ' + loggedUser.errors?.join(', '));
          }
        },
        error: (err) => {
          console.error('Login request error', err);
          alert('An error occurred while trying to login.');
        }
      });
  }

}
