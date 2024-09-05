import Navigation from "./components/Navigation";
import { Outlet } from "react-router-dom";

function Layout() {
  return (
      <div id="main">
        <Navigation />
        <Outlet />
      </div>
  );
}

export default Layout;