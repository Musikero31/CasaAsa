import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthApiService } from '../../../shared/services/features/auth-api.service';
import { passwordsShouldBeSame } from '../../../shared/validators/passwords-should-be-same.validator';
import { shouldBeNumeric } from '../../../shared/validators/should-be-numeric.validator';
import { AddressModel } from '../../../shared/models/address.model';
import { CustomerModel } from '../../../shared/models/customer.model';
import { NgSelectComponent } from '@ng-select/ng-select';
import { USStatesService } from '../../../core/services/us-states.service';

@Component({
  selector: 'casa-register',
  imports: [RouterLink, ReactiveFormsModule, NgSelectComponent],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private _formBuilder = inject(FormBuilder)
  private _authApi = inject(AuthApiService)
  private _router = inject(Router);
  private _usStatesService = inject(USStatesService);

  states = this._usStatesService.getStates();

  registerForm = this._formBuilder.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    username: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
    isContactSameAsUser: [false],
    addressLine1: ['', [Validators.required]],
    addressLine2: [''],
    addressLine3: [''],
    city: ['', [Validators.required]],
    state: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(2)]],
    zipCode: ['', [Validators.required, shouldBeNumeric(), Validators.minLength(5), Validators.maxLength(10)]],
    contactPerson: ['', [Validators.required]],
    contactNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]]
  }, {
    validators: [passwordsShouldBeSame()]
  });

  register() {
      if (this.registerForm.invalid) {
        this.registerForm.markAllAsTouched();
        return;
      }
  
      let address: AddressModel = {
        addressLine1: this.registerForm.get('addressLine1')?.value as string,
        addressLine2: this.registerForm.get('addressLine2')?.value as string,
        addressLine3: this.registerForm.get('addressLine3')?.value as string,
        city: this.registerForm.get('city')?.value as string,
        state: this.registerForm.get('state')?.value as string,
        zipCode: this.registerForm.get('zipCode')?.value as string,
        contactPerson: this.registerForm.get('contactPerson')?.value as string,
        contactNumber: this.registerForm.get('contactNumber')?.value as string,
        addressId: 0,
        isDefaultAddress: true,
        contactIsSameAsUser: this.registerForm.get('isContactSameAsUser')?.value ?? false
      };      
  
      let customer: CustomerModel = {
        firstName: this.registerForm.get('firstName')?.value as string,
        lastName: this.registerForm.get('lastName')?.value as string,
        username: this.registerForm.get('username')?.value as string,
        password: this.registerForm.get('password')?.value as string,
        phoneNumber: this.registerForm.get('phoneNumber')?.value as string,
        addresses: [address],
        userId: null
      };
  
      this._authApi.register(customer)
        .subscribe({
          next: (registeredUser) => {
            // Handle successful registration
            if (registeredUser.succeeded) {
              // Show that the registration was successful, and check email for confirmation
              alert('Registration successful! Please check your email for confirmation.');
              this._router.navigate(['/login']);
            }
            else {
              console.error('Registration failed', registeredUser.errors);
              alert('Registration failed: ' + registeredUser.errors?.join(', '));
            }
          },
          error: (error: Error) => {
            console.error('Registration request error', error);
            alert('An error occurred while trying to register.');
          }
        })
    }
    
    clear(event: Event) {
      event.preventDefault();
      this.registerForm.reset();
    }
  
    setContactSameAsUser() {
      // TODO: Implement logic to set contact number same as user's phone number if the checkbox is checked
      // TODO: Once the checkbox is checked, disable the contact number input field and set its value to the user's phone number
      if (this.registerForm.get('isContactSameAsUser')?.value) {
        this.registerForm.get('contactNumber')?.setValue(this.registerForm.get('phoneNumber')?.value ?? '');
        this.registerForm.get("contactPerson")?.setValue(this.registerForm.get("firstName")?.value + " " + this.registerForm.get("lastName")?.value);
        this.registerForm.get('contactNumber')?.disable();
        this.registerForm.get("contactPerson")?.disable();
      } else {
        this.registerForm.get('contactNumber')?.enable();
        this.registerForm.get("contactPerson")?.enable();
      }
    }

    setSameContactName() {
      if (this.registerForm.get('isContactSameAsUser')?.value) {
        this.registerForm.get("contactPerson")?.setValue(this.registerForm.get("firstName")?.value + " " + this.registerForm.get("lastName")?.value);
      }
    }

    setSamePhoneNumber() {
      if (this.registerForm.get('isContactSameAsUser')?.value) {
        this.registerForm.get('contactNumber')?.setValue(this.registerForm.get('phoneNumber')?.value ?? '');
      }
    }
}
