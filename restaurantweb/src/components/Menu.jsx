import React, { useEffect,useState } from 'react';
import MenuItem from './MenuItem';
import './Menu.css';
import { Spinner, SpinnerSize } from '@fluentui/react/lib/Spinner';

function Menu() {
    const [menuItems, setMenuItems] = useState();
    useEffect(() => {
        populateMenuItems();
    }, []);

    const spinnerStyles = {
        label: {
            fontSize: '2rem',
        },
    };

    return (
        <div id="menuContainer">
        { !menuItems ?
            (<div>
                    <Spinner label="I am definitely loading..." size={SpinnerSize.large} styles={spinnerStyles} />
            </div>) :
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