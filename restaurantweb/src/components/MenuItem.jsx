import React from 'react';
import PropTypes from 'prop-types';

function MenuItem({ id, name, emojiName, price, description, category }) {
    return (
        <div className="menu-item" id={id} data-menu-category={category}>
            <h2>{name}</h2>
            <h3>{emojiName}</h3>
            <p>{description}</p>
            <p>Price: ${price?.toFixed(2)}</p>
        </div>
    );
}

MenuItem.propTypes = {
    id: PropTypes.number.isRequired,
    name: PropTypes.string,
    emojiName: PropTypes.string,
    price: PropTypes.number,
    description: PropTypes.string,
    category: PropTypes.string,
};

export default MenuItem;