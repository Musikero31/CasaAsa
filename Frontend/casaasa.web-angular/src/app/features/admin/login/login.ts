import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';

@Component({
  selector: 'casa-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  username: string = "";
  password: string = "";

  constructor(private authApi: AuthApiService) { }

  ngOnInit(): void {
    // Check if user is already logged in, if so, redirect to dashboard

    if (localStorage.getItem('token')) {
      // Redirect to dashboard
      window.location.href = '/admin/dashboard';
    }
  }

  login() {
    // Call authentication service to login user
    // If successful, store token in local storage and redirect to dashboard
    // If unsuccessful, display error message
    this.authApi.login(this.username, this.password).subscribe({
      next: (response) => {
        if (response.succeeded && response.tokenResponse?.token) {
          localStorage.setItem('token', response.tokenResponse.token);
          window.location.href = '/dashboard';
        } 
        else {
          console.error('Login failed', response.errors);
        }
      },
      error: (err) => {
        console.error('Login request error', err);
      }
    });
  }

  clear() {
    this.username = "";
    this.password = "";
  }

}
