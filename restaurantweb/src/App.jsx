import { useEffect, useState } from 'react';
import './App.css';
//import MenuItem from './components/MenuItem';
import Menu from './components/Menu';

function App() {
    const [menuItems, setForecasts] = useState();
    useEffect(() => {
        populateWeatherData();
    }, []);
    const contents = menuItems === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <Menu items={menuItems} />;
    return (
        <div>
            <h1 id="tableLabel">Menu Items</h1>

            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('api/menuitem');
        const data = await response.json();
        setForecasts(data);
    }
}
export default App;