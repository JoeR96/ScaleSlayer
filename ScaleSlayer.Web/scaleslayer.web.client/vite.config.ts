import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';
import { fileURLToPath, URL } from 'node:url';

// Define the folder to store certificates based on the OS environment
const baseFolder = env.APPDATA !== undefined && env.APPDATA !== ''
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`;

// Target URL for the backend, depending on the environment variables
const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
        ? env.ASPNETCORE_URLS.split(';')[0]
        : 'https://localhost:7220';

// Export Vite config
export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)) // Resolves alias
        }
    },
    server: {
        proxy: {
            '/api/notes/notes': {
                target, // Proxy the API requests to the backend server
                secure: false
            },
            '/api/scales': {
                target, // Use the dynamically determined target for the backend server
                changeOrigin: true,
                secure: false,
                // No need to rewrite since the API endpoint structure is handled by the backend
                rewrite: (path) => path.replace(/^\/api\/scales/, '/api/scales'),
            },
        },
        port: 5173,
    }
});
