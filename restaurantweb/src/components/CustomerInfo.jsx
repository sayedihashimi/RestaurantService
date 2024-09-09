import React, { useEffect, useState } from 'react';
import './CustomerInfo.css';
function CustomerInfo() {
    const [contacts, setContacts] = useState([]);
    const [selectedContact, setSelectedContact] = useState('');
    const [selectedContactDetails, setSelectedContactDetails] = useState(null);

    useEffect(() => {
        // Fetch the contacts from the API
        fetch('/api/contact')
            .then(response => response.json())
            .then(data => setContacts(data))
            .catch(error => console.error('Error fetching contacts:', error));
    }, []);

    useEffect(() => {
        // Update selectedContactDetails when selectedContact changes
        const contact = contacts.find(contact => contact.id === parseInt(selectedContact));
        setSelectedContactDetails(contact);
    }, [selectedContact, contacts]);

    const handleContactChange = (event) => {
        setSelectedContact(event.target.value);
    };
  return (
      <div className="customerContainer">
          <p>Customer name:&nbsp;
              <select value={selectedContact} onChange={handleContactChange}>
                  <option value="">Select a customer</option>
                  {contacts.map(contact => (
                      <option key={contact.id} value={contact.id}>
                          {contact.name}
                      </option>
                  ))}
              </select></p>

          {selectedContactDetails && (
              <ul>
                  {Object.entries(selectedContactDetails).map(([key, value]) => (
                      <li key={key} hidden={key == 'name'}>
                          <strong>{key}:</strong> <span>{value}</span>
                      </li>
                  ))}
              </ul>
          )}
      </div>
  );
}

export default CustomerInfo;