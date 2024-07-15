
import { RadioGroup } from '@headlessui/react';
import constants from '@/app/lib/constants'
import {PropostaData} from '@/app/lib/integrationInterface'

export const criar_proposta = async (checkout : PropostaData) => 
{
    console.log('api criar_proposta');
    const url = constants.API_URL_BRONCO_INTEGRATION + '/criar_proposta';
    console.log(url);
    console.log(JSON.stringify(checkout));
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(checkout)
        });
        console.log("response");
        console.log(response)
        if (response.ok) 
        {
            console.log(response);
            console.log(response.statusText);
            console.log(response.body);
            // Handle successful login
            const responseData = await response.json();

            return responseData;
        } 
        else 
        {
            // Handle login error]
            console.log(response.statusText);
            return response.statusText;
        }
    } catch (error) {
        console.error('An error occurred', error);
    }
};

