import React, { useEffect,useState } from 'react';
import MenuItem from './MenuItem';
import './Menu.css';
import { makeStyles, Spinner } from "@fluentui/react-components";

const useStyles = makeStyles({
    container: {
        "> div": { padding: "20px" },
    },
});
function Menu() {
    const styles = useStyles();
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
            (<div className={styles.container}>
                    <Spinner label="I am definitely loading..."
                        styles={spinnerStyles}
                        labelPosition="below"
                        size="huge" />
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
        console.log("Fetching menu items...");
        const response = await fetch('api/menuitem');
        const data = await response.json();
        setMenuItems(data);
    }
}

export default Menu;