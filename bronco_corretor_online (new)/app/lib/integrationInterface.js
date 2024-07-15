export interface CoberturaData{
    codigo_Susep:	string,
    codigo_Identificador:	string,
    descricao:	string,
    comentario:	string,
    valor_DanoMaximo:	number,
    valor_Premio:	number,
    valor_IOF:	number,
    valor_Custo_Emiss:	number,
    valor_Add_Fraq:	number,
    valor_Cosseg_Cedido:	number,
    valor_LMI:	number,
    valor_Is:	number,
    valor_Comiss:	number,
    codigo_Moeda:	string,
    codigo_Grupo_Ramo:	string
  }

  export interface QuestionarioRiscoData{
    numero:	number,
    identificador:	string,
    pergunta:	string,
    tipo_Pergunta:	string,
    resposta:	string
  }
  
  export interface ProdutoData {
    identificador: 	string,
    identicador_Ramo:	string,
    produto_Descricao:	string,
    comentario_Contratacao:	string,
    comentario_Produto:	string,
    preco_Produto:	number,
    moeda:string,
    includedFeatures:string[],
    questionario_Riscos: QuestionarioRiscoData[]
  }
