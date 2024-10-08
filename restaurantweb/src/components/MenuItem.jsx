﻿import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Button } from "@fluentui/react-components";

function MenuItem({ menuItem, truncate = true }) {
    const { id, name, emojiName, price, description, category } = menuItem;
    const [truncateDescription, setTruncateDescription] = useState(truncate);

    let truncateLength = 50;
    let desc = truncateDescription ? description.slice(0, truncateLength) + '...' : description;

    return (
        <div className="menu-item" id={id} data-menu-category={category}>
            <h2>{name}</h2>
            <h3 className="emoji">{emojiName}</h3>
            <p>{desc}</p>
            <p>Price: ${price?.toFixed(2)}</p>
            <Button onClick={() => setTruncateDescription((previous) => !previous)} hidden={(description.length < truncateLength)}>
                {truncateDescription ? 'More' : 'Less'}
            </Button>
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
        category: PropTypes.number,
    }).isRequired,
    truncate: PropTypes.bool,
};

export default MenuItem;