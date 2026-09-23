import { AddressModel } from "./address.model";

export interface CustomerModel {
    userId: string | null;
    success: boolean;
    firstName: string;
    lastName: string;
    username: string;
    password: string;
    phoneNumber: string;
    addresses: AddressModel[];
}