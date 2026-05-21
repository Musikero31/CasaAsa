const API_BASE_URL = "https://api.example.com";

export const httpClient = {
    async get<T>(url: string): Promise<T> {
        const response = await fetch(`${API_BASE_URL}${url}`);

        if (!response.ok) {
            throw new Error("GET request failed");
        }

        return response.json();
    },

    async post<T>(url: string, data: unknown): Promise<T> {
        const response = await fetch(`${API_BASE_URL}${url}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            throw new Error("POST request failed");
        }

        return response.json();
    },

    async put<T>(url: string, data: unknown): Promise<T> {
        const response = await fetch(`${API_BASE_URL}${url}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            throw new Error("PUT request failed");
        }

        return response.json();
    },

    async delete(url: string): Promise<void> {
        const response = await fetch(`${API_BASE_URL}${url}`, {
            method: "DELETE",
        });

        if (!response.ok) {
            throw new Error("DELETE request failed");
        }
    },
};