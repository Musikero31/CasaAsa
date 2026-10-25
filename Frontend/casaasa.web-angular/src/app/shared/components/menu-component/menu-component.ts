import { Component } from '@angular/core';

@Component({
  selector: 'casa-menu-component',
  imports: [],
  templateUrl: './menu-component.html',
  styleUrl: './menu-component.scss',
})
// This is where the main menu lives. 
// It is a shared component that is used in the AppAdmin and AppCustomer components. 
// It is not used in the Login component because the menu is not needed there.
export class MenuComponent {
  
}
