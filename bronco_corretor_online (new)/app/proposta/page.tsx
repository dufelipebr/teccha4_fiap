
'use client';
import { CheckIcon } from '@heroicons/react/20/solid'
import { get_lista_produtos } from './api';
import { useEffect, useState  } from 'react';
import { Button } from 'flowbite-react';
import { useRouter } from 'next/navigation';



  //  const produtos = [
  //   {
  //       identificador: 'aaaaa-bbbb-123',
  //       ramo_descricao: 'Seguro de Vida',
  //       produto_descricao: 'Bronco Premium Asset',
  //       comentario_produto: 'O Seguro de Vida - Bronco Premium Asset é o perfil de seguro com cobertura completa para morte no valor de até R$ 300.000,00, tambem realiza cobertura em caso de danos de terceiros no valor de R$ 50.000,00, possui cobertura funeral de até R$ 10.000,00, e permite saque de até 100.000,00 em caso de incapacidade temporaria por acidente.',
  //       comentario_contratacao: '*seguradora se provê do direito de alterar os custos do seguro a qualquer momento sem aviso prévio',
  //       preco_seguro: 'R$79,99',
  //       moeda: '/mês',
  //       includedFeatures : [
  //           'Seguro contra Morte',
  //           'Seguro contra Danos de Terceiros',
  //           'Seguro Funeral',
  //           'Seguro contra Incapacidade Temporária'
  //       ]
  //   }
  // ]

export default function Home() {
    //const [mobileMenuOpen, setMobileMenuOpen] = useState(false)
    const [produtos, setProdutos] = useState<ProdutoInfo[]>([])
    const [isLoading, setLoading] = useState(true);
    const router = useRouter();

    useEffect(() => {
      async function carregaProdutos () {
        const resposta  = await get_lista_produtos('') 
        // const produtos = await resposta.json();
        // return produtos;
        setProdutos(resposta);
        setLoading(false);
      }
      carregaProdutos();
    }, []);


    const handleClick =  (event) => 
    {
        console.log('handleClick');
        
        produtos.forEach(function (produto)
        {
          if (produto.identificador == event.target.value)
          {
            localStorage.setItem('product', produto.identificador);
            localStorage.setItem('productInfo', JSON.stringify(produto));
            localStorage.setItem('QuestionarioRiscoData', JSON.stringify(produto.questionario_Riscos));
            router.push("/proposta/proposta_detail");
          }
        });
    }

    function classNames(...classes) 
    {
      return classes.filter(Boolean).join(' ')
    }

  return (
    <div 
      className={classNames(
        !isLoading ? '' : 'hidden',
        'bg-orange-300 py-24 sm:py-32',
    )}
    >
      <div className="mx-auto max-w-7xl px-6 lg:px-8">
        <div className="mx-auto max-w-2xl sm:text-center">
          <h2 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">Escolha abaixo o tipo de seguro</h2>
          <p className="mt-6 text-lg leading-8 text-gray-600">
            Abaixo você poderá escolher qual tipo de seguro você deseja contratar
          </p>
        </div>
{/* Aqui começa o primeiro bloco de seguro*/}
        {produtos.map((seguro) => (
        <div className="mx-auto mt-16 max-w-2xl rounded-3xl ring-1 ring-gray-200 sm:mt-20 lg:mx-0 lg:flex lg:max-w-none">
          <div className="p-8 sm:p-10 lg:flex-auto">
            <h3 className="text-2xl font-bold tracking-tight text-gray-900">{seguro.produto_Descricao}</h3>
            <p className="mt-6 text-base leading-7 text-gray-600">
                {seguro.comentario_Produto}              
            </p>
            <div className="mt-10 flex items-center gap-x-4">
              <h4 className="flex-none text-sm font-semibold leading-6 text-indigo-600">O que está incluido</h4>
              <div className="h-px flex-auto bg-gray-100" />
            </div>
            <ul
              role="list"
              className="mt-8 grid grid-cols-1 gap-4 text-sm leading-6 text-gray-600 sm:grid-cols-2 sm:gap-6"
            >
              {seguro.includedFeatures.map((feature) => (
                <li key={feature} className="flex gap-x-3">
                  <CheckIcon className="h-6 w-5 flex-none text-indigo-600" aria-hidden="true" />
                  {feature}
                </li>
              ))}
            </ul>
          </div>
          <div className="-mt-2 p-2 lg:mt-0 lg:w-full lg:max-w-md lg:flex-shrink-0">
            <div className="rounded-2xl bg-gray-50 py-10 text-center ring-1 ring-inset ring-gray-900/5 lg:flex lg:flex-col lg:justify-center lg:py-16">
              <div className="mx-auto max-w-xs px-8">
                <p className="text-base font-semibold text-gray-600">Pague apenas</p>
                <p className="mt-6 flex items-baseline justify-center gap-x-2">
                  <span className="text-5xl font-bold tracking-tight text-gray-900">{seguro.preco_Produto}</span>
                  <span className="text-sm font-semibold leading-6 tracking-wide text-gray-600">{seguro.moeda}/mês</span>
                </p>
                <button
                    type="button"
                    onClick={handleClick}
                    value={seguro.identificador}
                    className="block w-full rounded-md bg-indigo-600 px-3.5 py-2.5 text-center text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                  >
                  Começar contratação
                </button>


                 {/* <button
                    type="button"
                    onClick={handleClick(seguro.identificador)}
                    className="block w-full rounded-md bg-indigo-600 px-3.5 py-2.5 text-center text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                  >
                    Começar contratação
              </button>*/}

                  {/* <Button className="block w-full rounded-md bg-indigo-600 px-3.5 py-2.5 text-center text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">
                    Começar contratação
                  </Button> */}

                
                <p className="mt-6 text-xs leading-5 text-gray-600">
                {seguro.comentario_Contratacao}
                </p>
              </div>
            </div>
          </div>
        </div>
        ))}

      </div>
    </div>
  )
}
