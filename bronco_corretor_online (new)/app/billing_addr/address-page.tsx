import React, { useState } from 'react';

interface Address 
{
    identificador: string;
    street: string;
    number: string;
    city: string;
    state: string;
    zip: string;
}

const BillingAddressPage: React.FC = () => 
{
    const [address, setAddress] = useState<Address>
    ({
        identificador : '',
        street: '',
        number: '',
        city: '',
        state: '',
        zip: '',
    });

    const Billing_Address: Address[] = [
        { identificador: '01', street: 'Rua George Smith', number: '100', city: 'são paulo', state: 'SP', zip: '05074-010'},
        { identificador: '02', street: 'Rua Tonelero',  number: '200', city: 'são paulo', state: 'SP', zip: '05000-000'}
    ];


    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setAddress((prevAddress) => ({
            ...prevAddress,
            [name]: value,
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Handle form submission here
        console.log(address);
    };

    return (
        <div>
            <h1>Billing Address</h1>
            <div>
                <select option="">
                    <option>{"Create New"}</option>
                    {Billing_Address.map((add) => (
                        <option value={add.identificador}>{add.street} - {add.number} / {add.city} - {add.state}</option>
                    ))}
                </select>
            </div>
            <form onSubmit={handleSubmit}>
                <label>
                    Street:
                    <input
                        type="text"
                        name="street"
                        value={address.street}
                        onChange={handleInputChange}
                    />
                </label>
                <label>
                    City:
                    <input
                        type="text"
                        name="city"
                        value={address.city}
                        onChange={handleInputChange}
                    />
                </label>
                <label>
                    State:
                    <input
                        type="text"
                        name="state"
                        value={address.state}
                        onChange={handleInputChange}
                    />
                </label>
                <label>
                    ZIP:
                    <input
                        type="text"
                        name="zip"
                        value={address.zip}
                        onChange={handleInputChange}
                    />
                </label>
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default BillingAddressPage;