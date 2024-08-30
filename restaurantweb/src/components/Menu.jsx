import React from 'react';
import PropTypes from 'prop-types';
import MenuItem from './MenuItem';
import './Menu.css';

function Menu({ items }) {
    return (
        <div className="menu-grid">
            {items.map(item => (
                <MenuItem key={item.id} {...item} />
            ))}
        </div>
    );
}

Menu.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string,
            emojiName: PropTypes.string,
            price: PropTypes.number,
            description: PropTypes.string,
            category: PropTypes.string,            
        })
    ).isRequired,
};

export default Menu;