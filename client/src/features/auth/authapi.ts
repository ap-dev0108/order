import api from "../../services/api/api";
import type { LoginData } from "../../types/auth/AuthType";

export const Login = async(data: LoginData) : Promise<LoginData> => {
    const response = await api.post<LoginData>("/auth/login", data);
    return response.data;
}