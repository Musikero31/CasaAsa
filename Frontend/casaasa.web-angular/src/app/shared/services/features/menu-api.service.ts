import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../../environments/environment";
import { MenuCategoryModel } from "../../models/menu/menu-category.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class MenuApiService {
    private _http = inject(HttpClient);
    private readonly _baseUrl = environment.apiUrl;

    constructor() {}

    getMenuCategories() : Observable<MenuCategoryModel[]>{
        return this._http.get<MenuCategoryModel[]>(`${this._baseUrl}/menu/GetMenuCategories`);
    }
}