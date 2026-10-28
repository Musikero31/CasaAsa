export interface MenuDetailModel {
    id: number;
    name: string;
    description: string;
    categoryId: number;
    categoryName: string;
    price: number;
    imageUrl: string; // To be replaced with the document model for images in the future
}