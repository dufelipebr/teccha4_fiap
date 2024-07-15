import constants from '../lib/constants'
import {ProdutoData} from '../lib/integrationInterface'



export const get_lista_produtos = async (token: string) : Promise<ProdutoData[]> => 
{
    console.log(constants.API_URL_BRONCO_INTEGRATION + '/listar_produtos');
    const url = constants.API_URL_BRONCO_INTEGRATION + '/listar_produtos';
    // const data = {
    //     email: username,
    //     senha: password
    // };

    try {
        //console.log(data)
        //console.log('ENV:' + process.env.API_URL_BRONCO);

        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
           // body: JSON.stringify(data)
        });

        if (response.ok) 
        {
            console.log('entrou no response.ok');
            // Handle successful login
          
            const responseData = await response.json();
            let i = 0;
            let seguros  = [];
            responseData.forEach(function (value) {
                seguros.push(value);
                // console.log(i)
                 console.log(value);
                // i++;
              });
              
              return seguros;
            // return {
            //     message: 'Ok', 
            //     data: seguros
            // };
        } else 
        {
            // Handle login error
            console.error('Failed');
            return null; //{message: 'fail', data: null };
        }
    } catch (error) {
        console.error('An error occurred', error);
    }
};

