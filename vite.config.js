﻿import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

/** @type {import('vite').UserConfig} */
export default defineConfig({
    plugins: [react({ jsxRuntime: 'classic'})], // jsxRuntime: 'classic' is required for fast-refresh for .js files
    root: "./src/JetBrainsDotnetDays2024.Client",
    server: {
        port: 8080,
        proxy: {
            '/api': 'http://localhost:5000',
        }
    },
    build: {
        outDir:"../../publish/app/public"
    }
})
