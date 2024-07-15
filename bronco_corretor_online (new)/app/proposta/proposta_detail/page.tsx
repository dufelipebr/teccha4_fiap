'use client';
import { useState, useEffect } from 'react'
import { TextInput, Alert } from "flowbite-react";
import { HiMail, HiInformationCircle } from "react-icons/hi";
import { useRouter } from 'next/navigation'
import {constants} from '../lib/integrationInterface'
import { ProdutoData, QuestionarioRiscoData } from '@/app/lib/integrationInterface';


// const questionario : Array<QuestionarioRiscoData> = [
//     {numero:1, identificador: 'pergunta-1',       pergunta: 'qual é a sua idade?',       tipo_pergunta: 'text',       resposta:''    },
//     {numero:2, identificador: 'pergunta-2', pergunta: 'você é fumante?', tipo_pergunta: 'bool', resposta:''},
//     {numero:3, identificador: 'pergunta-3', pergunta: 'Indique os problemas de saude (pressão alta, cardiaco, etc...)', tipo_pergunta: 'text', resposta:''},
//     {numero:4, identificador: 'pergunta-4', pergunta: 'Já realizou alguma cirurgia? detalhe', tipo_pergunta: 'text', resposta:''},
// ]


    // },
    // {
    //     identificador: 'aaaaa-bbbb-124',
    //     ramo_descricao: 'Seguro Rural',
    //     produto_descricao: 'Bronco Rural HHWW 01 ',
    //     comentario_produto: 'Esse seguro rural protege sua propriedade rural com danos em até 1.000,000,00, contra queimadas, invasões de terra, furações e tornados, inundações com perda de colheita. ',
    //     comentario_contratacao: '*seguradora se provê do direito de alterar os custos do seguro em qualquer momento',
    //     preco_seguro: 'R$599,99',
    //     moeda: '/mês',
    //     includedFeatures : [
    //         'Seguro plantação contra queimadas',
    //         'Seguro plantação contra danos causados por invasões',
    //         'Seguro plantação  contra furações e tornados',
    //         'Seguro plantação contra inundações'
    //     ]
    // }
  

  function classNames(...classes) 
  {
    return classes.filter(Boolean).join(' ')
  }
    
export default function Home() 
{
    //const [mobileMenuOpen, setMobileMenuOpen] = useState(false)
    //const [perguntas, setPerguntas] = useState(questionario)
    const[produto, setProduto] = useState<ProdutoData>({})
    const [paginaAtual, setPaginaAtual] = useState(1)
    const [respostaAtual, setRespostaAtual] = useState<string>("")
    const [perguntaAtual, setPerguntaAtual] = useState<QuestionarioRiscoData>({})
    const [validationMessage, setValidationMessage] = useState("");
    const [errorDisplay, setErrorDisplay] = useState(false);   
    const [isLoading, setLoading] = useState(true);
    const [questionario, setQuestionario] = useState<QuestionarioRiscoData[]>({});
    const [totalPaginas, setTotalPaginas] = useState(0);
    const router = useRouter()

    useEffect(() => 
    {
      console.log('useEffect')
      const produto : ProdutoData = JSON.parse(localStorage.getItem('productInfo'));
      const questionario : QuestionarioRiscoData[] = JSON.parse(localStorage.getItem('QuestionarioRiscoData'));

      if (produto != null && questionario != null) {
        setProduto(produto);
        setQuestionario(questionario);
        setRespostaAtual("");
        setPerguntaAtual(questionario[0]);
        setLoading(false);
        setTotalPaginas(questionario.length)
      }
    },[])

    const paginate = async  (pageNumber) => 
    {
      setErrorDisplay(false);
      setValidationMessage("");

      if (respostaAtual == "")
      {
        setErrorDisplay(true);
        setValidationMessage("Por favor informe uma resposta para pergunta.");
        return;
      }
      perguntaAtual.resposta = respostaAtual;//persistindo no array.

      if(pageNumber <= totalPaginas)
      {
        setPaginaAtual(pageNumber);
        setPerguntaAtual(questionario[pageNumber-1])
        setRespostaAtual("");
      }
      else
      {
        localStorage.setItem("questionario", JSON.stringify(questionario));
        router.push("/proposta/checkout")
      }
   }

   const handleChange = (event) => 
   {
      setRespostaAtual(event.target.value);
   }
 




    //perguntaAtual = perguntas[0]
  return (
    <div className="bg-orange-300 py-24 sm:py-32">
      <div className="mx-auto max-w-7xl px-6 lg:px-8">
        <div className="mx-auto max-w-2xl sm:text-center">
          <h2 className="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">Por favor nos conte mais sobre você</h2>
          <p className="mt-6 text-lg leading-8 text-gray-600">
            Abaixo preencha as informações para que possamos configurar o seu produto.
          </p>
        </div>
        <br></br>
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
        {/* Aqui começa o primeiro bloco de seguro*/}
        <div 
          className={classNames(
            isLoading ? 'hidden' : '',
            'mx-auto',
          )}>
            <h2 className="text-3xl font-bold tracking-tight text-blue-700 ">{perguntaAtual.pergunta}</h2>
            <div className=
            "mt-6 flex max-w-md gap-x-4">
                {perguntaAtual.tipo_pergunta == 'lista'   ?  ( <div> LISTA !!!</div>)
                  : (
                    <input 
                      type="text"
                      id={perguntaAtual.identificador} 
                      name={perguntaAtual.identificador}    
                      onChange={handleChange}
                      value={respostaAtual}
                      required 
                      className="min-w-0 flex-auto rounded-md border-0 bg-black/5 px-3.5 py-2 text-black shadow-sm ring-1 ring-inset ring-black/10 focus:ring-2 focus:ring-inset focus:ring-indigo-500 sm:text-sm sm:leading-6" 
                      placeholder="Preencha a informação" 
                      />
                    )
                }
              
              <button
                type="submit"
                onClick={() => paginate(perguntaAtual.numero+1)}
                className="flex-none rounded-md bg-indigo-500 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500"
              >
                Enviar
              </button>
              {perguntaAtual.numero} / {totalPaginas} 
            </div>
          </div>
        
      </div>
    </div>
  )
}
