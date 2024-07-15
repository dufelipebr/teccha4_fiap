//using apibronco.bronco.com.br.Repository;
//using apibronco.bronco.com.br.Entity;
//using apibronco.bronco.com.br.Interfaces;
//using System.Collections.Generic;
//using System.Data.SqlClient;

//namespace apibronco.bronco.com.br.Repository
//{
//    public class AZUsuario : DapperRepository<Usuario>, IUsuarioRepository
//    {
//        public AZUsuario(IConfiguration configuration) : base(configuration)
//        {

//        }

//        public override void Alterar(Usuario entidade)
//        {
//            using var dbConnection = new SqlConnection(ConnectionString);

//            try
//            {
//                SqlCommand cmd = dbConnection.CreateCommand();
//                //dbConnection.Query("");
//                cmd.CommandText = "update Usuario set " +
//                    "   nome = @Nome " +
//                    "   ,Email = @Email " +
//                    "   ,Senha = @Senha" +
//                    "   ,TipoAcesso = @TipoAcesso " +
//                    "where Id = @Id";
//                cmd.Parameters.AddWithValue("@Nome", entidade.Nome.ToString());
//                cmd.Parameters.AddWithValue("@Email", entidade.Email.ToString());
//                cmd.Parameters.AddWithValue("@Senha", entidade.Senha.ToString());
//                cmd.Parameters.AddWithValue("@TipoAcesso", (int)entidade.TipoPermissao);
//                cmd.Parameters.AddWithValue("@Id", entidade.Id);

//                dbConnection.Open();
//                cmd.ExecuteNonQuery();
//            }
//            catch
//            {
//                dbConnection.Close();
//                throw;
//            }

//        }

//        public override void Cadastrar(Usuario entidade)
//        {
//            using var dbConnection = new SqlConnection(ConnectionString);


//            try
//            {
//                using (SqlCommand cmd = dbConnection.CreateCommand())
//                {
//                    //dbConnection.Query("");
//                    cmd.CommandText = "insert into Usuario (Nome, Email, Senha, TipoAcesso) values (@Nome, @Email, @Senha, @TipoAcesso)";
//                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
//                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome.ToString());
//                    cmd.Parameters.AddWithValue("@Email", entidade.Email.ToString());
//                    cmd.Parameters.AddWithValue("@Senha", entidade.Senha.ToString());
//                    cmd.Parameters.AddWithValue("@TipoAcesso", (int)entidade.TipoPermissao);
//                    dbConnection.Open();
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch
//            {
//                throw;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//        }

//        public override void Deletar(Usuario entidade)
//        {
//            using var dbConnection = new SqlConnection(ConnectionString);

//            try
//            {
//                SqlCommand cmd = dbConnection.CreateCommand();
//                //dbConnection.Query("");
//                cmd.CommandText = "delete from Usuario where Id = @Id";
//                //            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
//                cmd.Parameters.AddWithValue("@Id", entidade.Id);

//                dbConnection.Open();
//                cmd.ExecuteNonQuery();
//            }
//            catch
//            {

//                throw;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//        }

//        public override Usuario ObterPorId(string id)
//        {
//            return ObterUsuarios(id).FirstOrDefault();
//        }

//        public override IList<Usuario> ObterTodos()
//        {
//            return ObterUsuarios(null);
//        }

//        protected IList<Usuario> ObterUsuarios(string? id)
//        {
//            IList<Usuario> list = new List<Usuario>();
//            using (var dbConnection = new SqlConnection(ConnectionString))
//            {
//                try
//                {
//                    dbConnection.Open();
//                    SqlCommand cmd = dbConnection.CreateCommand();
//                    cmd.CommandText = "select * from  Usuario where (Id = @Id or @Id is null) ";

//                    //if (id != null)
//                    cmd.Parameters.AddWithValue("@Id", (id == null ? DBNull.Value : id));


//                    var rd = cmd.ExecuteReader();

//                    while (rd.Read())
//                    {
//                        list.Add(new Usuario()
//                        {
//                            Id = rd["Id"].ToString(),
//                            Nome = rd["Nome"].ToString(),
//                            Email = rd["Email"].ToString(),
//                            Senha = rd["Senha"].ToString(),
//                            TipoPermissao = (EnumTipoAcesso)Int32.Parse(rd["TipoAcesso"].ToString()),
//                        });
//                    }
//                }
//                finally
//                {
//                    dbConnection.Close();
//                }

//                return list;
//            }
//        }

//        public Usuario ObterPorNomeUsuarioESenha(
//            string email,
//            string senha)
//        {

//            Usuario user = null;
//            using var dbConnection = new SqlConnection(ConnectionString);

//            try
//            {
//                SqlCommand cmd = dbConnection.CreateCommand();
//                //dbConnection.Query("");
//                cmd.CommandText = "select * from Usuario where Email = @Email and Senha = @senha";
//                //            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
//                cmd.Parameters.AddWithValue("@Email", email);
//                cmd.Parameters.AddWithValue("@Senha", senha);

//                dbConnection.Open();
//                var rd = cmd.ExecuteReader();

//                if (rd.Read())
//                {
//                    user = new Usuario()
//                    {
//                        //Id = (rd["Id"].ToString(),
//                        //Nome = rd["Nome"].ToString(),
//                        //Email = rd["Email"].ToString(),
//                        //Senha = rd["Senha"].ToString(),
//                        //TipoPermissao = (EnumTipoAcesso)Int32.Parse(rd["TipoAcesso"].ToString())
//                    };
//                }
//            }
//            catch
//            {
//                throw;
//            }
//            finally
//            {
//                dbConnection.Close();
//            }

//            return user;
//        }

//    }
//}
