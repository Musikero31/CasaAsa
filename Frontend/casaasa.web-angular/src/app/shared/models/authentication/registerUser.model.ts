import { AddressModel } from "../address.model";

export interface RegisterUserModel {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  addresses: AddressModel[];
}