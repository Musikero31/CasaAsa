export interface CurrentUserModel {
    succeeded: boolean;
    errors: string[];
    fullName: string;
    username: string;
    roles: string[];
}