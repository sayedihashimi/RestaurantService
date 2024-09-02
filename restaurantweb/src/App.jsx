import { useEffect, useState } from 'react';
import './App.css';
import Menu from './components/Menu';

function App() {
    const [menuItems, setMenuItems] = useState();
    useEffect(() => {
        populateMenuItems();
    }, []);

    return (
        <div>
            <h1>Menu Items</h1>
            {menuItems === undefined ?
                (<p><em>Loading... Waiting for backend to start, try again soon.</em></p>) :
                (<Menu items={menuItems} />)
            }
        </div>
    );

    async function populateMenuItems() {
        const response = await fetch('api/menuitem');
        const data = await response.json();
        setMenuItems(data);
    }
}
export default App;