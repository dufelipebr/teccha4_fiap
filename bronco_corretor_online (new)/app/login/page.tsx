
'use client'; 
import { useState, useEffect } from 'react'
import { login } from './api';
import { HiMail, HiInformationCircle } from "react-icons/hi";
import { Alert } from "flowbite-react";
import { useRouter } from 'next/navigation';
import { SaveLoginInfo } from '../lib/login';

const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regular expression for email validation
const fetcher = (...args) => fetch(...args).then((res) => res.json())



export  default function Page() 
{
  // console.log('initialized Example');
  // console.log('ENV' + process.env.API_URL_BRONCO);

  const [formData, setFormData] = useState({email: '',  password: ''});
  const [errors, setErrors] = useState({ email: ''});
  const [isLoading, setLoading] = useState(false)
  const [validationMessage, setValidationMessage] = useState("");
  const [errorDisplay, setErrorDisplay] = useState(false);
  const router = useRouter()

  
  // const [data, setData] = useState(null)
  
 
  // useEffect(() => {
  //   fetch(process.env.API_URL_BRONCO + '/api/profile-data')
  //     .then((res) => res.json())
  //     .then((data) => {
  //       setData(data)
  //       setLoading(false)
  //     })
  // }, [])

 
 



  const handleChange = (event) => 
  {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });

    // Update error message on change
    if (event.target.name === 'email') {
      setErrors({
        email: validateEmail(event.target.value) ? '' : 'Email inválido',
      });
    }
  };

  const validateEmail = (email) => 
  {
    return EMAIL_REGEX.test(email);
  };



  const handleSubmit = async (event) => 
  {
    event.preventDefault();
    console.log('Email:' + formData.email);
    console.log('Pwd:' + formData.password);
    setLoading(true);
    if (!errors.email) 
      var result = await login(formData.email, formData.password)

    setLoading(false); 
    if (result?.message == 'Login success')
    {
      setErrorDisplay(false);
      SaveLoginInfo(result.data.nome, result.data.email, result.data.id, result.data.tipoPermissao, result.data.token);
      router.back();
    } 
    else
    {
      setValidationMessage('usuario e ou senha não encontrados, verifique as informações.');
      setErrorDisplay(true);
    }

    
  }

  function classNames(...classes) 
  {
    return classes.filter(Boolean).join(' ')
  }



  return (
    <>
      <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          {/*<img
            className="mx-auto h-10 w-auto"
            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
            alt="Your Company"
          />*/}
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
           Entrar na minha conta BRONCO
          </h2>
        </div>
        <p  
         className={classNames(
          isLoading ? '' : 'hidden',
          'border-red-500 bg-red-100 text-red-700 dark:bg-red-200 dark:text-red-800 max-w-xl align-middle mx-auto mt-4 px-4 py-3 rounded-md shadow-sm ring-1 ring-inset ring-gray-300 dark:ring-gray-700 dark:ring-inset focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6',
        )}
        >{"Carregando informações..."}</p>
          <Alert 
              color="failure" 
              icon={HiInformationCircle} 
              className={classNames(
                errorDisplay ? '' : 'hidden',
                'border-red-500 bg-red-100 text-red-700 dark:bg-red-200 dark:text-red-800 max-w-xl align-middle mx-auto mt-4 px-4 py-3 rounded-md shadow-sm ring-1 ring-inset ring-gray-300 dark:ring-gray-700 dark:ring-inset focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6',
              )}
            >
            <span className="font-medium">Info alert! {validationMessage}</span>
          </Alert> 
        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm text-xs text-orange-400">
          <form className="space-y-6" onSubmit={handleSubmit} method="POST">
            <div>
              <label htmlFor="email" className="block text-sm font-medium leading-6 text-gray-900">
                Email
              </label>
              <div className="mt-2">
                <input
                  id="email"
                  name="email"
                  type="email"
                  autoComplete="email"
                  required
                  value={formData.email}
                  onChange={handleChange}
                  className={"block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 px-2"}
                />
              </div>
            </div>

            <div>
              <div className="flex items-center justify-between">
                <label htmlFor="password" className="block text-sm font-medium leading-6 text-gray-900">
                  Senha
                </label>
                <div className="text-sm">
                  <a href="#" className="font-semibold text-indigo-600 hover:text-indigo-500">
                    Esqueci minha senha?
                  </a>
                </div>
              </div>
              <div className="mt-2">
                <input
                  id="password"
                  name="password"
                  type="password"
                  autoComplete="current-password"
                  required
                  value={formData.password}
                  onChange={handleChange}
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 px-2"
                />
              </div>
            </div>

            <div>
              <button
                type="submit"
                className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 "
              >
                Entrar
              </button>
            </div>
          </form>

          <p className="mt-10 text-center text-sm text-gray-500">
            Ainda não sou membro?{' '}<br></br>
            <a href="/register" className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500">
              Registrar-se agora para obter o meu seguro
            </a>
          </p>
        </div>
      </div>
    </>
  )
}
