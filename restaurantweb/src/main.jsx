import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import { initializeIcons } from '@fluentui/font-icons-mdl2';
import { FluentProvider, webDarkTheme } from '@fluentui/react-components';
initializeIcons();

createRoot(document.getElementById('root')).render(
  <FluentProvider theme={webDarkTheme}>
    <App />
  </FluentProvider>,
)
