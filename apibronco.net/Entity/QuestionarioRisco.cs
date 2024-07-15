using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Interfaces;

namespace apibronco.bronco.com.br.Entity
{
    public class QuestionarioRisco 
    {
        //public QuestionarioRisco(IntegrationQuestionarioRiscoDTO dto) 
        //{
        //    this.Numero = dto.Numero;
        //    this.Identificador = dto.Identificador;
        //    this.Pergunta = dto.Pergunta;
        //    this.Tipo_Pergunta = dto.Tipo_Pergunta;
            
        //    IsValid();
        //}
        public int Numero { get; set; } 
        public string Identificador { get; set; }
        public string Pergunta { get; set; }
        public string Tipo_Pergunta { get; set; }
        
        public string Resposta { get; set; }

        public bool IsValid()
        {
            AssertionConcern.AssertArgumentRange(Numero, 1, 20, "Ordem precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Identificador, "Identificador precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Pergunta, "Pergunta precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Tipo_Pergunta, "Tipo_Pergunta precisa ser preenchido.");

            AssertionConcern.AssertArgumentMatches(Tipo_Pergunta, "text", "Tipo_Pergunta precisa ser [text].");
            //AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo, "Codigo_Grupo precisa ser preenchido.");
            //AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo_SUSEP, "Codigo_Grupo_SUSEP precisa ser preenchido.");
            //AssertionConcern.AssertArgumentNotEmpty(Descricao_Grupo, "Descricao_Grupo precisa ser preenchido.");

            return true;
            //throw new NotImplementedException();
        }
    }
}
