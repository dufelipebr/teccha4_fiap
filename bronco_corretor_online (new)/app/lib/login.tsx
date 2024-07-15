
export function LoginInfo(): { username: string, password: string } | null 
{
    const loginData = localStorage.getItem('loginInfo');
    
    if (loginData) {
        return JSON.parse(loginData);
    }
    
    return null;
}

export function SaveLoginInfo(username: string, email: string, id:string, permission: number  , token: string )
{
    //const loginData = localStorage.getItem('loginInfo');
    console.log('SaveLoginInfo:' + username + ' ' + email + ' ' + id + ' ' + permission + ' ' + token);
    localStorage.clear();
    localStorage.setItem('loginInfo', JSON.stringify({ username, email, id, permission, token }));

    // localStorage.setItem('token', result.data.token);
    // localStorage.setItem('id', result.data.id);
    // localStorage.setItem('nome', result.data.nome);
    // localStorage.setItem('email', result.data.email);
    // localStorage.setItem('tipoPermissao', result.data.tipoPermissao);
    
    // if (loginData) {
    //     return JSON.parse(loginData);
    // }
}
