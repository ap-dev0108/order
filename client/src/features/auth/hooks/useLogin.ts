import { QueryClient, useMutation, useQueryClient } from "@tanstack/react-query";
import { Login } from "../authapi";
import { useNavigate } from "react-router-dom";

export const useLogin = () => {
    const query = useQueryClient();
    const navigate = useNavigate();

    return useMutation({
        mutationKey: ["login"],
        mutationFn: Login,
        
        onSuccess: () => {
            navigate("/admin");
            query.invalidateQueries({ queryKey: ["login"] });
        },

    })
}