import React from 'react';
import PropTypes from 'prop-types';
import MenuItem from './MenuItem';
import './Menu.css';

function Menu({ items }) {
    return (
        <div className="menu-grid">
            {items.map(item => (
                <MenuItem key={item.id} menuItem={item} />
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
            category: PropTypes.int,
        })
    ).isRequired,
};

export default Menu;