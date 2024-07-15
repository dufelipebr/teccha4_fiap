import React, { useState } from 'react';

const PaymentInfoPage: React.FC = () => {
    const [paymentMethod, setPaymentMethod] = useState('');
    const [creditCardNumber, setCreditCardNumber] = useState('');
    const [expirationDate, setExpirationDate] = useState('');
    const [cardholderName, setCardholderName] = useState('');

    const handlePaymentMethodChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setPaymentMethod(event.target.value);
    };

    const handleCreditCardNumberChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCreditCardNumber(event.target.value);
    };

    const handleExpirationDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setExpirationDate(event.target.value);
    };

    const handleCardholderNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCardholderName(event.target.value);
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        // Perform validation and submit the payment information
        // You can use the collected payment information here

        // Reset the form after submission
        setPaymentMethod('');
        setCreditCardNumber('');
        setExpirationDate('');
        setCardholderName('');
    };

    return (
        <div>
            <h1>Payment Information</h1>
            <form onSubmit={handleSubmit}>
                <label>
                    Payment Method:
                    <select value={paymentMethod} onChange={handlePaymentMethodChange}>
                        <option value="">Select Payment Method</option>
                        <option value="creditCard">Credit Card</option>
                        <option value="debitCard">Debit Card</option>
                        <option value="pix">PIX</option>
                        <option value="boleto">Boleto</option>
                    </select>
                </label>
                <br />
                {paymentMethod === 'creditCard' && (
                    <>
                        <label>
                            Credit Card Number:
                            <input type="text" value={creditCardNumber} onChange={handleCreditCardNumberChange} />
                        </label>
                        <br />
                        <label>
                            Expiration Date:
                            <input type="text" value={expirationDate} onChange={handleExpirationDateChange} />
                        </label>
                        <br />
                        <label>
                            Cardholder Name:
                            <input type="text" value={cardholderName} onChange={handleCardholderNameChange} />
                        </label>
                        <br />
                    </>
                )}
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default PaymentInfoPage;