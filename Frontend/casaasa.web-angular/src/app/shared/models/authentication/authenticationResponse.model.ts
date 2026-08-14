export interface AuthenticationResponseModel {
    success: boolean;
    message: string;
    errorCode: string;
    errors: string[];
}