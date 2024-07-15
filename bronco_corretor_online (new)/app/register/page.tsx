'use client';
import { useState } from 'react'
import { ChevronDownIcon } from '@heroicons/react/20/solid'
import { Field, Label, Switch } from '@headlessui/react'
import { Datepicker, Modal, Button, TextInput, Alert } from "flowbite-react";
import { HiMail, HiInformationCircle } from "react-icons/hi";
import { text } from 'stream/consumers';
import { registrar_usuario } from './api';

const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regular expression for email validation

interface ErroInfo {
  id: string;
  message: string;
  isValid: boolean;
  type?: string;
}

interface ErrorState {
  errors: ErroInfo[];
}


function classNames(...classes) 
{
  return classes.filter(Boolean).join(' ')
}

export default function Page() 
{
  const [agreed, setAgreed] = useState(false)
  const [agreedNomeSocial, setAgreedNomeSocial] = useState(false)
  const [openModal, setOpenModal] = useState(false);
  const [modalSize, setModalSize] = useState<string>('md');
  const [policyApproval, setPolicyApproval] = useState(false);
  const [validationMessage, setValidationMessage] = useState("");
  const [errorDisplay, setErrorDisplay] = useState(false);
  const [formData, setFormData] = useState({
    nome: '',
    sobrenome: '',
    possui_nomesocial: false,
    nome_social: '',
    rg: '',
    cpf:'',
    sexo:'',
    data_nascimento:'',
    email: '',
    phone: '',
    profissao:'',
    renda:'',
    endereco_cep:'',
    endereco_rua:'',
    endereco_numero:'',
    endereco_complemento:'',
    endereco_cidade:'',
    endereco_bairro:'',
    senha:'', 
    confirmacao_senha: ''
  });

  // const [errors, setErrors] = useState<ErrorState>({
  //   errors: [
  //     {id: 'nome', message: 'Nome precisa ser preenchido', isValid:false}, 
  //     {id: 'sobrenome', message: 'SobreNome precisa ser preenchido', isValid:false},
  //     {id: 'nome_social', message: 'Nome Social precisa ser preenchido', isValid:false}, 
  //     {id: 'rg', message:'RG precisa ser preenchido', isValid:false},
  //     {id: 'cpf', message: 'CPF precisa ser preenchido', isValid:false},
  //     {id: 'email', message: 'Email precisa ser preenchido e estar no formato correto', type:'valide_email', isValid:false},
  //     {id: 'phone', message: 'Telefone precisa ser preenchido', isValid:false},
  //     {id: 'profissao', message: 'Profissão precisa ser preenchido', isValid:false},
  //     {id: 'renda', message: 'Renda precisa ser preenchido', type:'valide_number', isValid:false},
  //     {id: 'endereco_cep', message: 'CEP precisa ser preenchido', type:'valide_cep', isValid:false},
  //     {id: 'endereco_rua', message: 'Endereço precisa ser preenchido', isValid:false},
  //     {id: 'endereco_numero', message: 'Número precisa ser preenchido', type:'valide_number', isValid:false},
  //     //{id: 'endereco_complemento', message: 'Complemento precisa ser preenchido'},
  //     {id: 'endereco_cidade', message: 'Cidade precisa ser preenchido', isValid:false},
  //     {id: 'endereco_bairro', message: 'Bairro precisa ser preenchido', isValid:false}
  //   ]
  // });

  // function findErrorElement (findItem: string) : ErroInfo | null | undefined 
  // {
  //   //console.log('finditem:'+ findItem);
  //   errors.errors.forEach(function (err) 
  //   {
  //     if (err.id.normalize("NFKC") === findItem.normalize("NFKC"))
  //     {
  //       console.log('found:'+ err.id);
  //       console.log('found:'+ err.message);
  //       console.log('found:'+ err.type);
  //       console.log('found:'+ err.isValid);
  //       return err;
  //     }
  //   })
  //   return {id: '', message: '', isValid:true};
  // };

  const handleComboBox = (param) => 
  {
    //setSelectedDate(date);
    //formData.data_nascimento = date;
    console.log(param);
    console.log(param.target);
    console.log(param.target.value);
    console.log(param.target.name);
  };

  const handleDatePickerChange = (date) => 
  {
    //setSelectedDate(date);
    formData.data_nascimento = date;
    //console.log(date);
  };

  const handleChange = (event) => 
  {
    setFormData( {
      ...formData,
      [event.target.name]: event.target.value,
    });

    // // console.log(event);
    // // console.log(event.target);
    // // console.log(event.target.name);
    // // console.log(event.target.value);
    // // console.log(formData[event.target.name])
    // // if (event.target.name === 'email') 
    // // {
    // //   setErrors({email: validateEmail(event.target.value) ? '' : 'Formato de email inválido'});
    // // }
    // // else
    // // {
    // //   if (event.target.value != '')
    // //     Errors[event.target.name] = '';
    // // }
    // //if (event.target.)
    // if (event.target.value != '')
    //   findErrorElement(event.target.name).isValid = true;

  };

  const validateEmail = (email) => 
  {
    return EMAIL_REGEX.test(email);
  };

  const validateText = (textInfo) => 
  {
    return (textInfo != '' && textInfo.length > 3);
  };

  const validatePassword = (textInfo: string) => 
  {
    var validationRegex = [
      { regex: /.{7,}/ }, // min 7 letters,
      { regex: /[0-9]/ }, // numbers from 0 - 9
       { regex: /[a-z]/ }, // letters from a - z (lowercase)
       { regex: /[A-Z]/}, // letters from A-Z (uppercase),
      { regex: /[^A-Za-z0-9]/} // special characters
    ]

    let IsValidPassword  = true;
    console.log(textInfo);
    validationRegex.forEach((item, i)=> 
    {
      let isValid = item.regex.test(textInfo);
      console.log('item.regex.test', i, isValid);
      if (isValid == false)
      {
        IsValidPassword  = false;
      }
    })

    return IsValidPassword;
  };

  const handleSubmit = async (event) => 
  {
    event.preventDefault();
    console.log('handleSubmit initialized');
    setErrorDisplay(false);
    if (!agreed)
    {
      setValidationMessage("é necessário aceitar os termos para prosseguir, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if (!validateText(formData.nome))
    {
      setValidationMessage( "problema no preenchimento do nome, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if (agreedNomeSocial && !validateText(formData.nome_social))
    {
      setValidationMessage( "problema no preenchimento do nome social, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if(!validateEmail(formData.email))
    {
      setValidationMessage( "problema no preenchimento do e-mail, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if(!(formData.data_nascimento != null && formData.data_nascimento != ''))
    {
      setValidationMessage( "problema no preenchimento da data de nascimento favor verificar");
      setErrorDisplay(true);
      return;
    }

    if(!(formData.sexo != null && formData.sexo != ''))
    {
      setValidationMessage( "problema no preenchimento sexo, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if(!(formData.renda != null && formData.renda != ''))
    {
      setValidationMessage( "problema no preenchimento renda aproximada, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if(!validatePassword(formData.senha)) 
    {
      setValidationMessage("senha não é forte o suficiente, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if (formData.senha != formData.confirmacao_senha)
    {
      setValidationMessage("confirmação de senha diferente da senha, favor verificar");
      setErrorDisplay(true);
      return;
    }

    if (!errorDisplay) 
      var result = await registrar_usuario(formData)
      
    console.log('result');
    console.log(result);  
    if (result.message == "Ok")
    {
      setValidationMessage('Cadastro realizado com sucesso');
      setErrorDisplay(true);
    }
    else
    {
      setValidationMessage(result.data);
      setErrorDisplay(true);
    }

    console.log('handleSubmit submit');

    // console.log('nome', formData.nome);
    // console.log('sobrenome', formData.sobrenome);
    // console.log('possui_nomesocial', formData.possui_nomesocial);
    // console.log('nome_social', formData.nome_social);
    // console.log('rg', formData.rg);
    // console.log('cpf', formData.cpf);
    // console.log('sexo', formData.sexo);
    // console.log('data_nascimento', formData.data_nascimento);
    // console.log('email', formData.email);
    // console.log('phone', formData.phone);
    // console.log('profissao', formData.profissao);
    // console.log('renda', formData.renda);
    // console.log('senha', formData.senha);
    // console.log('confirmacao_senha', formData.confirmacao_senha);


  };

  // const submit = (event) => 
  // {
  //   console.log('submit initialized');
  // }


  return (
    <div className="isolate bg-orange-300 px-6 py-24 sm:py-32 lg:px-8">
      <div
        className="absolute inset-x-0 top-[-10rem] -z-10 transform-gpu overflow-hidden blur-3xl sm:top-[-20rem]"
        aria-hidden="true"
      >
        <div
          className="relative left-1/2 -z-10 aspect-[1155/678] w-[36.125rem] max-w-none -translate-x-1/2 rotate-[30deg] bg-gradient-to-tr from-[#ff80b5] to-[#9089fc] opacity-30 sm:left-[calc(50%-40rem)] sm:w-[72.1875rem]"
          style={{
            clipPath:
              'polygon(74.1% 44.1%, 100% 61.6%, 97.5% 26.9%, 85.5% 0.1%, 80.7% 2%, 72.5% 32.5%, 60.2% 62.4%, 52.4% 68.1%, 47.5% 58.3%, 45.2% 34.5%, 27.5% 76.7%, 0.1% 64.9%, 17.9% 100%, 27.6% 76.8%, 76.1% 97.7%, 74.1% 44.1%)',
          }}
        />
      </div>
      <div className="mx-auto max-w-2xl text-center">
        <h2 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">Registrar</h2>
        <p className="mt-2 leading-8 text-gray-600">
          {"Preencha abaixo a suas informações "}
        </p>
        <div>
        
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
        </div>
      </div>
      <form onSubmit={handleSubmit} method="POST" className="mx-auto mt-16 max-w-xl sm:mt-20">
        <div className="grid grid-cols-1 gap-x-8 gap-y-6 sm:grid-cols-2">
          <div>
            <label htmlFor="first-name" className="block text-sm font-semibold leading-6 text-gray-900">
              Nome
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="nome"
                id="nome"
                required
                value={formData.nome}
                onChange={handleChange}
                maxLength="50"
                autoComplete="given-name"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
              {/* <label 
                className={classNames(
                  errorDisplay ? '' : 'hidden',
                  'border-red-500 bg-red-100 text-red-700 dark:bg-red-200 dark:text-red-800',
                )}
              >
                {/////findErrorElement('aqui nome').message}
              </label> */}
              
            </div>
          </div>
           <div>
            <label htmlFor="last-name" className="block text-sm font-semibold leading-6 text-gray-900">
              SobreNome
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="sobrenome"
                id="sobrenome"
                autoComplete="family-name"
               required
                value={formData.sobrenome}
                onChange={handleChange}
                maxLength="50"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <div>
            <label htmlFor="last-name" className="block text-sm font-semibold leading-6 text-gray-900">Informar o nome social? </label>
            <Switch
                checked={agreedNomeSocial}
                onChange={setAgreedNomeSocial}
                className={classNames(
                  agreedNomeSocial ? 'bg-indigo-600' : 'bg-gray-200',
                  'flex w-8 flex-none cursor-pointer rounded-full p-px ring-1 ring-inset ring-gray-900/5 transition-colors duration-200 ease-in-out focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mt-3',
                )}
              >
                <span className="sr-only">Agree to policies</span>
                <span
                  aria-hidden="true"
                  className={classNames(
                    agreedNomeSocial ? 'translate-x-3.5' : 'translate-x-0',
                    'h-4 w-4 transform rounded-full bg-white shadow-sm ring-1 ring-gray-900/5 transition duration-200 ease-in-out',
                  )}
                />
              </Switch>
          </div>
          <div >
            <label htmlFor="nome_social" className="block text-sm font-semibold leading-6 text-gray-900">
              Nome Social
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="nome_social"
                id="nome_social"
                autoComplete="nome_social"
                value={agreedNomeSocial && formData.nome_social ? formData.nome_social : ""}
                onChange={handleChange}
                maxLength="50"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>
          
          <div >
            <label htmlFor="rg" className="block text-sm font-semibold leading-6 text-gray-900">
              RG
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="rg"
                id="rg"
               required
                value={formData.rg}
                onChange={handleChange}
                autoComplete="rg"
                maxLength="12"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>

                    
          <div >
            <label htmlFor="cpf" className="block text-sm font-semibold leading-6 text-gray-900">
              CPF
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="cpf"
                id="cpf"
                autoComplete="cpf"
               required
                value={formData.cpf}
                onChange={handleChange}
                maxLength="15"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <div>
            <label htmlFor="genero" className="block text-sm font-semibold leading-6 text-gray-900">
              Sexo
            </label>
            <select name="sexo" id="sexo"  onChange={handleChange} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-white dark:border-gray-600 dark:placeholder-gray-400 dark:text-slate-600 dark:focus:ring-blue-500 dark:focus:border-blue-500"> 
              <option value="">Selecionar uma opção</option>
              <option value="F">Feminino</option>
              <option value="M">Masculino</option>
              <option value="O">Não binário</option>
            </select>
          </div>

          <div >
            <label htmlFor="nascimento" className="block text-sm font-semibold leading-6 text-gray-900">
              Data de Nascimento            
            </label>
           

            <div>
              <Datepicker 
                onSelectedDateChanged={handleDatePickerChange} 
                id="data_nascimento" 
                name="data_nascimento" 
                minDate={new Date(1930, 1, 1)} 
                maxDate={new Date(2010, 1, 1)} 
                language="pt-BR" 
                labelTodayButton="Hoje" 
                labelClearButton="Limpar" />
            </div>


          </div>

        
          
          <div className="sm:col-span-2" >
            <label htmlFor="email" className="block text-sm font-semibold leading-6 text-gray-900">
              Email
            </label>
            <div className="mt-2.5">
            <TextInput 
                id="email" 
                name="email" 
                type="email"
               required
                onChange={handleChange} 
                rightIcon={HiMail} 
                value={formData.email}
                maxLength="50"
                placeholder="myaccount@gmail.com" 
                />
            </div>
          </div>
          
          <div>
            <label htmlFor="phone-number" className="block text-sm font-semibold leading-6 text-gray-900">
              Telefone
            </label>
            <div className="relative mt-2.5">
              <div className="absolute inset-y-0 left-0 flex items-center">
                <label htmlFor="country" className="sr-only">
                  Country
                </label>
                <select
                  id="country"
                  name="country"
                  className="h-full rounded-md border-0 bg-transparent bg-none py-0 pl-4 pr-9 text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm"
                >
                  <option>BR</option>
                </select>
              </div>
              <input
                type="tel"
                name="phone"
                id="phone"
                autoComplete="phone"
               required
                value={formData.phone}
                onChange={handleChange}
                className="block w-full rounded-md border-0 px-3.5 py-2 pl-20 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>
          <div>

          </div>
          
          <div >
            <label htmlFor="email" className="block text-sm font-semibold leading-6 text-gray-900">
              Profissão
            </label>
            <div className="mt-2.5">
              <input
                type="profissao"
                name="profissao"
                id="profissao"
               required
                autoComplete="profissao"
                value={formData.profissao}
                onChange={handleChange}
                maxLength="30"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>


          <div >
            <label htmlFor="renda" className="block text-sm font-semibold leading-6 text-gray-900">
              Renda aproximada
            </label>
            <div className="mt-2.5">
               <select onChange={handleChange} name="renda" id="renda" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-white dark:border-gray-600 dark:placeholder-gray-400 dark:text-slate-600 dark:focus:ring-blue-500 dark:focus:border-blue-500">
                <option value="">Selecionar uma opção</option>
                <option value="1">até 1.500,00</option>
                <option value="2">de  1.500,00 - 3.000,00</option>
                <option value="3">de 3.000,00 - 5.000,00</option>
                <option value="4">de 5.000,00 - 8.000,00</option>
                <option value="5">de 8.000,00 - 12.000,00</option>
                <option value="6">acima de 12.000,0</option>
                <option value="7">não quero informar</option>
              </select> 

  
            </div>
          </div>

          <div >
            <label htmlFor="password" className="block text-sm font-semibold leading-6 text-gray-900">
              Senha
            </label>
            <div className="mt-2.5">
              <input
                type="password"
                name="senha"
                id="senha"
                required
                value={formData.senha}
                onChange={handleChange}
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
            <div>
              <ul className="text-xs leading-2 text-gray-600 text-opacity-80">
                <li >Ao menos 7 caracteres</li>
                <li >Pelo menos 1 numero</li>
                <li >Pelo menos 1 letra minuscula</li>
                <li >Pelo menos 1 letra maiscula</li>
                <li >Pelo menos 1 caracter especial</li>
              </ul>
            </div>
          </div>

          <div >
            <label htmlFor="email" className="block text-sm font-semibold leading-6 text-gray-900">
              Confirmação de Senha
            </label>
            <div className="mt-2.5">
              <input
                type="password"
                name="confirmacao_senha"
                id="confirmacao_senha"
                required
                value={formData.confirmacao_senha}
                onChange={handleChange}

                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          {/* <div>
            <label htmlFor="complemento" className="block text-sm font-semibold leading-6 text-gray-900 w-24">
              CEP
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="bairro"
                id="bairro"
               required
                autoComplete="bairro"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 w-32"
              />
            </div> 
          </div>

          
          <div >
            <label htmlFor="email" className="block text-sm font-semibold leading-6 text-gray-900 w-30">
              Endereço
            </label>
            <div className="mt-2.5">
              <input
                type="endereco"
                name="endereco"
                id="endereco"
               required
                autoComplete="endereco"
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 w-30"
              />
            </div>
          </div>  */}

          {/* <div>
            <label htmlFor="numero-endereco" className="block text-sm font-semibold leading-6 text-gray-900">
              Numero
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="numero-endereco"
                id="numero-endereco"
                autoComplete="numero-endereco"
               required
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <div>
            <label htmlFor="complemento" className="block text-sm font-semibold leading-6 text-gray-900">
              Complemento
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="complemento"
                id="complemento"
                autoComplete="complemento"
               required
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div> 

          <div>
            <label htmlFor="cidade" className="block text-sm font-semibold leading-6 text-gray-900">
              Cidade
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="cidade"
                id="cidade"
                autoComplete="cidade"
               required
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div>
          <div>
            <label htmlFor="complemento" className="block text-sm font-semibold leading-6 text-gray-900">
              Bairro
            </label>
            <div className="mt-2.5">
              <input
                type="text"
                name="bairro"
                id="bairro"
                autoComplete="bairro"
               required
                className="block w-full rounded-md border-0 px-3.5 py-2 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              />
            </div>
          </div> */}
           
          
          


          <Field as="div" className="flex gap-x-4 sm:col-span-2">
            <div className="flex h-6 items-center">
              <Switch
                checked={agreed}
                onChange={setAgreed}
                className={classNames(
                  agreed ? 'bg-indigo-600' : 'bg-gray-200',
                  'flex w-8 flex-none cursor-pointer rounded-full p-px ring-1 ring-inset ring-gray-900/5 transition-colors duration-200 ease-in-out focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600',
                )}
              >
                <span className="sr-only">Agree to policies</span>
                <span
                  aria-hidden="true"
                  className={classNames(
                    agreed ? 'translate-x-3.5' : 'translate-x-0',
                    'h-4 w-4 transform rounded-full bg-white shadow-sm ring-1 ring-gray-900/5 transition duration-200 ease-in-out',
                  )}
                />
              </Switch>
            </div>
            <Label className="text-sm leading-6 text-gray-600">
              Ao clicar no botão abaixo, você confirma que está de acordo com nossa politica.{' '}
              <a href="#" onClick={() => setOpenModal(true)} data-modal-target="default-modal" className="font-semibold text-indigo-600">
                privacy&nbsp;policy
              </a>
              .
            </Label>

          <Modal show={openModal} size={modalSize} onClose={() => setOpenModal(false)}>
            <Modal.Header>Termos de aceite</Modal.Header>
            <Modal.Body>
              <div className="space-y-6 p-6">
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  With less than a month to go before the European Union enacts new consumer privacy laws for its citizens,
                  companies around the world are updating their terms of service agreements to comply.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
                <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                  The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                  to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                  soon as possible of high-risk data breaches that could personally affect them.
                </p>
              </div>
            </Modal.Body>
            <Modal.Footer>
              {/* <Button onClick={() => setOpenModal(false)}>I accept</Button>
              <Button color="gray" onClick={() => setOpenModal(false)}>
                Decline
              </Button> */}
            </Modal.Footer>
          </Modal>




          </Field>
        </div>
        <div className="mt-10">
          <button
            type="submit"
            className="block w-full rounded-md bg-indigo-600 px-3.5 py-2.5 text-center text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
          >
            Confirmar
          </button>
        </div>
      </form>
    </div>
  )
}
