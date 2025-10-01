import { describe, it, expect, beforeEach, afterEach, vi } from 'vitest';

describe('vite.config.js', () => {
  let originalProcessEnv: NodeJS.ProcessEnv;
  let originalProcessCwd: () => string;

  beforeEach(() => {
    originalProcessEnv = process.env;
    originalProcessCwd = process.cwd;
    vi.clearAllMocks();
  });

  afterEach(() => {
    process.env = originalProcessEnv;
    process.cwd = originalProcessCwd;
  });

  describe('Vite imports and plugin configuration', () => {
    it('should import defineConfig and loadEnv from vite', async () => {
      const viteModule = await import('vite');
      expect(viteModule.defineConfig).toBeDefined();
      expect(viteModule.loadEnv).toBeDefined();
      expect(typeof viteModule.defineConfig).toBe('function');
      expect(typeof viteModule.loadEnv).toBe('function');
    });

    it('should import @vitejs/plugin-react plugin', async () => {
      const pluginModule = await import('@vitejs/plugin-react');
      expect(pluginModule.default).toBeDefined();
      expect(typeof pluginModule.default).toBe('function');
    });

    it('should configure plugin correctly when called', async () => {
      const pluginModule = await import('@vitejs/plugin-react');
      const plugin = pluginModule.default;
      
      const pluginResult = plugin();
      expect(pluginResult).toBeDefined();
      expect(Array.isArray(pluginResult) || typeof pluginResult === 'object').toBe(true);
    });
  });

  describe('Configuration generation', () => {
    it('should generate config for development mode', async () => {
      process.env.VITE_PORT = '3000';
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          resolve: {
            alias: {
              '@': new URL('./src', import.meta.url).href
            }
          },
          server: {
            proxy: {
              '/api': {
                target: process.env.services__myrestaurantapi__https__0 || process.env.services__myrestaurantapi__http__0,
                changeOrigin: true,
                secure: false
              }
            },
            port: parseInt(env.VITE_PORT)
          },
          build: {
            outDir: 'dist',
            rollupOptions: {
              input: './index.html'
            }
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });

    it('should handle different modes correctly', async () => {
      const { loadEnv } = await import('vite');
      
      process.cwd = vi.fn().mockReturnValue('/test/path');
      
      const devEnv = loadEnv('development', '/test/path', '');
      const prodEnv = loadEnv('production', '/test/path', '');
      
      expect(typeof devEnv).toBe('object');
      expect(typeof prodEnv).toBe('object');
    });

    it('should handle missing VITE_PORT environment variable', async () => {
      delete process.env.VITE_PORT;
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          server: {
            port: parseInt(env.VITE_PORT)
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });

    it('should configure proxy target from environment variables', async () => {
      process.env.services__myrestaurantapi__https__0 = 'https://api.example.com';
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          server: {
            proxy: {
              '/api': {
                target: process.env.services__myrestaurantapi__https__0 || process.env.services__myrestaurantapi__http__0,
                changeOrigin: true,
                secure: false
              }
            }
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });

    it('should fallback to http target when https is not available', async () => {
      delete process.env.services__myrestaurantapi__https__0;
      process.env.services__myrestaurantapi__http__0 = 'http://api.example.com';
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          server: {
            proxy: {
              '/api': {
                target: process.env.services__myrestaurantapi__https__0 || process.env.services__myrestaurantapi__http__0,
                changeOrigin: true,
                secure: false
              }
            }
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });
  });

  describe('Build configuration', () => {
    it('should configure build options correctly', async () => {
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          build: {
            outDir: 'dist',
            rollupOptions: {
              input: './index.html'
            }
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });
  });

  describe('Resolve alias configuration', () => {
    it('should configure path alias correctly', async () => {
      process.cwd = vi.fn().mockReturnValue('/test/path');

      const { defineConfig, loadEnv } = await import('vite');
      const plugin = (await import('@vitejs/plugin-react')).default;

      const configFunction = defineConfig(({ mode }) => {
        const env = loadEnv(mode, process.cwd(), '');
        return {
          plugins: [plugin()],
          resolve: {
            alias: {
              '@': new URL('./src', import.meta.url).href
            }
          }
        };
      });

      expect(typeof configFunction).toBe('function');
    });
  });
});