import constants from '../lib/constants'

interface ResponseData {
    message: string, 
    data: {
        "id": string,
        "email": string,
        "nome": string,
        "tipoPermissao": string,
        "token": string
    }
}


export const login = async (username: string, password: string) => 
{
    //const url = 'https://dev-azure-brazilsouth-broncoapi.azurewebsites.net/login';
    //const url = process.env.API_URL_BRONCO + '/login';
    const url = constants.API_URL_BRONCO_CORRETOR + '/login';
    console.log(url);
    const data = {
        email: username,
        senha: password
    };

    try {
        console.log(data)
        console.log('ENV:' + process.env.API_URL_BRONCO);

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) 
        {
            // Handle successful login
            const responseData = await response.json();

            return {
                message: 'Login success', 
                data: {id:responseData.usuario.id, 
                email: responseData.usuario.email, 
                nome: responseData.usuario.nome,  
                tipoPermissao: responseData.usuario.tipoPermissao, 
                token: responseData.token} 
            };
        } else 
        {
            // Handle login error
            console.error('Login failed');
            return {message: 'Login failed', data: {id:'', email: '', nome: '', senha: '', tipoPermissao: '', token: ''} };
        }
    } catch (error) {
        console.error('An error occurred', error);
    }
};

