import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    outDir: "build",
    sourcemap: false,
  },
  server: {
    proxy: {
      "^/api/.*": "http://localhost:5000",
    },
  }
})
