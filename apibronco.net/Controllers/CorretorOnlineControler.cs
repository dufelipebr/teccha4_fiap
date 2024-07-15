
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.DTOs;
namespace apibronco.bronco.com.br.Controllers
{
    [ApiController]
    [Route("corretor_online")]
    public class CorretorOnlineControler : ControllerBase
    {
        private readonly ISeguradoRepository _seguradoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<CorretorOnlineControler> _logger;


        public CorretorOnlineControler(
            IUsuarioRepository usuarioRepository, 
            ISeguradoRepository seguradoRepository, 
            ITokenService tokenService,
            ILogger<CorretorOnlineControler> logger
        )
        {
            _seguradoRepository = seguradoRepository;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// permite a criação de usuarios - segurados no corretor online
        /// </summary>
        /// <param name="RegisterInfo">informações do usuario segurado que será criado</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("registrar_usuario")]
        public IActionResult registrar_usuario([FromBody] RegisterInfo info)
        {
            _logger.Log(LogLevel.Information, "Iniciando registro_usuario...");

            try
            {
                // criar usuario na base de dados
                Usuario usr = new Usuario(info);
                _usuarioRepository.Cadastrar(usr);

                // criar segurado na base de dados
                Cliente_Segurado seg = new Cliente_Segurado(info);
                seg.Identificador_Usuario = usr.Id;
                _seguradoRepository.Cadastrar(seg);

                //Pagamento pag = new Pagamento(pagamentoDTO);
                //pag.Reference = prop.Codigo_Interno;
                //_pagamentoRepository.Cadastrar(pag);

            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar registro_usuario() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        /// <summary>
        /// Permite realizar login do cliente segurado no portal corretor online
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginDTO login)
        {
            var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(login.Email, login.Senha);
            Usuario usr;

            if (usuario == null)
                return NotFound(new { mensagem = "Usuario e ou Senha invalidos" });

            var token = _tokenService.GerarToken(usuario);

            usuario.Senha = null;

            return Ok(new
            {
                Usuario = usuario,
                Token = token
            });
        }

        /// <summary>
        /// lista de produtos disponiveis
        /// </summary>
        /// <returns>array de ProdutoInfo[]</returns>
        [HttpPost("obter_produtos")]
        public IActionResult obter_produtos()
        {
            var listaProdutos = _produtoRepository.ObterTodos();

            return Ok(listaProdutos);
        }
    }
}
