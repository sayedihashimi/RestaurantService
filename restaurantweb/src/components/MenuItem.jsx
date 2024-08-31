import React, { useState } from 'react';
import PropTypes from 'prop-types';

function MenuItem({ menuItem, truncate = true }) {
    const { id, name, emojiName, price, description, category } = menuItem;
    const [truncateDescription, setTruncateDescription] = useState(truncate);

    let truncateLength = 50;
    let desc = truncateDescription ? description.slice(0, truncateLength) + '...' : description;

    return (
        <div className="menu-item" id={id} data-menu-category={category}>
            <h2>{name}</h2>
            <h3>{emojiName}</h3>
            <p>{desc}</p>
            <p>Price: ${price?.toFixed(2)}</p>
            <button onClick={() => setTruncateDescription((previous) => !previous)} hidden={(description.length <= truncateLength)}>
                {truncateDescription ? 'More' : 'Less'}
            </button>
        </div>
    );
}

MenuItem.propTypes = {
    menuItem: PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string,
        emojiName: PropTypes.string,
        price: PropTypes.number,
        description: PropTypes.string,
        category: PropTypes.int,
    }).isRequired,
    truncate: PropTypes.bool,
};

export default MenuItem;