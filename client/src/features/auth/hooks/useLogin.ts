import { QueryClient, useMutation, useQueryClient } from "@tanstack/react-query";
import { Login } from "../authapi";

export const useLogin = () => {
    const query = useQueryClient();

    return useMutation({
        mutationKey: ["login"],
        mutationFn: Login,
        
        onSuccess: () => {
            query.invalidateQueries({ queryKey: ["login"] });
        },

    })
}