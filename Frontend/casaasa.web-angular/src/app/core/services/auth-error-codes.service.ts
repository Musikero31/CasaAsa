import { Injectable } from "@angular/core";
import authErrorCodes from "../../../assets/data/auth-error-codes.json"
import { AuthErrorCodeModel } from "../../shared/models/auth-error-code.model";

@Injectable({
    providedIn: 'root'
})
export class AuthErrorCodesService {
    getAuthErrorCodes(): AuthErrorCodeModel[] {
        return authErrorCodes as AuthErrorCodeModel[]
    }
}