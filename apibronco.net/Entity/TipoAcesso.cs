namespace apibronco.bronco.com.br.Entity
{
    public enum EnumTipoAcesso
    {
        Admin = 1, 
        Funcionario  = 2,
        CorretorOnline = 3, 
    }

    public static class Permissoes
    {
        public const string Administrador = "Administrador";
        public const string Funcionario = "Funcionario";
        public const string Corretor_Online = "Corretor_Online";
    }
}
