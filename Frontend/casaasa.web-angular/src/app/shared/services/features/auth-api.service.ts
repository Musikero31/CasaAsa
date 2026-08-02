import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CustomerModel } from "../../models/customer.model";
import { Observable } from "rxjs";
import { CurrentUserModel } from "../../models/authentication/currentUser.model";
import { ChangePasswordModel } from "../../models/authentication/changePassword.model";
import { environment } from "../../../../environments/environment";
import { ConfirmUserModel } from "../../models/authentication/confirmUser.model";
import { AuthenticationResponseModel } from "../../models/authentication/authenticationResponse.model";

// Manages the calls to the authentication API
@Injectable({
    providedIn: 'root'
})
export class AuthApiService {
    constructor() { }
    private readonly _baseUrl = environment.apiUrl;

    private _http = inject(HttpClient)

    login(username: string, password: string) : Observable<CurrentUserModel> {
        const loginData = {
            username: username,
            password: password
        };

        return this._http.post<CurrentUserModel>(`${this._baseUrl}/auth/login`, 
            loginData);
    }

    logout() : Observable<string> {
        return this._http.post<string>(`${this._baseUrl}/auth/logout`, {});
    }

    register(customer: CustomerModel) : Observable<CurrentUserModel> {
        return this._http.post<CurrentUserModel>(`${this._baseUrl}/auth/register`, 
            customer);
    }

    resetPassword(email: string) : Observable<CurrentUserModel> {
        return this._http.post<CurrentUserModel >(`${this._baseUrl}/auth/ResetPassword`, 
            { email });
    }

    changePassword(changePasswordModel: ChangePasswordModel) : Observable<string> {
        return this._http.post<string>(`${this._baseUrl}/auth/ChangePassword`, 
            changePasswordModel);
    }

    confirmEmail(confirmUserModel: ConfirmUserModel) : Observable<AuthenticationResponseModel> {
        return this._http.post<AuthenticationResponseModel>(`${this._baseUrl}/auth/Confirm`, 
            confirmUserModel);
    }

    loadCurrentUser(): Observable<CurrentUserModel> {
        return this._http.get<CurrentUserModel>(`${this._baseUrl}/auth/GetCurrentUser`)
    }
}