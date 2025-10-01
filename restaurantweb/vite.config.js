import { fileURLToPath, URL } from 'node:url';
import process from 'node:process';
import { defineConfig, loadEnv } from 'vite'
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
// Node-specific script; define Node globals for ESLint
/* eslint-env node */
import { env as nodeEnv } from 'process';
const baseFolder =
    nodeEnv.APPDATA !== undefined && nodeEnv.APPDATA !== ''
        ? `${nodeEnv.APPDATA}/ASP.NET/https`
        : `${nodeEnv.HOME}/.aspnet/https`;
const certificateName = "restaurantweb";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}
// const target = nodeEnv.ASPNETCORE_HTTPS_PORT ? `https://localhost:${nodeEnv.ASPNETCORE_HTTPS_PORT}` :
//     nodeEnv.ASPNETCORE_URLS ? nodeEnv.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7293';
// https://vitejs.dev/config/
export default defineConfig(({mode}) =>{
    const loadedEnv = loadEnv(mode, process.cwd(), '');

    return {
        plugins: [plugin()],
        resolve: {
            alias: {
                '@': fileURLToPath(new URL('./src', import.meta.url))
            }
        },
        server: {
            proxy: {
                '/api': {
                    // Use imported env (from process) instead of global process to satisfy lint rule
                    target: loadedEnv.services__myrestaurantapi__https__0 || loadedEnv.services__myrestaurantapi__http__0,
                    changeOrigin: true,
                    secure: false //,
                    // rewrite: (path) => path.replace(/^\/api/, '')
                }
            },
            port: parseInt(loadedEnv.VITE_PORT) //,
            // https: {
            //     key: fs.readFileSync(keyFilePath),
            //     cert: fs.readFileSync(certFilePath),
            // }
        },
        build:{
            outDir: 'dist',
            rollupOptions: {
                input: './index.html'
            }
        }
    }

})

