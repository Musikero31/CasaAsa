export interface AuthResponseModel {
    succeeded: boolean;
    errors: string[];
    fullName: string;
    tokenResponse: {
        token: string;
        userId: string;
        email: string;
        roles: string[];
    }
}