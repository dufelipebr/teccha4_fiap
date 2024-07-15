using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace apibronco.bronco.com.br.Controllers
{
    [ApiController]
    [Route("Log")]
    public class LogControler: ControllerBase
    {
        private ILogRepository _logRepository;

        public LogControler(ILogRepository repository)
        {
            _logRepository = repository;
        }


        [HttpGet("listar_logs_comfiltro/logDate&logType:string")]        
        public IActionResult GetLogInfoListByFilter(DateTime? logDate, string? logType)
        {
            IEnumerable<LogInfo> list;
            try
            {
                LogFilter filter = new LogFilter();
                if (logDate != null)
                    filter.Data_Log = logDate;

                if (logType != null)
                    filter.Tipo_Log = logType;

                //Pro
                list = _logRepository.ObterTodosByFilter(filter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(list);
        }


        /// <summary>
        /// listar logs da aplicação.
        /// </summary>
        /// <returns>IActionResult contedo lista de logs /<Investimento/></returns>
        /// <remarks> </remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">N�o autenticado</response>
        /// <response code="403">N�o autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        //https://apibroncodev.azurewebsites.net/log/listar_logs
        [HttpGet("listar_logs")]
        public IActionResult GetLogInfoList()
        {
            IEnumerable<LogInfo> list;
            try
            {
                //Pro
                list = _logRepository.ObterTodos();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok(list);
        }


        /// <summary>
        /// adicionar log
        /// </summary>
        /// <returns>Log cadastrado com sucesos</returns>
        /// <remarks> AddLog("propostas", "Error", "argument exception")</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">N�o autenticado</response>
        /// <response code="403">N�o autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpPost("adicionar_log")]
        //public IEnumerable<Proposta> GetPropostaList()
        public IActionResult AddLog(string moduleName, string tipoLog, string message)
        {
            try
            {
                
                //Pro
               _logRepository.Cadastrar(new LogInfo(message, DateTime.Now, tipoLog, null, moduleName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Log cadastrado com sucesso");
        }
    }
}

