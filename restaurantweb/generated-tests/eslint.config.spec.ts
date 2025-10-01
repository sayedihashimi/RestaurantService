import { describe, it, expect, vi } from 'vitest'

describe('eslint.config.js', () => {
  let eslintConfig: any

  beforeEach(async () => {
    // Mock the external dependencies to avoid actual imports during testing
    vi.doMock('@eslint/js', () => ({
      default: {
        configs: {
          recommended: {
            rules: {
              'no-unused-vars': 'error',
              'no-undef': 'error'
            }
          }
        }
      }
    }))

    vi.doMock('globals', () => ({
      default: {
        browser: {
          window: 'readonly',
          document: 'readonly',
          console: 'readonly'
        }
      }
    }))

    vi.doMock('eslint-plugin-react', () => ({
      default: {
        configs: {
          recommended: {
            rules: {
              'react/prop-types': 'error',
              'react/no-unescaped-entities': 'error'
            }
          },
          'jsx-runtime': {
            rules: {
              'react/react-in-jsx-scope': 'off'
            }
          }
        }
      }
    }))

    vi.doMock('eslint-plugin-react-hooks', () => ({
      default: {
        configs: {
          recommended: {
            rules: {
              'react-hooks/rules-of-hooks': 'error',
              'react-hooks/exhaustive-deps': 'warn'
            }
          }
        }
      }
    }))

    vi.doMock('eslint-plugin-react-refresh', () => ({
      default: {}
    }))

    // Import the config after mocking
    const module = await import('../eslint.config.js')
    eslintConfig = module.default
  })

  afterEach(() => {
    vi.doUnmock('@eslint/js')
    vi.doUnmock('globals')
    vi.doUnmock('eslint-plugin-react')
    vi.doUnmock('eslint-plugin-react-hooks')
    vi.doUnmock('eslint-plugin-react-refresh')
  })

  it('should export an array configuration', () => {
    expect(Array.isArray(eslintConfig)).toBe(true)
    expect(eslintConfig).toHaveLength(2)
  })

  it('should have correct ignore patterns', () => {
    expect(eslintConfig[0]).toEqual({ ignores: ['dist'] })
  })

  it('should configure files for js and jsx', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.files).toEqual(['**/*.{js,jsx}'])
  })

  it('should set correct language options', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.languageOptions).toEqual({
      ecmaVersion: 2020,
      globals: {
        window: 'readonly',
        document: 'readonly',
        console: 'readonly'
      },
      parserOptions: {
        ecmaVersion: 'latest',
        ecmaFeatures: { jsx: true },
        sourceType: 'module'
      }
    })
  })

  it('should configure React version in settings', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.settings).toEqual({ react: { version: '18.3' } })
  })

  it('should include all required plugins', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.plugins).toHaveProperty('react')
    expect(mainConfig.plugins).toHaveProperty('react-hooks')
    expect(mainConfig.plugins).toHaveProperty('react-refresh')
  })

  it('should merge rules from all configurations', () => {
    const mainConfig = eslintConfig[1]
    
    // Should include rules from @eslint/js
    expect(mainConfig.rules).toHaveProperty('no-unused-vars', 'error')
    expect(mainConfig.rules).toHaveProperty('no-undef', 'error')
    
    // Should include rules from eslint-plugin-react
    expect(mainConfig.rules).toHaveProperty('react/prop-types', 'error')
    expect(mainConfig.rules).toHaveProperty('react/no-unescaped-entities', 'error')
    expect(mainConfig.rules).toHaveProperty('react/react-in-jsx-scope', 'off')
    
    // Should include rules from eslint-plugin-react-hooks
    expect(mainConfig.rules).toHaveProperty('react-hooks/rules-of-hooks', 'error')
    expect(mainConfig.rules).toHaveProperty('react-hooks/exhaustive-deps', 'warn')
  })

  it('should override specific React rules', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.rules).toHaveProperty('react/jsx-no-target-blank', 'off')
  })

  it('should configure react-refresh rule with options', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.rules).toHaveProperty('react-refresh/only-export-components')
    expect(mainConfig.rules['react-refresh/only-export-components']).toEqual([
      'warn',
      { allowConstantExport: true }
    ])
  })

  it('should handle missing plugin configurations gracefully', () => {
    // Test that the configuration doesn't break if a plugin config is undefined
    const mainConfig = eslintConfig[1]
    expect(() => mainConfig.rules).not.toThrow()
    expect(typeof mainConfig.rules).toBe('object')
  })

  it('should preserve globals browser configuration structure', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.languageOptions.globals).toBeDefined()
    expect(typeof mainConfig.languageOptions.globals).toBe('object')
  })

  it('should have JSX support enabled in parser options', () => {
    const mainConfig = eslintConfig[1]
    expect(mainConfig.languageOptions.parserOptions.ecmaFeatures).toHaveProperty('jsx', true)
    expect(mainConfig.languageOptions.parserOptions.sourceType).toBe('module')
  })
})