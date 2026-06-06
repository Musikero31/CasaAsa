import { AddressModel } from "./address.model";

export interface CustomerModel {
    userId: string | null;
    firstName: string;
    lastName: string;
    username: string;
    password: string;
    phoneNumber: string;
    addresses: AddressModel[];
}