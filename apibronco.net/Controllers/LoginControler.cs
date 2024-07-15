
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.DTOs;
namespace apibronco.bronco.com.br.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginControler : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISeguradoRepository _seguradoRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<IntegrationControler> _logger;

        public LoginControler(
            IUsuarioRepository usuarioRepository,
            ISeguradoRepository seguradoRepository, 
            ITokenService tokenService, 
            ILogger<IntegrationControler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _seguradoRepository = seguradoRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        //[HttpPost]
        //public IActionResult Autenticar([FromBody] LoginDTO login)
        //{
        //    //throw NotImplementedException()
        //    ////var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(login.Email, login.Senha);
        //    //Usuario usr;
        //    //if (login.Email == "du.felipe.br@gmail.com" && login.Senha == "adm")
        //    //{ 
        //    //    usr = new Usuario("1", "carlos");
        //    //    usr.Email = "du.felipe.br@gmail.com";
        //    //    usr.Senha = "adm";
        //    //}
        //    //else
        //    //    usr = null;

        //    //if (usr == null)
        //    //    return NotFound(new { mensagem = "Usuario e ou Senha invalidos" });
            
        //    //var token = _tokenService.GerarToken(usr);

        //    //usr.Senha = null;

        //    //return Ok(new
        //    //{
        //    //    Usuario = usr, 
        //    //    Token = token
        //    //});
        //}



    }
}
