import React, { useEffect,useState } from 'react';
import PropTypes from 'prop-types';
import MenuItem from './MenuItem';
import './Menu.css';

function Menu() {
    const [menuItems, setMenuItems] = useState();
    useEffect(() => {
        populateMenuItems();
    }, []);

    return (
        <div id="menuContainer">
        { !menuItems ?
            (<p><em>Loading... Waiting for backend to start, try again soon.</em></p>) :
            (
                <div className="menu-grid">
                    {menuItems.map(item => (
                        <MenuItem key={item.id} menuItem={item} />
                    ))}
                 </div>
            )
        }
        </div>
    );

    async function populateMenuItems() {
        const response = await fetch('api/menuitem');
        const data = await response.json();
        setMenuItems(data);
    }
}

export default Menu;