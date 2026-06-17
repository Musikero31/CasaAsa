import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CustomerModel } from "../../../models/customer.model";
import { Observable } from "rxjs";
import { AuthResponseModel } from "../../../models/authentication/auth.model";
import { ChangePasswordModel } from "../../../models/authentication/changePassword.model";

@Injectable({
    providedIn: 'root'
})
export class AuthApiService {
    constructor() { }
    private readonly _baseUrl = "https://localhost:7218";

    private _http = inject(HttpClient)

    login(username: string, password: string) : Observable<AuthResponseModel> {
        var loginData = {
            username: username,
            password: password
        };

        return this._http.post<AuthResponseModel>(`${this._baseUrl}/api/auth/login`, 
            loginData, { withCredentials: true });
    }

    logout() : Observable<string> {
        return this._http.post<string>(`${this._baseUrl}/api/auth/logout`, {}, 
            { withCredentials: true });
    }

    register(customer: CustomerModel) : Observable<AuthResponseModel> {
        return this._http.post<AuthResponseModel>(`${this._baseUrl}/api/auth/register`, 
            customer, { withCredentials: true });
    }

    resetPassword(email: string) : Observable<AuthResponseModel> {
        return this._http.post<AuthResponseModel>(`${this._baseUrl}/api/auth/reset-password`, 
            { email }, { withCredentials: true });
    }

    changePassword(changePasswordModel: ChangePasswordModel) : Observable<string> {
        return this._http.post<string>(`${this._baseUrl}/api/auth/change-password`, 
            changePasswordModel, { withCredentials: true });
    }
}