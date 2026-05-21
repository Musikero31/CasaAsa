import { httpClient } from "../api/httpClient";

export class BaseRepository<T> {
    constructor(private endpoint: string) {}

    async getAll(): Promise<T[]> {
        return httpClient.get<T[]>(this.endpoint);
    }

    async getById(id: number | string): Promise<T> {
        return httpClient.get<T>(`${this.endpoint}/${id}`);
    }

    async create(entity: T): Promise<T> {
        return httpClient.post<T>(this.endpoint, entity);
    }

    async update(id: number | string, entity: T): Promise<T> {
        return httpClient.put<T>(`${this.endpoint}/${id}`, entity);
    }

    async delete(id: number | string): Promise<void> {
        return httpClient.delete(`${this.endpoint}/${id}`);
    }
}