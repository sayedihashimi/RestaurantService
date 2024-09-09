import './App.css';
import Menu from './components/Menu';
import { BrowserRouter, Route,Routes } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/about';
import Contact from './pages/Contact';
import NotFound from './pages/NotFound';
import NewOrderPage from './pages/NewOrderPage';
import Layout from './Layout';
import SignIn from './pages/SignIn';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Layout /> }>
                    <Route index element={<Home />} />
                    <Route path='/about' element={<About />} />
                    <Route path='/contact' element={<Contact />} />
                    <Route path='/menu' element={<Menu />} />
                    <Route path='/new-order' element={<NewOrderPage />} />
                    <Route path='/sign-in' element={<SignIn />} />
                    <Route path='*' element={<NotFound />} />
                </Route>                
            </Routes>            
        </BrowserRouter>
    );
}
export default App;