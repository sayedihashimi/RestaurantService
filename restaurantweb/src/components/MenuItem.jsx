import React, { useState } from 'react';
import PropTypes from 'prop-types';

function MenuItem({ id, name, emojiName, price, description, category, truncate = true }) {
    const [truncateDescription, setTruncateDescription] = useState(truncate);

    let desc = truncateDescription ? description.slice(0, 50) + '...' : description;

    return (
            <div className="menu-item" id={id} data-menu-category={category}>
                <h2>{name}</h2>
                <h3>{emojiName}</h3>
                <p>{desc}</p>
                <p>Price: ${price?.toFixed(2)}</p>
                <button onClick={()=>setTruncateDescription((previous)=>!previous)}>{truncateDescription ? 'More' : 'Less'}</button>
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
    truncate: PropTypes.bool,
};

export default MenuItem;