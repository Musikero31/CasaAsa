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

    private _http = inject(HttpClient)

    login(username: string, password: string) : Observable<AuthResponseModel> {
        return this._http.post<AuthResponseModel>('/api/auth/login', { username, password });
    }

    logout() : Observable<string> {
        return this._http.post<string>('/api/auth/logout', {});
    }

    register(customer: CustomerModel) : Observable<AuthResponseModel> {
        return this._http.post<AuthResponseModel>('/api/auth/register', customer);
    }

    resetPassword(email: string) : Observable<AuthResponseModel> {
        return this._http.post<AuthResponseModel>('/api/auth/reset-password', { email });
    }

    changePassword(changePasswordModel: ChangePasswordModel) : Observable<string> {
        return this._http.post<string>('/api/auth/change-password', changePasswordModel);
    }
}