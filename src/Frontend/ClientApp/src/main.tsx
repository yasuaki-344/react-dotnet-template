import React from "react";
import ReactDOM from "react-dom/client";
import App from "./components/pages/App";
import "./index.css";
import { StorageProvider } from "./services/StorageProvider";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <StorageProvider>
      <App />
    </StorageProvider>
  </React.StrictMode>
);
