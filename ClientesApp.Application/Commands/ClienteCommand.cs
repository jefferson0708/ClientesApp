using ClientesApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Commands
{
    public class ClienteCommand: IRequest
    {
        public LogClienteModel? LogCliente { get; set; }


    }
}
