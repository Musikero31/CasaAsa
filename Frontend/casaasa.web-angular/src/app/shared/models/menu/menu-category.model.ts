import { MenuDetailModel } from "./menu-detail.model";

export interface MenuCategoryModel {
    categoryId: number;
    categoryName: string;
    menus: MenuDetailModel[];
}