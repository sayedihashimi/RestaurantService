import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'

describe('eslint.config.js', () => {
  let mockJs: any
  let mockGlobals: any
  let mockReact: any
  let mockReactHooks: any
  let mockReactRefresh: any
  let originalModules: Map<string, any>

  beforeEach(() => {
    // Store original modules
    originalModules = new Map()

    // Mock @eslint/js
    mockJs = {
      configs: {
        recommended: {
          rules: {
            'no-unused-vars': 'error',
            'no-undef': 'error'
          }
        }
      }
    }

    // Mock globals
    mockGlobals = {
      browser: {
        window: 'readonly',
        document: 'readonly',
        console: 'readonly'
      }
    }

    // Mock eslint-plugin-react
    mockReact = {
      configs: {
        recommended: {
          rules: {
            'react/prop-types': 'error',
            'react/jsx-uses-react': 'error'
          }
        },
        'jsx-runtime': {
          rules: {
            'react/jsx-uses-react': 'off',
            'react/react-in-jsx-scope': 'off'
          }
        }
      }
    }

    // Mock eslint-plugin-react-hooks
    mockReactHooks = {
      configs: {
        recommended: {
          rules: {
            'react-hooks/rules-of-hooks': 'error',
            'react-hooks/exhaustive-deps': 'warn'
          }
        }
      }
    }

    // Mock eslint-plugin-react-refresh
    mockReactRefresh = {
      rules: {
        'only-export-components': () => ({})
      }
    }

    // Mock the modules
    vi.doMock('@eslint/js', () => ({ default: mockJs }))
    vi.doMock('globals', () => ({ default: mockGlobals }))
    vi.doMock('eslint-plugin-react', () => ({ default: mockReact }))
    vi.doMock('eslint-plugin-react-hooks', () => ({ default: mockReactHooks }))
    vi.doMock('eslint-plugin-react-refresh', () => ({ default: mockReactRefresh }))
  })

  afterEach(() => {
    vi.doUnmock('@eslint/js')
    vi.doUnmock('globals')
    vi.doUnmock('eslint-plugin-react')
    vi.doUnmock('eslint-plugin-react-hooks')
    vi.doUnmock('eslint-plugin-react-refresh')
    vi.resetAllMocks()
  })

  it('should import all required ESLint dependencies', async () => {
    const config = await import('./eslint.config.js')
    expect(config.default).toBeDefined()
    expect(Array.isArray(config.default)).toBe(true)
  })

  it('should configure proper file ignores', async () => {
    const config = await import('./eslint.config.js')
    const ignoreConfig = config.default.find((c: any) => c.ignores)
    
    expect(ignoreConfig).toBeDefined()
    expect(ignoreConfig.ignores).toEqual(['dist'])
  })

  it('should configure language options with browser globals', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.languageOptions.globals).toBe(mockGlobals.browser)
    expect(mainConfig.languageOptions.ecmaVersion).toBe(2020)
    expect(mainConfig.languageOptions.parserOptions.sourceType).toBe('module')
    expect(mainConfig.languageOptions.parserOptions.ecmaFeatures.jsx).toBe(true)
  })

  it('should configure React version in settings', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.settings.react.version).toBe('18.3')
  })

  it('should register all required plugins', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.plugins.react).toBe(mockReact)
    expect(mainConfig.plugins['react-hooks']).toBe(mockReactHooks)
    expect(mainConfig.plugins['react-refresh']).toBe(mockReactRefresh)
  })

  it('should merge rules from all plugin configurations', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    // Should include rules from @eslint/js
    expect(mainConfig.rules['no-unused-vars']).toBe('error')
    expect(mainConfig.rules['no-undef']).toBe('error')
    
    // Should include rules from react recommended
    expect(mainConfig.rules['react/prop-types']).toBe('error')
    
    // Should include rules from react jsx-runtime
    expect(mainConfig.rules['react/jsx-uses-react']).toBe('off')
    expect(mainConfig.rules['react/react-in-jsx-scope']).toBe('off')
    
    // Should include rules from react-hooks
    expect(mainConfig.rules['react-hooks/rules-of-hooks']).toBe('error')
    expect(mainConfig.rules['react-hooks/exhaustive-deps']).toBe('warn')
  })

  it('should override specific rules', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.rules['react/jsx-no-target-blank']).toBe('off')
    expect(mainConfig.rules['react-refresh/only-export-components']).toEqual([
      'warn',
      { allowConstantExport: true }
    ])
  })

  it('should handle files pattern correctly', async () => {
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.files).toEqual(['**/*.{js,jsx}'])
  })

  it('should handle missing plugin configurations gracefully', async () => {
    // Mock a plugin without configs
    const mockPluginWithoutConfigs = {}
    vi.doMock('eslint-plugin-react', () => ({ default: mockPluginWithoutConfigs }))
    
    const config = await import('./eslint.config.js')
    const mainConfig = config.default.find((c: any) => c.files)
    
    expect(mainConfig.rules).toBeDefined()
    // Should still have other rules even if one plugin fails
    expect(mainConfig.rules['no-unused-vars']).toBe('error')
  })

  it('should maintain proper config array structure', async () => {
    const config = await import('./eslint.config.js')
    
    expect(Array.isArray(config.default)).toBe(true)
    expect(config.default).toHaveLength(2)
    
    // First config should be ignore config
    expect(config.default[0]).toHaveProperty('ignores')
    
    // Second config should be main config
    expect(config.default[1]).toHaveProperty('files')
    expect(config.default[1]).toHaveProperty('languageOptions')
    expect(config.default[1]).toHaveProperty('settings')
    expect(config.default[1]).toHaveProperty('plugins')
    expect(config.default[1]).toHaveProperty('rules')
  })
})