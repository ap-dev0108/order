import { useLogin } from "../hooks/useLogin";
import { useState } from "react";
import type { LoginData } from "../../../types/auth/AuthType";
import { Mail, Lock, LogIn } from "lucide-react";

function LoginForm() {
  const [loginData, setLoginData] = useState<LoginData>({
    email: "",
    password: "",
  });

  const loginMutation = useLogin();

  const handleSubmit = (e: React.SubmitEvent) => {
    e.preventDefault();
    loginMutation.mutate(loginData);
  };

  return (
    <div className="min-h-screen w-full bg-gray-50 flex items-center justify-center p-6">
      <div className="w-full max-w-[544px] bg-white rounded-2xl shadow-sm p-12">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-5xl font-extrabold text-orange-800 tracking-tight">
            RestoFlow
          </h1>
          <p className="mt-3 text-lg text-gray-500">
            Kitchen Professional Portal
          </p>
        </div>

        {/* Form */}
        <form className="space-y-6" onSubmit={handleSubmit}>
          {/* Email */}
          <div>
            <label className="block text-sm font-bold text-gray-800 mb-2">
              Email Address
            </label>
            <div className="relative">
              <Mail
                className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400"
                size={20}
                strokeWidth={1.75}
              />
              <input
                type="email"
                typeof="email"
                value={loginData?.email}
                onChange={(e) =>
                  setLoginData({
                    ...loginData,
                    email: e.target.value,
                  })
                }
                placeholder="staff@restaurant.com"
                className="w-full pl-11 pr-4 py-3 rounded-lg border border-gray-200 text-gray-700 placeholder-gray-400 text-base focus:outline-none focus:ring-2 focus:ring-orange-200 focus:border-orange-400"
              />
            </div>
          </div>

          {/* Password */}
          <div>
            <label className="block text-sm font-bold text-gray-800 mb-2">
              Password
            </label>
            <div className="relative">
              <Lock
                className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400"
                size={20}
                strokeWidth={1.75}
              />
              <input
                type="password"
                value={loginData?.password}
                onChange={(e) =>
                  setLoginData({
                    ...loginData,
                    password: e.target.value,
                  })
                }
                placeholder="••••••••"
                className="w-full pl-11 pr-4 py-3 rounded-lg border border-gray-200 text-gray-700 placeholder-gray-400 text-base focus:outline-none focus:ring-2 focus:ring-orange-200 focus:border-orange-400"
              />
            </div>
          </div>

          {/* Login button */}
          <button
            type="submit"
            className="w-full flex items-center justify-center gap-2 bg-orange-500 hover:bg-orange-600 active:bg-orange-700 transition-colors text-white font-bold text-base py-3.5 rounded-lg"
          >
            <LogIn size={18} strokeWidth={2} />
            Login
          </button>
        </form>

        {/* Divider */}
        <hr className="mt-8 mb-6 border-gray-200" />

        {/* Footer */}
        <p className="text-center text-gray-500 text-base">
          Don't have an account?{" "}
          <a
            href="#"
            className="text-orange-500 font-bold hover:text-orange-600"
          >
            Register here
          </a>
        </p>
      </div>
    </div>
  );
}

export default LoginForm;
