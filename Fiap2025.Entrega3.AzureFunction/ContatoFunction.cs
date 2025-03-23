using System;
using System.Threading.Tasks;
using Fiap2025.Entrega3.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using System.Security.Cryptography.Xml;

namespace Fiap2025.Entrega3.AzureFunction
{
    public class ContatoFunction
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoFunction(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [Function("heathcheck")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            return new OkObjectResult("Funcionando!");
        }

        [Function("GetContatoById")]
        public async Task<IActionResult> GetContatoById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contato/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            var contato = await _contatoRepository.GetContatoByIdAsync(id);
            if (contato == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(contato);
        }

        [Function("GetContatosByDDD")]
        public async Task<IActionResult> GetContatosByDDD(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contato/ddd/{ddd}")] HttpRequest req,
            ILogger log, string ddd)
        {
            var contatos = await _contatoRepository.GetContatoByDDDAsync(ddd);
            return new OkObjectResult(contatos);
        }

        [Function("GetAllContatos")]
        public async Task<IActionResult> GetAllContatos(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contato")] HttpRequest req,
            ILogger log)
        {
            var contatos = await _contatoRepository.GetAllContatosAsync();
            return new OkObjectResult(contatos);
        }
    }
}