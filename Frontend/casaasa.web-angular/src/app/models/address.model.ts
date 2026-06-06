export interface AddressModel {
    addressId: number;
    addressLine1: string;
    addressLine2?: string;
    addressLine3?: string;
    city: string;
    state: string;
    postcode: string;
  
    contactPerson: string;
    contactNumber: string;
    isDefaultAddress: boolean;
    contactIsSameAsUser: boolean;
}