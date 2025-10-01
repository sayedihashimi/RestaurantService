import { describe, it, expect, beforeEach, afterEach, vi } from 'vitest';

describe('vite.config.js', () => {
  let originalEnv: NodeJS.ProcessEnv;
  let originalProcessCwd: () => string;
  let mockDefineConfig: any;
  let mockLoadEnv: any;
  let mockPlugin: any;
  let mockFileURLToPath: any;
  let mockFs: any;
  let mockPath: any;
  let mockChildProcess: any;

  beforeEach(() => {
    originalEnv = process.env;
    originalProcessCwd = process.cwd;
    
    // Mock vite functions
    mockDefineConfig = vi.fn();
    mockLoadEnv = vi.fn();
    
    // Mock @vitejs/plugin-react
    mockPlugin = vi.fn(() => ({ name: 'react-plugin' }));
    
    // Mock other dependencies
    mockFileURLToPath = vi.fn(() => '/mocked/src/path');
    mockFs = {
      existsSync: vi.fn(),
      readFileSync: vi.fn()
    };
    mockPath = {
      join: vi.fn((...args) => args.join('/'))
    };
    mockChildProcess = {
      spawnSync: vi.fn()
    };

    // Mock modules
    vi.doMock('vite', () => ({
      defineConfig: mockDefineConfig,
      loadEnv: mockLoadEnv
    }));
    
    vi.doMock('@vitejs/plugin-react', () => ({
      default: mockPlugin
    }));
    
    vi.doMock('node:url', () => ({
      fileURLToPath: mockFileURLToPath,
      URL: class MockURL {
        constructor(url: string, base: string) {}
      }
    }));
    
    vi.doMock('fs', () => mockFs);
    vi.doMock('path', () => mockPath);
    vi.doMock('child_process', () => mockChildProcess);
  });

  afterEach(() => {
    process.env = originalEnv;
    process.cwd = originalProcessCwd;
    vi.clearAllMocks();
    vi.doUnmock('vite');
    vi.doUnmock('@vitejs/plugin-react');
    vi.doUnmock('node:url');
    vi.doUnmock('fs');
    vi.doUnmock('path');
    vi.doUnmock('child_process');
  });

  it('should import defineConfig and loadEnv from vite correctly', async () => {
    mockDefineConfig.mockImplementation((configFn) => configFn);
    mockLoadEnv.mockReturnValue({ VITE_PORT: '3000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    const module = await import('./vite.config.js');
    
    expect(mockDefineConfig).toHaveBeenCalled();
    expect(typeof mockDefineConfig.mock.calls[0][0]).toBe('function');
  });

  it('should import and use @vitejs/plugin-react correctly', async () => {
    mockDefineConfig.mockImplementation((configFn) => {
      const config = configFn({ mode: 'development' });
      return config;
    });
    mockLoadEnv.mockReturnValue({ VITE_PORT: '3000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(mockPlugin).toHaveBeenCalled();
  });

  it('should configure plugins array with react plugin', async () => {
    const mockReactPlugin = { name: 'react-plugin', options: {} };
    mockPlugin.mockReturnValue(mockReactPlugin);
    
    let capturedConfig: any;
    mockDefineConfig.mockImplementation((configFn) => {
      capturedConfig = configFn({ mode: 'development' });
      return capturedConfig;
    });
    
    mockLoadEnv.mockReturnValue({ VITE_PORT: '3000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(capturedConfig.plugins).toEqual([mockReactPlugin]);
    expect(mockPlugin).toHaveBeenCalledWith();
  });

  it('should use loadEnv with correct parameters', async () => {
    const testMode = 'production';
    const testCwd = '/test/cwd';
    
    process.cwd = vi.fn(() => testCwd);
    
    mockDefineConfig.mockImplementation((configFn) => {
      configFn({ mode: testMode });
      return {};
    });
    
    mockLoadEnv.mockReturnValue({ VITE_PORT: '5000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(mockLoadEnv).toHaveBeenCalledWith(testMode, testCwd, '');
  });

  it('should handle different modes in defineConfig', async () => {
    const testModes = ['development', 'production', 'test'];
    
    mockDefineConfig.mockImplementation((configFn) => {
      testModes.forEach(mode => {
        configFn({ mode });
      });
      return {};
    });
    
    mockLoadEnv.mockReturnValue({ VITE_PORT: '3000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(mockLoadEnv).toHaveBeenCalledTimes(testModes.length);
    testModes.forEach(mode => {
      expect(mockLoadEnv).toHaveBeenCalledWith(mode, expect.any(String), '');
    });
  });

  it('should configure server port from loaded environment', async () => {
    const testPort = '4000';
    
    let capturedConfig: any;
    mockDefineConfig.mockImplementation((configFn) => {
      capturedConfig = configFn({ mode: 'development' });
      return capturedConfig;
    });
    
    mockLoadEnv.mockReturnValue({ VITE_PORT: testPort });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(capturedConfig.server.port).toBe(parseInt(testPort));
  });

  it('should handle missing VITE_PORT in environment', async () => {
    let capturedConfig: any;
    mockDefineConfig.mockImplementation((configFn) => {
      capturedConfig = configFn({ mode: 'development' });
      return capturedConfig;
    });
    
    mockLoadEnv.mockReturnValue({});
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(Number.isNaN(capturedConfig.server.port)).toBe(true);
  });

  it('should configure resolve alias correctly', async () => {
    let capturedConfig: any;
    mockDefineConfig.mockImplementation((configFn) => {
      capturedConfig = configFn({ mode: 'development' });
      return capturedConfig;
    });
    
    mockLoadEnv.mockReturnValue({ VITE_PORT: '3000' });
    mockFs.existsSync.mockReturnValue(true);
    
    process.env.APPDATA = '/test/appdata';
    
    await import('./vite.config.js');
    
    expect(capturedConfig.resolve.alias['@']).toBe('/mocked/src/path');
    expect(mockFileURLToPath).toHaveBeenCalled();
  });

  it('should handle certificate generation when certificates do not exist', async () => {
    mockFs.existsSync.mockReturnValue(false);
    mockChildProcess.spawnSync.mockReturnValue({ status: 0 });
    mockDefineConfig.mockImplementation(() => ({}));
    mockLoadEnv.mockReturnValue({});
    
    process.env.APPDATA = '/test/appdata';
    
    expect(async () => {
      await import('./vite.config.js');
    }).not.toThrow();
    
    expect(mockChildProcess.spawnSync).toHaveBeenCalledWith('dotnet', [
      'dev-certs',
      'https',
      '--export-path',
      expect.stringContaining('restaurantweb.pem'),
      '--format',
      'Pem',
      '--no-password',
    ], { stdio: 'inherit' });
  });

  it('should throw error when certificate generation fails', async () => {
    mockFs.existsSync.mockReturnValue(false);
    mockChildProcess.spawnSync.mockReturnValue({ status: 1 });
    
    process.env.APPDATA = '/test/appdata';
    
    expect(async () => {
      await import('./vite.config.js');
    }).rejects.toThrow('Could not create certificate.');
  });

  it('should use HOME directory when APPDATA is not available', async () => {
    process.env.APPDATA = '';
    process.env.HOME = '/home/user';
    
    mockFs.existsSync.mockReturnValue(true);
    mockDefineConfig.mockImplementation(() => ({}));
    mockLoadEnv.mockReturnValue({});
    
    await import('./vite.config.js');
    
    expect(mockPath.join).toHaveBeenCalledWith('/home/user/.aspnet/https', 'restaurantweb.pem');
    expect(mockPath.join).toHaveBeenCalledWith('/home/user/.aspnet/https', 'restaurantweb.key');
  });
});