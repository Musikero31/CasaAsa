import { Component, inject, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../core/services/authentication.service';

@Component({
  selector: 'casa-meal-of-the-day-admin',
  imports: [],
  templateUrl: './meal-of-the-day.admin.html',
  styleUrl: './meal-of-the-day.admin.scss',
})
export class MealOfTheDayAdmin implements OnInit {
  private _authSvc = inject(AuthenticationService);

  constructor() { }

  ngOnInit(): void {
    const roles = this._authSvc.roles();

    if (!roles.includes('Admin')) {
      // Redirect to unauthorized page or show an error message
      alert("Unauthorized access. You do not have permission to view this page.");
      // Optionally, you can navigate to a different route
      // this._router.navigate(['/unauthorized']);
    }

    
  }

}
