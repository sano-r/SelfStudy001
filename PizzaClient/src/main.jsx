import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
//import App from './App.jsx'
//import './index.css'
import Pizza from './Pizza.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Pizza />
  </StrictMode>,
)
