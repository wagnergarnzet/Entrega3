using Fiap2025.Entrega3.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap2025.Entrega3.Producer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContatoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddContatoAsync(AddContatoCommand command)
        {
            var contatoId = await _mediator.Send(command);
            return Ok (contatoId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContatoAsync(Guid id)
        {
            await _mediator.Send(new DeleteContatoCommand { Id = id });
            return NoContent();
        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> UpdateContatoAsync(Guid id, UpdateContatoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Cliente não encontrado!");
            }

            await _mediator.Send(command);
            return NoContent();
        }


    }
}
