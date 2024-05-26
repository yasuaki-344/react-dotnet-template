import { resolve } from "path";
import { PluginOption, defineConfig } from "vite";
import dts from "vite-plugin-dts";

export default defineConfig({
  build: {
    lib: {
      entry: resolve(__dirname, "lib/main.ts"),
      name: "ApplicationCore",
      fileName: "application-core",
      formats: ["es"],
    },
  },
  plugins: [dts({ rollupTypes: true }) as PluginOption],
});
