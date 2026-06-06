import { AddressModel } from "./address.model";

export interface CustomerModel {
    userId: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    addresses: AddressModel[];
}