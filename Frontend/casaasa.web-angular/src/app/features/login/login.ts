import { Component, DestroyRef, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthApiService } from '../../shared/services/features/auth-api.service';
import { Router, RouterLink } from '@angular/router';
import { AuthenticationService } from '../../core/services/authentication.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';

@Component({
  selector: 'casa-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private _router = inject(Router);
  private _authApi = inject(AuthApiService);
  private _formBuilder = inject(FormBuilder);
  private _destroyRef = inject(DestroyRef);
  
  loginForm = this._formBuilder.group({
    username: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  constructor(public authSvc: AuthenticationService) { }
  
  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
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
            this.authSvc.setCurrentUser(loggedUser);
            
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
