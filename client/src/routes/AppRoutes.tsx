import { Routes, Route } from "react-router-dom";
import AuthLayout from "../layout/AuthLayout";

import { LoginFormPage } from "../pages/Login";
import MainLayout from "../layout/MainLayout";
import { Admin } from "../pages/Admin";

function AppRoutes() {
  return (
    <Routes>
      {/* Application routes */}
      <Route element={<MainLayout />}>
                <Route path="/admin" element={<Admin />} />
            </Route>

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
