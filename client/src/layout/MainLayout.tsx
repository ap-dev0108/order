import { Outlet } from "react-router-dom";

function MainLayout() {
  return (
    <div className="min-h-screen">
      <header>Navbar</header>

      <main>
        <Outlet />
      </main>
    </div>
  );
}

export default MainLayout;
