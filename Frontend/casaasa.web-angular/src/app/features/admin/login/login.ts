import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'casa-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  private _router = inject(Router);
  private _authApi = inject(AuthApiService);
  username: string = "";
  password: string = "";

  constructor() { }

  ngOnInit(): void {
    // Check if user is already logged in, if so, redirect to dashboard

    if (localStorage.getItem('token')) {
      // Redirect to dashboard
      this._router.navigate(['/dashboard']);
    }
  }

  login() {
    // Call authentication service to login user
    // If successful, store token in local storage and redirect to dashboard
    // If unsuccessful, display error message
    this._authApi.login(this.username, this.password).subscribe({
      next: (response) => {
        if (response.succeeded && response.tokenResponse?.token) {
          localStorage.setItem('token', response.tokenResponse.token);
          localStorage.setItem('username', this.username);
          localStorage.setItem('roles', response.tokenResponse.roles.join(','));
          localStorage.setItem('fullName', response.fullName);

          this._router.navigate(['/dashboard']);
        } 
        else {
          console.error('Login failed', response.errors);
          alert('Login failed: ' + response.errors?.join(', '));
        }
      },
      error: (err) => {
        console.error('Login request error', err);
        alert('An error occurred while trying to login.');
      }
    });
  }

  clear() {
    this.username = "";
    this.password = "";
  }

}
