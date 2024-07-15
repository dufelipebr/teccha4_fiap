using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Repository;
using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

namespace apibronco.bronco.com.br.HostedService
{

    public class Registro
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }

    public class ConsumerQueue : IHostedService
    {
        static IQueueClient queueClient;
        private readonly IConfiguration _config;

        public ConsumerQueue(IConfiguration config)
        {
            _config = config;
            var serviceBusConnection = _config.GetValue<string>("AzureBusConnectionString");
            queueClient = new QueueClient(serviceBusConnection, "filaregistros");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("############## INICIANDO CONSUMER DA FILA ####################");
            ProcessMessageHandler();
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("############## DESLIGANDO CONSUMER DA FILA ####################");
            await queueClient.CloseAsync();
            await Task.CompletedTask;
        }

        private void ProcessMessageHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            //Console.WriteLine("### PROCESSANDO MENSAGEM FILA ###");
            //Console.WriteLine($"{DateTime.Now}");
            //Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            //try
            //{
            //    Registro msg = JsonSerializer.Deserialize<Registro>(message.Body);
            //    Console.WriteLine($"Registro deserializado:{msg.Email}-{msg.Nome}-{msg.Telefone}");
            //    UsuarioRepository rp = new UsuarioRepository(_config);
            //    CadastrarUsuarioDTO cad = new CadastrarUsuarioDTO();
            //    cad.Nome = msg.Nome;
            //    cad.Email = msg.Email;
            //    //cad. = msg.Telefone;
            //    rp.Cadastrar(new Usuario(cad));
            //    Console.WriteLine($"gravou usuario:{msg.Email}-{msg.Nome}-{msg.Telefone}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Erro ao deserializar e gravar na base:{ex.Message}");
            //}
            ////catch  (Exception ex)
            ////{
            ////    Console.WriteLine($"Erro ao gravar na base:{ex.Message}");
            ////}
            
            

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
