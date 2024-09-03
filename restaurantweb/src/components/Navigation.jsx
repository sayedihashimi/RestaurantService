import { NavLink } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import './Navigation.css';
import { Link } from '@fluentui/react';
import { DefaultButton, CommandBar } from '@fluentui/react';
import { FocusTrapZone } from '@fluentui/react/lib/FocusTrapZone';

function Navigation() {
    const navigate = useNavigate();

    const items = [
        {
            key: 'home',
            text: 'Home',
            iconProps: { iconName: 'HomeSolid' },
            onClick: () => navigate('/'),
        },
        {
            key: 'menu',
            text: 'Menu',
            iconProps: { iconName: 'ContextMenu' },
            onClick: () => navigate('/menu'),
        },
        {
            key: 'contact',
            text: 'Contact',
            iconProps: { iconName: 'ContactInfo' },
            onClick: () => navigate('/contact'),
        },
    ];
    // TODO: far items don't do anything yet, they should do something.
    const _farItems = [
        {
            key: 'tile',
            text: 'Grid view',
            // This needs an ariaLabel since it's icon-only
            ariaLabel: 'Grid view',
            iconOnly: true,
            iconProps: { iconName: 'Tiles' },
            onClick: () => console.log('Tiles'),
        },
        {
            key: 'info',
            text: 'Info',
            ariaLabel: 'Info',
            iconOnly: true,
            iconProps: { iconName: 'Info' },
            onClick: () => console.log('Info'),
        },
    ];
    return (
        <nav>
            <CommandBar
                items={items}
                farItems={_farItems}
                styles={{
                    root: {
                        padding: '.7rem 1.4rem', // Add vertical padding to the CommandBar
                    },
                    primarySet: {
                        gap: '.7rem', // Add gap between primary items
                    },
                    secondarySet: {
                        gap: '.7rem', // Add gap between far items
                    },
                    item: {
                        margin: '0 .7rem', // Add horizontal margin to each item
                        padding: '.35rem 0', // Add vertical padding to each item
                    },
                }}
            />
        </nav>
    );
}

export default Navigation;