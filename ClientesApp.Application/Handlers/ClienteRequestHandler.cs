using ClientesApp.Application.Commands;
using ClientesApp.Application.Interfaces.Logs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Handlers
{
    public class ClienteRequestHandler : IRequestHandler<ClienteCommand>
    {
        private readonly ILogClienteDataStore _logClienteDataStore;

        public ClienteRequestHandler(ILogClienteDataStore logClienteDataStore)
        {
            this._logClienteDataStore = logClienteDataStore;
        }

        public async Task Handle(ClienteCommand request, CancellationToken cancellationToken)
        {
            await _logClienteDataStore.AddSync(request.LogCliente);
        }
    }
}
