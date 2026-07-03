import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Casa Asa');

  /*
  TODO: 
  * If localStorage is not available, show the login component
  * If localStorage is available, check for a valid session token
  * > If role is 'admin', show the admin component
  * > If role is 'user', show the user component
  */
}
