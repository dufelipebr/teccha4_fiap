'use client';
import { useState, useEffect } from 'react'
import { Dialog, DialogBackdrop, DialogPanel, DialogTitle, Field, Switch, Label} from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/outline'
import { Modal, Alert } from "flowbite-react";
import { CheckIcon } from '@heroicons/react/20/solid'
import { PayMethod } from '@/app/billing_addr/billing_ui';
import { LoginInfo } from '@/app/lib/login';
import { HiMail, HiInformationCircle } from "react-icons/hi";
import { ProdutoData, QuestionarioRiscoData, PropostaData } from '@/app/lib/integrationInterface';
import { criar_proposta } from './api';
import { useRouter } from 'next/navigation'

function classNames(...classes) 
{
  return classes.filter(Boolean).join(' ')
}


export default function Page() {
  const [open, setOpen] = useState(false)

  const [aceito1, setAceito1] = useState(false)
  const [aceito2, setAceito2] = useState(false)
  const [aceito3, setAceito3] = useState(false)

  const [openModal1, setOpenModal1] = useState(false)
  const [openModal2, setOpenModal2] = useState(false)
  const [openModal3, setOpenModal3] = useState(false)

  const [questionario, setQuestionario] = useState<QuestionarioRiscoData[]>([]);
  //const[produto, setProduto] = useState<ProdutoData>({})
  const [produtos, setProdutos] = useState<ProdutoData[]>([]);


  //const [mobileMenuOpen, setMobileMenuOpen] = useState(false)
  const [isLoggedIn, setLoggedIn] = useState(false)
  const [userInfo, setUserInfo] = useState({nome: '', email: ''})

  const [validationMessage, setValidationMessage] = useState("");
  const [errorDisplay, setErrorDisplay] = useState(false);

  const [pagamento, setPayMethod] = useState<PaymentInfo>({errorNumber:0, payType: 'B'});
  
  const router = useRouter()

 
  useEffect(() => 
  {
    console.log('useEffect')
    const produto : ProdutoData = JSON.parse(localStorage.getItem('productInfo'));
    const questionario : QuestionarioRiscoData[] = JSON.parse(localStorage.getItem('questionario'));
    const loginData = LoginInfo();

    if (produto != null && questionario != null) {
      //setProduto(produto);
      setProdutos([]);
      var prodArray : ProdutoData[] = [];
      prodArray.push(produto);
      setProdutos(prodArray);
      setQuestionario(questionario);
      //setProdutos();
    }
    
    console.log("loginData");
    console.log(loginData);
    if (loginData) {
      setLoggedIn(true)
      setUserInfo({nome: loginData.username, email: loginData.email});
    }

  },[])

  const handleClick =  (event) => 
    {
      if (aceito1 && aceito2 && aceito3)
      {
        setOpen(true);
        // localStorage.setItem('product', produto.identificador);
        // localStorage.setItem('productInfo', JSON.stringify(produto));
        // localStorage.setItem('QuestionarioRiscoData', JSON.stringify(produto.questionario_Riscos));
        // router.push("/proposta/proposta_detail");
      }
    }

    const handleChangePaymethod =  (event) => 
    {
      console.log("handleChangePaymethod");
      setPayMethod(event);
      setValidationMessage("");
      setErrorDisplay(false);
      //console.log(event.target.value);
    }

    const handleCheckout =  async (event) => 
    {
      console.log("handleCheckout");
      setValidationMessage("");
      setErrorDisplay(false);

      if (isLoggedIn == false) // checar se usuario está logado
      {
        setValidationMessage("Por favor realizar o login na plataforma");
        setErrorDisplay(true);
        return;
      }

      console.log(pagamento.errorNumber);
      if (pagamento.errorNumber != 0) // checar se forma de pagamento ok.
      {
        setValidationMessage("Por favor verifique as informações de pagamento");
        setErrorDisplay(true);
        return;
      }

      if(produtos.length == 0)
      {
        setValidationMessage("Produto de seguro não foi selecionado.");
        setErrorDisplay(true);
        return;
      }

      var date = new Date();
      var codigo_interno =  'BR'  + date.getTimezoneOffset().toString() +  Math.trunc((Math.random() * (100000- 9999) + 9999)).toString();
      console.log(codigo_interno);

      var filledCard = null; 
      if (pagamento.payType == "D" || pagamento.payType == "C")
        filledCard = {
            cC_Nome: pagamento.name,
            cC_Numero: pagamento.num,
            cC_CVV: pagamento.cvv,
            cC_Expira: pagamento.exp
          }
        
      

      var proposta : PropostaData = {
        codigo_Interno: codigo_interno.toString(),
        codigo_Produto: produtos[0].identificador,
        codigo_Corretor: "0",
        codigo_Segurado: userInfo.email,
        condicao_Pagto: {
          codigo_Condicao_Pagto: pagamento.payType,
          valor_Pagamento: produtos[0].preco_Produto,
          parcelas: "1",
          cartao_Info: filledCard,
           reference: codigo_interno.toString()
        },
        questionario_Risco: questionario,
        cobertura_Total: 0,
        premio_Total: produtos[0].preco_Produto,
        endereco_Faturamento: {
          logradouro: "Rua George Smith",
          numero: "357",
          complemento: "apto185a",
          bairro: "lapa",
          cidade: "São Paulo",
          uf: "SP",
          pais: "Brasil",
          cep: "05053-060"
          },
        codigo_Empresa: "1"
      };
      
      await criar_proposta(proposta);
      console.log("submit proposta");
      localStorage.setItem('proposta_concluida', proposta.codigo_Interno);
      router.push("/proposta/concluded");
    }


  //const [modalSize, setModalSize] = useState<string>('md');

  return (
    <div className="bg-orange-300 py-24 sm:py-32 md">
      <div className="mx-auto max-w-7xl px-6 lg:px-8">
        <div className="mx-auto max-w-2xl sm:text-center">
          <h2 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">Estamos quase lá</h2>
          <p className="mt-6 text-lg leading-8 text-gray-600">
          Agora você precisa confirmar que as informações são verdadeiras e aceitar nossa política.
          </p>
        </div>
        

        <Field as="div" className="flex gap-x-4 sm:col-span-2">
            <div className="flex h-6 items-center">
              <Switch
                checked={aceito1}
                onChange={setAceito1}
                className={classNames(
                  aceito1 ? 'bg-indigo-600' : 'bg-gray-200',
                  'flex w-8 flex-none cursor-pointer rounded-full p-px ring-1 ring-inset ring-gray-900/5 transition-colors duration-200 ease-in-out focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600',
                )}
              >
                <span className="sr-only">Concordar</span>
                <span
                  aria-hidden="true"
                  className={classNames(
                    aceito1 ? 'translate-x-3.5' : 'translate-x-0',
                    'h-4 w-4 transform rounded-full bg-white shadow-sm ring-1 ring-gray-900/5 transition duration-200 ease-in-out',
                  )}
                />
              </Switch>
            </div>
            <Label className="text-sm leading-6 text-gray-600">
              Declaro que as informações preenchidas são verdadeiras.{' '}
              <a href="#" onClick={() => setOpenModal1(true)} data-modal-target="default-modal" className="font-semibold text-indigo-600">
                  saiba mais.
                </a>
            </Label>
          </Field>



        <div className="text-sm font-medium text-indigo-600">
          

        <Field as="div" className="flex gap-x-4 sm:col-span-2">
            <div className="flex h-6 items-center">
              <Switch
                checked={aceito2}
                onChange={setAceito2}
                className={classNames(
                  aceito2 ? 'bg-indigo-600' : 'bg-gray-200',
                  'flex w-8 flex-none cursor-pointer rounded-full p-px ring-1 ring-inset ring-gray-900/5 transition-colors duration-200 ease-in-out focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600',
                )}
              >
                <span className="sr-only">Concordar</span>
                <span
                  aria-hidden="true"
                  className={classNames(
                    aceito2 ? 'translate-x-3.5' : 'translate-x-0',
                    'h-4 w-4 transform rounded-full bg-white shadow-sm ring-1 ring-gray-900/5 transition duration-200 ease-in-out',
                  )}
                />
              </Switch>
            </div>
            <Label className="text-sm leading-6 text-gray-600">
            Declaro que as aceito os termos de LGPD e em compartilhar meus dados com a seguradora. {' '}
              <a href="#" onClick={() => setOpenModal2(true)} data-modal-target="default-modal" className="font-semibold text-indigo-600">
                saiba mais.
              </a>
            </Label>
          </Field>  
          
        </div>

        <div className="text-sm font-medium text-indigo-600">
          

          <Field as="div" className="flex gap-x-4 sm:col-span-2">
              <div className="flex h-6 items-center">
                <Switch
                  checked={aceito3}
                  onChange={setAceito3}
                  className={classNames(
                    aceito3 ? 'bg-indigo-600' : 'bg-gray-200',
                    'flex w-8 flex-none cursor-pointer rounded-full p-px ring-1 ring-inset ring-gray-900/5 transition-colors duration-200 ease-in-out focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600',
                  )}
                >
                  <span className="sr-only">Concordar</span>
                  <span
                    aria-hidden="true"
                    className={classNames(
                      aceito3 ? 'translate-x-3.5' : 'translate-x-0',
                      'h-4 w-4 transform rounded-full bg-white shadow-sm ring-1 ring-gray-900/5 transition duration-200 ease-in-out',
                    )}
                  />
                </Switch>
              </div>
              <Label className="text-sm leading-6 text-gray-600">
              Declaro que estou de acordo com as politicas de seguros da empresa. {' '}
                <a href="#" onClick={() => setOpenModal3(true)} data-modal-target="default-modal" className="font-semibold text-indigo-600">
                  saiba mais.
                </a>
              </Label>
            </Field>  
            
          </div>
          <div>
            <br></br>
            <br></br>
          <button
              type="button"
              onClick={handleClick}
              className="block w-40 rounded-md bg-indigo-600 px-3.5 py-2.5 text-center text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
            >
            Concluir
          </button>
      </div>


    <Dialog className="relative z-10" open={open} onClose={setOpen}>
      <DialogBackdrop
        transition
        className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity duration-500 ease-in-out data-[closed]:opacity-0"
      >
      </DialogBackdrop>

      <div className="fixed inset-0 overflow-hidden">
        <div className="absolute inset-0 overflow-hidden">
          <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10 align-top">
            <DialogPanel
              transition
              className="pointer-events-auto w-screen max-w-md transform transition duration-500 ease-in-out data-[closed]:translate-x-full sm:duration-700"
            >
              <div className=" flex h-full flex-col overflow-y-scroll bg-white shadow-xl  mt-20">
                <div className="px-4 py-2 ">
                  <div className="flex items-start justify-between">
                    <DialogTitle className="text-lg leading-3 font-extrabold text-gray-900 mb-5">Checkout Seguro</DialogTitle>
                    <div className="ml-3 leading-3 flex h-7">
                      <button
                        type="button"
                        className="relative -m-2 p-2 text-gray-400 hover:text-gray-500"
                        onClick={() => setOpen(false)}
                      >
                        <span className="absolute -inset-0.5" />
                        <span className="sr-only">Close panel</span>
                        <XMarkIcon className="h-6 w-6" aria-hidden="true" />
                      </button>
                    </div>
                  </div>
                </div>
                <div>
                  {produtos.map((produto) => (
                  <div>
                    <div className="border-t border-gray-200 px-1 py-2 ml-5 flex font-semibold text-gray-900"> Produto Segurado</div>
                    {/* <div className="flex items-center space-x-4 rtl:space-x-reverse">
                      <div className="min-w-0 flex-1">
                        <p className="ml-8 truncate text-sm font-medium text-gray-900 dark:text-white">Neil Sims</p>
                        <p className="ml-10 truncate text-sm text-gray-500 dark:text-gray-400">email@flowbite.com</p>
                      </div>
                      <div className="inline-flex items-center text-base font-semibold text-gray-900 dark:text-white">$320</div>
                    </div> */}
                    <div >
                      <ul role="list" className="-my-6 divide-y divide-gray-200 pr-4">
                      
                          <li key={produto.identificador} className="flex py-6">
                            {/* <div className="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
                              <img
                                src="/images.png"
                                className="h-full w-full object-cover object-center"
                              />
                            </div> */}

                            <div className="ml-4 flex flex-1 flex-col">
                              <div>
                                <div className="ml-5 flex justify-between text-base font-medium text-gray-900">
                                  <h3>
                                    {produto.produto_Descricao}
                                  </h3>
                                </div>
                              </div>
                              <div>
                                <div>
                                  <div className="ml-6 truncate text-sm text-gray-500 dark:text-gray-400 text-sm">
                                    {produto.includedFeatures.map((feature) => (
                                      <li key={feature} className="flex gap-x-3 sm">
                                        {feature}
                                      </li>
                                    ))}
                                  </div>
                                  <p className="text-right mr-3 font-semibold text-gray-900 py-3 pr-4">
                                    Prêmio: {produto.preco_Produto.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })} / mês</p>
                                </div>
                              </div>
                            </div>
                          </li>
                      
                      </ul>
                    </div>
                  </div>
                   ))}

                </div>
                <div className="border-t border-gray-200 px-1 py-2 ml-5 flex font-semibold text-gray-900">
                  Dados do Segurado
                </div>
                <div className="ml-8 text-sm py-2 pb-8 text-gray-900 pr-4">
                  <div 
                    className={classNames(
                      isLoggedIn ? 'hidden' : '',
                      '',
                    )}
                  >
                   
                      Você deve estar autenticado para continuar. <br></br>
                      Clique<a href="/login" className="font-semibold text-indigo-600 px-1 py-1 ml-2" >aqui</a> para fazer o login. 
                    
                  </div>

                  <div
                    className={classNames(
                      isLoggedIn ? '' : 'hidden',
                      '',
                    )}
                  >
                      <p> {userInfo.nome} </p>
                      <p>{userInfo.email} </p>
                  </div>


                </div>
                <div className="border-t border-gray-200 px-1 py-2 ml-5 flex font-semibold text-gray-900">
                  Endereço de Faturamento 
                  <a href="#" className="font-semibold text-indigo-600  ml-2">[Editar...]</a>
                </div>
                <div className="ml-8 text-sm py-2 pb-8 text-gray-900 pr-4">
                  Rua George Smith 357, apto 1885A, São Paulo-SP. 
                </div>
                <div className="border-t border-gray-200 px-1 py-2 ml-5 flex font-semibold text-gray-900">
                  <div >Forma de Pagamento  </div>
                </div>
                <div className="ml-8 text-sm py-2 pb-8 text-gray-900 pr-4">
                  <PayMethod onChangeValue={handleChangePaymethod} /> 
                </div>
                <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                  {/* <div className="flex justify-between text-base font-medium text-gray-900">
                    <p>Subtotal</p>
                    <p>$262.00</p>
                  </div>
                  <p className="mt-0.5 text-sm text-gray-500">Shipping and taxes calculated at checkout.</p> */}

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
                  <br></br>

                  <div className="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-indigo-700">
                          <button
                      type="button"
                      onClick={handleCheckout}
                      className=""
                    >
                    Checkout
                  </button>
                    {/* <a
                      href={handleCheckout}
                      className="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-indigo-700"
                    >
                      Checkout
                    </a> */}
                  </div>
                  <div className="mt-6 flex justify-center text-center text-sm text-gray-500 py-6">
                    
                  </div>
                </div>
              </div>
            </DialogPanel>
          </div>
        </div>
      </div>
    </Dialog>

    <Modal show={openModal1} position="center" onClose={() => setOpenModal1(false)}>
        <Modal.Header>Informações verdadeiras</Modal.Header>
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

      <Modal show={openModal2} size="md" onClose={() => setOpenModal2(false)}>
        <Modal.Header>Política LGPD</Modal.Header>
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

      <Modal show={openModal3} position="center" onClose={() => setOpenModal3(false)}>
        <Modal.Header>Termos de seguro</Modal.Header>
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


  </div>
</div>

  )
}
