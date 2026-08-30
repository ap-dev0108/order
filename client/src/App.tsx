import { Routes, Route } from "react-router-dom";

import MainLayout from "./layout/MainLayout";
import AuthLayout from "./layout/AuthLayout";

import { LoginFormPage } from "./pages/Login";

function AppRoutes() {
  return (
    <Routes>
      {/* Application routes */}
      {/* <Route element={<MainLayout />}>
                <Route path="/" element={<HomePage />} />
            </Route> */}

      {/* Authentication routes */}
      <Route element={<AuthLayout />}>
        <Route path="/login" element={<LoginFormPage />} />
      </Route>

      {/* Catch-all */}
      {/* <Route path="*" element={<NotFoundPage />} /> */}
    </Routes>
  );
}

export default AppRoutes;
