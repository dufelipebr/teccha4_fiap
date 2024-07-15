using System.Data;

namespace apibronco.bronco.com.br.Entity
{
    public enum EnStatusProposta
    {
        Cancelado = 0,
        Aberto = 1, 
        Em_Analise_Underwriter = 2,
        Fechado = 4
    }

    public class StatusProposta
    {
        public int Status { get; set; }

        public static bool IsValid(EnStatusProposta status)
        {
            if (status > EnStatusProposta.Fechado || status < EnStatusProposta.Cancelado)
                throw new Exception("Invalid StatusProposta");

            return true;
        }
    }
}
