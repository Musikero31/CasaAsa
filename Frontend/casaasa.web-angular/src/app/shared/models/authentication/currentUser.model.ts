import { AuthenticationResponseModel } from "./authenticationResponse.model";

export interface CurrentUserModel extends AuthenticationResponseModel{
    fullName: string;
    username: string;
    roles: string[];
}