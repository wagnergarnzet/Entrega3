using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap2025.Entrega3.Application.Commands
{
    public class DeleteContatoCommand :IRequest
    {
        public Guid Id { get; set; }
    }
}
