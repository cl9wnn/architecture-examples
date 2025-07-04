import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://backend:8080', // http://backend:8080, if starts from IDE
        changeOrigin: true,
        secure: false,
      },
    },
  }
})